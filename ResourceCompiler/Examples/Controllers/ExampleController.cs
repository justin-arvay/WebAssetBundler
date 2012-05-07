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
            var sections = WebConfigurationManager.GetWebApplicationSection("reco") as SharedGroupConfigurationSection;

            foreach (GroupConfigurationElementCollection group in sections.StyleSheets)
            {
                foreach (AssetConfigurationElement asset in group)
                {
                    var e = asset.Source;
                }

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
