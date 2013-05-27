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
    using System.Web;
    using System.Web.Mvc;

    public static class Bundler
    {
        private static StyleSheetBundler styleSheetBundler;
        private static ScriptBundler scriptBundler;
        private static ImageBundler imageBundler;

        static Bundler()
        {
            styleSheetBundler = WabHttpModule.Host.Container.Resolve<StyleSheetBundler>();
            scriptBundler = WabHttpModule.Host.Container.Resolve<ScriptBundler>();
            imageBundler = WabHttpModule.Host.Container.Resolve<ImageBundler>();
        }

        public static StyleSheetBundler StyleSheets
        {
            get
            {
                styleSheetBundler.State = GetBundlerState("StyleSheetBundlerState");
                return styleSheetBundler;
            }
        }

        public static ScriptBundler Scripts
        {
            get
            {
                scriptBundler.State = GetBundlerState("ScriptBundlerState");
                return scriptBundler;
            }
        }

        public static ImageBundler Images
        {
            get
            {
                return imageBundler;
            }
        }

        private static BundlerState GetBundlerState(string name)
        {
            var context = WabHttpModule.Host.Container.Resolve<HttpContextBase>();
            var obj = (BundlerState)context.Items[name];

            if (obj == null)
            {
                obj = new BundlerState();
                context.Items[name] = obj;
            }

            return obj;
        }
    }
}
