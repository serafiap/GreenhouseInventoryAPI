﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenhouseInventoryAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Response.Redirect("http://greenhouse.aaronserafin.net/wordpress");
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
