using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceCompiler.WebAsset
{
    public interface IAssetsBuilder<TBuilder>
    {
        TBuilder Add(string path);

       // TBuilder Path(string path, Action<PathOnlyBuilder<TBuilder>> action);
    }
}
