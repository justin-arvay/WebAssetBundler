﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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

    public interface IHtmlNode
    {
        string Name
        {
            get;
        }

        bool SelfClosing
        {
            get;
        }

        IList<IHtmlNode> Children
        {
            get;
        }

        /// <summary>
        /// Returns a dictionary of all attributes added to the node.
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> GetAttributes();

        /// <summary>
        /// Gets an attributes value by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetAttribute(string key);

        /// <summary>
        /// Adds an attribute.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IHtmlNode AddAttribute(string key, string value);

        /// <summary>
        /// Adds an attribute with no value to the node. Example: <script asnyc type="text/javascript" src="http://www.google.com/file.js"></script>
        /// Where asnyc is a value-less attribute.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IHtmlNode AddValuelessAttribute(string key);

        /// <summary>
        /// Merges with the attributes in the node. Replaces all values that already exist in the node with matching key.
        /// </summary>
        /// <param name="attributesIn"></param>
        /// <returns></returns>
        IHtmlNode MergeAttributes(IDictionary<string, string> attributesIn);

        /// <summary>
        /// Adds class to attributes. Appends new class if it already contains a class.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IHtmlNode AddClass(string value);

        /// <summary>
        /// Writes the node to the output. Empty string returned if attribute doesnt exist.
        /// </summary>
        /// <param name="output"></param>
        void WriteTo(TextWriter output);
    }
}
