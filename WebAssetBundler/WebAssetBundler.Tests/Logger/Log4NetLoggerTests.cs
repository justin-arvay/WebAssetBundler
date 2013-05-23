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
    using log4net;
    using System;

    [TestFixture]
    public class Log4NetLoggerTests
    {
        private Log4NetLogger logger;
        private Mock<ILog> log;

        [SetUp]
        public void Setup()
        {
            log = new Mock<ILog>();

            logger = new Log4NetLogger(log.Object);
        }

        [Test]
        public void Should_Log_Info()
        {
            logger.Info("info");

            log.Verify(i => i.Info("info"));
        }

        [Test]
        public void Should_Log_Info_And_Exception()
        {
            var exception = new Exception();
            logger.Info("info", exception);

            log.Verify(i => i.Info("info", exception));
        }

        [Test]
        public void Should_Log_Error()
        {
            logger.Error("error");

            log.Verify(i => i.Error("error"));
        }

        [Test]
        public void Should_Log_Error_And_Exception()
        {
            var exception = new Exception();
            logger.Error("error", exception);

            log.Verify(i => i.Error("error", exception));
        }

        [Test]
        public void Should_Have_Info_Enabled()
        {
            log.Setup(l => l.IsInfoEnabled).Returns(true);
            Assert.IsTrue(logger.IsInfoEnabled);
        }

        [Test]
        public void Should_Have_Error_Enabled()
        {
            log.Setup(l => l.IsErrorEnabled).Returns(true);
            Assert.IsTrue(logger.IsErrorEnabled);
        }
    }
}
