using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseInventoryAPI.Database
{
    public static class DatabaseStrings
    {
        //Tables
        public static string PlantInformation = "PlantInformation";
        public static string CurrentPlants = "CurrentPlants";
        public static string AccessCodes = "AccessCodes";

        //Columns
        //PlantInformation
        public static string Genus = "Genus";
        public static string Species = "Species";
        public static string CommonNames = "CommonNames";
        //public static string ID = "ID";

        public static string PIGenus = PlantInformation + ".Genus";
        public static string PISpecies = PlantInformation + ".Species";
        public static string PICommNames = PlantInformation + ".CommonNames";
        public static string PIID = PlantInformation + ".ID";

        //CurrentPlants
        public static string ID = "ID";
        public static string DatePlanted = "DatePlanted";
        public static string LastRepot = "LastRepot";
        public static string LastFert = "LastFertillized";
        public static string PlantID = "PlantID";
        public static string Active = "Active";
        public static string Barcode = "Barcode";
        public static string Location = "Location";

        public static string CPID = CurrentPlants + ".ID";
        public static string CPDatePlanted = CurrentPlants + ".DatePlanted";
        public static string CPLastRepot = CurrentPlants + ".LastRepot";
        public static string CPLastFert = CurrentPlants + ".LastFertillized";
        public static string CPPlantID = CurrentPlants + ".PlantID";
        public static string CPActive = CurrentPlants + ".Active";
        public static string CPBarcode = CurrentPlants + ".Barcode";

        //AccessCodes
        public static string ACCode = AccessCodes + ".Code";

        public static string reference = "SELECT PlantInformation.Genus, PlantInformation.Species, PlantInformation.CommonNames," +
                " CurrentPlants.DatePlanted, CurrentPlants.LastRepot, CurrentPlants.LastFertillized " +
                "FROM CurrentPlants INNER JOIN PlantInformation " +
                "ON CurrentPlants.PlantID = PlantInformation.ID " +
                "WHERE (Barcode = {0} AND Active = true)";
    }
}

namespace GreenhouseInventoryAPI.Database.Strings
{
    public static class Tables
    {
        public static string PlantInformation = "PlantInformation";
        public static string CurrentPlants = "CurrentPlants";
        public static string AccessCodes = "AccessCodes";
    }

    public static class PlantInformationColumns
    {
        public static string ID = "ID";
        public static string Genus = "Genus";
        public static string Species = "Species";
        public static string CommonNames = "CommonNames";
        public static string LowWater = "LowWater";
        public static string MedWater = "MedWater";
        public static string HighWater = "HighWater";
        public static string MaxHeight = "MaxHeight";
        public static string MaxSpread = "MaxSpread";
        public static string CuttingPropagation = "CuttingPropagation";
        public static string DivisionPropagation = "DivisionPropagation";
        public static string LeafPropagation = "LeafPropagation";
        public static string Poisonous = "Poisonous";
        public static string Sharp = "Sharp";
        public static string Irritant = "Irritant";
        public static string SpecialInstructions = "SpecialInstructions";
    }

}