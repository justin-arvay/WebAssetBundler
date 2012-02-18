using System.Collections.Generic;

namespace ResourceCompiler.Resolvers
{
    public interface IFileResolver
    {        
        string Resolve(string file);
    }
}