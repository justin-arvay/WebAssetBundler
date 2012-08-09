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

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections.ObjectModel;

    public class WebAssetGroup
    {
        public WebAssetGroup(string name, bool isShared)
        {
            Name = name;
            IsShared = isShared;            
            Assets = new InternalCollection();                    
        }

        public string Name
        {
            get;
            private set;
        }

        public bool IsShared
        {
            get;
            private set;
        }      

        public bool Enabled
        {
            get;
            set;
        }

        public string Version
        {
            get;
            set;
        }

        public bool Compress
        {
            get;
            set;
        }

        public bool Combine
        {
            get;
            set;
        }

        public string DefaultPath
        {
            get;
            set;
        }

        public IList<IWebAsset> Assets
        {
            get;
            private set;
        }


        private sealed class InternalCollection : Collection<IWebAsset>
        {
            protected override void InsertItem(int index, IWebAsset item)
            {
                if (AlreadyExists(item))
                {
                    throw new ArgumentException(string.Format(TextResource.Exceptions.ItemWithSpecifiedSourceAlreadyExists, item.Source), "item");
                }

                base.InsertItem(index, item);
            }

            protected override void SetItem(int index, IWebAsset item)
            {
                if (AlreadyExists(item))
                {
                    throw new ArgumentException(string.Format(TextResource.Exceptions.ItemWithSpecifiedSourceAlreadyExists, item.Source), "item");
                }

                base.SetItem(index, item);
            }

            private bool AlreadyExists(IWebAsset item)
            {
                return this.Any(i => i != item && i.Source.Equals(item.Source));
            }

            private string Message(IWebAsset item)
            {
                return " Asset: \"" + item.Source + "\"";
            }
        }
    }




}
