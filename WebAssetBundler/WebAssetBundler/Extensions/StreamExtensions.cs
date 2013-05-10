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

    public static class StreamExtensions
    {
        /// <summary>
        /// Reads all the bytes to a string. The stream is closed after read.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReadToEnd(this Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {                
                return reader.ReadToEnd();
            }           
        }

        public static byte[] ToArray(this Stream input)
        {
            MemoryStream ms = new MemoryStream();
                
            input.Position = 0;
            input.CopyTo(ms);
            input.Position = 0;

            return ms.ToArray();
        }

        public static byte[] ReadAllBytes(this Stream stream)
        {
            byte[] assetBytes = null;

            // Create the file stream to be used 
            // to read the asset file.
            using (stream)
            {
                // Instantiate the byte array.
                int bytesInFile = (int)stream.Length;
                assetBytes = new Byte[bytesInFile];

                // Convert the file stream into a byte array.
                stream.Read(assetBytes, 0, bytesInFile);

            }

            return assetBytes;
        }
    }
}
