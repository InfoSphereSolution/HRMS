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
    public partial class Leave : System.Web.UI.Page
    {
        Leave leave = new Leave();
        LeaveModel leaveModel = new LeaveModel();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            GetHolidayDate();
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            GetHolidayDate();
        }

        //get FromDate and ToDate
        private void GetHolidayDate()
        {
            if (txtFromDate.Text != "" || txtFromDate.Text != null)
            {
                if (txtToDate.Text == "" || txtToDate.Text == null)
                {
                    txtToDate.Text = txtFromDate.Text;
                }
                bindIsHalfday(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            }
        }

        //Bind HalfDay grid according to the FromDate and ToDate
        private void bindIsHalfday(DateTime FromDate, DateTime ToDate)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            for (DateTime StartDate = FromDate.Date; StartDate.Date <= ToDate.Date; StartDate = StartDate.AddDays(1))
            {
                dt.Rows.Add(StartDate.Date.ToString("dd'/'MM'/'yyyy"));
            }
            try
            {
                gvHalfdayDetails.DataSource = dt;
                gvHalfdayDetails.DataBind();
                gvHalfdayDetails.Visible = true;
                lblIsHalfDay.Visible = true;
            }
            catch (Exception)
            {
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {

        }

        protected LeaveModel getLeaveInfo()
        {
            LeaveModel leaveModel = new LeaveModel();
            leaveModel.LeaveType = Convert.ToInt32(ddlLeaveType.SelectedItem);
            leaveModel.FromDate = Convert.ToDateTime(txtFromDate.Text);
            leaveModel.ToDate = Convert.ToDateTime(txtToDate.Text);
            leaveModel.Contact = txtContactNo.Text;
            leaveModel.Reason = txtReason.Text;
            return leaveModel;
        }
    }
}