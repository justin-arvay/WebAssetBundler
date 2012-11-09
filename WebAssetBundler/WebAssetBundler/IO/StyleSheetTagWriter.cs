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
    using System.IO;
    using System.Collections.Generic;

    public class StyleSheetTagWriter : ITagWriter
    {
        private IUrlGenerator urlGenerator;

        public StyleSheetTagWriter(IUrlGenerator urlGenerator)
        {
            this.urlGenerator = urlGenerator;
        }

        public void Write(TextWriter writer, IList<MergedBundle> results, BuilderContext context)
        {
            var url = "";
            var link = "<link type=\"text/css\" href=\"{0}\" rel=\"stylesheet\"/>";

            foreach (var result in results)
            {
                url = urlGenerator.Generate(result.Name, result.Hash.ToHexString(), result.Host, context);
                writer.WriteLine(link.FormatWith(url));                
            }
        }
    }
}
