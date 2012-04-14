namespace ResourceCompiler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using ResourceCompiler.Web.Mvc;
    using ResourceCompiler.Web.Mvc;
    using Moq;

    [TestFixture]
    public class WebAssetResolverFactoryTests
    {
        private Mock<IPathResolver> pathResolver;

        public WebAssetResolverFactoryTests()
        {
            pathResolver = new Mock<IPathResolver>();
        }

        [Test]
        public void Should_Return_Group_Resolver()
        {
            var factory = new WebAssetResolverFactory(pathResolver.Object);

            Assert.IsInstanceOf<WebAssetGroupResolver>(factory.Create(new WebAssetGroup("", false)));
        }

        [Test]
        public void Should_Return_Combined_Group_Resolver()
        {
            var factory = new WebAssetResolverFactory(pathResolver.Object);
            var group = new WebAssetGroup("", false) { Combine = true };

            Assert.IsInstanceOf<CombinedWebAssetGroupResolver>(factory.Create(group));
        }
    }
}
