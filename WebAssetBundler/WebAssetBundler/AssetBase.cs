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
        private byte[] assetBytes;

        public string Name
        {
            get
            {
                return Path.GetFileNameWithoutExtension(Source);
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

        public void Modify(IAssetModifier modifier)
        {
            var stream = modifier.Modify(OpenInternalStream());            

            if (stream.CanRead == false)
            {
                throw new InvalidDataException(TextResource.Exceptions.ModifierNotReadable.FormatWith(modifier.GetType().FullName));
            }

            SaveInternalStream(stream);
        }

        public Stream OpenStream()
        {
            return OpenInternalStream();
        }

        private void SaveInternalStream(Stream stream)
        {
            stream.Position = 0;
            assetBytes = stream.ReadAllBytes();
        }

        private Stream OpenInternalStream()
        {
            //if we havent modified it yet, create a snapshot after this point OpenSourceStream will not be used anymore
            if (assetBytes == null)
            {
                assetBytes = OpenSourceStream().ReadAllBytes();
            }

            return new MemoryStream(assetBytes.ToArray());
        }
        
        protected abstract Stream OpenSourceStream();        
    }
}