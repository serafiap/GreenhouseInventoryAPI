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
    public class PlantIDController : ApiController
    {
        public List<PlantID> Post ([FromBody] string ID)
        {
            PlantID incomingInformation = Newtonsoft.Json.JsonConvert.DeserializeObject<PlantID>(ID);

            return DBQueries.SearchForPlantID(incomingInformation);
        }
    }
}
