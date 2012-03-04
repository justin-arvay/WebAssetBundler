using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ResourceCompiler
{
    public static class HtmlHelperExtension
    {
        private static readonly object syncLock = new object();
        private static ComponentFactory factory;

        public static ComponentFactory Reco(this HtmlHelper helper)
        {
            if (factory == null)
            {
                lock (syncLock)
                {
                    if (factory == null)
                    {
                        factory = new ComponentFactory(helper.ViewContext);
                    }
                }
            }

            return factory;
        }
    }
}
