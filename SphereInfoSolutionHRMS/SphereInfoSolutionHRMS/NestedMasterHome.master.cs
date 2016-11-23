using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SphereInfoSolutionHRMS
{
    public partial class NestedMasterHome : System.Web.UI.MasterPage
    {
        public string PageName
        {
            get { return this.lblPageName.Text; }
            set { this.lblPageName.Text = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            lbtnlogout.Visible = false;
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}