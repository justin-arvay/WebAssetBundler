
namespace ResourceCompiler.Resolvers
{
    using System.Collections.Generic;

    public interface IResourceResolver
    {
        IEnumerable<string> Resolve();
    }
}
