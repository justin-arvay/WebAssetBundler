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

    [TestFixture]
    public class ImagePipelineRunnerTests
    {
        private ImagePipelineRunner runner;
        private Mock<IBundleCache<ImageBundle>> bundleCache;
        private Mock<IBundlePipeline<ImageBundle>> pipeline;
        private ImagePipelineRunnerContext context;
        private string root;
        private Mock<IDirectory> directory;
        private Mock<IBundleFactory<ImageBundle>> bundleFactory;

        [SetUp]
        public void Setup()
        {
            root = PathHelper.NormalizePath(AppDomain.CurrentDomain.BaseDirectory + "/../../");
            directory = new Mock<IDirectory>();
            bundleFactory = new Mock<IBundleFactory<ImageBundle>>();

            context = new ImagePipelineRunnerContext();
            context.AppRootDirectory = directory.Object;

            bundleCache = new Mock<IBundleCache<ImageBundle>>();
            pipeline = new Mock<IBundlePipeline<ImageBundle>>();
            runner = new ImagePipelineRunner(pipeline.Object, bundleCache.Object, bundleFactory.Object);
        }

        [Test]
        public void Should_Execute_Pipeline()
        {
            var newUrl = "/wab.axd/image/aaa/aaa.png";
            var bundle = new ImageBundle("image/png");
            bundle.Url = newUrl;

            context.ImagePath = "../Image/image.png";
            context.SourcePath = root + "Files/Test.css";

            var fileDirectory = new Mock<IDirectory>();
            var file = new Mock<IFile>();

            file.Setup(f => f.Path).Returns(root + "/Image/image.png");

            directory.Setup(d => d.GetDirectory(It.IsAny<string>()))
                .Returns(fileDirectory.Object);

            fileDirectory.Setup(d => d.GetFile(It.IsAny<string>()))
                .Returns(file.Object);

            bundleFactory.Setup(b => b.Create(It.IsAny<AssetBase>()))
                .Callback((AssetBase asset) => {

                    //make sure the created asset uses the correct source
                    Assert.AreEqual(root + "/Image/image.png", asset.Source);
                })
                .Returns(bundle);

            pipeline.Setup(c => c.Process(It.IsAny<ImageBundle>()))
                .Callback((ImageBundle calledBundle) => {

                    //set the url like we would if the processor actually ran
                    bundle.Url = newUrl;
                });

            var result = runner.Execute(context);

            Assert.IsTrue(result.Changed);
            Assert.AreEqual(newUrl, result.NewPath);
            Assert.AreEqual(context.ImagePath, result.OldPath); 
            pipeline.Verify(p => p.Process(It.IsAny<ImageBundle>()));
            bundleCache.Verify(b => b.Add(It.IsAny<ImageBundle>()));
        }

        [Test]
        public void Should_Not_Execute_Pipeline_For_Http()
        {
            var newUrl = "/wab.axd/image/aaa/aaa.png";

            context.ImagePath = "http://www.google.com/Image/image.png";
            context.SourcePath = root + "Files/Test.css";

            var result = runner.Execute(context);

            Assert.IsFalse(result.Changed);
            Assert.AreEqual(null, result.NewPath);
            Assert.AreEqual(context.ImagePath, result.OldPath);
            pipeline.Verify(p => p.Process(It.IsAny<ImageBundle>()), Times.Never());
            bundleCache.Verify(b => b.Add(It.IsAny<ImageBundle>()), Times.Never());
        }

        [Test]
        public void Should_Not_Execute_Pipeline_For_Https()
        {
            var newUrl = "/wab.axd/image/aaa/aaa.png";

            context.ImagePath = "https://www.google.com/Image/image.png";
            context.SourcePath = root + "Files/Test.css";

            var result = runner.Execute(context);

            Assert.IsFalse(result.Changed);
            Assert.AreEqual(null, result.NewPath);
            Assert.AreEqual(context.ImagePath, result.OldPath);
            pipeline.Verify(p => p.Process(It.IsAny<ImageBundle>()), Times.Never());
            bundleCache.Verify(b => b.Add(It.IsAny<ImageBundle>()), Times.Never());
        }
    }
}
