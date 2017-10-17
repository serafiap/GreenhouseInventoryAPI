using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseInventoryAPI.Models
{
    public class BarcodeAssignmentModel : IBarcodeModifier
    {
        public int Barcode = 0;
        public int PlantID = 0;
        public int Location = 0;
        public int AccessCode = 0;
        public string User = "";

        public int Execute()
        {
            return 0;
        }
    }
}