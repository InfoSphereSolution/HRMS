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
             *  1: Leave Applied
             *  2: Leave Already Applied             *  
             */

            return 1;
        }

        //get the leavetypes
        public DataTable FetchLeaveTypes()
        {            
            DataTable dt = null;
            return dt;
        }

        //get the leave details
        public DataTable FetchLeaveDetails(Int32 UserID)
        {
            //get the leave details
            DataTable dt = null;
            return dt;
        }

        //get the leave child details
        public DataTable FetchLeaveChildDetails(Int32 LeaveID)
        {            
            DataTable dt = null;
            return dt;
        }

        //get the pending leave details
        public DataTable FetchApprovalLeaveDetails(Int32 UserID)
        {            
            DataTable dt = null;
            return dt;
        }

        //get the pending leave child details
        public DataTable FetchApprovalLeaveChildDetails(Int32 LeaveID)
        {            
            DataTable dt = null;
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

            int rows = DAL.SQLHelp.ExecuteNonQuery("sp_UpdateLeaveChild", sqUpdate);

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
