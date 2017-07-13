using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using Newtonsoft.Json;
using GreenhouseInventoryAPI.Database;

namespace GreenhouseInventoryAPI.Models
{
    

    public class DbNonQuerier
    {
        private string _commandText;
        
        public DbNonQuerier(string query)
        {
            _commandText = query;
        }

        public string connectionString
        {
            get
            {
                return new Database.MySqlDBConnector(DBCredentials.Address, DBCredentials.DatabaseName, DBCredentials.UserName, DBCredentials.Password)
                    .GetConnectionString();
            }
        }

        public void SendNonQuery()
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try

                {

                    connection.Open();
                    
                    switch (connection.State)
                    {

                        case System.Data.ConnectionState.Open:

                            // Connection has been made
                            new MySqlCommand(_commandText, connection).ExecuteNonQuery();
                            break;

                        case System.Data.ConnectionState.Closed:

                            // Connection could not be made, throw an error

                            throw new Exception("The database connection state is Closed");
                            

                        default:

                            // Connection is actively doing something else

                            break;

                    }

                    

                }

                catch (MySql.Data.MySqlClient.MySqlException mySqlException)

                {

                    // Use the mySqlException object to handle specific MySql errors

                }

                catch (Exception exception)

                {

                    // Use the exception object to handle all other non-MySql specific errors

                }

                finally

                {

                    // Make sure to only close connections that are not in a closed state

                    if (connection.State != System.Data.ConnectionState.Closed)

                    {

                        // Close the connection as a good Garbage Collecting practice

                        connection.Close();

                    }

                }
            }
        }


    }

    public class DbQuerier
    {
        private string _UserName = "serafina1";
        private string _Password = "N0m0rehax";
        //private DeviceReport _data;

        private string _commandText;

        //public DbDataPoster(string username, string password, DeviceReport deviceReport)
        //{
        //    _UserName = username;
        //    _Password = password;
        //    _data = deviceReport;

        //    _commandText = string.Format("INSERT INTO `Measurements` (`systemID`, `coarseLevel`, `temp1`, `temp2`, `temp3`, `sonicLevel`) VALUES('1', '{0}', '{1}', '{2}', '{3}', '{4}')",
        //        Enum.GetName(typeof(Level), _data.CoarseWaterLevel), _data.Temperature1, _data.Temperature2, _data.Temperature3, _data.SonicWaterDepth);
        //}

        public DbQuerier(string query)
        {
            _commandText = query;
        }
        public DbQuerier(string username, string password, string query)
        {
            _UserName = username;
            _Password = password;

            _commandText = query;
        }
        public string connectionString
        {
            get
            {
                return new Database.MySqlDBConnector(DBCredentials.Address, DBCredentials.DatabaseName, _UserName, _Password)
                    .GetConnectionString();
            }
        }

        public DataTable SendQuery()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    switch (connection.State)
                    {
                        case System.Data.ConnectionState.Open:

                            // Connection has been made
                            MySqlCommand cmd = new MySqlCommand(_commandText, connection);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                            }

                            break;

                        case System.Data.ConnectionState.Closed:

                            // Connection could not be made, throw an error

                            throw new Exception("The database connection state is Closed");


                        default:

                            // Connection is actively doing something else

                            break;

                    }
                    
                }

                catch (MySql.Data.MySqlClient.MySqlException mySqlException)

                {
                    
                }

                catch (Exception exception)

                {
                    
                }

                finally

                {

                    // Make sure to only close connections that are not in a closed state

                    if (connection.State != System.Data.ConnectionState.Closed)

                    {

                        // Close the connection as a good Garbage Collecting practice

                        connection.Close();

                    }

                }
            }

            return dt;

        }
    }
    
    public static class DBHelperMethods
    {
        public static T DatatableRowToObject<T>(DataRow row)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(row));
        }
    }

}