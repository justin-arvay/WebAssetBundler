
namespace ResourceCompiler.Web.Mvc
{
    using ResourceCompiler.Web.Mvc;

    public class StyleSheetRegistrar
    {
        public StyleSheetRegistrar(WebAssetGroupCollection styleSheets)
        {
            StyleSheets = styleSheets;

            DefaultGroup = new WebAssetGroup("Default", false)
            {
                DefaultPath = DefaultSettings.StyleSheetFilesPath
            };

            StyleSheets.Add(DefaultGroup);
        }

        public WebAssetGroup DefaultGroup 
        { 
            get; 
            private set; 
        }

        public WebAssetGroupCollection StyleSheets
        {
            get;
            private set;
        }
    }
}
