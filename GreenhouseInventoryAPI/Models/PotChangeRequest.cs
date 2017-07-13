using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenhouseInventoryAPI.Database;
using System.Data;

namespace GreenhouseInventoryAPI.Models
{
    using ds = DatabaseStrings;
    public class PotChangeRequest
    {
        //TODO Add a ChangeType for changing the pot number but keeping everything else the same.
        public string ChangeType;
        public int Barcode;
        public int NewPlantID;
        public DateTime Date;
        public int AccessCode;


        private string _StringDate;

        public int FulfillRequest()
        {
            _StringDate = string.Format("\"{0}\"", Date.ToString("yyyy-MM-dd"));
            int result = -100;
            if (HasAccess() == 1)
            {
                switch (ChangeType)
                {
                    case ChangeTypes.Activate:
                        result = ActivatePot();
                        break;
                    case ChangeTypes.Deactivate:
                        result = DeactivatePot();
                        break;
                    case ChangeTypes.Replace:
                        result = ReplacePot();
                        break;
                    case ChangeTypes.Fertillize:
                        result = Fertillize();
                        break;
                    case ChangeTypes.Repot:
                        result = Repot();
                        break;
                    default:
                        break;
                } 
            }
            return result;
        }

        private int HasAccess()
        {
            try
            {
                DbQuerier db = new DbQuerier(string.Format(
                        "SELECT * FROM {0} WHERE {1} = {2}", ds.AccessCodes, ds.ACCode, AccessCode));
                DataTable dt = db.SendQuery();
                if (dt.Rows.Count > 0)
                    return 1;
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //Tries to get the current active ID for the barcode number
        //-1 = error
        //0 = no active pot
        //others = ID#
        private int GetActiveID(int barcode)
        {
            try
            {
                DbQuerier db = new DbQuerier(string.Format(
                        "SELECT {0} FROM {1} WHERE ({2} = {3} AND {4} = true) ORDER BY {5} DESC LIMIT 1",
                        ds.ID, ds.CurrentPlants, ds.Barcode, barcode, ds.Active, ds.ID));
                DataTable dt = db.SendQuery();

                switch (dt.Rows.Count)
                {
                    case 0:
                        return 0;
                    case 1:
                        int i = 0;
                        int.TryParse(dt.Rows[0][ds.ID].ToString(), out i);
                        return i;
                    default:
                        return -1;
                }
            }
            catch (Exception e)
            {

                return -1;
            }
        }
        //-1 = error
        private int DeactivatePot()
        {
            //Checks if there is a pot to deactivate and return ID number
            int currentlyActiveID = GetActiveID(Barcode);

            switch (currentlyActiveID)
            {
                case 0:
                    return 1;
                case -1:
                    return -1;
                default:

                    try
                    {
                        DbNonQuerier db = new DbNonQuerier(string.Format(
                                        "UPDATE {0} SET {1}  = 0 WHERE ({2} = {3} AND {1} = true)",
                                        ds.CurrentPlants, ds.Active, ds.Barcode, Barcode));
                        db.SendNonQuery();
                        return DeactivatePot();
                    }
                    catch (Exception)
                    {

                        return -2;
                    }
            }
            //if (currentlyActiveID != -1 && currentlyActiveID != 0)
            //{
            //    try
            //    {
            //        DbNonQuerier db = new DbNonQuerier(string.Format(
            //            "UPDATE {0} SET {1}  = 0 WHERE ({2} = {3} AND {4} = true)",
            //            ds.CurrentPlants, ds.Active, ds.Barcode, Barcode));
            //        db.SendNonQuery();
            //        return 1;
            //    }
            //    catch (Exception)
            //    {
            //        return -2;
            //    }
            //}
            //return currentlyActiveID;
        }
        private int DeactivatePot(int id)
        {
            int currentlyActiveID = GetActiveID(id);

            switch (currentlyActiveID)
            {
                case 0:
                    return 1;
                case -1:
                    return -1;
                default:

                    try
                    {
                        string cmd = string.Format(
                                        "UPDATE {0} SET {1}  = 0 WHERE ({2} = {3} AND {1} = true)",
                                        ds.CurrentPlants, ds.Active, ds.Barcode, id);
                        DbNonQuerier db = new DbNonQuerier(cmd);
                        db.SendNonQuery();
                        return DeactivatePot();
                    }
                    catch (Exception)
                    {

                        return -2;
                    }
            }
        }

        //-3 = error
        private int ActivatePot()
        {
            int successCode = DeactivatePot();
            if (successCode == 1)
            {
                try
                {
                    DbNonQuerier db = new DbNonQuerier(string.Format(
                                "INSERT INTO {0} ({1}, {2}, {3}, {4}) VALUES ({5}, {6}, {7}, true)",
                                ds.CurrentPlants,
                                ds.Barcode, ds.PlantID, ds.DatePlanted, ds.Active,
                                Barcode, NewPlantID, _StringDate));
                    db.SendNonQuery();
                    return 1;
                }
                catch (Exception)
                {
                    return -3;
                }
            }
            else
            {
                return successCode;
            }
        }

        private int ReplacePot()
        {
            return ActivatePot();
        }

        //-4 = error
        private int Fertillize()
        {
            int entryToUpdate = GetActiveID(Barcode);
            if (entryToUpdate > 0)
            {
                int potDeactivated = DeactivatePot(Barcode);
                if (potDeactivated == 1)
                {
                    try
                    {
                        DbNonQuerier db = new DbNonQuerier(string.Format(
                                        "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}) SELECT {1}, {2}, {3}, {4}, {7}, true FROM {0} WHERE {8} = {9}",
                                        ds.CurrentPlants,
                                        ds.Barcode, ds.PlantID, ds.DatePlanted, ds.LastRepot, ds.LastFert, ds.Active,
                                        _StringDate, ds.ID, entryToUpdate));
                        db.SendNonQuery();
                        return 1;
                    }
                    catch (Exception)
                    {
                        return -4;
                    }
                }
            }
            
            return -1;
        }

        private int Repot()
        {
            int entryToUpdate = GetActiveID(Barcode);
            if (entryToUpdate > 0)
            {
                int potDeactivated = DeactivatePot(Barcode);
                if (potDeactivated == 1)
                {
                    try
                    {
                        DbNonQuerier db = new DbNonQuerier(string.Format(
                                        "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}) SELECT {1}, {2}, {3}, {7}, {5}, true FROM {0} WHERE {8} = {9}",
                                        ds.CurrentPlants,
                                        ds.Barcode, ds.PlantID, ds.DatePlanted, ds.LastRepot, ds.LastFert, ds.Active,
                                        _StringDate, ds.CPID, entryToUpdate));
                        db.SendNonQuery();
                        return 1;
                    }
                    catch (Exception)
                    {
                        return -4;
                    }
                }
            }

            return -1;
        }


    }

    public static class ChangeTypes
    {
        public const string Deactivate = "deactivate";
        public const string Activate = "activate";
        public const string Replace = "replace";
        public const string Fertillize = "fertillize";
        public const string Repot = "repot";
    }
}