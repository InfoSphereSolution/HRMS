using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SphereInfoSolutionHRMS.Admin
{
    public partial class ClientDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((NestedMasterHome)this.Master).PageName = "Client Details";
            }
        }
    }
}