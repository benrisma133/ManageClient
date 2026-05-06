using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageClient.Service
{
    static public class clsLogin
    {
        static public bool IsLogin(string username, string password)
        {
            if (username == null || password == null)
            {
                return false;
            }

            if (username != "admin" && password != "admin")
                return false;

            return true;
        }
    }
}
