namespace Tests.Resolvers
{
    using NUnit.Framework;
    using ResourceCompiler.Resolvers;
    using ResourceCompiler.WebAsset;

    [TestFixture]
    public class WebAssetResolverFactoryTests
    {
        [Test]
        public void Should_Return_Group_Resolver()
        {
            var factory = new WebAssetResolverFactory();

            Assert.IsInstanceOf<WebAssetGroupResolver>(factory.Create(new WebAssetGroup("", false)));
        }

        [Test]
        public void Should_Return_Combined_Group_Resolver()
        {
            var factory = new WebAssetResolverFactory();
            var group = new WebAssetGroup("", false) { Combined = true };

            Assert.IsInstanceOf<CombinedWebAssetGroupResolver>(factory.Create(group));
        }
    }
}
