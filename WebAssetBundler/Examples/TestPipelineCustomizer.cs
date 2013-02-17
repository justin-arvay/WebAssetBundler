using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examples
{
    using WebAssetBundler.Web.Mvc;

    public class TestPipelineCustomizer : IPipelineCustomizer<ScriptBundle>
    {
        public void Customize(IBundlePipeline<ScriptBundle> pipeline)
        {
            throw new NotImplementedException();
        }
    }
}