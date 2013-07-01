using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Examples
{
    using WebAssetBundler.Web.Mvc;

    public class TestLogger : ILogger
    {
        public bool IsInfoEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsErrorEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}