
namespace ResourceCompiler.Web.Mvc
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    public class ImagePathContentFilter : IWebAssetContentFilter
    {
        private readonly IUrlResolver resolver;

        private static readonly Regex urlRegex = new Regex(@"url\s*\((\""|\')?(?<url>[^)]+)?(\""|\')?\)",
            RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

        public ImagePathContentFilter(IUrlResolver resolver)
        {            
            this.resolver = resolver;
        }

        public string Filter(string basePath, string content)
        {
            content = urlRegex.Replace(content, match =>
            {
                string url = match.Groups["url"].Value.Trim("'\"".ToCharArray());
                
                if (url.IsNotNullOrEmpty()
                    && !url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                    && !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                    && !url.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                {
                    url = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(basePath), url);                    

                    return "url('{0}')".FormatWith(resolver.Resolve(url));
                }

                return "url('{0}')".FormatWith(url);
            });

            return content;
        }
    }
}
