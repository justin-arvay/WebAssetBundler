namespace Tests.Resolvers
{
    using NUnit.Framework;
    using System;
    using ResourceCompiler.Resolvers;
    using ResourceCompiler.Resource;
    using System.Collections.Generic;

    [TestFixture]
    public class ResourceGroupResolverTests
    {

        [Test]
        public void Should_Resolve_A_List_Of_Sources_For_Each_Item_In_Group()
        {
            var group = new ResourceGroup("Test", false);
            group.Resources.Add(new ResourceCompiler.Files.Resource("~/Files/test.css"));
            group.Resources.Add(new ResourceCompiler.Files.Resource("~/Files/test2.css"));

            var resolver = new ResourceGroupResolver(group);

            Assert.AreEqual(2, ((IList<string>)resolver.Resolve()).Count);
        }

    }
}
