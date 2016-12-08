using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using BAL;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{

    public class Leave
    {
        //Appy leave 
        public int ApplyLeave(LeaveModel leaveModel, DataTable dt)
        {
            /*
             * 1: LeaveApplied
             * 0: Leave Already Exists
             */

            List<SqlParameter> sqUpdate = new List<SqlParameter>();
            sqUpdate.Add(new SqlParameter("@UserId", leaveModel.UserID));
            sqUpdate.Add(new SqlParameter("@LeaveTypeId", leaveModel.LeaveType));
            sqUpdate.Add(new SqlParameter("@ContactNo", leaveModel.Contact));
            sqUpdate.Add(new SqlParameter("@Reason", leaveModel.Reason));
            sqUpdate.Add(new SqlParameter("@FromDate", leaveModel.FromDate));
            sqUpdate.Add(new SqlParameter("@ToDate", leaveModel.ToDate));

            DataTable returnCode = DAL.SQLHelp.ExecuteReader("Usp_ApplyLeave", sqUpdate);

            Int32 LeaveID = Convert.ToInt32(returnCode.Rows[0][0]);
            Int32 ClientID = Convert.ToInt32(returnCode.Rows[0][1]);
            Boolean IsPaid = true;
            Int32 LeaveQuotaUsed = 1;

            if ((LeaveID != 0) && (ClientID != 0))
            {
                foreach(DataRow row in dt.Rows)
                {
                    row["ClientId"] = ClientID;
                    row["IsPaid"] = IsPaid;
                    row["LeaveId"] = LeaveID;
                    row["LeaveQuotaUsed"] = LeaveQuotaUsed;
                }                

                try
                {
                    DAL.SQLHelp.CopyToServer("Tbl_Attendance_Details", dt);
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
                return 1;
            }
            else
            {
                return 0;
            }
        }
       

        //get the leavetypes
        public DataTable FetchLeaveTypes()
        {   
            DataTable dt = DAL.SQLHelp.ExecuteSelect("Select * from Tbl_LeaveType");
            return dt;
        }

        //get the leave details
        public DataTable FetchLeaveDetails(Int32 UserID)
        {
            List<SqlParameter> sqUpdate = new List<SqlParameter>();
            sqUpdate.Add(new SqlParameter("@UserId", UserID));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetLeaveDetails", sqUpdate);
            return dt;
        }

        //get the leave child details
        public DataTable FetchLeaveChildDetails(Int32 LeaveID)
        {
            List<SqlParameter> sqUpdate = new List<SqlParameter>();
            sqUpdate.Add(new SqlParameter("@LeaveId", LeaveID));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetPendingLeaveChildDetails", sqUpdate);
            return dt;
        }

        //get the pending leave details
        public DataTable FetchApprovalLeaveDetails(Int32 UserID)
        {
            List<SqlParameter> sqUpdate = new List<SqlParameter>();
            sqUpdate.Add(new SqlParameter("@UserId", UserID));            
            DataTable dt =  DAL.SQLHelp.ExecuteReader("Usp_GetPendingLeaveDetails", sqUpdate);
            return dt;
        }

        //get the pending leave child details
        public DataTable FetchApprovalLeaveChildDetails(Int32 LeaveID)
        {
            List<SqlParameter> sqUpdate = new List<SqlParameter>();
            sqUpdate.Add(new SqlParameter("@LeaveId", LeaveID));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetPendingLeaveChildDetails", sqUpdate);
            return dt;
        }

        //update the status of the users leave
        public Boolean UpdateLeaveChild(LeaveModel leaveModel)
        {
            List<SqlParameter> sqUpdate = new List<SqlParameter>();
            sqUpdate.Add(new SqlParameter("@LeaveID", leaveModel.LeaveID));
            sqUpdate.Add(new SqlParameter("@LeaveDate", leaveModel.FromDate));
            sqUpdate.Add(new SqlParameter("@LeaveStatus", leaveModel.LeaveStatus));
            sqUpdate.Add(new SqlParameter("@UpdatedBy", leaveModel.UpdatedBy));

            int rows = DAL.SQLHelp.ExecuteNonQuery("Usp_UpdateLeaveChild", sqUpdate);

            if (rows >= 1)
            {                
                return true;
            }
            else
            {
                return false;
            }
        }

        // Fetch the users available leave
        public Int32 FetchAvailableLeave(Int32 UserID)
        {
            return 1;
        }
    }
}

