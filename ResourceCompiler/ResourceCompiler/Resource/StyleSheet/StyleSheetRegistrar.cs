namespace ResourceCompiler.Resource.StyleSheet
{
    public class StyleSheetRegistrar
    {
        public StyleSheetRegistrar()
        {
            DefaultGroup = new ResourceGroup("Default", false)
            {
                DefaultPath = DefaultSettings.StyleSheetFilesPath
            };
        }

        public ResourceGroup DefaultGroup 
        { 
            get; 
            private set; 
        }
    }
}
