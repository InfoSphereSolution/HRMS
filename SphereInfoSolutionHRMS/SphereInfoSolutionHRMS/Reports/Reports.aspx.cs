using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {

        }

        protected ReportModel getReportInfo()
        {
            ReportModel reportModel = new ReportModel();
            reportModel.ReportType = getReportType();
            reportModel.ClientID = Convert.ToInt32(ddlClient.SelectedValue);
            reportModel.UserID = Convert.ToInt32(ddlEmployee.SelectedValue);
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

        protected void rbAttendance_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}