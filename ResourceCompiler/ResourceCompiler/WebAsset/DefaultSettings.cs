using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ResourceCompiler.WebAsset
{
    public static class DefaultSettings
    {
        private static string styleSheetFilesPath = "~/Content";
        private static string scriptFilesPath = "~/Scripts";
        private static string version = new AssemblyName(typeof(DefaultSettings).Assembly.FullName).Version.ToString(3);
        private static bool compressed = true;
        private static string defaultGroupName = "Default";
        
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
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public static string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
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
        /// Gets or sets a value indicating whether assets shoule be combined.
        /// </summary>
        /// <value><c>true</c> if combined; otherwise, <c>false</c>.</value>
        public static bool Combined
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default group name.
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
    }
}
