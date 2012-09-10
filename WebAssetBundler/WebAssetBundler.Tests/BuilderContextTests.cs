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
    public class BuilderContextTests
    {
        private BuilderContext context;
        private WebAssetGroup group;

        [SetUp]
        public void Setup()
        {            
            context = new BuilderContext();
            group = new WebAssetGroup("", false);
        }

        [Test]
        public void Should_Be_Able_To_Combine()
        {
            group.Combine = true;
            context.DebugMode = true;
            context.EnableCombining = true;
            Assert.IsTrue(context.CanCombine(group), "First");

            group.Combine = true;
            context.DebugMode = false;
            context.EnableCombining = true;
            Assert.IsTrue(context.CanCombine(group), "Second");
        }

        [Test]
        public void Should_Not_Be_Able_To_Combine()
        {
            group.Combine = false;
            Assert.IsFalse(context.CanCombine(group), "first");

            group.Combine = true;
            context.DebugMode = true;
            context.EnableCombining = false;
            Assert.IsFalse(context.CanCombine(group), "second");

        }

        [Test]
        public void Should_Be_Able_To_Compress()
        {
            group.Compress = true;
            context.DebugMode = true;
            context.EnableCompressing = true;
            Assert.IsTrue(context.CanCompress(group), "first");

            group.Compress = true;
            context.DebugMode = false;
            context.EnableCompressing = true;
            Assert.IsTrue(context.CanCompress(group), "second");
        }

        [Test]
        public void Should_Not_Be_Able_To_Compress()
        {
            group.Compress = false;
            Assert.IsFalse(context.CanCompress(group), "first");

            group.Compress = true;
            context.DebugMode = true;
            context.EnableCompressing = false;
            Assert.IsFalse(context.CanCompress(group), "second");

        }

    }
}
