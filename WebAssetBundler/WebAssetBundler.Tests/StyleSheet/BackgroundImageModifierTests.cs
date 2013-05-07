// Web Asset Bundler - Bundles web assets so you dont have to.
// Copyright (C) 2012  Justin Arvay
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace WebAssetBundler.Web.Mvc.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;

    [TestFixture]
    public class BackgroundImageModifierTests
    {
        private BackgroundImageModifier modifier;
        private IList<ImagePipelineRunnerResult> results;

        [SetUp]
        public void Setup()
        {
            results = new List<ImagePipelineRunnerResult>();
            modifier = new BackgroundImageModifier(results);            
        }

        [Test]
        public void Should_Replace_Old_With_New()
        {
            var stream = "/Image/test.png".ToStream();
            var result = new ImagePipelineRunnerResult
            {
                OldPath = "/Image/test.png",
                NewPath = "/New/Image/test.png"
            };

            results.Add(result);

            var returnStream = modifier.Modify(stream, new AssetBaseImpl());

            Assert.AreEqual("/New/Image/test.png", returnStream.ReadToEnd());
        }
    }
}
