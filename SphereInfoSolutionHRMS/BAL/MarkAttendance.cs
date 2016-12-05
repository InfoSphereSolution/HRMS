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
            sqlparam.Add(new SqlParameter("@UserID", attendanceModel.UserID));
            sqlparam.Add(new SqlParameter("@IP_Address", attendanceModel.IPAddress));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_IsMarkedAttendance", sqlparam);
            int i = Convert.ToInt32(dt.Rows[0][0]);
            return i;

            /*
             *  3 : Have To Punch - In  
             * -1 : In time exist
             * -2 : Out Time Exist
             *  2 : Out Time Does not exist
             * -3 : Not Eligible to Punch-In
             * -4 : IP Does not match
             */

        }

        public Boolean MarkUserAttendance(AttendanceModel attendanceModel)
        {
            return true;
        }


        public DataTable FetchAttendance(AttendanceModel attendanceModel)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserID", attendanceModel.UserID));
            sqlparam.Add(new SqlParameter("@FromDate", attendanceModel.FromDate));
            sqlparam.Add(new SqlParameter("@ToDate", attendanceModel.ToDate));
            DataTable dt = DAL.SQLHelp.ExecuteReader("", sqlparam);
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
