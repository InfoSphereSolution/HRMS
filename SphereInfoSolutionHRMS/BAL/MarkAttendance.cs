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
        
        public Int32 CheckMarkAttendance(Int32 UserID)
        {
            /*Logic to check if Current User has marked attendance */




            /*
             * 1: Have To Punch - In
             * 2: Already Punched-In but not Punched - Out
             * 3: Punched - In & Punched - Out
             */
            return 1;
        }

        public Boolean MarkUserAttendance(MarkAttendanceModel markAttendanceModel)
        {
            return true;
        }


        public DataTable FetchAttendance(MarkAttendanceModel markAttendanceModel)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserID", markAttendanceModel.UserID));
            sqlparam.Add(new SqlParameter("@FromDate", markAttendanceModel.FromDate));
            sqlparam.Add(new SqlParameter("@ToDate", markAttendanceModel.ToDate));
            DataTable dt = DAL.SQLHelp.ExecuteReader("", sqlparam);
            return dt;
        }

        public DataTable FetchAttendanceSummary(MarkAttendanceModel markAttendanceModel)
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
