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
    public partial class LeaveDetails : System.Web.UI.Page
    {
        Leave leave = new Leave();
        LeaveModel leaveModel = new LeaveModel();
        Int32 UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //BindLeaveType();
                //BindLeaveDetails();
                //BindApprovalLeaveDetails();
                //BindAvailableLeave();
            }
        }
        
        //Bind Available leaves
        protected void BindAvailableLeave()
        {            
            int AvailableLeave = leave.FetchAvailableLeave(UserID);
            lblAvailableLeaves.Text = AvailableLeave.ToString();
        } 

        //Bind dropdown leave type
        private void BindLeaveType()
        {
            ddlLeaveType.Items.Clear();
            //Models.Attendance attModels = new Models.Attendance();
            DataTable dt = leave.FetchLeaveTypes();
            ddlLeaveType.DataSource = dt;
            ddlLeaveType.DataTextField = "LeaveType";
            ddlLeaveType.DataValueField = "Id";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("Select Leave Type", "0"));
        }

        //bind gvLeaveDetails
        private void BindLeaveDetails()
        {
            DataTable dt = leave.FetchLeaveDetails(UserID);
            if (dt.Rows.Count > 0)
            {
                gvLeaveDetails.DataSource = dt;
                gvLeaveDetails.DataBind();
            }
            else
            {
                //no data found
            }

        }

        //bind gvLeaveChild
        protected void gvLeaveDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvLeaveChild = (GridView)e.Row.FindControl("gvLeaveChild");
                DataTable dt = leave.FetchLeaveChildDetails(Convert.ToInt32(gvLeaveDetails.DataKeys[e.Row.RowIndex].Value));
                if (dt.Rows.Count > 0)
                {
                    gvLeaveChild.DataSource = dt;
                    gvLeaveChild.DataBind();
                }
                else
                {
                    //no data found
                }

            }
        }


        //Check if leave cancelled
        protected void gvLeaveChild_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            

            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label lblID = (Label)row.FindControl("lblId");
            Label lblDate = (Label)row.FindControl("lblFrom");
            Int32 LeaveID = Convert.ToInt32(lblID.Text);
            DateTime LeaveDate = Convert.ToDateTime(lblDate.Text);
            LeaveDate.ToString("YYYY-mm-dd");
            Boolean result = false;

            if (e.CommandName.Equals("CancelLeave"))
            {
                result = UpdateLeaveChild(LeaveID, LeaveDate, "Cancelled");
            }


            if (result)
            {
                //Send Mail                
            }

            //BindLeaveDetails();

        }

        //bind gvApprovalLeaveDetails
        private void BindApprovalLeaveDetails()
        {
            DataTable dt = leave.FetchApprovalLeaveDetails(UserID);
            if (dt.Rows.Count > 0)
            {
                gvApprovalLeave.DataSource = dt;
                gvApprovalLeave.DataBind();
            }
            else
            {
                //no data found
            }

        }

        //bind gvApprovalLeaveChild
        protected void gvApprovalLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvApprovalLeaveChild = (GridView)e.Row.FindControl("gvApprovalLeaveChild");
                DataTable dt = leave.FetchApprovalLeaveChildDetails(Convert.ToInt32(gvApprovalLeave.DataKeys[e.Row.RowIndex].Value));
                if (dt.Rows.Count > 0)
                {
                    gvApprovalLeaveChild.DataSource = dt;
                    gvApprovalLeaveChild.DataBind();
                }
                else
                {
                    //no data found
                }
            }
        }


        //Check Leave Approved or Rejected and updated the LeaveStatus
        protected void gvApprovalLeaveChild_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label lblID = (Label)row.FindControl("lblId");
            Label lblDate = (Label)row.FindControl("lblFrom");

            Int32 LeaveID = Convert.ToInt32(lblID.Text);
            DateTime LeaveDate = Convert.ToDateTime(lblDate.Text);
            LeaveDate.ToString("YYYY-mm-dd");
            Boolean result = false;

            if (e.CommandName.Equals("Approve"))
            {
                result = UpdateLeaveChild(LeaveID, LeaveDate, "Approved");
            }

            else if (e.CommandName.Equals("Reject"))
            {
                result = UpdateLeaveChild(LeaveID, LeaveDate, "Rejected");
            }

            if (result)
            {
                //SendEmail();
            }

            //BindApprovalLeaveDetails();
        }


        //Update the status of the leave request
        protected Boolean UpdateLeaveChild(Int32 LeaveID, DateTime LeaveDate, String Status)
        {
            try
            {
                leaveModel.LeaveID = LeaveID;
                leaveModel.FromDate = LeaveDate;
                leaveModel.LeaveStatus = Status;
                leaveModel.UpdatedBy = UserID;
                Boolean result = leave.UpdateLeaveChild(leaveModel);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //bind the half day gridview according to the ToDate
        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            GetHolidayDate();
        }

        //bind the half day gridview according to the ToDate
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

        //Apply leave 
        protected void btnApply_Click(object sender, EventArgs e)
        {
            int i = leave.ApplyLeave(getLeaveInfo(), getHalfdayDetails());
            if (i == 1) //Successfull
            {
            }
            else if (1 == -1) // Already Applied
            {
            }
            else
            { }
        }

        //get the details of the leave
        protected LeaveModel getLeaveInfo()
        {
            LeaveModel leaveModel = new LeaveModel();
            leaveModel.UserID = UserID;
            leaveModel.LeaveType = Convert.ToInt32(ddlLeaveType.SelectedValue);
            leaveModel.FromDate = Convert.ToDateTime(txtFromDate.Text);
            leaveModel.ToDate = Convert.ToDateTime(txtToDate.Text);
            leaveModel.Contact = txtContactNo.Text;
            leaveModel.Reason = txtReason.Text;
            return leaveModel;
        }

        //get the half day details for each day
        private DataTable getHalfdayDetails()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("LeaveDate", typeof(DateTime));
            dt.Columns.Add("IsHalfDay", typeof(int));


            DateTime HolidayDate = DateTime.Now;
            Int32 IsHalfDay = 0;

            foreach (GridViewRow row in gvHalfdayDetails.Rows)
            {
                HolidayDate = Convert.ToDateTime((row.FindControl("lblDate") as Label).Text);
                //Get the checked value of the CheckBox.
                bool isSelected = (row.FindControl("cbIsHalfday") as CheckBox).Checked;

                if (isSelected)
                {
                    IsHalfDay = 1;
                }
                else
                {
                    IsHalfDay = 0;
                }
                dt.Rows.Add(HolidayDate, IsHalfDay);
            }

            return dt;

        }
    }
}