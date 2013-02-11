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

    public class FileAsset : AssetBase
    {
        private IFile sourceFile;
        private string content;

        public FileAsset(IFile sourceFile)
        {
            this.sourceFile = sourceFile;
        }

        public override string Source
        {
            get 
            {
                return sourceFile.Source;
            }
        }

        public override string Content
        {
            get
            {
                if (String.IsNullOrEmpty(content))
                {
                    content = (new StreamReader(sourceFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read))).ReadToEnd();
                }

                return content;
            }
            set
            {
                this.content = value;
            }
        }
    }
}