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
    public class AssignBarcodeController : ApiController
    {
        public int Post ([FromBody] string json)
        {
            BarcodeAssignmentModel assignment = JsonConvert.DeserializeObject<BarcodeAssignmentModel>(json);
            if (DBQueries.PotInfo(assignment.Barcode) == new PotInformation())
            {
                if (assignment.PlantID > 0)
                {
                    //TODO Method for inserting
                    return 1;
                }
                return -1; //Improper plant ID
            }
                    
            return -2; //Barcode already exists
        }
    }
}
