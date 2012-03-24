namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
    using ResourceCompiler.Web.Mvc;
    using ResourceCompiler.Web.Mvc;
    using System.Collections.Generic;

    [TestFixture]
    public class WebAssetGroupResolverTests
    {

        [Test]
        public void Should_Resolve_A_List_Of_Sources_For_Each_Item_In_Group()
        {
            var group = new WebAssetGroup("Test", false);
            group.Assets.Add(new WebAsset("~/Files/test.css"));
            group.Assets.Add(new WebAsset("~/Files/test2.css"));

            var resolver = new WebAssetGroupResolver(group);

            Assert.AreEqual(2, ((IList<string>)resolver.Resolve()).Count);
        }

    }
}
