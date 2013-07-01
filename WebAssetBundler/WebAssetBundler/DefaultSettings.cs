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
    using TinyIoC;

    public static class DefaultSettings
    {
        private static IScriptMinifier scriptMinifier = new MsScriptMinifier();
        private static IStyleSheetMinifier styleSheetMinfier = new MsStyleSheetMinifier();
        private static ConfigurationDriverCollection drivers = new ConfigurationDriverCollection();

        private static ILogger logger = new DoNothingLogger();

        /// <summary>
        /// Gets or sets the logger used by the application to log events and errors.
        /// </summary>
        public static ILogger Logger
        {
            get
            {
                return logger;
            }
            set
            {
                logger = value;
            }
        }

        public static ConfigurationDriverCollection Drivers
        {
            get { return drivers; }
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
    }
}
