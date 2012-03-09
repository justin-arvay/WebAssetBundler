namespace Tests.Resolvers
{
    using NUnit.Framework;
    using ResourceCompiler.Resolvers;
    using ResourceCompiler.Resource;

    [TestFixture]
    public class ResourceResolverFactoryTests
    {
        [Test]
        public void Should_Return_Group_Resolver()
        {
            var factory = new ResourceResolverFactory();

            Assert.IsInstanceOf<ResourceGroupResolver>(factory.Create(new ResourceGroup("", false)));
        }

        [Test]
        public void Should_Return_Combined_Group_Resolver()
        {
            var factory = new ResourceResolverFactory();
            var group = new ResourceGroup("", false) { Combined = true };

            Assert.IsInstanceOf<CombinedResourceGroupResolver>(factory.Create(group));
        }
    }
}
