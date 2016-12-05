using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Models;

namespace SphereInfoSolutionHRMS.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Formsauthentication is in system.web.security
            string encryptedpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
            AuthenticateUser(txtUserName.Text, encryptedpassword);
        }

        private void AuthenticateUser(string username, string password)
        {            
            BAL.Login bAL = new BAL.Login();
            Models.LoginModel loginModel = new Models.LoginModel();
            loginModel.Username = username;
            loginModel.Password = password;
            DataTable dt = bAL.checkUser(loginModel);

            foreach (DataRow row in dt.Rows)
            {

                int RetryAttempts = Convert.ToInt32(row["RetryAttempts"]);
                if (Convert.ToBoolean(row["AccountLocked"]))
                {
                    lblMessage.Text = "Account locked. Please contact administrator";
                }
                else if (RetryAttempts > 0)
                {
                    int AttemptsLeft = (4 - RetryAttempts);
                    lblMessage.Text = "Invalid user name and/or password. " +
                        AttemptsLeft.ToString() + "attempt(s) left";
                }
                else if (Convert.ToBoolean(row["Authenticated"]))
                {
                    string UserId=(row["UserId"].ToString());
                    FormsAuthentication.RedirectFromLoginPage(UserId, cbRememberMe.Checked);
                }


            }

        }
    }
}