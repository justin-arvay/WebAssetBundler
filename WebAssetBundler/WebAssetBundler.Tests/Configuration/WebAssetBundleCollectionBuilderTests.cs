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

namespace WebAssetBundler.Web.Mvc.Tests
{
    using System;
    using NUnit.Framework;
    using Moq;
    using WebAssetBundler.Web.Mvc;

    [TestFixture]
    public class WebAssetBundleCollectionBuilderTests
    {
        private BundleCollection collection;
        private WebAssetBundleCollectionBuilder builder;
        private BuilderContext context;
        private Mock<IAssetFactory> assetFactory;

        [SetUp]
        public void Setup()
        {
            assetFactory = new Mock<IAssetFactory>();
            context = new BuilderContext();
            context.AssetFactory = assetFactory.Object;

            collection = new BundleCollection();
            builder = new WebAssetBundleCollectionBuilder(collection, context);

        }
             

        [Test]
        public void Should_Have_Nothing_In_Collection_By_Default()
        {            
            Assert.AreEqual(0, collection.Count);
        }

        [Test]
        public void Adding_File_Should_Add_New_Group_With_Asset()
        {
            assetFactory.Setup(f => f.CreateBundle<BundleImpl>(It.IsAny<string>())).Returns(new BundleImpl());
            assetFactory.Setup(f => f.CreateAsset(It.IsAny<string>(), It.IsAny<string>())).Returns(new WebAsset("~/Files/test.css"));

            builder.Add<StyleSheetBundle>("~/Files/test.css");

            assetFactory.Verify(f => f.CreateBundle<BundleImpl>("test"), Times.Exactly(1));
            assetFactory.Verify(f => f.CreateAsset("~/Files/test.css", ""), Times.Exactly(1));

            //should be 2 items in the collection, and 1 item in each collections bundle
            Assert.AreEqual(1, collection.Count);
            foreach (var group in collection) 
            {
                Assert.AreEqual(1, group.Assets.Count);
            }
        }

        [Test]
        public void Add_Should_Return_Self_For_Chaining()
        {
            assetFactory.Setup(f =>f.CreateAsset(It.IsAny<string>(), It.IsAny<string>())).Returns(new WebAsset("test.css"));
            assetFactory.Setup(f => f.CreateBundle<BundleImpl>(It.IsAny<string>())).Returns(new BundleImpl());

            Assert.IsInstanceOf<WebAssetBundleCollectionBuilder>(builder.Add<BundleImpl>("~/Files/test.css"));
        }            
    }
}
