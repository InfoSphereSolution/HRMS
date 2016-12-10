using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BAL;
using System.Data;
namespace SphereInfoSolutionHRMS.Login
{
    public partial class MyProfile : System.Web.UI.Page
    {
        LoginModel login = new LoginModel();
        Employee emp = new Employee();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetEmployeeProfile();

            }
        }
        protected void GetEmployeeProfile()
        {
            login.UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            dt = emp.EmployeeProfile(login);
            if (dt.Rows.Count > 0)
            {
                for(int i=0;i<dt.Rows.Count;i++)
                {
                   Empname.Text = (dt.Rows[i]["Name"].ToString());
                    lblmobile.Text=dt.Rows[i]["MobileNo"].ToString();
                    lbldesigantion.Text = dt.Rows[i]["Designation"].ToString();
                    lblclientname.Text = dt.Rows[i]["ClientName"].ToString();
                    imgEmployeePicture.ImageUrl = (dt.Rows[i]["PhotoUrl"].ToString());
             
             }
            }

        }
    }
}