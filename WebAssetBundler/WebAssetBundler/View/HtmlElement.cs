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
    using System.Linq;

    public class HtmlElement : IHtmlNode
    {
        private IDictionary<string, string> attributes = new Dictionary<string, string>();
        private IList<IHtmlNode> children = new List<IHtmlNode>();
        private bool selfClosing = false;
        private string name;        

        public HtmlElement(string name)
        {
            this.name = name;
        }

        public HtmlElement(string name, bool selfClosing) :
            this(name)
        {
            this.selfClosing = selfClosing;
        }

        public string Name
        {
            get 
            { 
                return name; 
            }
        }

        public bool SelfClosing
        {
            get 
            { 
                return selfClosing; 
            }
        }

        public IList<IHtmlNode> Children
        {
            get 
            { 
                return children; 
            }
        }

        public IHtmlNode AddAttribute(string key, string value)
        {
            if (value != null && key != null)
            {
                if (value.Length > 0 && key.Length > 0)
                {
                    if (attributes.ContainsKey(key))
                    {
                        attributes[key] = value;
                    }
                    else
                    {
                        attributes.Add(key, value);
                    }                    
                }
            }

            return this;
        }

        public IHtmlNode MergeAttributes(IDictionary<string, string> attributesIn)
        {
            foreach (var attributeIn in attributesIn)
            {
                AddAttribute(attributeIn.Key, attributeIn.Value);
            }

            return this;
        }

        public IHtmlNode AddClass(string value)
        {
            string key = "class";
            if (attributes.ContainsKey(key))
            {
                var newValue = attributes[key] + " " + value;
                attributes[key] = newValue;
            }
            else
            {
                attributes.Add(key, value);
            }

            return this;
        }

        public void WriteTo(TextWriter output)
        {
            var outputBuilder = new StringBuilder();
            outputBuilder.Append("<");
            outputBuilder.Append(Name);

            foreach (var pair in attributes)
            {                
                outputBuilder.Append(" {0}=\"{1}\"".FormatWith(pair.Key, pair.Value));
            }

            if (SelfClosing)
            {
                outputBuilder.Append("/>");
            }
            else
            {
                outputBuilder.Append(">");
                
                if (children.Count > 0)
                {
                    foreach (var child in Children)
                    {
                        child.WriteTo(output);
                    }
                }

                outputBuilder.Append("</{0}>".FormatWith(name));
            }

            output.Write(outputBuilder);
        }


        public IDictionary<string, string> GetAttributes()
        {
            return attributes;
        }


        public string GetAttribute(string key)
        {
            if (attributes.ContainsKey(key))
            {
                return attributes[key];
            }

            return string.Empty;
        }
    }
}
