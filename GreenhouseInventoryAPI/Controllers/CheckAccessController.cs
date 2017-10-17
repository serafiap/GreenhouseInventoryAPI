using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GreenhouseInventoryAPI.Controllers
{
    public class CheckAccessController : ApiController
    {
        public string Get()
        {
            string clientAddress = HttpContext.Current.Request.UserHostAddress;
            return clientAddress;
        }

        public bool Get(int code)
        {
            return Database.DBQueries.CheckAccess(code);
        }
    }
}
