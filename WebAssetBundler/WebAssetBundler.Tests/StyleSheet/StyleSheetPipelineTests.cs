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
    using System.Web;

    [TestFixture]
    public class StyleSheetPipelineTests
    {
        private StyleSheetPipeline pipeline;
        private TinyIoCContainer container;

        [SetUp]
        public void Setup()
        {
            var compressor = new Mock<IStyleSheetMinifier>();
            var server = new Mock<HttpServerUtilityBase>();

            container = new TinyIoCContainer();
            container.Register<IStyleSheetMinifier>((a, c) => compressor.Object);
            container.Register<HttpServerUtilityBase>((a, c) => server.Object);

            container.Register<ICacheProvider, CacheProvider>();
            container.Register<IBundlesCache<ImageBundle>, BundlesCache<ImageBundle>>();
            container.Register<IUrlGenerator<ImageBundle>, ImageUrlGenerator>();
            container.Register<SettingsContext>(new SettingsContext());
            container.Register<IImagePathResolverProvider, ImagePathResolverProvider>();

            pipeline = new StyleSheetPipeline(container);  
        }

        [Test]
        public void Should_Contain_Default_Processors()
        {
            Assert.IsInstanceOf<StyleSheetMinifyProcessor>(pipeline[0]);
            Assert.IsInstanceOf<UrlAssignmentProcessor<StyleSheetBundle>>(pipeline[1]);
            Assert.IsInstanceOf<ExpandPathProcessor>(pipeline[2]);
            Assert.IsInstanceOf<StyleSheetMergeProcessor>(pipeline[3]);
        }
    }


}
