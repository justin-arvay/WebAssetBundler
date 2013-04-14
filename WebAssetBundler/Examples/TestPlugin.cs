using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examples
{
    using WebAssetBundler.Web.Mvc;
    public class TestPlugin : IPlugin<StyleSheetBundle>
    {
        public void Initialize(TinyIoCContainer container)
        {
            throw new NotImplementedException();
        }

        public void ModifyPipeline(IBundlePipeline<StyleSheetBundle> pipeline)
        {
            throw new NotImplementedException();
        }

        public void ModifySearchPatterns(ICollection<string> patterns)
        {
            patterns.Add("*.tst");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}