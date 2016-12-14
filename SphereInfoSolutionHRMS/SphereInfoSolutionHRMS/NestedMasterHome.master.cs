using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        Profile profile = new Profile();
        Int32 UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        

        public string PageName
        {
            get { return this.lblPageName.Text; }
            set { this.lblPageName.Text = value; }
        }

        public Int32 PageID { get; set; }
        public DataTable dtUserAccess { get; set; } 


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                    ChangePunch();
                    bindSideMenu(FetchMenuItem(UserID, 0), 0, null);                
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
                lbtnMarkAttendance.Text = "Disabled";
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
            attendanceModel.IPAddress = "192.168.1.102";
            return attendanceModel;
        }

        protected DataTable FetchMenuItem(Int32 UserID, Int32 ParentID)
        {            
            DataTable dt = profile.FetchSideMenu(UserID, ParentID);
            return dt;
        }

        private void bindSideMenu(DataTable dt, int parentMenuId, MenuItem parentMenuItem)
        {
            string currentPage = Path.GetFileName(Request.Url.AbsolutePath);
            foreach (DataRow row in dt.Rows)
            {
                MenuItem menuItem = new MenuItem
                {
                    Value = row["MenuId"].ToString(),
                    Text = row["MenuText"].ToString(),
                    NavigateUrl = row["PageName"].ToString(),
                    Selected = row["PageName"].ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                };
                if (parentMenuId == 0)
                {
                    menuSide.Items.Add(menuItem);
                    DataTable dtChild = this.FetchMenuItem(UserID, int.Parse(menuItem.Value));
                    bindSideMenu(dtChild, int.Parse(menuItem.Value), menuItem);
                }
                else
                {
                    parentMenuItem.ChildItems.Add(menuItem);
                }
            }
        }
    }
}
