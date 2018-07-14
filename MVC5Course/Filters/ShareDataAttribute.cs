﻿using System;
using System.Web.Mvc;

namespace MVC5Course.Filters
{
    public class ShareDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "Your application description page.";
            base.OnActionExecuting(filterContext);
        }
    }
}