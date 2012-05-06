using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResourceCompiler.Web.Mvc;
using System.Configuration;
using System.Web.Configuration;

namespace Examples.Controllers
{
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            MySection sections1 = (MySection)ConfigurationManager.GetSection("mySection");

            var sections = WebConfigurationManager.GetWebApplicationSection("reco") as SharedGroupConfigurationSection;

            foreach (var groups in sections.StyleSheets)
            {
                var e = groups;
            }
            //var config = new SharedGroupConfigurationSection();

            return View();
        }

        public ActionResult DefaultGroup()
        {
            return View();
        }
    }
}
