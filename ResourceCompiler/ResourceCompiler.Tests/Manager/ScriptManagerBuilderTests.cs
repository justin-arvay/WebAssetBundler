// ResourceCompiler - Compiles web assets so you dont have to.
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

namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Web.Mvc;
    using System.Web;
    using System;
    using System.IO;
    using System.Collections.Generic;

    [TestFixture]
    public class ScriptManagerBuilderTests
    {

        private ScriptManagerBuilder CreateBuilder(ViewContext context, Mock<ITagWriter> tagWriter)
        {
            var server = new Mock<HttpServerUtilityBase>();
            var collection = new WebAssetGroupCollection();
            var pathResolver = new Mock<IPathResolver>();
            var resolverFactory = new WebAssetResolverFactory(pathResolver.Object);
            var collectionResolver = new WebAssetGroupCollectionResolver(resolverFactory);
            var generator = new Mock<IWebAssetGenerator>();
            
            return new ScriptManagerBuilder(
                new ScriptManager(collection), 
                collection,
                context, 
                collectionResolver, 
                tagWriter.Object,                 
                generator.Object);
        }

        private ScriptManagerBuilder CreateBuilder(ViewContext context)
        {
            return CreateBuilder(context, new Mock<ITagWriter>());
        }

        [Test]
        public void Default_Group_Returns_Self_For_Chaining()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            Assert.IsInstanceOf<ScriptManagerBuilder>(builder.DefaultGroup(g => g.ToString()));
        }

        [Test]
        public void Scripts_Return_Self_For_Chaining()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());
            Assert.IsInstanceOf<ScriptManagerBuilder>(builder.Scripts(s => s.ToString()));
        }

        [Test]
        public void Can_Configure_Default_Group()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.DefaultGroup(g => g.Add("test/test.js"));

            Assert.AreEqual(1, builder.Manager.DefaultGroup.Assets.Count);
        }

        [Test]
        public void Can_Configure_Scripts()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.Scripts(s => s.AddGroup("test", group => group.ToString()));

            //there is 2 because of default group
            Assert.AreEqual(2, builder.Manager.Scripts.Count);
        }

        [Test]
        public void Should_Throw_Exception_When_Render_Called_More_Than_Once()
        {
            var builder = CreateBuilder(TestHelper.CreateViewContext());

            builder.Render();

            Assert.Throws<InvalidOperationException>(() => builder.Render());
        }

        [Test]
        public void Constructor_Should_Set_Manage()
        {
            Assert.NotNull(CreateBuilder(TestHelper.CreateViewContext()).Manager);
        }

        [Test]
        public void Should_Write_Tags()
        {
            var tagWriter = new Mock<ITagWriter>();
            var builder = CreateBuilder(TestHelper.CreateViewContext(), tagWriter);

            builder.Scripts(style => style
                .AddGroup("test", group => group
                    .Add("~/Files/test.js")
                    .Add("~/Files/test2.js")
                    .Combine(false)));

            builder.Render();

            tagWriter.Verify(t => t.Write(It.IsAny<TextWriter>(), It.IsAny<IList<ResolverResult>>()), Times.Exactly(1));
        }
    }
}
