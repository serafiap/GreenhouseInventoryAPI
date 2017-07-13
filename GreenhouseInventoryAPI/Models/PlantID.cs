using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace GreenhouseInventoryAPI.Models
{
    using ds = Database.DatabaseStrings;
    public class PlantID
    {
        public int PlantIDNumber;
        public string Genus;
        public string Species;
        public List<string> CommonNames;

        public PlantID()
        {
            PlantIDNumber = 0;
            Genus = "";
            Species = "";
            CommonNames = new List<string>();
        }

        public PlantID(int plantIDNumber, string genus, string species, string commonNames)
        {
            PlantIDNumber = plantIDNumber;
            Genus = genus;
            Species = species;
            CommonNames = DeserializeCommonNames(commonNames);
        }


        public List<string> DeserializeCommonNames(string names)
        {
            return names.Split(';').ToList<string>();
        }

        public string SearchableGenus()
        {
                return string.Format("%{0}%", Genus);
        }

        public string SearchableSpecies()
        {
                return string.Format("%{0}%", Species);
        }

        public string SearchableCommonNames()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var name in CommonNames)
            {
                string[] splitName = name.Split(' ');
                foreach (var namelet in splitName)
                {
                    sb.Append(string.Format("%{0}%", namelet));
                }
            }
            return sb.ToString();
        }

        public static List<PlantID> DatatableToList(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    List<PlantID> informationList = new List<PlantID>();

                    foreach (DataRow row in dt.Rows)
                    {
                        int ID;
                        int.TryParse(row[ds.ID].ToString(), out ID);
                        var plantID = new PlantID(
                            ID,
                            row[ds.Genus].ToString(),
                            row[ds.Species].ToString(),
                            row[ds.CommonNames].ToString()
                        );
                        informationList.Add(plantID);
                    }

                    return informationList;
                }
            }
            catch (Exception)
            {

                return new List<PlantID>() { new PlantID() };
            }
            return new List<PlantID>() { new PlantID() };
        }
    }
}