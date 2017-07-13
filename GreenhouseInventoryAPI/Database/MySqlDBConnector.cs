using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseInventoryAPI.Database
{
    public class MySqlDBConnector : IDatabaseConnector
    {
        public string Address { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="databaseName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public MySqlDBConnector(string address, string databaseName, string userName, string password)
        {
            Address = address;
            DatabaseName = databaseName;
            UserName = userName;
            Password = password;
        }
        public string GetConnectionString()
        {
            return string.Format("Server={0};Database={1};Uid={2};Pwd={3};", Address, DatabaseName, UserName, Password);
        }
    }
}