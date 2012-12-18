// WebAssetBundler - Bundles web assets so you dont have to.
// Copyright (C) 2012  Justin Arvay
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.



namespace WebAssetBundler.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;

    public static class DefaultSettings
    {
        private static string styleSheetFilesPath = "~/Content";
        private static string scriptFilesPath = "~/Scripts";

        private static bool compressed = true;

        private static string defaultGroupName = "Default";
        
        private static IScriptCompressor scriptCompressor = new MsScriptCompressor();
        private static IStyleSheetCompressor styleSheetCompressor = new MsStyleSheetCompressor();

        private static IStyleSheetConfigProvider styleSheetConfigProvider = new DefaultStyleSheetConfigProvider();
        private static IScriptConfigProvider scriptConfigProvider = new DefaultScriptConfigProvider();

        /// <summary>
        /// Gets or sets the style sheet files path. Path must be a virtual path.
        /// </summary>
        /// <value>The style sheet files path.</value>
        public static string StyleSheetFilesPath
        {
            get
            {
                return styleSheetFilesPath;
            }
            set
            {
                styleSheetFilesPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the script files path. Path must be a virtual path.
        /// </summary>
        /// <value>The style sheet files path.</value>
        public static string ScriptFilesPath
        {
            get
            {
                return scriptFilesPath;
            }
            set
            {
                scriptFilesPath = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether assets should be served as compressed.
        /// </summary>
        /// <value><c>true</c> if compressed; otherwise, <c>false</c>.</value>
        public static bool Compressed
        {
            get
            {
                return compressed;
            }
            set
            {
                compressed = value;
            }
        }

        /// <summary>
        /// Gets or sets the default bundle name.
        /// </summary>
        public static string DefaultGroupName
        {
            get
            {
                return defaultGroupName;
            }
            set
            {
                defaultGroupName = value;
            }
        }

        /// <summary>
        /// Gets or sets the default script compressor.
        /// </summary>
        public static IScriptCompressor ScriptCompressor
        {
            get
            {
                return scriptCompressor;
            }
            set
            {
                scriptCompressor = value;
            }
        }

        /// <summary>
        /// Gets or sets the default style sheet compressor.
        /// </summary>
        public static IStyleSheetCompressor StyleSheetCompressor
        {
            get
            {
                return styleSheetCompressor;
            }
            set
            {
                styleSheetCompressor = value;
            }
        }

        /// <summary>
        /// Gets or sets the javascript host.
        /// </summary>
        public static string ScriptHost
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the style sheet host.
        /// </summary>
        /// <returns></returns>
        public static string StyleSheetHost
        {
            get;
            set;
        }

        /// <summary>
        /// Enables or disables debug mode.
        /// </summary>
        public static bool DebugMode
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the configuration factory to be used when loading the style sheet bundles.
        /// </summary>
        public static IStyleSheetConfigProvider StyleSheetConfigProvider
        {
            get
            {
                return styleSheetConfigProvider;
            }
            set
            {
                styleSheetConfigProvider = value;
            }
        }

        /// <summary>
        /// Sets the configuration factory to be used when loading the script bundles.
        /// </summary>
        public static IScriptConfigProvider ScriptConfigProvider
        {
            get
            {
                return scriptConfigProvider;
            }
            set
            {
                scriptConfigProvider = value;
            }
        }
    }
}
