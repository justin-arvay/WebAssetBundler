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
        private Stream modifiedStream;

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

        public void SaveModifiedStream(Stream stream)
        {
            using (stream)
            {
                stream.Position = 1;
                //forcing the stream to an array allows us to close the underlying stream without affecting the new stream
                //aka frees resources
                this.modifiedStream = new MemoryStream(stream.ToArray());
            }
        }

        public Stream Content
        {
            get
            {
                //if we havent modified it yet, create a snapshot after this point OpenSourceStream will not be used anymore
                if (modifiedStream == null)
                {
                    SaveModifiedStream(OpenSourceStream());
                }                

                var createStream = modifiers.Aggregate<IAssetModifier, Stream>(
                modifiedStream,
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

                //save the changes made to the stream
                SaveModifiedStream(createStream);

                //clear modifiers because we saved the snapshot and do not want to re-process next time the stream is opened
                Modifiers.Clear();

                return modifiedStream;
            }
        }

        protected abstract Stream OpenSourceStream();        
    }
}