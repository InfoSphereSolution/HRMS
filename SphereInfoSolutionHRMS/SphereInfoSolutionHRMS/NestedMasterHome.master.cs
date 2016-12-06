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
        AttendanceModel attendanceModel = new AttendanceModel();
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
            if (IsAttendanceMarked() == 1)
            {
                lbtnMarkAttendance.Text = "Punch-In";
                lbtnMarkAttendance.Enabled = true;
                lbtnMarkAttendance.Visible = true;
            }
            else if (IsAttendanceMarked() == 3)
            {
                lbtnMarkAttendance.Text = "Punch-Out";
                lbtnMarkAttendance.Enabled = true;
                lbtnMarkAttendance.Visible = true;
            }
            else if (IsAttendanceMarked() == 2 )
            {
                lbtnMarkAttendance.Text = "Attendance Marked";
                lbtnMarkAttendance.Enabled = false;
                lbtnMarkAttendance.Visible = true;
            }
            else 
            {
                lbtnMarkAttendance.Enabled = false;
                lbtnMarkAttendance.Visible = false;
            }

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
        

        protected AttendanceModel getPunchUserInfo()
        {
            AttendanceModel attendanceModel = new AttendanceModel();
            attendanceModel.UserID = UserID;
            attendanceModel.IPAddress = "198.168.121.1";
            return attendanceModel;
        }
    }
}