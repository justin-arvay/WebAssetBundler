
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ResourceCompiler.Assets;

namespace ResourceCompiler
{
    public class RecoAssets
    {
        //singleton instantiation to avoid using lock
        private static IStyleSheetAssetsBuilder _styleSheetRegistrar = new StyleSheetAssetsBuilder();
        private static IJavaScriptAssetsBuilder _jsRegistrar = new JavaScriptAssetsBuilder();

        public static IStyleSheetAssetsBuilder StyleSheet()
        {
            return _styleSheetRegistrar;
        }

        public static IJavaScriptAssetsBuilder JavaScript()
        {
            return _jsRegistrar;
        }

        /*
        public static string CurrentVersion()
        {
            try
            {
                return ConfigurationManager.AppSettings["version"];
            }
            catch (Exception)
            {
                return "ERROR";
            }
        }*/

    }
}
