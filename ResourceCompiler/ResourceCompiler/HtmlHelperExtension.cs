using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ResourceCompiler
{
    public static class HtmlHelperExtension
    {

        public static ComponentFactory Reco(this HtmlHelper helper)
        {
            return new ComponentFactory(helper.ViewContext);   
        }
    }
}
