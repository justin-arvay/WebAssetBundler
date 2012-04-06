namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using System;
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

            Assert.AreEqual(2, resolver.Resolve().Count);
        }

        [Test]
        public void Should_Use_Source_For_Result_Path()
        {
            var path = "~/Files/test.css";
            var group = new WebAssetGroup("Test", false);
            group.Assets.Add(new WebAsset(path));

            var resolver = new WebAssetGroupResolver(group);
            var results = (List<WebAssetResolverResult>)resolver.Resolve();

            Assert.AreEqual(path, results[0].Path);
        }

    }
}
