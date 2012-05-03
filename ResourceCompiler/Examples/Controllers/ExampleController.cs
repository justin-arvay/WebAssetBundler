using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResourceCompiler.Web.Mvc;
using System.Configuration;

namespace Examples.Controllers
{
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            var section = ConfigurationManager.GetSection("reco");
            var config = new SharedGroupConfigurationSection();

            return View();
        }

        public ActionResult DefaultGroup()
        {
            return View();
        }
    }
}
