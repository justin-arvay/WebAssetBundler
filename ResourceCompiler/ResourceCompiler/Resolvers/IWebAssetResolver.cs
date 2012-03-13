
namespace ResourceCompiler.Resolvers
{
    using System.Collections.Generic;

    public interface IWebAssetResolver
    {
        IEnumerable<string> Resolve();
    }
}
