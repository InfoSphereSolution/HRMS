using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL;
using Models;

namespace BAL
{
    public class ReportStatistics
    {
        //Get the Detail report
        public DataTable FetchReport(ReportModel reportModel, Int32 Operation)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@ReportType", reportModel.ReportType));
            if (reportModel.ClientID == -1)
            {
                sqlparam.Add(new SqlParameter("@ClientId", null));
            }
            else
            {
                sqlparam.Add(new SqlParameter("@ClientId", reportModel.ClientID));
            }
            if (reportModel.EmployeeID == -1)
            {
                sqlparam.Add(new SqlParameter("@EmpId", null));  
            }
            else
            {
                sqlparam.Add(new SqlParameter("@EmpId", reportModel.EmployeeID));  
            } 
                      
            sqlparam.Add(new SqlParameter("@FromDate", reportModel.FromDate));
            sqlparam.Add(new SqlParameter("@ToDate", reportModel.ToDate));
            sqlparam.Add(new SqlParameter("@ReportOperation", Operation));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetReports", sqlparam);            
            return dt;
        }

        //Get the Detail report Head
        public DataTable FetchReportHeader(ReportModel reportModel)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@ReportType", reportModel.ReportType));                                    
            sqlparam.Add(new SqlParameter("@EmpId", reportModel.EmployeeID));            
            sqlparam.Add(new SqlParameter("@FromDate", reportModel.FromDate));
            sqlparam.Add(new SqlParameter("@ToDate", reportModel.ToDate));            
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetReportsHeader", sqlparam);            
            return dt;
        }

        public DataTable FetchClient()
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("Select ClientId, ClientName from Mst_Client");
            return dt;
        }

        public DataTable FetchEmployee(Int32 ClientID)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            if (ClientID == -1)
            {
                sqlparam.Add(new SqlParameter("@CLientId", null));
            }
            else
            {
                sqlparam.Add(new SqlParameter("@CLientId", ClientID));
            }
            
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetEmployeeName", sqlparam);            
            //DataTable dt = DAL.SQLHelp.ExecuteSelect("Select UserId, (FirstName + ' ' + LastName) as Name from Tbl_PersonalDetail");
            return dt;
        }
        
    }
}
