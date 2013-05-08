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
                //Passing func to the modifier allows the modifier to control when the stream is opened
                //Additionally this allows the user to open multi streams if needed

                var createStream = modifiers.Aggregate<IAssetModifier, Stream>(
                OpenSourceStream(),
                (openStream, modifier) =>                    
                    {
                        var stream = modifier.Modify(openStream);

                        //make sure position is 0 
                        stream.Position = 0;

                        //if (streamReader.CanRead == false)
                        //{
                        //    //if we get here the stream is not in a correct state
                        //    //TODO: check if the stream is closed and log / throw exception
                        //    //stream must be open for next modifier
                        //}

                        return stream;
                    });

                return createStream;
            }
        }

        protected abstract Stream OpenSourceStream();
    }
}