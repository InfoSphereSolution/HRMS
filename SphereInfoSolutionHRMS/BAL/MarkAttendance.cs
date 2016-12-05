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

        public Int32 CheckMarkAttendance(AttendanceModel markAttendanceModel)
        {
            /*Logic to check if Current User has marked attendance */
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserID", markAttendanceModel.UserID));            
            sqlparam.Add(new SqlParameter("@IP_Address", markAttendanceModel.IPAddress));
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

        public Boolean MarkUserAttendance(AttendanceModel markAttendanceModel)
        {
            return true;
        }


        public DataTable FetchAttendance(AttendanceModel markAttendanceModel)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserID", markAttendanceModel.UserID));
            sqlparam.Add(new SqlParameter("@FromDate", markAttendanceModel.FromDate));
            sqlparam.Add(new SqlParameter("@ToDate", markAttendanceModel.ToDate));
            DataTable dt = DAL.SQLHelp.ExecuteReader("", sqlparam);
            return dt;
        }

        public DataTable FetchAttendanceSummary(AttendanceModel markAttendanceModel)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserID", markAttendanceModel.UserID));
            sqlparam.Add(new SqlParameter("@FromDate", markAttendanceModel.FromDate));
            sqlparam.Add(new SqlParameter("@ToDate", markAttendanceModel.ToDate));
            DataTable dt = DAL.SQLHelp.ExecuteReader("", sqlparam);
            return dt;
        }
       
    }
}
