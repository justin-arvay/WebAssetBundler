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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    public class AssetProvider : IAssetProvider
    {
        private SettingsContext settings;
        private IDirectoryFactory directoryFactory;
        private HttpServerUtilityBase server;

        public AssetProvider(IDirectoryFactory directoryFactory, HttpServerUtilityBase server, SettingsContext settings)
        {
            this.settings = settings;
            this.directoryFactory = directoryFactory;
            this.server = server;
        }

        public AssetBase GetAsset(string source)
        {
            IFile file = settings.AppRootDirectory.GetFile(source);

            if (file.Exists == false)
            {
                throw new FileNotFoundException(TextResource.Exceptions.FileNotFound.FormatWith(source));
            }

            return CreateAsset(ResolveFile(file));
        }        

        public ICollection<AssetBase> GetAssets(string source, IDirectorySearch context)
        {
            var directory = directoryFactory.Create(source);

            if (directory.Exists == false)
            {
                throw new DirectoryNotFoundException(TextResource.Exceptions.DirectoryNotFound.FormatWith(source));
            }

            //retrieve all files from the directory where the file ends in the extension
            var files = context.FindFiles(directory);
            var assets = new List<AssetBase>(files.Select((file) => CreateAsset(file)));

            return RemoveDuplicates(assets);

        }

        /// <summary>
        /// Creates an assets from the file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public FileAsset CreateAsset(IFile file)
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

            //figure out if it is minified or not and then create the other
            if (IsMinifed(file)) 
            {
                minifedFile = file;
                rawFile = new FileSystemFile(GetRawSource(file.Path), file.Directory);
            } 
            else 
            {
                rawFile = file;
                minifedFile = new FileSystemFile(GetMinifiedSource(file.Path), file.Directory);
            }

            if (rawFile.Exists && minifedFile.Exists)
            {
                if (settings.DebugMode)
                {
                    return rawFile;
                }
                else
                {
                    return minifedFile;
                }
            }

            
            return rawFile.Exists ? rawFile : minifedFile;
        }

        /// <summary>
        /// Checks if the source is a minified assets.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool IsMinifed(IFile file)
        {
            return Path.GetFileNameWithoutExtension(file.Path).EndsWith(settings.MinifyIdentifier);
        }

        /// <summary>
        /// Changes the source to its raw version.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string GetRawSource(string source)
        {
            string ext = Path.GetExtension(source);
            return source.Substring(0, source.LastIndexOf(settings.MinifyIdentifier)) + ext;
        }

        /// <summary>
        /// Changes the source to its minifed version.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string GetMinifiedSource(string source)
        {
            string ext = Path.GetExtension(source);
            return source.Insert(source.LastIndexOf(ext), settings.MinifyIdentifier);
        }

        private IList<AssetBase> RemoveDuplicates(IList<AssetBase> assets)
        {
            var filteredAssets = new List<AssetBase>();

            foreach (var asset in assets)
            {
                if (filteredAssets.Where((a) => a.Source.Equals(asset.Source)).Count() == 0)
                {
                    filteredAssets.Add(asset);
                }
            }

            return filteredAssets;
        }
    }
}
