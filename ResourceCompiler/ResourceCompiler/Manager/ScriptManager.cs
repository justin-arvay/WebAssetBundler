
namespace ResourceCompiler.Web.Mvc
{
    using ResourceCompiler.Web.Mvc;

    public class ScriptManager
    {
        public ScriptManager(WebAssetGroupCollection scripts)
        {
            Scripts = scripts;

            DefaultGroup = new WebAssetGroup("Default", false)
            {
                DefaultPath = DefaultSettings.StyleSheetFilesPath
            };

            Scripts.Add(DefaultGroup);
        }

        public WebAssetGroup DefaultGroup
        {
            get;
            private set;
        }

        public WebAssetGroupCollection Scripts
        {
            get;
            private set;
        }
    }
}
