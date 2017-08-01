using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using GreenhouseInventoryAPI.Models;
using GreenhouseInventoryAPI.Database;

namespace GreenhouseInventoryAPI.Controllers
{
    public class RemoveBarcodeController : ApiController
    {
        public int Post ([FromBody] string json)
        {
            var remover = JsonConvert.DeserializeObject<BarcodeRemovalModel>(json);
            if (DBQueries.PotInfo(remover.Barcode) != new PotInformation())
            {
                //Run delete code and check access
                return -1;
            }
            //Nothing to delete
            return -1;
        }
    }
}
