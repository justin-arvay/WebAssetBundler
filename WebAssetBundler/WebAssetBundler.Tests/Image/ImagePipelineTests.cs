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
    using TinyIoC;

    [TestFixture]
    public class ImagePipelineTests
    {
        private ImagePipeline pipeline;
        private SettingsContext settings;
        private TinyIoCContainer container;
        private Mock<ILogger> logger;

        [SetUp]
        public void Setup()
        {
            settings = new SettingsContext();
            container = new TinyIoCContainer();
        }

        [Test]
        public void Should_Use_Default_Processors()
        {
            pipeline = new ImagePipeline(container, settings, logger.Object);

            Assert.IsInstanceOf<AssignHashProcessor>(pipeline[0]);
            Assert.IsInstanceOf<UrlAssignmentProcessor<ImageBundle>>(pipeline[1]);                           
        }
    }
}
