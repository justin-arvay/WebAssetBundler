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
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class AssetConfigurationFactoryTests
    {
        public IAssetConfigurationFactory factory;

        [SetUp]
        public void Setup()
        {
            factory = new AssetConfigurationFactory();
        }

        [Test]
        public void Should_Use_Source()
        {            
            var element = new AssetConfigurationElement() { Source = "~/Test/File.css" };

            Assert.AreEqual("~/Test/File.css", factory.CreateAsset(element).Source);
        }

        [Test]
        public void Should_Be_Shared_Group()
        {
            Assert.IsTrue(factory.CreateGroup(new GroupConfigurationElementCollection()).IsShared);

        }

        [Test]
        public void Should_Map_Name()
        {
            var collection = new GroupConfigurationElementCollection();
            collection.Name = "Foo";

            Assert.AreEqual("Foo", factory.CreateGroup(collection).Name);
        }

        [Test]
        public void Should_Map_Compress()
        {
            var collection = new GroupConfigurationElementCollection();
            collection.Compress = true;

            Assert.IsTrue(factory.CreateGroup(collection).Compress);
        }

        [Test]
        public void Should_Map_Combine()
        {
            var collection = new GroupConfigurationElementCollection();
            collection.Combine = true;

            Assert.IsTrue(factory.CreateGroup(collection).Combine);
        }
    }
}
