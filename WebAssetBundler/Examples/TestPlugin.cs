
namespace Examples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using WebAssetBundler.Web.Mvc;

    public class TestPlugin : IPluginConfiguration<StyleSheetBundle>
    {

        public void Configure(TinyIoCContainer container)
        {
            throw new NotImplementedException();
        }

        public void ConfigurePipelineModifiers(ICollection<IPipelineModifier<StyleSheetBundle>> modifiers)
        {
            throw new NotImplementedException();
        }

        public void ConfigurePatternModifiers(ICollection<string> patterns)
        {
            throw new NotImplementedException();
        }
    }

    public class TestTwoPlugin : IPluginConfiguration<StyleSheetBundle>
    {

        public void Configure(TinyIoCContainer container)
        {
            throw new NotImplementedException();
        }

        public void ConfigurePipelineModifiers(ICollection<IPipelineModifier<StyleSheetBundle>> modifiers)
        {
            throw new NotImplementedException();
        }

        public void ConfigurePatternModifiers(ICollection<string> patterns)
        {
            throw new NotImplementedException();
        }
    }

    public class TestThreePlugin : IPluginConfiguration<ScriptBundle>
    {

        public void Configure(TinyIoCContainer container)
        {
            throw new NotImplementedException();
        }

        public void ConfigurePipelineModifiers(ICollection<IPipelineModifier<ScriptBundle>> modifiers)
        {
            throw new NotImplementedException();
        }

        public void ConfigurePatternModifiers(ICollection<string> patterns)
        {
            throw new NotImplementedException();
        }
    }
}