using BAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SphereInfoSolutionHRMS.Admin
{
    public partial class UpdateAttendance : System.Web.UI.Page
    {
        MarkAttendance markAttendance = new MarkAttendance();
        Models.AttendanceModel attendanceModel = new AttendanceModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((NestedMasterHome)this.Master).PageName = "Mark Attendance";
                BindEmployee();
                ceTDate.EndDate = DateTime.Now;
                txtDate.Attributes.Add("readonly", "readonly");
            }
        }

        private void FetchEmployeeShift(int UserId)
        {
            DataTable dt = markAttendance.FetchShiftOfEmployee(UserId);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    txtShift.Text = dt.Rows[i]["ShiftsName"].ToString();
                    ViewState["ShiftName"] = dt.Rows[i]["ShiftsName"].ToString();
                    txtShiftStartTime.Text = dt.Rows[i]["StartTime"].ToString();
                    txtShiftEndTime.Text = dt.Rows[i]["EndTime"].ToString();
                }


            }
        }

        private void BindEmployee()
        {
            ddlEmployee.DataSource = markAttendance.FetchEmployee();
            ddlEmployee.DataValueField = "UserId";
            ddlEmployee.DataTextField = "Name";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("--Select Employee--", "-1"));
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            int UserId = Convert.ToInt32(ddlEmployee.SelectedValue);
            FetchEmployeeShift(UserId);
            ChangeData();
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            ChangeData();
        }

        private DataTable CheckMarkedAttendance()
        {
            int UserId = Convert.ToInt32(ddlEmployee.SelectedValue);
            DateTime date = Convert.ToDateTime(txtDate.Text);
            DataTable dt = markAttendance.CheckMarkedAttendance(UserId, date);
            return dt;
        }

        private void ChangeData()
        {
            string InTime = "", OutTime = "", Status = "";
            string ShiftName = ViewState["ShiftName"].ToString();
            if (txtDate.Text != "")
            {
                DataTable dt = CheckMarkedAttendance();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        InTime = dt.Rows[i]["InTime"].ToString();
                        OutTime = dt.Rows[i]["OutTime"].ToString();
                        Status = dt.Rows[i]["AttendanceStatus"].ToString();
                    }
                }
                if (InTime != "" && OutTime != "" && Status == "Present")
                {
                    //Intime & OutTime is not null
                    txtEnterST.Text = InTime;
                    txtEnterET.Text = OutTime;
                    txtEnterST.Enabled = false;
                    txtEnterET.Enabled = false;
                    btnMark.Enabled = false;
                }
                else if (InTime != "" && OutTime == "" && Status == "Waiting for Punch out")
                {
                    //Intime is not null & OutTime is null
                    txtEnterST.Text = InTime;
                    txtEnterET.Text = OutTime;
                    txtEnterST.Enabled = false;
                    txtEnterET.Enabled = true;
                    if (ShiftName == "Flexible (9 hours)")
                    {
                        txtEnterET.Enabled = false;
                        txtEnterST.Enabled = false;
                    }
                    else if (ShiftName == "Night Shift(23:00 -8:00)")
                    {
                        DateTime dtET = Convert.ToDateTime(txtDate.Text);
                        txtEnterET.Text = dtET.AddDays(1).ToString("yyyy/M/dd") + " " + txtShiftEndTime.Text;
                        txtEnterST.Text = InTime;
                        txtEnterET.Enabled = true;
                        txtEnterST.Enabled = false;
                    }

                    btnMark.Enabled = true;
                }
                else
                {
                    if (ShiftName == "Flexible (9 hours)")
                    {
                        txtEnterST.Text = "";
                        txtEnterET.Text = "";
                        txtEnterET.Enabled = false;
                        txtEnterST.Enabled = false;
                    }
                    else if (ShiftName == "Night Shift(23:00 -8:00)")
                    {
                        DateTime dtET = Convert.ToDateTime(txtDate.Text);
                        txtEnterET.Text = dtET.AddDays(1).ToString("yyyy/M/dd") + " " + txtShiftEndTime.Text;
                        txtEnterST.Text = txtDate.Text + " " + txtShiftStartTime.Text;
                        txtEnterET.Enabled = true;
                        txtEnterST.Enabled = true;
                    }
                    else
                    {
                        txtEnterST.Text = txtDate.Text + " " + txtShiftStartTime.Text;
                        txtEnterET.Text = txtDate.Text + " " + txtShiftEndTime.Text;
                        txtEnterET.Enabled = true;
                        txtEnterST.Enabled = true;
                        
                    }
                    btnMark.Enabled = true;
                }
            }
        }

        protected void btnMark_Click(object sender, EventArgs e)
        {
            int UserId=Convert.ToInt32(ddlEmployee.SelectedValue);
            DateTime InTime = Convert.ToDateTime(txtEnterST.Text);
            DateTime OutTime = Convert.ToDateTime(txtEnterET.Text);
            DateTime date = Convert.ToDateTime(txtDate.Text);
           int i= markAttendance.MarkAttendanceByHr(UserId, date, InTime, OutTime);
        }
    }
}