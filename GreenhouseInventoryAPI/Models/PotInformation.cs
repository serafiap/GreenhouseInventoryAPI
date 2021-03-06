﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace GreenhouseInventoryAPI.Models
{
    using ds = Database.DatabaseStrings;
    public class PotInformation
    {
        public string Barcode;
        public int PlantID;
        public int Location;
        //public string Genus;
        //public string Species;
        //public List<string> CommonNames;
        //public DateTime DatePlanted;
        //public DateTime LastRepot;
        //public DateTime LastFertillized;

        //public PotInformation (string plantID, string genus, string species, string commonName, string datePlanted, string lastRepot, string lastFertillized)
        //{
        //    PlantID = Convert.ToInt32(plantID);
        //    Genus = genus;
        //    Species = species;
        //    CommonNames = commonName.Split(';').ToList<string>();
        //    DateTime.TryParse(datePlanted, out DatePlanted);
        //    DateTime.TryParse(lastRepot, out LastRepot);
        //    DateTime.TryParse(lastFertillized, out LastFertillized);
        //}

        public PotInformation()
        {

        }

        public static List<PotInformation> DatatableToList(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    List<PotInformation> informationList = new List<PotInformation>();

                    foreach (DataRow row in dt.Rows)
                    {
                        var potInformation = new PotInformation()
                        {
                            Barcode = row[ds.Barcode].ToString(),
                            PlantID = Convert.ToInt32(row[ds.PlantID].ToString()),
                            Location = Convert.ToInt16(row[ds.Location].ToString())
                        };
                        //var potInformation = new PotInformation(

                        //    row[ds.PlantID].ToString(),
                        //    //row[ds.Genus].ToString(),
                        //    //row[ds.Species].ToString(),
                        //    //row[ds.CommonNames].ToString(),
                        //    //row[ds.DatePlanted].ToString(),
                        //    //row[ds.LastFert].ToString(),
                        //    //row[ds.LastRepot].ToString()
                        //);
                        //if (dt.Columns.Contains(ds.Barcode))
                        //    potInformation.Barcode = row[ds.Barcode].ToString();

                        informationList.Add(potInformation);
                    }

                    return informationList;
                }
                return new List<PotInformation>() { new PotInformation() };
            }
            catch (Exception)
            {

                return new List<PotInformation>() { new PotInformation()};
            }
        }
    }



    
}