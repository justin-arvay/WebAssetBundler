using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examples.Controllers
{
    public class DocumentationController : Controller
    {
        /// <summary>
        /// One ation for all documentation.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ActionResult Index(string path)
        {
            string viewName;

            if (string.IsNullOrEmpty(path))
            {
                viewName = "Index";
            }
            else
            {
                viewName = path.Replace("-", "");
            }

            viewName = "~/Views/Documentation/" + viewName + ".cshtml";

            var result = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);

            if (result.View == null)
            {
                return HttpNotFound();
            }

            return View(viewName);
        }        
    }
}
