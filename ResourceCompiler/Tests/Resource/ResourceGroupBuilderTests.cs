using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ResourceCompiler.Resource;

namespace Tests.Resource
{
    [TestFixture]
    public class ResourceGroupBuilderTests
    {
        [Test]
        public void Can_Add_Resource_To_Group()
        {
            var group = new ResourceGroup("", false);
            var builder = new ResourceGroupBuilder(group);

            builder.Add("test.js");

            Assert.AreEqual(1, group.Resources.Count);

        }

        [Test]
        public void Add_Returns_Self_For_Chaining()
        {
            var group = new ResourceGroup("", false);
            var builder = new ResourceGroupBuilder(group);

            Assert.IsInstanceOf<ResourceGroupBuilder>(builder.Add("test.js"));
        }

        [Test]
        public void Can_Enable_Or_Disable_Group()
        {
            var group = new ResourceGroup("", false);
            var builder = new ResourceGroupBuilder(group);

            builder.Enable(true);
            Assert.True(group.Enabled, "Enabled Group");

            builder.Enable(false);
            Assert.False(group.Enabled, "Disabled Group");
        }

        [Test]
        public void Enable_Returns_Self_For_Chaining()
        {
            var group = new ResourceGroup("", false);
            var builder = new ResourceGroupBuilder(group);

            Assert.IsInstanceOf<ResourceGroupBuilder>(builder.Enable(true));
        }

        [Test]
        public void Can_Enable_Or_Disable_Compression()
        {
            var group = new ResourceGroup("", false);
            var builder = new ResourceGroupBuilder(group);

            builder.Compress(true);
            Assert.True(group.Compressed, "Enabled Compression");

            builder.Compress(false);
            Assert.False(group.Compressed, "Disabled Compression");
        }

        [Test]
        public void Compress_Returns_Self_For_Chaining()
        {
            var group = new ResourceGroup("", false);
            var builder = new ResourceGroupBuilder(group);

            Assert.IsInstanceOf<ResourceGroupBuilder>(builder.Compress(true));
        }

        [Test]
        public void Can_Enable_Or_Disable_Combining()
        {
            var group = new ResourceGroup("", false);
            var builder = new ResourceGroupBuilder(group);

            builder.Combine(true);
            Assert.True(group.Combined, "Enabled Combining");

            builder.Combine(false);
            Assert.False(group.Combined, "Disabled Combining");
        }

        [Test]
        public void Combine_Returns_Self_For_Chaining()
        {
            var group = new ResourceGroup("", false);
            var builder = new ResourceGroupBuilder(group);

            Assert.IsInstanceOf<ResourceGroupBuilder>(builder.Combine(true));
        }

        [Test]
        public void Can_Set_Version()
        {
            var group = new ResourceGroup("", false);
            var builder = new ResourceGroupBuilder(group);

            builder.Version("1.1.1");

            Assert.AreEqual("1.1.1", group.Version);
        }

        [Test]
        public void Version_Returns_Self_For_Chaining()
        {
            var group = new ResourceGroup("", false);
            var builder = new ResourceGroupBuilder(group);

            Assert.IsInstanceOf<ResourceGroupBuilder>(builder.Version("1.1"));
        }
    }
}
