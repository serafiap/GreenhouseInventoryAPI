using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenhouseInventoryAPI.Database
{
    public interface IDatabaseConnector
    {
        string Address { get; set; }
        string DatabaseName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string GetConnectionString();
    }
}
