using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MB
        public ActionResult Index()
        {
            ViewBag.Clients = db.Client.Take(5).ToList();
            ViewData.Model = "modelData";
            ViewData["a"] = "msgA";
            ViewBag.b = "msgB";
            TempData["c"] = "msgC";

            return View();
        }
    }
}