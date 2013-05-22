using log4net;
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

namespace WebAssetBundler.Web.Mvc
{
    using System;

    public class Log4NetLogger : ILogger
    {
        private ILog infoLog;
        private ILog errorLog;

        public Log4NetLogger()
        {
            infoLog = LogManager.GetLogger("WabInfoLogger");
            errorLog = LogManager.GetLogger("WabErrorLogger");
        }

        public Log4NetLogger(ILog infoLog, ILog errorLog)
        {
            this.infoLog = infoLog;
            this.errorLog = errorLog;
        }

        public void Info(string message)
        {
            infoLog.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            infoLog.Info(message, exception);
        }

        public void Error(string message)
        {
            errorLog.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            errorLog.Error(message, exception);
        }
    }
}
