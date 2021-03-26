using Hitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitter.DBML;

namespace Hitter.Controllers
{
    public class EmployeeController
    {
        public void InsertEmployee(Models.Employee emp)
        {
            using(hitterDBDataContext db=new hitterDBDataContext())
            {
                DBML.Employee dbemp = new DBML.Employee();
                dbemp.empName = emp.empName;
                dbemp.Salary = emp.Salary;
                dbemp.DeptName = emp.DeptName;
                dbemp.Designation = emp.Designation;

                db.Employees.InsertOnSubmit(dbemp);
                db.SubmitChanges();
            }
        }

    }
}