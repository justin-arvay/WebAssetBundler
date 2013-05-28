// Web Asset Bundler - Bundles web assets so you dont have to.
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
    using System.IO;
    using System.Web;

    public class BundlerRenderer<TBundle> : IBundleRenderer<TBundle>
        where TBundle : Bundle
    {
        private ITagWriter<TBundle> tagWriter;

        public BundlerRenderer(ITagWriter<TBundle> tagWriter)
        {
            this.tagWriter = tagWriter;
        }

        public IHtmlString Render(TBundle bundle, BundlerState state)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                if (state.IsRendered(bundle) == false)
                {
                    state.MarkRendered(bundle);
                    tagWriter.Write(textWriter, bundle);
                }

                return new HtmlString(textWriter.ToString());
            }            
        }

        public IHtmlString RenderAll(IEnumerable<TBundle> bundles, BundlerState state)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                foreach (var bundle in bundles)
                {
                    if (state.IsRendered(bundle) == false)
                    {
                        state.MarkRendered(bundle);
                        tagWriter.Write(textWriter, bundle);
                    }
                }

                return new HtmlString(textWriter.ToString());
            }
        }
    }
}
