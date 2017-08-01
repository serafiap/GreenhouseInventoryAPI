using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GreenhouseInventoryAPI.Models;
using GreenhouseInventoryAPI.Database;
using Newtonsoft.Json;

namespace GreenhouseInventoryAPI.Controllers
{
    public class ChangeBarcodeController : ApiController
    {
        public int Post ([FromBody] string json)
        {
            var changer = JsonConvert.DeserializeObject<BarcodeChangeModel>(json);

            if (DBQueries.CheckAccess(changer.AccessCode))
            {
                if (DBQueries.PotInfo(changer.OriginalBarcode) != new PotInformation())
                {
                    //Original barcode exists
                    if(DBQueries.PotInfo(changer.NewBarcode) == new PotInformation())
                    {
                        //New barcode doesn't already exist in database.
                        //Free to add new barcode
                        //TODO Code for UPDATing entry to new barcode
                        return 1;
                    }
                    return -1; //New barcode already exists
                }
                return -2; //Old barcode doesn't exist
            }
            return -100; //Access code doesn't match
        }
    }
}
