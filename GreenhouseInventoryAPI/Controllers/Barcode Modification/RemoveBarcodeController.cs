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
            if (DBQueries.CheckAccess(remover.AccessCode))
            {
                if (DBQueries.PotInfo(remover.Barcode) != null)
                {
                    return DBQueries.DeleteBarcode(remover);//1 if success, -500 if fail
                }
                return (int)ErrorCodes.BarcodeDoesNotExist;//Nothing to delete
            }
            return (int)ErrorCodes.AccessError; //Access code issue
        }
    }
}
