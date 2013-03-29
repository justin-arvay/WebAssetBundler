
namespace Examples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using WebAssetBundler.Web.Mvc;

    public class TestPlugin : IPlugin<StyleSheetBundle>
    {

        public void Initialize(TinyIoCContainer container)
        {

        }


        public void Dispose()
        {
            
        }


        public IEnumerable<IPipelineModifier<StyleSheetBundle>> GetPipelineModifers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetSearchPatterns()
        {
            throw new NotImplementedException();
        }
    }    
}