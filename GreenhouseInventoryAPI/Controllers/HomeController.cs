using System;
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

        public ActionResult PlantIDSubmission()
        {
            ViewBag.Title = "Search for ID";
            return View();
        }

        public ActionResult Table()
        {
            return View(new List<string>());
        }

        
        public ActionResult IDSearch(object id)
        {
            if (!(id is IConvertible))
                id = 0;
            var plantInfo = Database.DBQueries.PlantInformation(Convert.ToInt32(id));
            if (plantInfo == null)
                plantInfo = new Models.CompletePlantInformation();
            return View( plantInfo );
        }
    }
}
