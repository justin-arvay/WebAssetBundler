
namespace ResourceCompiler.Web.Mvc
{
    using ResourceCompiler.Web.Mvc;
    using System.Collections.Generic;

    public interface IWebAssetGenerator
    {
        void Generate(IList<WebAssetResolverResult> resolverResults);
    }
}
