using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using GreenhouseInventoryAPI.Models;
using System.Data;
using GreenhouseInventoryAPI.Database;


namespace GreenhouseInventoryAPI.Controllers
{
    using ds = DatabaseStrings;
    public class PlantInventoryController : ApiController
    {
        //TODO Add contents
        public List<PotInformation> Get()
        {
            return new List<PotInformation>();
        }
        //Request plant information by barcode ID
        public List<PotInformation> Get(int id)
        {
            if (id > 0)
            {
                return new List<PotInformation>() { DBQueries.PotInfo(id) };
            }
            return DBQueries.CurrentActiveInventory();
        }

        public int Post([FromBody] string json)
        {
            PotChangeRequest request = JsonConvert.DeserializeObject<PotChangeRequest>(json);

            return request.FulfillRequest();
        }

        

        


    }
}
