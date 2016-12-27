using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BAL;

namespace SphereInfoSolutionHRMS
{
    public partial class home : System.Web.UI.Page
    {
        Int32 UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        Profile profile = new Profile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((NestedMasterHome)this.Master).PageName = "Overview";
                ((NestedMasterHome)this.Master).PageID = 0;
                lblGreetings.Text = "Welcome, " + getUserName(UserId);
            }
        }

       

        protected String getUserName(Int32 UserID)
        {
            String UserName = profile.FetchUserName(UserID);
            return UserName;
        }

        
    }
}