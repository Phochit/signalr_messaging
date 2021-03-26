using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hitter.Models;
using Hitter.Controllers;

namespace Hitter
{
    public partial class EmployeeForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSaveEmployee_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            EmployeeController ec = new EmployeeController();
            emp.empName = txtEmpName.Text.Trim();
            emp.Salary = Convert.ToInt32(txtSalary.Text.Trim());
            emp.DeptName = txtDept.Text.Trim();
            emp.Designation = txtDesignation.Text.Trim();

            ec.InsertEmployee(emp);
        }
    }
}