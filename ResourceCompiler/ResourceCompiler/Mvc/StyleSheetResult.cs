using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    public class StyleSheetResult : ContentResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            ContentType = "text/css";
            base.ExecuteResult(context);
        }
    }
}
