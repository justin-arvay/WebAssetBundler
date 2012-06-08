using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAssetBundler.Web.Mvc;
using System.Configuration;
using System.Web.Configuration;

namespace Examples.Controllers
{
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            

            return View();
        }

        public ActionResult DefaultGroup()
        {
            return View();
        }
    }
}
