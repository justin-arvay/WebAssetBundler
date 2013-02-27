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

        private static string minifyIdentifier = ".min";

        private static bool compressed = true;

        private static string defaultGroupName = "Default";
        
        private static IScriptMinifier scriptMinifier = new MsScriptMinifier();
        private static IStyleSheetMinifier styleSheetMinfier = new MsStyleSheetMinifier();

        private static Func<TinyIoCContainer, IBundleConfigurationProvider<StyleSheetBundle>> styleSheetConfigurationProvider =
            (c) => c.Resolve<DefaultBundleConfigurationProvider<StyleSheetBundle>>();

        private static Func<TinyIoCContainer, IBundleConfigurationProvider<ScriptBundle>> scriptConfigurationProvider =
            (c) => c.Resolve<DefaultBundleConfigurationProvider<ScriptBundle>>();

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
        /// Gets or sets the default script minfier.
        /// </summary>
        public static IScriptMinifier ScriptMinifier
        {
            get
            {
                return scriptMinifier;
            }
            set
            {
                scriptMinifier = value;
            }
        }

        /// <summary>
        /// Gets or sets the default style sheet minifier.
        /// </summary>
        public static IStyleSheetMinifier StyleSheetMinifier
        {
            get
            {
                return styleSheetMinfier;
            }
            set
            {
                styleSheetMinfier = value;
            }
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
        public static Func<TinyIoCContainer, IBundleConfigurationProvider<StyleSheetBundle>> StyleSheetConfigurationProvider
        {
            get
            {
                return styleSheetConfigurationProvider;
            }
            set
            {
                styleSheetConfigurationProvider = value;
            }
        }

        /// <summary>
        /// Sets the configuration factory to be used when loading the script bundles.
        /// </summary>
        public static Func<TinyIoCContainer, IBundleConfigurationProvider<ScriptBundle>> ScriptConfigurationProvider
        {
            get
            {
                return scriptConfigurationProvider;
            }
            set
            {
                scriptConfigurationProvider = value;
            }
        }

        /// <summary>
        /// Sets the identifier use to identify files that are already minified. Eg: ~/file.min.js is a file that has already been 
        /// minified. The application will not attempt to minify it again.
        /// </summary>
        public static string MinifyIdentifier
        {
            get
            {
                return minifyIdentifier;
            }
            set
            {
                minifyIdentifier = value;
            }
        }
    }
}
