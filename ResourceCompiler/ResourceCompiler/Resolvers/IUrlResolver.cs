
namespace ResourceCompiler.Resolvers
{
    using System;

    public interface IUrlResolver
    {
        /// <summary>
        /// Returns the path for the sepcified virtual path.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        string Resolve(string url);
    }
}
