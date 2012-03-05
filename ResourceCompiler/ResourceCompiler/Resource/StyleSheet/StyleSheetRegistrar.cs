namespace ResourceCompiler.Resource.StyleSheet
{
    public class StyleSheetRegistrar
    {
        public StyleSheetRegistrar(ResourceGroupCollection styleSheets)
        {
            StyleSheets = styleSheets;

            DefaultGroup = new ResourceGroup("Default", false)
            {
                DefaultPath = DefaultSettings.StyleSheetFilesPath
            };

            StyleSheets.Add(DefaultGroup);
        }

        public ResourceGroup DefaultGroup 
        { 
            get; 
            private set; 
        }

        public ResourceGroupCollection StyleSheets
        {
            get;
            private set;
        }
    }
}
