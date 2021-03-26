using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitter.DBML;
using Hitter.Models;

namespace Hitter.Controllers
{
    public class AuthController
    {

        public void Login(string username)
        {

            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                var cu = db.Customers.FirstOrDefault(c => c.name == username);

                if (cu == null)
                {
                    cu = new DBML.Customer { name = username, created_at = DateTime.UtcNow };

                    db.Customers.InsertOnSubmit(cu);
                    db.SubmitChanges();

                    
                }
                else
                {
                    Models.LoginStatus lgs = new Models.LoginStatus();
                    lgs.loginid = cu.id;
                    lgs.LastLoginTime = DateTime.UtcNow.AddMinutes(390);
                    lgs.loginstatus = 1;

                    LoginStatusController con = new LoginStatusController();
                    con.InsertLgStatus(lgs);
                }

            }
        }
        public Models.Customer GetCustomer(string name)
        {
            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                var data = db.Customers.FirstOrDefault(c => c.name == name);

                Models.Customer cu = new Models.Customer();
                cu.id = data.id;
                cu.name = data.name;
                cu.created_at = data.created_at;

                return cu;

            }
        }

    }
}