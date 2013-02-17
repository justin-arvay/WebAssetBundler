using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examples
{
    using WebAssetBundler.Web.Mvc;

    public class TestPipelineCustomizer : IPipelineCustomizer<StyleSheetBundle>
    {
        public void Customize(IBundlePipeline<StyleSheetBundle> pipeline)
        {
            throw new NotImplementedException();
        }
    }
}