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
    public class BundlePipelineTests
    {
        private BundlePipeline<BundleImpl> pipeline;
        private TinyIoCContainer ioc;

        [SetUp]
        public void Setup()
        {
            ioc = new TinyIoCContainer();
            pipeline = new BundlePipelineImpl(ioc);
        }

        [Test]
        public void Should_Process_Bundle()
        {
            var processor = new Mock<IPipelineProcessor<BundleImpl>>();
            var bundle = new BundleImpl();

            pipeline.Add(processor.Object);
            pipeline.Add(processor.Object);

            pipeline.Process(bundle);
            processor.Verify(p => p.Process(bundle), Times.Exactly(2));
        }

        [Test]
        public void Should_Add_Processor()
        {
            var processor = new Mock<IPipelineProcessor<BundleImpl>>();
            ioc.Register<IPipelineProcessor<BundleImpl>>(processor.Object);

            pipeline.Add<IPipelineProcessor<BundleImpl>>();

            Assert.AreEqual(1, pipeline.Count);
        }

        /*
        [Test]
        public void Should_Add_Processor_With_Factory()
        {
            var processor = new Mock<IPipelineProcessor<BundleImpl>>();
            ioc.Register<IPipelineProcessor<BundleImpl>>(processor.Object);

            pipeline.Add<ProcessorImpl.Factory>(c => c("test"));

            Assert.AreEqual(1, pipeline.Count);
        }
        */

        [Test]
        public void Should_Insert_Processor()
        {
            ioc.Register<IPipelineProcessor<BundleImpl>>(new ProcessorImpl());

            pipeline.Add(new FakeProcessor());
            pipeline.Add(new FakeProcessor());

            pipeline.Insert<IPipelineProcessor<BundleImpl>>(1);

            Assert.IsInstanceOf<ProcessorImpl>(pipeline[1]);
        }

        /*
        [Test]
        public void Should_Insert_Processor_With_Factory()
        {
            ioc.Register<IPipelineProcessor<BundleImpl>>(new ProcessorImpl());

            pipeline.Add(new FakeProcessor());
            pipeline.Add(new FakeProcessor());

            pipeline.Insert<ProcessorImpl.Factory>(1, c => c("test"));

            Assert.IsInstanceOf<ProcessorImpl>(pipeline[1]);
        }
        */

        [Test]
        public void Should_Remove_Processor()
        {
            pipeline.Add(new ProcessorImpl());
            pipeline.Add(new FakeProcessor());
            pipeline.Add(new ProcessorImpl());            

            pipeline.Remove<FakeProcessor>();

            Assert.IsInstanceOf<ProcessorImpl>(pipeline[0]);
            Assert.IsInstanceOf<ProcessorImpl>(pipeline[1]);
        }

        [Test]
        public void Should_Replace_Processor()
        {
            ioc.Register<IPipelineProcessor<BundleImpl>>(new ProcessorImpl());

            pipeline.Add(new ProcessorImpl());
            pipeline.Add(new FakeProcessor());
            pipeline.Add(new ProcessorImpl());

            pipeline.Replace<FakeProcessor, ProcessorImpl>();

            Assert.IsInstanceOf<ProcessorImpl>(pipeline[0]);
            Assert.IsInstanceOf<ProcessorImpl>(pipeline[1]);
            Assert.IsInstanceOf<ProcessorImpl>(pipeline[2]);
        }

        class FakeProcessor : IPipelineProcessor<BundleImpl>
        {
            public void Process(BundleImpl bundle)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
