using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler;
using ResourceCompiler.Resource;
using System.Web;

namespace ResourceCompiler
{
    public static class Reco
    {
        private static string _linkTemplate = "<link rel=\"stylesheet\" type=\"text/css\" {0} href=\"{1}\" />";
        private static string _scriptTemplate = "<script type=\"text/javascript\" src=\"{0}\" ></script>";

        public static HtmlString Link(string path)
        {
            //check if path has a starting slash, if not add it
            //dont add v=? if not versioning
            //handle situation where we dont want to combine
            IStyleSheetAssetsBuilder assets = RecoAssets.StyleSheet();

            string media = "media=\"{0}\"";
            string version = string.Empty;
            string url = "{0}?v={1}";

            if (assets.Versioned)
            {
                version = assets.GetLastWriteTimestamp();
            }

            media = string.Format(media, assets.MediaType);
            url = string.Format(url, path, version);
            return new HtmlString(String.Format(_linkTemplate, media, url));
        }

        public static HtmlString Script(string path)
        {
            IJavaScriptAssetsBuilder assets = RecoAssets.JavaScript();

            string version = string.Empty;
            string url = "{0}?v={1}";

            if (assets.Versioned)
            {
                version = assets.GetLastWriteTimestamp();
            }

            url = string.Format(url, path, version);
            return new HtmlString(String.Format(_scriptTemplate, url));
        }
    }
}
