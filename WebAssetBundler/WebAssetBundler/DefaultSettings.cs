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

        private static Func<TinyIoCContainer, IBundleConfigurationProvider<StyleSheetBundle>> styleSheetConfigurationProvider =
            (c) => c.Resolve<DefaultBundleConfigurationProvider<StyleSheetBundle>>();

        private static Func<TinyIoCContainer, IBundleConfigurationProvider<ScriptBundle>> scriptConfigurationProvider =
            (c) => c.Resolve<DefaultBundleConfigurationProvider<ScriptBundle>>();

        private static Func<TinyIoCContainer, IBundleConfigurationFactory<StyleSheetBundle>> styleSheetConfigurationFactory =
             (c) => c.Resolve<DefaultBundleConfigurationFactory<StyleSheetBundle>>();

        private static Func<TinyIoCContainer, IBundleConfigurationFactory<ScriptBundle>> scriptConfigurationFactory =
            (c) => c.Resolve<DefaultBundleConfigurationFactory<ScriptBundle>>();

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
        /// Sets the configuration provider to be used when providing configuration for style sheet bundles.
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
        /// Sets the configuration provider to be used when providing configuration for script bundles.
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
        /// Sets the configuration factory that is used when instantiating style sheet configuration classes.
        /// </summary>
        public static Func<TinyIoCContainer, IBundleConfigurationFactory<StyleSheetBundle>> StyleSheetConfigurationFactory
        {
            get
            {
                return styleSheetConfigurationFactory;
            }
            set
            {
                styleSheetConfigurationFactory = value;
            }
        }

        /// <summary>
        /// Sets the configuration factory that is used when instantiating script configuration classes.
        /// </summary>
        public static Func<TinyIoCContainer, IBundleConfigurationFactory<ScriptBundle>> ScriptConfigurationFactory
        {
            get
            {
                return scriptConfigurationFactory;
            }
            set
            {
                scriptConfigurationFactory = value;
            }
        }
    }
}
