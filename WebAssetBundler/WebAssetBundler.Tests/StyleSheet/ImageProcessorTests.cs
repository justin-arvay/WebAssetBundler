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
    public class ImageProcessorTests
    {
        private ImageProcessor processor;
        private SettingsContext settings;
        private Mock<IImagePipelineRunner> runner;
        private Mock<IDirectory> directory;

        [SetUp]
        public void Setup()
        {
            directory = new Mock<IDirectory>();

            settings = new SettingsContext();
            settings.AppRootDirectory = directory.Object;

            runner = new Mock<IImagePipelineRunner>();
            processor = new ImageProcessor(settings, runner.Object);
        }

        [Test]
        public void Should_Execute_Runner_And_Add_Modifier()
        {
            var asset = new AssetBaseImpl();
            asset.StreamContent = "url(../image/file.css);";
            asset.Source = "/Source/file.css";

            var bundle = new StyleSheetBundle();
            bundle.Assets.Add(asset);

            var context = processor.CreateRunnerContext("/image/file.css", asset);
            var result = new Mock<ImagePipelineRunnerResult>();
            result.Object.Changed = true;

            runner.Setup(r => r.Execute(context)).Returns(result.Object);
            
            processor.Process(bundle);
            asset.Content.ReadToEnd(); //execute modifier so we can test for results
            
            runner.Verify(r => r.Execute(context));
            Assert.IsInstanceOf<BackgroundImageModifier>(asset.Modifiers[0]);
        }

        [Test]
        public void Should_Execute_Runner_And_Not_Modifier()
        {

        }
    }
}
