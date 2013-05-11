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
    using System.Web;
    using TinyIoC;

    [TestFixture]
    public class LoadSettingsTaskTests
    {
        private LoadSettingsTask task;
        private TinyIoCContainer container;
        private Mock<ITypeProvider> typeProvider;

        [SetUp]
        public void Setup()
        {
            task = new LoadSettingsTask();
            typeProvider = new Mock<ITypeProvider>();
            container = new TinyIoCContainer();
        }

        [Test]
        public void Should_Load_Settings()
        {
            var httpContext = TestHelper.CreateMockedHttpContext(true);

            container.Register<HttpContextBase>(httpContext.Object);

            task.StartUp(container, typeProvider.Object);

            var settings = container.Resolve<SettingsContext>();

            //useful for checking default of config section
            Assert.IsFalse(settings.DebugMode);
            Assert.AreEqual(".min", settings.MinifyIdentifier);
            Assert.IsTrue(settings.EnableImagePipeline);
            Assert.AreEqual(TestHelper.ApplicationPath, settings.AppRootDirectory.FullPath);
        }
    }
}
