using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class FacilitiesController : Controller
    {
        public ActionResult Library()
        {
            return View();
        }

        public ActionResult ComputerLab()
        {
            return View();
        }

        public ActionResult Bus()
        {
            return View();
        }
    }
}