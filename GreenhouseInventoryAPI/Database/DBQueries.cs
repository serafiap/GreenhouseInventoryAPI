using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenhouseInventoryAPI.Models;
using System.Data;
using GreenhouseInventoryAPI.Database;

namespace GreenhouseInventoryAPI.Database
{
    using ds = DatabaseStrings;

    public static class DBQueries
    {
        /// <summary>
        /// Returns most recent information of pot contents
        /// </summary>
        /// <param name="BarcodeNumber"></param>
        /// <returns></returns>
        public static PotInformation PotInfo(int BarcodeNumber)
        {
            DbQuerier query = new DbQuerier(
                string.Format("SELECT {0}, {1}, {2},{3}, {4}, {5}, {6}, {7} ",
                ds.PIID, ds.PIGenus, ds.PISpecies, ds.PICommNames, ds.CPDatePlanted, ds.CPLastRepot, ds.CPLastFert, ds.PIID) +
                string.Format("FROM {0} INNER JOIN {1} ", ds.CurrentPlants, ds.PlantInformation) +
                string.Format("ON {0} = {1} ", ds.CPPlantID, ds.PIID) +
                string.Format("WHERE ({1} = {0} AND {2} = true) ", BarcodeNumber, ds.CPBarcode, ds.CPActive) +
                string.Format("ORDER BY {0} DESC LIMIT 1", ds.CPID));
            DataTable dt = query.SendQuery();
            
            return PotInformation.DatatableToList(dt)[0];
        }

        /// <summary>
        /// Returns information on all pots flagged as ACTIVE
        /// </summary>
        /// <returns></returns>
        public static List<PotInformation> CurrentActiveInventory()
        {
            DbQuerier query = new DbQuerier(
                string.Format("SELECT {0}, {1}, {2},{3}, {4}, {5}, {6}, {7} ",
                ds.PIID, ds.PIGenus, ds.PISpecies, ds.PICommNames, ds.CPDatePlanted, ds.CPLastRepot, ds.CPLastFert, ds.PIID) +
                string.Format("FROM {0} INNER JOIN {1} ", ds.CurrentPlants, ds.PlantInformation) +
                string.Format("ON {0} = {1} ", ds.CPPlantID, ds.PIID) +
                string.Format("WHERE {0} = true ", ds.CPActive) +
                string.Format("ORDER BY {0} ASC", ds.Barcode));
            DataTable dt = query.SendQuery();

            return PotInformation.DatatableToList(dt);
        }
        
        /// <summary>
        /// Searches for plant ID Number, Genus, Species, and Common Names
        /// Based on Missing, Incomplete, or Complete PlantID
        /// </summary>
        /// <param name="pid"></param>
        /// <returns>List of plant information that matches the search</returns>
        public static List<PlantID> SearchForPlantID(PlantID pid)
        {
            DbQuerier query = new DbQuerier(
                string.Format("SELECT {0}, {1}, {2}, {3} ", ds.PIID, ds.PIGenus, ds.PISpecies, ds.PICommNames) +
                string.Format("FROM {0} WHERE (", ds.PlantInformation) +
                string.Format("{0} LIKE '{1}' AND ", ds.Genus, pid.SearchableGenus()) +
                string.Format("{0} LIKE '{1}' AND ", ds.Species, pid.SearchableSpecies()) +
                string.Format("{0} LIKE '{1}')", ds.CommonNames, pid.SearchableCommonNames())
                );
            DataTable dt = query.SendQuery();
            return PlantID.DatatableToList(dt);
        }
    }
}