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
        /// Returns information on all pots flagged as ACTIVE
        /// </summary>
        /// <returns></returns>
        public static List<PotInformation> CurrentActiveInventory()
        {
            DbQuerier query = new DbQuerier(
                string.Format("SELECT * ") +
                string.Format("FROM {0}  ", ds.CurrentPlants) +
                string.Format("ORDER BY {0} ASC", ds.Barcode));
            //DbQuerier query = new DbQuerier(
            //    string.Format("SELECT {0}, {1}, {2},{3}, {4}, {5}, {6}, {7}, {8} ",
            //    ds.CPID, ds.CPBarcode, ds.PIGenus, ds.PISpecies, ds.PICommNames, ds.CPDatePlanted, ds.CPLastRepot, ds.CPLastFert, ds.CPPlantID) +
            //    string.Format("FROM {0} INNER JOIN {1} ", ds.CurrentPlants, ds.PlantInformation) +
            //    string.Format("ON {0} = {1} ", ds.CPPlantID, ds.PIID) +
            //    string.Format("WHERE {0} = true ", ds.CPActive) +
            //    string.Format("ORDER BY {0} ASC", ds.Barcode));
            DataTable dt = query.SendQuery();

            return PotInformation.DatatableToList(dt);
        }

        /// <summary>
        /// Returns most recent information of pot contents
        /// </summary>
        /// <param name="BarcodeNumber"></param>
        /// <returns></returns>
        public static PotInformation PotInfo(int BarcodeNumber)
        {
            return CurrentActiveInventory().Find(i => i.Barcode == BarcodeNumber.ToString());
            //DbQuerier query = new DbQuerier(
            //    string.Format("SELECT {0}, {1}, {2},{3}, {4}, {5}, {6}, {7} ",
            //    ds.Barcode, ds.PIGenus, ds.PISpecies, ds.PICommNames, ds.CPDatePlanted, ds.CPLastRepot, ds.CPLastFert, ds.PIID) +
            //    string.Format("FROM {0} INNER JOIN {1} ", ds.CurrentPlants, ds.PlantInformation) +
            //    string.Format("ON {0} = {1} ", ds.CPPlantID, ds.PIID) +
            //    string.Format("WHERE ({1} = {0} AND {2} = true) ", BarcodeNumber, ds.CPBarcode, ds.CPActive) +
            //    string.Format("ORDER BY {0} DESC LIMIT 1", ds.CPID));
            //DataTable dt = query.SendQuery();

            //return PotInformation.DatatableToList(dt)[0];
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
        

        public static List<CompletePlantInformation> PlantInformation()
        {
            DbQuerier query = new DbQuerier(
                string.Format("SELECT * FROM {0} ORDER BY {1} ASC, {2} ASC",
                Strings.Tables.PlantInformation, Strings.PlantInformationColumns.Genus, Strings.PlantInformationColumns.Species)
                );
            DataTable dt = query.SendQuery();
            return CompletePlantInformation.DatatableToList(dt);
        }

        public static CompletePlantInformation PlantInformation(int plantID)
        {
            return PlantInformation().Find(i => i.ID == plantID.ToString());
        }

        public static int AssignBarcode(BarcodeAssignmentModel assignment)
        {
            try
            {
                DbNonQuerier query = new DbNonQuerier(
                        string.Format("INSERT INTO {0} ({1}, {2}, {3}) ", Strings.Tables.CurrentPlants, ds.Barcode, ds.PlantID, ds.Location) +
                        string.Format("VALUES ({0}, {1}, {2})", assignment.Barcode, assignment.PlantID, assignment.Location)
                        );
                query.SendNonQuery();
                return (int)ErrorCodes.Success;
            }
            catch (Exception e)
            {
                return (int)ErrorCodes.SQLError;
            }
        }

        public static int DeleteBarcode(BarcodeRemovalModel remover)
        {
            try
            {
                DbNonQuerier query = new DbNonQuerier(
                        string.Format("DELETE FROM {0} WHERE {1} = {2}", Strings.Tables.CurrentPlants, ds.Barcode, remover.Barcode)
                        );
                query.SendNonQuery();
                return (int) ErrorCodes.Success;
            }
            catch (Exception)
            {
                return (int) ErrorCodes.SQLError;
            }
        }

        public static int RelocateBarcode(BarcodeRelocationModel relocator)
        {
            try
            {
                DbNonQuerier query = new DbNonQuerier(
                    string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}", Strings.Tables.CurrentPlants, ds.Location, relocator.Location,
                                                                              ds.Barcode, relocator.Barcode)
                    );
                query.SendNonQuery();
                return (int)ErrorCodes.Success;
            }
            catch (Exception e)
            {
                return (int)ErrorCodes.SQLError;
            }
        }

        public static bool CheckAccess(int AccessCode)
        {
            //TODO Add AccessCode Query
            return true;
        }
    }
}