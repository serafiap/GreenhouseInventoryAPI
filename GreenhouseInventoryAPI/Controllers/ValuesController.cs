using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GreenhouseInventoryAPI.Models;
using Newtonsoft.Json;

namespace GreenhouseInventoryAPI.Controllers
{
    public class ValuesController : ApiController
    {
        string userInput = DateTime.Now.ToString();
        // GET api/values
        public string Get()
        {
            DbQuerier db = new DbQuerier("SELECT * FROM `Measurements`");
            var queryResults = db.SendQuery();
            return JsonConvert.SerializeObject(queryResults);
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public string Post([FromBody]string value)
        {
            DbNonQuerier db = new DbNonQuerier("INSERT INTO `Measurements` (`systemID`) VALUES('1')");
            db.SendNonQuery();
            userInput = value;
            return userInput + " " + DateTime.Now.ToString();
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
