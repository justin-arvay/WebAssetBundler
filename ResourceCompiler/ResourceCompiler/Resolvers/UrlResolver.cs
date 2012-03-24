
namespace ResourceCompiler.Web.Mvc
{
    using System.Web;
    using System.Web.Routing;
    using System.Web.Mvc;

    public class UrlResolver : IUrlResolver
    {
        /// <summary>
        /// Returns the relative path for the specified virtual path.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public string Resolve(string url)
        {
            HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
            RequestContext requestContext = httpContext.Request.RequestContext;
            UrlHelper helper = new UrlHelper(requestContext);

            return helper.Content(url);
        }
    }
}
