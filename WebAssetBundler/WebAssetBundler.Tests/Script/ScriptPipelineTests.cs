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

    [TestFixture]
    public class ScriptPipelineTests
    {
        private ScriptPipeline pipeline;
        private TinyIoCContainer container;

        [SetUp]
        public void Setup()
        {
            var compressor = new Mock<IScriptCompressor>();

            container = new TinyIoCContainer();
            pipeline = new ScriptPipeline(container);

            container.Register<IScriptCompressor>((a, c) => compressor.Object);
        }

        [Test]
        public void Should_Contain_Default_Processors()
        {
            Assert.IsInstanceOf<ScriptMergeProcessor>(pipeline[0]);
            Assert.IsInstanceOf<ScriptCompressProcessor>(pipeline[1]);
        }
    }
}
