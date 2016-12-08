using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BAL
{
    public class MarkAttendance
    {

        public Int32 CheckMarkAttendance(AttendanceModel attendanceModel)
        {
            /*Logic to check if Current User has marked attendance */
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserId", attendanceModel.UserID));
            sqlparam.Add(new SqlParameter("@IP_Address", attendanceModel.IPAddress));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_IsMarkedAttendance", sqlparam);
            int i = Convert.ToInt32(dt.Rows[0][0]);
            return i;

            /*
             *  1 : Have To Punch - In  
             *  2 : Disabled
             *  3 : Have To Punch- Out
             */
        }

        public Boolean MarkUserAttendance(AttendanceModel attendanceModel)
        {
            /* Mark Attendance */
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserId", attendanceModel.UserID));
            sqlparam.Add(new SqlParameter("@IP_Address", attendanceModel.IPAddress));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_Update_AttendanceDetail", sqlparam);
            int i = Convert.ToInt32(dt.Rows[0][0]);
            return true;            
        }


        public DataTable FetchAttendance(AttendanceModel attendanceModel)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserId", attendanceModel.UserID));
            sqlparam.Add(new SqlParameter("@FromDate", attendanceModel.FromDate));
            sqlparam.Add(new SqlParameter("@ToDate", attendanceModel.ToDate));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetAttendanceDetail", sqlparam);
            return dt;
        }

        public DataTable FetchAttendanceSummary(AttendanceModel attendanceModel)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserID", attendanceModel.UserID));
            sqlparam.Add(new SqlParameter("@FromDate", attendanceModel.FromDate));
            sqlparam.Add(new SqlParameter("@ToDate", attendanceModel.ToDate));
            DataTable dt = DAL.SQLHelp.ExecuteReader("", sqlparam);
            return dt;
        }
       
    }
}
