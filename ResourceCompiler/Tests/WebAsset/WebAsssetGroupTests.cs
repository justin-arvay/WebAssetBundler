using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceCompiler.WebAsset;
using NUnit.Framework;

namespace Tests.WebAsset
{
    [TestFixture]
    public class WebAsssetGroupTests
    {
        [Test]
        public void Constructor_Sets_Name()
        {
            var group = new WebAssetGroup("John", false);

            Assert.AreEqual("John", group.Name);
        }

        [Test]
        public void Constructor_Sets_IsShared()
        {
            var group = new WebAssetGroup("", true);

            Assert.True(group.IsShared);
        }

        [Test]
        public void Can_Set_Combined()
        {
            var group = new WebAssetGroup("", false);

            //get the opposite of its current value so we always know it changed
            var value = group.Combined ? false : true;
                        
            group.Combined = value;

            Assert.AreEqual(value, group.Combined);
        }

        [Test]
        public void Can_Set_Compressed()
        {
            var group = new WebAssetGroup("", false);

            //get the opposite of its current value so we always know it changed
            var value = group.Compressed ? false : true;

            group.Compressed = value;

            Assert.AreEqual(value, group.Compressed);
        }

        [Test]
        public void Can_Set_Version()
        {
            var group = new WebAssetGroup("", false);

            group.Version = "2.0";

            Assert.AreEqual("2.0", group.Version);
        }

        [Test]
        public void Can_Set_Enabled()
        {
            var group = new WebAssetGroup("", false);

            //get the opposite of its current value so we always know it changed
            var value = group.Enabled ? false : true;

            group.Enabled = value;

            Assert.AreEqual(value, group.Enabled);
        }

        [Test]
        public void Can_Set_DefaultPath()
        {
            var group = new WebAssetGroup("", false);

            group.DefaultPath = "/path";

            Assert.AreEqual("/path", group.DefaultPath);
        }

        [Test]
        public void Resources_Are_Empty_By_Default()
        {
            var group = new WebAssetGroup("", false);

            Assert.AreEqual(0, group.Assets.Count);
        }

        [Test]
        public void Can_Add_Resource()
        {
            var group = new WebAssetGroup("", false);
            group.Assets.Add(new ResourceCompiler.Files.SourceWebAsset("/path/file.js"));

            Assert.AreEqual(1, group.Assets.Count);
        }

        public void Adding_Duplicate_Item_Throws_Exception()
        {
            var group = new WebAssetGroup("", false);

            group.Assets.Add(new ResourceCompiler.Files.SourceWebAsset("/path/file.js"));
            Assert.Throws<ArgumentException>(() => group.Assets.Add(new ResourceCompiler.Files.SourceWebAsset("/path/file.js")));

        }

        public void Setting_Duplicate_Item_To_Existing_Index_Throws_Exception()
        {
            var group = new WebAssetGroup("", false);

            group.Assets.Add(new ResourceCompiler.Files.SourceWebAsset("/path/file.js"));
            Assert.Throws<ArgumentException>(() => group.Assets.Insert(1, new ResourceCompiler.Files.SourceWebAsset("/path/file.js")));
        }

    }
}
