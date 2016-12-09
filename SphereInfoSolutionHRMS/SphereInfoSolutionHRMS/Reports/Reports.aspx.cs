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
            if(!IsPostBack)
            {
                bindClient();
                bindEmployee(-1);
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
            DataTable dtDetail = report.FetchReport(getReportInfo(),1);
            DataTable dtDetailHead = report.FetchReportHeader(getReportInfo());
            GetAttendanceDetailReport(dtDetail,dtDetailHead);
        }

        protected void btnSummary_Click(object sender, EventArgs e)
        {
            DataTable dtSummary = report.FetchReport(getReportInfo(),0);            
        }

        protected ReportModel getReportInfo()
        {
            ReportModel reportModel = new ReportModel();
            reportModel.UserID = UserID;
            reportModel.ReportType = getReportType();            
            reportModel.ClientID = Convert.ToInt32(ddlClient.SelectedValue);            
            reportModel.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
            reportModel.FromDate = Convert.ToDateTime(txtFromDate.Text);
            reportModel.ToDate = Convert.ToDateTime(txtToDate.Text);
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
        }

        private void GetAttendanceDetailReport(DataTable dtDetails, DataTable dtHeader)
        {
            
            ReportViewerAttendanceDetails.ProcessingMode = ProcessingMode.Local;
            ReportViewerAttendanceDetails.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLCReports/Attendance/Details/ReportAttendanceDetails.rdlc");
            if (dtDetails.Rows.Count >= 1)
            {
                ReportDataSource Details = new ReportDataSource("DSAttendanceDetails", dtDetails);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Clear();

                string EmpCode="", EmpName="", Dept="", Desig="", ClientName="",PhotoUrl="";
                string FromDate="", ToDate="";
                string NoOfDaysWorked="", NoOfAbsentDays="";
                string TotalHrsWorked="";

                if (dtHeader.Rows.Count >= 1)
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
               
                List<ReportParameter> param = new List<ReportParameter>();
                param.Add(new ReportParameter("EmpCode", EmpCode));
                param.Add(new ReportParameter("EmpName", EmpName));
                param.Add(new ReportParameter("Department_Name", Dept));
                param.Add(new ReportParameter("Designation_Name", Desig));
                param.Add(new ReportParameter("ClientName", ClientName));
                param.Add(new ReportParameter("FromDate", FromDate));
                param.Add(new ReportParameter("ToDate", ToDate));
                param.Add(new ReportParameter("PhotoUrl", PhotoUrl));
                param.Add(new ReportParameter("NoOfAbsentDays", NoOfAbsentDays));
                param.Add(new ReportParameter("NoOfDaysWorked", NoOfDaysWorked));
                param.Add(new ReportParameter("TotalHrs", TotalHrsWorked));

                this.ReportViewerAttendanceDetails.LocalReport.SetParameters(param);
                ReportViewerAttendanceDetails.LocalReport.DataSources.Add(Details);
                
            }
            else
            {
                ReportViewerAttendanceDetails.Visible = false;
                //lblmsg.Text = "No Records Found";
            }
        }
        
    }
}
