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
    public class ImagePathResolverProviderTests
    {
        private Mock<IUrlGenerator<ImageBundle>> urlGenerator;
        private Mock<IBundlesCache<ImageBundle>> bundleCache;
        private ImagePathResolverProvider provider;
        private SettingsContext settings;

        [SetUp]
        public void SetUp()
        {
            urlGenerator = new Mock<IUrlGenerator<ImageBundle>>();
            bundleCache = new Mock<IBundlesCache<ImageBundle>>();
            provider = new ImagePathResolverProvider();
            settings = new SettingsContext();
        }

        [Test]
        public void Should_Create_Unversioned_Resolver()
        {
            settings.VersionCssImages = false;

            var resolver = provider.GetResolver(settings);

            Assert.IsInstanceOf<UnversionedImagePathResolver>(resolver);
        }

        [Test]
        public void Should_Create_Versioned_Resolver()
        {
            settings.VersionCssImages = true;

            var resolver = provider.GetResolver(settings);

            Assert.IsInstanceOf<VersionedImagePathResolver>(resolver);
        }

        [Test]
        public void Should_Reuse_Instances_For_Performance()
        {
            settings.VersionCssImages = true;
            var resolver1 = provider.GetResolver(settings);
            var resolver2 = provider.GetResolver(settings);

            Assert.AreSame(resolver1, resolver2);

            settings.VersionCssImages = false;
            var resolver3 = provider.GetResolver(settings);
            var resolver4 = provider.GetResolver(settings);

            Assert.AreSame(resolver3, resolver4);
        }
    }
}
