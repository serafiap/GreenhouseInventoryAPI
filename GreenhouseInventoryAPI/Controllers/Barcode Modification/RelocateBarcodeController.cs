using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GreenhouseInventoryAPI.Models;
using Newtonsoft.Json;
using GreenhouseInventoryAPI.Database;

namespace GreenhouseInventoryAPI.Controllers
{
    public class RelocateBarcodeController : ApiController
    {
        public int Post([FromBody] string json)
        {
            BarcodeRelocationModel relocator = JsonConvert.DeserializeObject<BarcodeRelocationModel>(json);
            if (DBQueries.CheckAccess(relocator.AccessCode))
            {
                if (DBQueries.PotInfo(relocator.Barcode) != null)
                {
                    return DBQueries.RelocateBarcode(relocator);
                }
                return (int)ErrorCodes.BarcodeDoesNotExist;
            }
            return (int)ErrorCodes.AccessError;
        }
    }
}
