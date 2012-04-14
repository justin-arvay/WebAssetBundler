namespace ResourceCompiler.Web.Mvc.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using ResourceCompiler.Web.Mvc;


    [TestFixture]
    public class WebAssetGroupBuilderTests
    {
        [Test]
        public void Can_Add_Resource_To_Group()
        {
            var group = new WebAssetGroup("", false);
            var builder = new WebAssetGroupBuilder(group);

            builder.Add("test.js");

            Assert.AreEqual(1, group.Assets.Count);

        }

        [Test]
        public void Add_Returns_Self_For_Chaining()
        {
            var group = new WebAssetGroup("", false);
            var builder = new WebAssetGroupBuilder(group);

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.Add("test.js"));
        }

        [Test]
        public void Can_Enable_Or_Disable_Group()
        {
            var group = new WebAssetGroup("", false);
            var builder = new WebAssetGroupBuilder(group);

            builder.Enable(true);
            Assert.True(group.Enabled, "Enabled Group");

            builder.Enable(false);
            Assert.False(group.Enabled, "Disabled Group");
        }

        [Test]
        public void Enable_Returns_Self_For_Chaining()
        {
            var group = new WebAssetGroup("", false);
            var builder = new WebAssetGroupBuilder(group);

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.Enable(true));
        }

        [Test]
        public void Can_Enable_Or_Disable_Compression()
        {
            var group = new WebAssetGroup("", false);
            var builder = new WebAssetGroupBuilder(group);

            builder.Compress(true);
            Assert.True(group.Compress, "Enabled Compression");

            builder.Compress(false);
            Assert.False(group.Compress, "Disabled Compression");
        }

        [Test]
        public void Compress_Returns_Self_For_Chaining()
        {
            var group = new WebAssetGroup("", false);
            var builder = new WebAssetGroupBuilder(group);

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.Compress(true));
        }

        [Test]
        public void Can_Enable_Or_Disable_Combining()
        {
            var group = new WebAssetGroup("", false);
            var builder = new WebAssetGroupBuilder(group);

            builder.Combine(true);
            Assert.True(group.Combine, "Enabled Combining");

            builder.Combine(false);
            Assert.False(group.Combine, "Disabled Combining");
        }

        [Test]
        public void Combine_Returns_Self_For_Chaining()
        {
            var group = new WebAssetGroup("", false);
            var builder = new WebAssetGroupBuilder(group);

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.Combine(true));
        }

        [Test]
        public void Can_Set_Version()
        {
            var group = new WebAssetGroup("", false);
            var builder = new WebAssetGroupBuilder(group);

            builder.Version("1.1.1");

            Assert.AreEqual("1.1.1", group.Version);
        }

        [Test]
        public void Version_Returns_Self_For_Chaining()
        {
            var group = new WebAssetGroup("", false);
            var builder = new WebAssetGroupBuilder(group);

            Assert.IsInstanceOf<WebAssetGroupBuilder>(builder.Version("1.1"));
        }
    }
}
