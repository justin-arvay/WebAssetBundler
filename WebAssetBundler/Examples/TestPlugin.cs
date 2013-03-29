
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



        public void AddPipelineModifers(ICollection<IPipelineModifier<StyleSheetBundle>> modifiers)
        {

        }

        public void AddSearchPatterns(ICollection<string> patterns)
        {

        }
    }    
}