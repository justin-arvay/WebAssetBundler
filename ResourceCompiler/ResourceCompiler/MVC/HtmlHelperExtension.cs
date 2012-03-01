using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ResourceCompiler.MVC
{
    public static class HtmlHelperExtension
    {
        public static ComponentFactory Reco(this HtmlHelper helper)
        {
            //todo: make this static
            return new ComponentFactory();
        }
    }
}
