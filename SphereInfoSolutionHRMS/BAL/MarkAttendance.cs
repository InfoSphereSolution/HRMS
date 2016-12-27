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

        //public DataTable FetchAttendanceSummary(AttendanceModel attendanceModel)
        //{
        //    List<SqlParameter> sqlparam = new List<SqlParameter>();
        //    sqlparam.Add(new SqlParameter("@UserID", attendanceModel.UserID));
        //    sqlparam.Add(new SqlParameter("@FromDate", attendanceModel.FromDate));
        //    sqlparam.Add(new SqlParameter("@ToDate", attendanceModel.ToDate));
        //    DataTable dt = DAL.SQLHelp.ExecuteReader("", sqlparam);
        //    return dt;
        //}

        public DataTable FetchEmployee()
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@CLientId", null));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetEmployeeName", sqlparam);
            return dt;
        }

        public DataTable FetchShiftOfEmployee(int UserId)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserId", UserId));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetShiftOfEmployee", sqlparam);
            return dt;
        }


        public DataTable CheckMarkedAttendance(int UserId, DateTime date)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sqlparam = new List<SqlParameter>();
                sqlparam.Add(new SqlParameter("@UserId", UserId));
                sqlparam.Add(new SqlParameter("@Date", date));
                dt = DAL.SQLHelp.ExecuteReader("Usp_CheckMarkedAttendance", sqlparam);
                
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public void MarkUnmarkedAttendance()
        {
            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_UpdateAttendance", null);
        }

        public int MarkAttendanceByHr(int UserId,DateTime Date,DateTime InTime,DateTime OutTime)
        {
            DataTable dt = new DataTable();
            int i = 0;
            try
            {
                List<SqlParameter> sqlparam = new List<SqlParameter>();
                sqlparam.Add(new SqlParameter("@UserId",UserId));
                sqlparam.Add(new SqlParameter("@Date", Date));
                sqlparam.Add(new SqlParameter("@InTime", InTime));
                sqlparam.Add(new SqlParameter("@OutTime", OutTime));
                 i = DAL.SQLHelp.ExecuteNonQuery("Usp_MarkForgotAttendance", sqlparam);

            }
            catch (Exception ex)
            {
            }
            return i;
        }
    }
}
