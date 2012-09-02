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
    using System.IO;

    [TestFixture]
    public class AssetFactoryTests
    {
        private AssetFactory factory;
        private BuilderContext context;

        [SetUp]
        public void Setup()
        {
            context = new BuilderContext();
            factory = new AssetFactory(context);
        }

        [Test]
        public void Should_Create_Asset_With_Contexts_Default_Path()
        {
            context.DefaultPath = "~/test/";
            var asset = factory.CreateAsset("file.css", "");

            Assert.IsInstanceOf<WebAsset>(asset);
            Assert.AreEqual(Path.Combine("~/test/file.css"), asset.Source);
        }

        [Test]
        public void Should_Create_Asset_With_Default_Path()
        {
            
            var asset = factory.CreateAsset("file.css", "~/test/");

            Assert.IsInstanceOf<WebAsset>(asset);
            Assert.AreEqual("~/test/file.css", asset.Source);
        }

        [Test]
        public void Should_Create_Asset_Without_Default_Path()
        {
            var asset = factory.CreateAsset("~/file.css", "");

            Assert.IsInstanceOf<WebAsset>(asset);
            Assert.AreEqual("~/file.css", asset.Source);
        }

        [Test]
        public void Should_Create_Group()
        {
            var group = factory.CreateGroup("test", false);

            Assert.NotNull(group);
            Assert.AreEqual(context.Combine, group.Combine);
            Assert.AreEqual(context.Compress, group.Compress);
            Assert.AreEqual(context.Host, group.Host);
        }
    }
}
