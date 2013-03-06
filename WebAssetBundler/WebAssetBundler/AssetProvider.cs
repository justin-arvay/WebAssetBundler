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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Collections;

    public class AssetProvider : IAssetProvider
    {
        private string minifyIdentifier;
        private bool debugMode;
        private IDirectoryFactory directoryFactory;
        private HttpServerUtilityBase server;

        public AssetProvider(IDirectoryFactory directoryFactory, HttpServerUtilityBase server, Func<string> minifyIdentifier, Func<bool> debugMode)
        {
            this.minifyIdentifier = minifyIdentifier();
            this.debugMode = debugMode();
            this.directoryFactory = directoryFactory;
            this.server = server;
        }

        public AssetBase GetAsset(string source)
        {
            if (source.StartsWith("~/") == false && source.StartsWith("/") == false)
            {
                throw new ArgumentException("Source must be virtual path.");  
            }

            IFile file = ResolveFile(new FileSystemFile(server.MapPath(source)));

            return CreateAsset(file);
        }        

        public ICollection<AssetBase> GetAssets(DirectorySearchContext context)
        {
            var directory = directoryFactory.Create(context.Source);

            //fileNames = RemoveDuplicates(fileNames);

            //retrieve all files from the directory where the file ends in the extension
            var files = directory.GetFiles(context.Pattern, context.SearchOption)
                .Where((file) => file.Path.EndsWith(context.Extension));

            return new List<AssetBase>(files.Select((file) => CreateAsset(file)));

        }


        /// <summary>
        /// Creates an asset from the file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public AssetBase CreateAsset(IFile file)
        {
            return new FileAsset(ResolveFile(file));
        }

        /// <summary>
        /// Changes the file to a minified or raw file depending on settings.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IFile ResolveFile(IFile file)
        {
            IFile rawFile = null;
            IFile minifedFile = null;

            if (IsMinifed(file.Path)) 
            {
                minifedFile = file;
                rawFile = new FileSystemFile(GetRawSource(file.Path), file.Directory);
            } 
            else 
            {
                rawFile = file;
                minifedFile = new FileSystemFile(GetMinifiedSource(file.Path), file.Directory);
            }


            if (minifedFile != null)
            {
                if (debugMode)
                {
                    return rawFile;
                }

                return minifedFile;
            }
            else
            {
                return rawFile;
            }
        }

        /// <summary>
        /// Checks if the source is a minified asset.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool IsMinifed(string source)
        {
            return Path.GetFileNameWithoutExtension(source).EndsWith(minifyIdentifier);
        }

        /// <summary>
        /// Changes the source to its raw version.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string GetRawSource(string source)
        {
            string ext = Path.GetExtension(source);
            return source.Substring(0, source.LastIndexOf(minifyIdentifier)) + ext;
        }

        /// <summary>
        /// Changes the source to its minifed version.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string GetMinifiedSource(string source)
        {
            string ext = Path.GetExtension(source);
            return source.Insert(source.LastIndexOf(ext), minifyIdentifier);
        }

        private IList<string> RemoveDuplicates(IList<string> fileNames)
        {
            var filteredFileNames = fileNames;

            for (int index = 0; index < fileNames.Count(); index++)
            {
                if (IsMinifed(fileNames[index]))
                {
                    var rawSource = GetRawSource(fileNames[index]);
                    if (fileNames.Contains(rawSource))
                    {
                        filteredFileNames.Remove(fileNames[index]);
                    }
                }
            }

            return filteredFileNames;
        }
    }
}
