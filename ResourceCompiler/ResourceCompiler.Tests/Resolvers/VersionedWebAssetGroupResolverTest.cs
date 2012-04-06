
namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class VersionedWebAssetGroupResolverTest
    {
        [Test]
        public void Should_Return_A_List_Of_Results()
        {
            var pathResolver = new Mock<IPathResolver>();
            var group = new WebAssetGroup("Test", false)
            {
                Version = "1.2"
            };

            group.Assets.Add(new WebAsset("~/Files/test.css"));
            group.Assets.Add(new WebAsset("~/Files/test2.css"));

            var resolver = new VersionedWebAssetGroupResolver(group, pathResolver.Object);

            Assert.AreEqual(2, resolver.Resolve().Count);
        }

        [Test]
        public void Should_Resolve_Path_For_Result()
        {
            var pathResolver = new Mock<IPathResolver>();
            var path = "/test/file.css";
            var group = new WebAssetGroup("Test", false) { Version = "1.2" };

            group.Assets.Add(new WebAsset("~/Files/test.css"));     
      
            pathResolver.Setup(m => m.Resolve(
                It.Is<string>(s => s.Equals(DefaultSettings.GeneratedFilesPath)),
                It.Is<string>(s => s.Equals(group.Version)),
                It.Is<string>(s => s.Equals(group.Name))))                
                .Returns(path);

            var resolver = new VersionedWebAssetGroupResolver(group, pathResolver.Object);
            var results = (List<WebAssetResolverResult>)resolver.Resolve();

            Assert.AreEqual(path, results[0].Path);
        }
    }
}
