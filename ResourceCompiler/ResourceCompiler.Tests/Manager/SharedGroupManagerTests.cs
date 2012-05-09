

namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class SharedGroupManagerTests
    {
        [Test]
        public void Should_Have_Empty_Scripts()
        {
            var manager = new SharedGroupManager();

            Assert.AreEqual(0, manager.Scripts.Count);
        }

        [Test]
        public void Should_Have_Empty_StyleSheets()
        {
            var manager = new SharedGroupManager();

            Assert.AreEqual(0, manager.StyleSheets.Count);
        }

        [Test]
        public void Should_Get_Script_Group_By_Name()
        {
            var manager = new SharedGroupManager();
            var group = new WebAssetGroup("Foo", true, "~/");

            manager.Scripts.Add(group);

            Assert.AreSame(group, manager.GetScriptGroup("Foo"), "Same Case");
            Assert.AreSame(group, manager.GetScriptGroup("fOO"), "Reverse Case");
        }

        [Test]
        public void Should_Get_StyleSheet_Group_By_Name()
        {
            var manager = new SharedGroupManager();
            var group = new WebAssetGroup("Foo", true, "~/");

            manager.StyleSheets.Add(group);

            
            Assert.AreSame(group, manager.GetStyleSheetGroup("Foo"), "Same Case");
            Assert.AreSame(group, manager.GetStyleSheetGroup("fOO"), "Reverse Case");
        }

        [Test]
        public void Should_Return_Null_Script_Group_Does_Not_Exist()
        {
            var manager = new SharedGroupManager();

            Assert.IsNull(manager.GetScriptGroup("Foo"));
        }

        [Test]
        public void Should_Return_Null_StyleSheet_Group_Does_Not_Exist()
        {
            var manager = new SharedGroupManager();

            Assert.IsNull(manager.GetStyleSheetGroup("Foo"));
        }
    }
}
