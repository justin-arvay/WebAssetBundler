using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.Resource;
using NUnit.Framework;

namespace Tests.Resource
{
    [TestFixture]
    public class ResourceGroupTests
    {
        [Test]
        public void Constructor_Sets_Name()
        {
            var group = new ResourceGroup("John", false);

            Assert.AreEqual("John", group.Name);
        }

        [Test]
        public void Constructor_Sets_IsShared()
        {
            var group = new ResourceGroup("", true);

            Assert.True(group.IsShared);
        }

        [Test]
        public void Can_Set_Combined()
        {
            var group = new ResourceGroup("", false);

            //get the opposite of its current value so we always know it changed
            var value = group.Combined ? false : true;
                        
            group.Combined = value;

            Assert.AreEqual(value, group.Combined);
        }

        [Test]
        public void Can_Set_Compressed()
        {
            var group = new ResourceGroup("", false);

            //get the opposite of its current value so we always know it changed
            var value = group.Compressed ? false : true;

            group.Compressed = value;

            Assert.AreEqual(value, group.Compressed);
        }

        [Test]
        public void Can_Set_Version()
        {
            var group = new ResourceGroup("", false);

            group.Version = "2.0";

            Assert.AreEqual("2.0", group.Version);
        }

        [Test]
        public void Can_Set_Enabled()
        {
            var group = new ResourceGroup("", false);

            //get the opposite of its current value so we always know it changed
            var value = group.Enabled ? false : true;

            group.Enabled = value;

            Assert.AreEqual(value, group.Enabled);
        }

        [Test]
        public void Can_Set_DefaultPath()
        {
            var group = new ResourceGroup("", false);

            group.DefaultPath = "/path";

            Assert.AreEqual("/path", group.DefaultPath);
        }

        [Test]
        public void Resources_Are_Empty_By_Default()
        {
            var group = new ResourceGroup("", false);

            Assert.AreEqual(0, group.Resources.Count);
        }

        [Test]
        public void Can_Add_Resource()
        {
            var group = new ResourceGroup("", false);
            group.Resources.Add(new ResourceCompiler.Files.Resource("/path/file.js"));

            Assert.AreEqual(1, group.Resources.Count);
        }

        public void Adding_Duplicate_Item_Throws_Exception()
        {
            var group = new ResourceGroup("", false);

            group.Resources.Add(new ResourceCompiler.Files.Resource("/path/file.js"));
            Assert.Throws<ArgumentException>(() => group.Resources.Add(new ResourceCompiler.Files.Resource("/path/file.js")));

        }

        public void Setting_Duplicate_Item_To_Existing_Index_Throws_Exception()
        {
            var group = new ResourceGroup("", false);

            group.Resources.Add(new ResourceCompiler.Files.Resource("/path/file.js"));
            Assert.Throws<ArgumentException>(() => group.Resources.Insert(1, new ResourceCompiler.Files.Resource("/path/file.js")));
        }

    }
}
