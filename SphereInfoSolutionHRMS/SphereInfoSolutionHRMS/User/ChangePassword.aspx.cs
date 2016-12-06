using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using Models;
using System.Security.Cryptography;
using System.Web.Security;
namespace SphereInfoSolutionHRMS.Admin
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        BAL.Login bAL = new BAL.Login();
        DataSet ds = new DataSet();
        Models.LoginModel loginModel = new Models.LoginModel();
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Clear All Control
        public void Clearall()
        {

            txtconpwd.Text = txtoldpwd.Text = txtnewpwd.Text = "";
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
          
            loginModel.UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);


            ds = bAL.CheckPassword(loginModel);
          
            string decrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(txtoldpwd.Text, "SHA1");
             
            if (ds.Tables[0].Rows.Count>0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0]["Password"].ToString()) == decrypt)
                {
                    string encrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(txtconpwd.Text, "SHA1");
                    loginModel.UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    loginModel.Password = encrypt;
                    bool value = bAL.UpdatePassword(loginModel);
                    if (value == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Password Changed Succesfully');", true);
                        Response.Redirect("~/Login/Login.aspx");
                        Session.Clear();
                        Session.Abandon();
                        Session.RemoveAll();
                        FormsAuthentication.SignOut();
                        FormsAuthentication.RedirectToLoginPage();

                    }
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('User Enter Wrong Password');", true);
                 

                }
            }
            Clearall();
        }
    }
}