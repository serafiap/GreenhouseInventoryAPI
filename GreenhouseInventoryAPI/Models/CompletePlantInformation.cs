using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace GreenhouseInventoryAPI.Models
{
    using ds = GreenhouseInventoryAPI.Database.Strings.PlantInformationColumns;
    public class CompletePlantInformation
    {
        public string ID = "";
        public string Genus ="";
        public string Species = "";
        public string CommonNames = "";
        public string LowWater = "";
        public string MedWater = "";
        public string HighWater = "";
        public string MaxHeight = "";
        public string MaxSpread = "";
        public string CuttingPropagation = "";
        public string DivisionPropagation = "";
        public string LeafPropagation = "";
        public string Poisonous = "";
        public string Sharp = "";
        public string Irritant = "";
        public string SpecialInstructions = "";

        public static List<CompletePlantInformation> DatatableToList(DataTable dt)
        {
            
            try
            {
                if (dt.Rows.Count > 0)
                {
                    List<CompletePlantInformation> informationList = new List<CompletePlantInformation>();

                    foreach (DataRow row in dt.Rows)
                    {
                        var plantInfo = new CompletePlantInformation() {
                            ID = row[ds.ID].ToString(),
                            Genus = row[ds.Genus].ToString(),
                            Species = row[ds.Species].ToString(),
                            CommonNames = row[ds.CommonNames].ToString(),
                            LowWater = row[ds.LowWater].ToString(),
                            MedWater = row[ds.MedWater].ToString(),
                            HighWater = row[ds.HighWater].ToString(),
                            MaxHeight = row[ds.MaxHeight].ToString(),
                            MaxSpread = row[ds.MaxSpread].ToString(),
                            CuttingPropagation = row[ds.CuttingPropagation].ToString(),
                            DivisionPropagation = row[ds.DivisionPropagation].ToString(),
                            LeafPropagation = row[ds.LeafPropagation].ToString(),
                            Poisonous = row[ds.Poisonous].ToString(),
                            Sharp = row[ds.Sharp].ToString(),
                            Irritant = row[ds.Irritant].ToString(),
                            SpecialInstructions = row[ds.SpecialInstructions].ToString()
                        };
                        informationList.Add(plantInfo);
                    }

                    return informationList;
                }
            }
            catch (Exception)
            {

                return new List<CompletePlantInformation>() { new CompletePlantInformation() };
            }
            return new List<CompletePlantInformation>() { new CompletePlantInformation() };
        }
    }

    
}