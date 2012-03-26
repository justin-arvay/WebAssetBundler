namespace ResourceCompiler.Web.Mvc.Tests
{
    using System;
    using NUnit.Framework;
    using ResourceCompiler.Web.Mvc;

    [TestFixture]
    public class UrlResolverTests
    {
        [Test]
        public void Resolve_Should_Throw_Excepton_When_Not_Running_In_Web_Server()
        {
            var resolver = new UrlResolver(TestHelper.CreateRequestContext());

            //Assert.Throws<ArgumentNullException>(() => resolver.Resolve("~/scripts/jquery-1.3.2.min.js"));
        }
        
    }
}
