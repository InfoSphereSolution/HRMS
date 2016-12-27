using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Models;
using Microsoft.Reporting.WebForms;

namespace SphereInfoSolutionHRMS.Reports
{
    public partial class Reports : System.Web.UI.Page
    {
        ReportStatistics report = new ReportStatistics();
        Int32 UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((NestedMasterHome)this.Master).PageName = "Reports";
                bindClient();
                bindEmployee(-1);
                EnableOrDisable("Attendance");
                EnableOrDisable();
            }
        }

        protected void bindClient()
        {
            DataTable dt = report.FetchClient();
            ddlClient.DataSource = dt;
            ddlClient.DataValueField = "ClientId";
            ddlClient.DataTextField = "ClientName";
            ddlClient.DataBind();
            ddlClient.Items.Insert(0, new ListItem("-- All --", "-1"));
        }

        protected void bindEmployee(Int32 ClientID)
        {
            DataTable dt = report.FetchEmployee(ClientID);
            ddlEmployee.DataSource = dt;
            ddlEmployee.DataValueField = "UserId";
            ddlEmployee.DataTextField = "Name";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("-- All --", "-1"));
        }



        protected void btnDetails_Click(object sender, EventArgs e)
        {
            DataTable dtDetail = report.FetchReport(getReportInfo(), 1);
            DataTable dtDetailHead = report.FetchReportHeader(getReportInfo());
            
            if (getReportType() == 1)
            {
                //attendance
                GetAttendanceDetailReport(dtDetail, dtDetailHead);
            }
            else if (getReportType() == 2)
            {
                //leave
                GetLeaveDetailReport(dtDetail, dtDetailHead);

            }
            else
            {
                //holiday
                GetHolidayDetailReport(dtDetail);
            }
            
        }

        protected void btnSummary_Click(object sender, EventArgs e)
        {
            DataTable dtSummary = report.FetchReport(getReportInfo(), 0);

            if (getReportType() == 1)
            {
                //attendance summary
                GetAttendanceSummaryReport(dtSummary);
            }
            else if (getReportType() == 2)
            {
                //leave summary
                GetLeaveSummaryReport(dtSummary);
            }
            else
            {
                //holiday
            }
     

        }

        protected ReportModel getReportInfo()
        {
            ReportModel reportModel = new ReportModel();
            reportModel.UserID = UserID;
            reportModel.ReportType = getReportType();
            reportModel.ClientID = Convert.ToInt32(ddlClient.SelectedValue);
            reportModel.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
            //reportModel.FromDate = Convert.ToDateTime(txtFromDate.Text);
            reportModel.FromDate = String.IsNullOrEmpty(txtFromDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtFromDate.Text);
            //reportModel.ToDate = Convert.ToDateTime(txtToDate.Text);
            reportModel.ToDate = String.IsNullOrEmpty(txtToDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtToDate.Text);
            return reportModel;
        }


        //check which report is needed
        protected Int32 getReportType()
        {
            /*
             * 1: Attendance
             * 2: Leave
             * 3: Holiday
             */

            Int32 reportType = 0;
            if (rbAttendance.Checked == true)
            {
                reportType = 1;
            }
            else if (rbLeave.Checked == true)
            {
                reportType = 2;
            }
            else if (rbHoliday.Checked == true)
            {
                reportType = 3;
            }

            return reportType;
        }

        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 ClientID = Convert.ToInt32(ddlClient.SelectedValue.ToString());
            bindEmployee(ClientID);
            ReportViewerAttendanceDetails.Visible = false;
        }

        #region AttednaceDetails
        private void GetAttendanceDetailReport(DataTable dtDetails, DataTable dtHeader)
        {

            ReportViewerAttendanceDetails.ProcessingMode = ProcessingMode.Local;
           
            ReportViewerAttendanceDetails.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLCReports/Attendance/Details/ReportAttendanceDetails.rdlc");
           
            if (dtDetails.Rows.Count > 0)
            {
                ReportViewerAttendanceDetails.Visible = true;
                lblmsg.Visible = false;
                ReportDataSource Details = new ReportDataSource("DSAttendanceDetails", dtDetails);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Clear();
                ReportViewerAttendanceDetails.LocalReport.EnableExternalImages = true;

                string EmpCode = "", EmpName = "", Dept = "", Desig = "", ClientName = "", PhotoUrl = "";
                string FromDate = "", ToDate = "";
                string NoOfDaysWorked = "", NoOfAbsentDays = "";
                string TotalHrsWorked = "";

                if (dtHeader.Rows.Count >0)
                {
                    EmpCode = Convert.ToString(dtHeader.Rows[0]["EmpCode"]);
                    EmpName = Convert.ToString(dtHeader.Rows[0]["EmpName"]);
                    Dept = Convert.ToString(dtHeader.Rows[0]["Department_Name"]);
                    Desig = Convert.ToString(dtHeader.Rows[0]["Designation_Name"]);
                    ClientName = Convert.ToString(dtHeader.Rows[0]["ClientName"]);
                    FromDate = Convert.ToString(txtFromDate.Text);
                    ToDate = Convert.ToString(txtToDate.Text);
                    PhotoUrl = Convert.ToString(dtHeader.Rows[0]["PhotoUrl"]);
                    NoOfAbsentDays = Convert.ToString(dtHeader.Rows[0]["NoOfAbsentDays"]);
                    NoOfDaysWorked = Convert.ToString(dtHeader.Rows[0]["NoOfDaysWorked"]);
                    TotalHrsWorked = Convert.ToString(dtHeader.Rows[0]["TotalHrs"]);
                }
                string imagePath = new Uri(Server.MapPath(PhotoUrl)).AbsoluteUri;
                List<ReportParameter> param = new List<ReportParameter>();
                param.Add(new ReportParameter("EmpCode", EmpCode));
                param.Add(new ReportParameter("EmpName", EmpName));
                param.Add(new ReportParameter("Department_Name", Dept));
                param.Add(new ReportParameter("Designation_Name", Desig));
                param.Add(new ReportParameter("ClientName", ClientName));
                param.Add(new ReportParameter("FromDate", FromDate));
                param.Add(new ReportParameter("ToDate", ToDate));                
                param.Add(new ReportParameter("PhotoUrl", imagePath));
                param.Add(new ReportParameter("NoOfAbsentDays", NoOfAbsentDays));
                param.Add(new ReportParameter("NoOfDaysWorked", NoOfDaysWorked));
                param.Add(new ReportParameter("TotalHrs", TotalHrsWorked));

                this.ReportViewerAttendanceDetails.LocalReport.SetParameters(param);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Add(Details);

            }
            else
            {
                ReportViewerAttendanceDetails.Visible = false;
                lblmsg.Visible = true;
                lblmsg.Text = "No Records Found";
            }
        }
        #endregion
        #region AttendanceSummary
        private void GetAttendanceSummaryReport(DataTable dtDetails)
        {
            
            ReportViewerAttendanceDetails.ProcessingMode = ProcessingMode.Local;
            ReportViewerAttendanceDetails.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLCReports/Attendance/Summary/AttendanceSummaryReport.rdlc");
            if (dtDetails.Rows.Count >0)
            {
                ReportViewerAttendanceDetails.Visible = true;
                lblmsg.Visible = false;
                ReportDataSource Details = new ReportDataSource("DataSet1", dtDetails);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Clear();
                ReportViewerAttendanceDetails.LocalReport.EnableExternalImages = true;

                string PhotoUrl = "";
                if (dtDetails.Rows.Count > 0)
                {
                    PhotoUrl = Convert.ToString(dtDetails.Rows[0]["PhotoUrl"]);
                }
                string imagePath = new Uri(Server.MapPath(PhotoUrl)).AbsoluteUri;
                List<ReportParameter> param = new List<ReportParameter>();

                param.Add(new ReportParameter("FromDate", txtFromDate.Text));
                param.Add(new ReportParameter("ToDate", txtToDate.Text));
                param.Add(new ReportParameter("PhotoUrl", imagePath));

                this.ReportViewerAttendanceDetails.LocalReport.SetParameters(param);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Add(Details);

            }
            else
            {
                ReportViewerAttendanceDetails.Visible = false;
                lblmsg.Visible = true;
                lblmsg.Text = "No Records Found";
            }
        }
        #endregion

        #region LeaveDetails
        private void GetLeaveDetailReport(DataTable dtDetails, DataTable dtHeader)
        {
            
            ReportViewerAttendanceDetails.ProcessingMode = ProcessingMode.Local;
            ReportViewerAttendanceDetails.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLCReports/Leave/Details/ReportLeaveDetails.rdlc");
            if (dtDetails.Rows.Count >0)
            {
                ReportViewerAttendanceDetails.Visible = true;
                lblmsg.Visible = false;
                ReportDataSource Details = new ReportDataSource("DataSet2", dtDetails);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Clear();
                ReportViewerAttendanceDetails.LocalReport.EnableExternalImages = true;

                string EmpCode = "", EmpName = "", Dept = "", Desig = "", ClientName = "", PhotoUrl = "";
                string FromDate = "", ToDate = "";
                string NoOfAppliedLeaves = "", NoOfRejectedLeaves = "",NoOfCancelledLeaves="",NoOfTakenLeaves="",NoOfApprovedLeaves="";
                if (dtHeader.Rows.Count >0)
                {
                    EmpCode = Convert.ToString(dtHeader.Rows[0]["EmpCode"]);
                    EmpName = Convert.ToString(dtHeader.Rows[0]["EmpName"]);
                    Dept = Convert.ToString(dtHeader.Rows[0]["Department_Name"]);
                    Desig = Convert.ToString(dtHeader.Rows[0]["Designation_Name"]);
                    ClientName = Convert.ToString(dtHeader.Rows[0]["ClientName"]);
                    FromDate = Convert.ToString(txtFromDate.Text);
                    ToDate = Convert.ToString(txtToDate.Text);
                    PhotoUrl = Convert.ToString(dtHeader.Rows[0]["PhotoUrl"]);
                    NoOfAppliedLeaves = Convert.ToString(dtHeader.Rows[0]["NoOfAppliedLeaves"]);
                    NoOfApprovedLeaves = Convert.ToString(dtHeader.Rows[0]["NoOfApprovedLeaves"]);
                    NoOfRejectedLeaves = Convert.ToString(dtHeader.Rows[0]["NoOfRejectedLeaves"]);
                    NoOfCancelledLeaves = Convert.ToString(dtHeader.Rows[0]["NoOfCancelledLeaves"]);
                    NoOfTakenLeaves = Convert.ToString(dtHeader.Rows[0]["NoOfTakenLeaves"]);
                 }
                string imagePath = new Uri(Server.MapPath(PhotoUrl)).AbsoluteUri;
                List<ReportParameter> param = new List<ReportParameter>();
                param.Add(new ReportParameter("EmpCode", EmpCode));
                param.Add(new ReportParameter("EmpName", EmpName));
                param.Add(new ReportParameter("Department_Name", Dept));
                param.Add(new ReportParameter("Designation_Name", Desig));
                param.Add(new ReportParameter("ClientName", ClientName));
                param.Add(new ReportParameter("FromDate", FromDate));
                param.Add(new ReportParameter("ToDate", ToDate));
                param.Add(new ReportParameter("PhotoUrl", imagePath));
                param.Add(new ReportParameter("NoOfAppliedLeaves", NoOfAppliedLeaves));
                param.Add(new ReportParameter("NoOfApprovedLeaves", NoOfApprovedLeaves));
                param.Add(new ReportParameter("NoOfRejectedLeaves", NoOfRejectedLeaves));
                param.Add(new ReportParameter("NoOfCancelledLeaves", NoOfCancelledLeaves));
                param.Add(new ReportParameter("NoOfTakenLeaves", NoOfTakenLeaves));

        
                this.ReportViewerAttendanceDetails.LocalReport.SetParameters(param);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Add(Details);

            }
            else
            {
                ReportViewerAttendanceDetails.Visible = false;
                lblmsg.Visible = true;
                lblmsg.Text = "No Records Found";
            }
        }
        #endregion

        #region LeaveSummary
        private void GetLeaveSummaryReport(DataTable dtLeaveSummary)
        {
            
            ReportViewerAttendanceDetails.ProcessingMode = ProcessingMode.Local;
            ReportViewerAttendanceDetails.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLCReports/Leave/Summary/ReportLeaveSummary.rdlc");
            if (dtLeaveSummary.Rows.Count >0)
            {
                ReportViewerAttendanceDetails.Visible = true;
                lblmsg.Visible = false;
                ReportDataSource Details = new ReportDataSource("DSLeaveSummary", dtLeaveSummary);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Clear();
                ReportViewerAttendanceDetails.LocalReport.EnableExternalImages = true;

                string PhotoUrl = "";
                if (dtLeaveSummary.Rows.Count > 0)
                {
                    PhotoUrl = Convert.ToString(dtLeaveSummary.Rows[0]["PhotoUrl"]);
                }
                string imagePath = new Uri(Server.MapPath(PhotoUrl)).AbsoluteUri;
                List<ReportParameter> param = new List<ReportParameter>();

                param.Add(new ReportParameter("FromDate", txtFromDate.Text));
                param.Add(new ReportParameter("ToDate", txtToDate.Text));
                param.Add(new ReportParameter("PhotoUrl", imagePath));


                this.ReportViewerAttendanceDetails.LocalReport.SetParameters(param);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Add(Details);

            }
            else
            {
                ReportViewerAttendanceDetails.Visible = false;
                lblmsg.Visible = true;
                lblmsg.Text = "No Records Found";
            }
        }
        #endregion

        #region HolidayDetail
        private void GetHolidayDetailReport(DataTable dtDetails)
        {
            
            ReportViewerAttendanceDetails.ProcessingMode = ProcessingMode.Local;
            ReportViewerAttendanceDetails.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLCReports/Holiday/Details/ReportHoliday.rdlc");
            if (dtDetails.Rows.Count >0)
            {
                ReportViewerAttendanceDetails.Visible = true;
                lblmsg.Visible = false;
                ReportDataSource Details = new ReportDataSource("DSHoliday", dtDetails);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Clear();
                ReportViewerAttendanceDetails.LocalReport.DataSources.Add(Details);

            }
            else
            {
                ReportViewerAttendanceDetails.Visible = false;
                lblmsg.Visible = true;
                lblmsg.Text = "No Records Found";
            }
        }
        #endregion

        //Active Employees
        protected void lnkbtnActive_Click(object sender, EventArgs e)
        {
            GetActiveOrInactiveEmployee(true);
        }
        //InActive Employees
        protected void lnkbtnNotActive_Click(object sender, EventArgs e)
        {
            GetActiveOrInactiveEmployee(false);
        }

        private void GetActiveOrInactiveEmployee(bool EmpType)
        {
            DataTable dtEmpReport = report.FetchEmployeeReport(EmpType);
            GetActiveOrInactiveEmployeeReport(dtEmpReport,EmpType);

            
        }
        private void GetActiveOrInactiveEmployeeReport(DataTable dtDetails, bool EmpType)
        {

            ReportViewerAttendanceDetails.ProcessingMode = ProcessingMode.Local;
            ReportViewerAttendanceDetails.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLCReports/Employees/ReportActiveOrNotActiveEmployees.rdlc");
            if (dtDetails.Rows.Count >= 1)
            {
                ReportViewerAttendanceDetails.Visible = true;
                ReportDataSource Details = new ReportDataSource("DSIsActiveEmployee", dtDetails);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Clear();
                ReportViewerAttendanceDetails.LocalReport.EnableExternalImages = true;

                string PhotoUrl = "";
                string EmployeeType = "";
                PhotoUrl = Convert.ToString(dtDetails.Rows[0]["PhotoUrl"]);
                string imagePath = new Uri(Server.MapPath(PhotoUrl)).AbsoluteUri;
                List<ReportParameter> param = new List<ReportParameter>(); 
                param.Add(new ReportParameter("PhotoUrl", imagePath));
                if (EmpType == true)
                {
                    EmployeeType = "Active";
                }
                else
                {
                    EmployeeType = "Not Active";
                }
                param.Add(new ReportParameter("EmployeeType", EmployeeType));
                this.ReportViewerAttendanceDetails.LocalReport.SetParameters(param);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Add(Details);

            }
            else
            {
                ReportViewerAttendanceDetails.Visible = false;
                //lblmsg.Text = "No Records Found";
            }
        }

        protected void rbAttendance_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAttendance.Checked == true)
            {
                EnableOrDisable("Attendance");
                ReportViewerAttendanceDetails.Visible = false;
            }
            else if (rbLeave.Checked == true)
            {
                EnableOrDisable("Leave");
               ReportViewerAttendanceDetails.Visible = false;
            }
            else
            {
                EnableOrDisable("Holiday");
                ReportViewerAttendanceDetails.Visible = false;
            }
            ClearControls();
        }

        //Enable and disable the controls based on selected radio button (Report type)
        private void EnableOrDisable(string ReportType)
        {
            if (ReportType == "Attendance")
            {
                ddlClient.Enabled = true;
                ddlEmployee.Enabled = true;
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                btnDetails.Enabled = true;
                btnSummary.Enabled = true;
                RFClient.Enabled = false;
                RFEmployee.Enabled = true;
                RFFromDate.Enabled = true;
                RFTodate.Enabled = true;
                EnableOrDisable();
            }
            else if (ReportType == "Leave")
            {
                ddlClient.Enabled = true;
                ddlEmployee.Enabled = true;
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                btnDetails.Enabled = true;
                btnSummary.Enabled = true;
                RFClient.Enabled = false;
                RFEmployee.Enabled = true;
                RFFromDate.Enabled = true;
                RFTodate.Enabled = true;
                EnableOrDisable();
            }
            else if (ReportType == "Holiday")
            {
                ddlClient.Enabled = true;
                ddlEmployee.Enabled = false;
                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
                btnDetails.Enabled = true;
                btnSummary.Enabled = false;
                RFClient.Enabled = true;
                RFFromDate.Enabled = false;
                RFTodate.Enabled = false;
                RFEmployee.Enabled = false;
            }
            else
            {
                //do nothing
            }
        }

        //Clear Controls
        private void ClearControls()
        {
            ddlClient.SelectedIndex = -1;
            ddlEmployee.SelectedIndex = -1;
            txtFromDate.Text = "";
            txtToDate.Text = "";
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
              EnableOrDisable();
              //ReportViewerAttendanceDetails.Visible = false;
        }
        //Enable and disable the controls based on selected Dropdown (Employee All or single)
        private void EnableOrDisable()
        {
            if (ddlEmployee.SelectedItem.Text != "-- All --")
            {
                btnDetails.Enabled = true;
            }
            else
            {
                btnDetails.Enabled = false;
            }
           
        }
    }
}
