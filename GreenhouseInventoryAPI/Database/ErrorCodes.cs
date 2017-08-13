using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseInventoryAPI.Database
{
    public enum ErrorCodes
    {
        Success = 1,
        BarcodeConflict = -1,
        BarcodeDoesNotExist = -2,
        PlantIDDoesNotExist = -3,
        AccessError = -100,
        SQLError = -500
    }
}