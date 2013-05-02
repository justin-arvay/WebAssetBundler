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
    using System.IO;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AssetBase
    {
        readonly List<IAssetModifier> modifiers = new List<IAssetModifier>();

        public string Name
        {
            get
            {
                return Path.GetFileNameWithoutExtension(Source);
            }
        }

        public List<IAssetModifier> Modifiers
        {
            get 
            { 
                return modifiers; 
            }
        }

        
        public virtual string Source
        {
            get;
            set;
        }

        public bool Minify
        {
            get;
            set;
        }

        public string Extension
        {
            get
            {
                return Path.GetExtension(Source).Replace(".", "");
            }
        }

        public virtual Stream Content
        {
            get
            {
                //TODO: isolate stream incase half read stream is returned?
                var createStream = modifiers.Aggregate<IAssetModifier, Stream>(
                OpenSourceStream(),
                (openStream, modifier) => 
                    {
                        var stream = modifier.Modify(openStream, this);
                        stream.Position = 0;
                        return stream;
                    });

                return createStream;
            }
        }

        protected abstract Stream OpenSourceStream();
    }
}