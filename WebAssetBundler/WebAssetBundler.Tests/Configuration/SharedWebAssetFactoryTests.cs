using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class SharedWebAssetFactoryTests
    {
        [Test]
        public void Should_Use_Source()
        {
            var factory = new SharedWebAssetFactory();
            var element = new AssetConfigurationElement() { Source = "~/Test/File.css" };

            Assert.AreEqual("~/Test/File.css", factory.Create(element).Source);
        }
    }
}
