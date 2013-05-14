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
    using System.IO;
    using System.Collections.Generic;
    using System.Text;

    public class HtmlElement : IHtmlNode
    {
        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public bool SelfClosing
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.Generic.IList<IHtmlNode> Children
        {
            get { throw new NotImplementedException(); }
        }

        public IDictionary<string, string> Attributes()
        {
            throw new NotImplementedException();
        }

        public string Attribute(string key)
        {
            throw new NotImplementedException();
        }

        public IHtmlNode Attribute(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IHtmlNode Attributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlNode AddClass(string key, string value)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(TextWriter output)
        {
            var output = new StringBuilder();

            if (SelfClosing)
            {
                output.Append(

                if (TemplateCallback != null)
                {
                    TemplateCallback(output);
                }
                else if (Children.Any())
                {
                    Children.Each(child => child.WriteTo(output));
                }
                else
                {
                    output.Write(tagBuilder.InnerHtml);
                }

                output.Write(tagBuilder.ToString(TagRenderMode.EndTag));
            }
            else
            {
                output.Write(tagBuilder.ToString(TagRenderMode.SelfClosing));
            }
        }
    }
}
