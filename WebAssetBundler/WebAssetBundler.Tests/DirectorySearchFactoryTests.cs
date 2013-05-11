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

namespace WebAssetBundler.Web.Mvc.Tests
{
    using System;
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using TinyIoC;

    [TestFixture]
    public class DirectorySearchFactoryTests
    {
        private IDirectorySearchFactory provider;
        private DirectorySearch dirSearch;
        private IList<string> patterns;
        private TinyIoCContainer container;

        [SetUp]
        public void Setup()
        {
            container = new TinyIoCContainer();
            patterns = new List<string>();
            dirSearch = new DirectorySearch();
            provider = new DirectorySearchFactory(container);
        }

        [Test]
        public void Should_Get_Directory_Search()
        {
            container.Register<IPluginCollection<BundleImpl>>(new PluginCollection<BundleImpl>());
            var bundle = new BundleImpl();
            var dirSearch = provider.CreateForType<BundleImpl>(bundle.Extension);

            Assert.AreEqual("*." + bundle.Extension, ((DirectorySearch)dirSearch).Patterns.ToList()[0]);
        }
    }
}
