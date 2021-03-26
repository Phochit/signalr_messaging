using Hitter.DBML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitter.Controllers
{
    public class LoginStatusController
    {
        public void InsertLgStatus(Models.LoginStatus lgs)
        {
            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                var cu = db.LoginStatus.FirstOrDefault(c => c.loginid == lgs.loginid);
                if (cu == null)
                {
                    DBML.LoginStatus dblg = new DBML.LoginStatus();
                    //dblg.id = lgs.id;
                    dblg.loginid = lgs.loginid;
                    dblg.LastLoginTime = lgs.LastLoginTime;
                    dblg.loginstatus1 = lgs.loginstatus;

                    db.LoginStatus.InsertOnSubmit(dblg);
                    db.SubmitChanges();
                }
                else
                {
                    cu.loginstatus1 = 1;
                    cu.LastLoginTime = DateTime.UtcNow.AddMinutes(390);
                    db.SubmitChanges();
                }
            }
        }
        public void Logout(int id)
        {
            using (hitterDBDataContext db = new hitterDBDataContext())
            {
                var data = (from a in db.LoginStatus where a.loginid == id select a).FirstOrDefault();
                data.loginstatus1 = 0;
                db.SubmitChanges();
                
            }
        }
    }
}