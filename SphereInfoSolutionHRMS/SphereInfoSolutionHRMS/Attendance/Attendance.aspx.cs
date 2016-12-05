using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Models;

namespace SphereInfoSolutionHRMS.Attendance
{
    public partial class Attendance : System.Web.UI.Page
    {
        MarkAttendance markAttendance = new MarkAttendance();
        AttendanceModel attendanceModel = new AttendanceModel();
        Int32 UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDates();
                //bindAttendance();
            }
        }

        protected void bindDates()
        {
            txtToDate.Text = Convert.ToString(DateTime.Now);
            txtFromDate.Text = Convert.ToString(DateTime.Now.AddMonths(-3));
        }



        protected void btnViewAttendance_Click(object sender, EventArgs e)
        {
            //bindAttendance();
        }

        protected void bindAttendance()
        {
            if (!String.IsNullOrEmpty(txtFromDate.Text) && !String.IsNullOrEmpty(txtToDate.Text))
            {

                DataTable dt = markAttendance.FetchAttendance(bindUserInfo());

                if (dt.Rows.Count > 0)
                {
                    gvEmployeeAttendance.DataSource = dt;
                    gvEmployeeAttendance.DataBind();
                    dt = markAttendance.FetchAttendanceSummary(bindUserInfo());
                    bindAttendanceSummary(dt);
                    displayAttendance();
                }
                else
                {
                    /* No records found */
                    hideAttendance();
                }
            }
            else
            {
                /* Select From date or To date */
            }
        }

        protected void bindAttendanceSummary(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                lblTotalDays.Text = Convert.ToString(dt.Rows[0][0]);
                lblPresentDays.Text = Convert.ToString(dt.Rows[0][1]);
                lblAbsentDays.Text = Convert.ToString(dt.Rows[0][2]);
                lblTotalHours.Text = Convert.ToString(dt.Rows[0][3]);
                lblLeaveTaken.Text = Convert.ToString(dt.Rows[0][4]);
                lblAvailableLeave.Text = Convert.ToString(dt.Rows[0][5]);                
            }
        }

        protected void displayAttendance()
        {
            gvEmployeeAttendance.Visible = true;
            lblTotalDays.Visible = true;
            lblPresentDays.Visible = true;
            lblAbsentDays.Visible = true;
            lblTotalHours.Visible = true;
            lblLeaveTaken.Visible = true;
            lblAvailableLeave.Visible = true;
            lblMessageAttendance.Visible = false;
        }

        protected void hideAttendance()
        {
            gvEmployeeAttendance.Visible = false;
            lblTotalDays.Visible = false;
            lblPresentDays.Visible = false;
            lblAbsentDays.Visible = false;
            lblTotalHours.Visible = false;
            lblLeaveTaken.Visible = false;
            lblAvailableLeave.Visible = false;
            lblMessageAttendance.Visible = true;
            lblMessageAttendance.Text = "No records found..";
        }


        protected AttendanceModel bindUserInfo()
        {
            AttendanceModel attendanceModel = new AttendanceModel();
            attendanceModel.UserID = UserID;
            attendanceModel.FromDate = Convert.ToDateTime(txtFromDate.Text);
            attendanceModel.ToDate = Convert.ToDateTime(txtToDate.Text);
            return attendanceModel;

        }




    }
}

