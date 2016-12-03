using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Models;

namespace SphereInfoSolutionHRMS
{
    public partial class NestedMasterHome : System.Web.UI.MasterPage
    {
        MarkAttendance markAttendance = new MarkAttendance();
        MarkAttendanceModel markAttendanceModel = new MarkAttendanceModel();
        Int32 UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        public string PageName
        {
            get { return this.lblPageName.Text; }
            set { this.lblPageName.Text = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChangePunch();
            }
        }

        protected Int32 IsAttendanceMarked()
        {
            Int32 IsAttendanceMarked = markAttendance.CheckMarkAttendance(getPunchUserInfo());
            return IsAttendanceMarked;
        }

        protected void ChangePunch()
        {
            if (IsAttendanceMarked() == 3)
            {
                lbtnMarkAttendance.Text = "Punch-In";
                lbtnMarkAttendance.Enabled = true;
            }
            else if (IsAttendanceMarked() == -1 || IsAttendanceMarked() == 2)
            {
                lbtnMarkAttendance.Text = "Punch-Out";
                lbtnMarkAttendance.Enabled = true;
            }
            else if (IsAttendanceMarked() == -2 )
            {
                lbtnMarkAttendance.Text = "Attendance Marked";
                lbtnMarkAttendance.Enabled = false;
            }
            else if (IsAttendanceMarked() == -3 || IsAttendanceMarked() == -4)
            {
                lbtnMarkAttendance.Text = "IP Address Not Matched";
                lbtnMarkAttendance.Enabled = false;
            }
            else if (IsAttendanceMarked() == -3)
            {
                lbtnMarkAttendance.Text = "Shift Ended";
                lbtnMarkAttendance.Enabled = false;
            }
             
            else { }

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

        protected void lbtnMarkAttendance_Click(object sender, EventArgs e)
        {
            Boolean result = markAttendance.MarkUserAttendance(getPunchUserInfo());
            if (result)
            {
                /*Attendance Marked*/
            }
            else
            {
                /*Failed.. Try again!*/
            }

            ChangePunch();
        }
        

        protected MarkAttendanceModel getPunchUserInfo()
        {
            MarkAttendanceModel markAttendanceModel = new MarkAttendanceModel();
            markAttendanceModel.UserID = UserID;
            markAttendanceModel.IPAddress = "198.168.121.1";
            return markAttendanceModel;
        }
    }
}