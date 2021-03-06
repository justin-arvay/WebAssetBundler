﻿// Web Asset Bundler - Bundles web assets so you dont have to.
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
    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Web;
    using System.IO;
    using TinyIoC;

    [TestFixture]
    public class WebHostTests
    {
        private WebHost host;

        [SetUp]
        public void Setup()
        {
            host = new WebHost();
        }

        [Test]
        public void Should_Get_Tasks_In_Correct_Order()
        {
            host.Container.Register<TinyIoCContainer>(host.Container);
            host.Container.Register<IPluginLoader>(new Mock<IPluginLoader>().Object);

            var tasks = new List<IBootstrapTask>(host.GetBootstrapTasks());

            Assert.IsInstanceOf<LoadSettingsTask>(tasks[0]);
            Assert.IsInstanceOf<ConfigureCommonTask>(tasks[1]);
            Assert.IsInstanceOf<ConfigureImagesTask>(tasks[2]);
            Assert.IsInstanceOf<ConfigureStyleSheetsTask>(tasks[3]);
            Assert.IsInstanceOf<ConfigureScriptsTask>(tasks[4]);
        }

        [Test]
        public void Should_Run_Tasks()
        {
            var host = new Mock<WebHost>();
            host.CallBase = true;

            var task = new Mock<IBootstrapTask>();
            var logger = new Mock<ILogger>();

            host.Object.Container.Register<ILogger>(logger.Object);

            host.Setup(h => h.GetBootstrapTasks())
                .Returns(new List<IBootstrapTask>()
                {
                    task.Object
                });

            host.Object.RunBootstrapTasks();

            task.Verify(t => t.StartUp(It.IsAny<TinyIoCContainer>(), It.IsAny<ITypeProvider>()));
            task.Verify(t => t.ShutDown());
        }

        [Test]
        public void Should_Log_Tasks()
        {
            var host = new Mock<WebHost>();
            host.CallBase = true;

            var task = new Mock<IBootstrapTask>();
            var logger = new Mock<ILogger>();
            logger.Setup(l => l.IsInfoEnabled).Returns(true);

            host.Object.Container.Register<ILogger>(logger.Object);

            host.Setup(h => h.GetBootstrapTasks())
                .Returns(new List<IBootstrapTask>()
                {
                    task.Object
                });

            host.Object.RunBootstrapTasks();

            logger.Verify(l => l.Info(TextResource.Logging.RunBootstrapTask.FormatWith(task.Object.GetType().Name)));
        }

        [Test]
        public void Should_Initialize()
        {
            var request = new HttpRequest("", "http://www.google.com", "");
            var response = new HttpResponse(new StringWriter());

            System.Web.HttpContext.Current = new HttpContext(request, response);

            var container = host.Container;

            host.Initialize();

            Assert.IsInstanceOf<TinyIoCContainer>(container.Resolve<TinyIoCContainer>());
            Assert.IsInstanceOf<TypeProvider>(container.Resolve<ITypeProvider>());
            Assert.IsInstanceOf<PluginLoader>(container.Resolve<IPluginLoader>());
            Assert.IsInstanceOf<HttpContextBase>(container.Resolve<HttpContextBase>());
            Assert.IsInstanceOf<DoNothingLogger>(container.Resolve<ILogger>());

            //ensures sequential calls creates a new HttpContextBase so we are always dealing with the current context.
            Assert.AreNotSame(container.Resolve<HttpContextBase>(), container.Resolve<HttpContextBase>());
        }
    }
}
