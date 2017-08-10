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
            if (DBQueries.CheckAccess(assignment.AccessCode))
            {
                var existingID = DBQueries.PotInfo(assignment.Barcode);
                if (existingID == null)
                {
                    if (DBQueries.PlantInformation(assignment.PlantID) != null)
                    {
                        return DBQueries.AssignBarcode(assignment);//1 if success, -500 if not
                    }
                    return -3; //Improper plant ID
                }

                return -1; //Barcode already exists 
            }
            return -100; //Access not correct
        }
    }
}
