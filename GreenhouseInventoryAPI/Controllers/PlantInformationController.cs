using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GreenhouseInventoryAPI.Models;
using GreenhouseInventoryAPI.Database;

namespace GreenhouseInventoryAPI.Controllers
{
    public class PlantInformationController : ApiController
    {
        public List<CompletePlantInformation> Get()
        {
            return DBQueries.PlantInformation();
        }

        public CompletePlantInformation Get (int id)
        {
            return DBQueries.PlantInformation(id);
        }


    }
}
