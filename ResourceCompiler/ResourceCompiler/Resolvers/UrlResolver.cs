
namespace ResourceCompiler.Web.Mvc
{
    using System.Web;
    using System.Web.Routing;
    using System.Web.Mvc;

    public class UrlResolver : IUrlResolver
    {
        private RequestContext context;

        public UrlResolver(RequestContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Returns the relative path for the specified virtual path.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public string Resolve(string url)
        {
            UrlHelper helper = new UrlHelper(context);

            return helper.Content(url);
        }
    }
}
