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
    using System;

    public class SharedWebAssetGroupCollectionBuilderTests
    {

        private WebAssetGroupCollection collection;
        private SharedWebAssetGroupCollectionBuilder builder;
        private BuilderContext context;
        private Mock<IAssetFactory> assetFactory;

        [SetUp]
        public void Setup()
        {
            assetFactory = new Mock<IAssetFactory>();
            context = new BuilderContext();
            context.AssetFactory = assetFactory.Object;
            collection = new WebAssetGroupCollection();
            builder = new SharedWebAssetGroupCollectionBuilder(collection, context);

        }

        [Test]
        public void Should_Be_Able_To_Add_Group()
        {
            builder.AddGroup("test", g => g.ToString());

            assetFactory.Verify(f => f.CreateGroup(It.Is<string>(s => s.Equals("test")), It.Is<bool>(b => b.Equals(false))), Times.Once());

            Assert.AreEqual(1, collection.Count);
        }

        [Test]
        public void Should_Throw_Exception_When_Adding_Group_That_Already_Exists()
        {
            assetFactory.Setup(f => f.CreateGroup(It.IsAny<string>(), It.IsAny<bool>())).Returns(new WebAssetGroup("test", false));

            builder.AddGroup("test", g => g.ToString());

            Assert.Throws<ArgumentException>(() => builder.AddGroup("test", g => g.ToString()));
        }

        [Test]
        public void Adding_Group_Should_Return_Self_For_Chaining()
        {
            Assert.IsInstanceOf<SharedWebAssetGroupCollectionBuilder>(builder.AddGroup("test", g => g.ToString()));
        }
    }
}
