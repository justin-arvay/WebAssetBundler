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
    using TinyIoC;

    [TestFixture]
    public class ScriptPipelineTests
    {
        private ScriptPipeline pipeline;
        private TinyIoCContainer container;
        private SettingsContext settings;
        private Mock<ILogger> logger;

        [SetUp]
        public void Setup()
        {
            var minifier = new Mock<IScriptMinifier>();

            logger = new Mock<ILogger>();

            container = new TinyIoCContainer();
            container.Register<IScriptMinifier>((a, c) => minifier.Object);
            container.Register<IUrlGenerator<ScriptBundle>, BasicUrlGenerator<ScriptBundle>>();
            container.Register<ILogger>(logger.Object);

            settings = new SettingsContext();            
        }

        [Test]
        public void Should_Contain_Default_Processors_In_Debug_Mode()
        {
            settings.DebugMode = true;
            settings.MinifyIdentifier = ".min";

            pipeline = new ScriptPipeline(container, settings, logger.Object);            

            Assert.IsInstanceOf<AssignHashProcessor>(pipeline[0]);
            Assert.IsInstanceOf<UrlAssignmentProcessor<ScriptBundle>>(pipeline[1]);
            Assert.IsInstanceOf<MergeProcessor<ScriptBundle>>(pipeline[2]);    
        }

        [Test]
        public void Should_Contain_Default_Processors_Non_Debug_Mode()
        {
            settings.DebugMode = false;

            pipeline = new ScriptPipeline(container, settings, logger.Object);            
           
            Assert.IsInstanceOf<AssignHashProcessor>(pipeline[0]);
            Assert.IsInstanceOf<UrlAssignmentProcessor<ScriptBundle>>(pipeline[1]);
            Assert.IsInstanceOf<MinifyProcessor<ScriptBundle>>(pipeline[2]);
            Assert.IsInstanceOf<MergeProcessor<ScriptBundle>>(pipeline[3]);            
        }
    }
}
