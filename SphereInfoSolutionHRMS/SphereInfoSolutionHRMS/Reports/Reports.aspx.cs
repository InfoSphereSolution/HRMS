using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Models;

namespace SphereInfoSolutionHRMS.Reports
{
    public partial class Reports : System.Web.UI.Page
    {
        Report report = new Report();
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
            DataTable dtDetailHead = report.FetchReportHead(getReportInfo(),1);
        }

        protected void btnSummary_Click(object sender, EventArgs e)
        {
            DataTable dtSummary = report.FetchReport(getReportInfo(),0);
            DataTable dtSummaryHead = report.FetchReportHead(getReportInfo(),0);
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
        
    }
}
