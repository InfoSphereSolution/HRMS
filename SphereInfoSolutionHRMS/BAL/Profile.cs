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
    public class Profile
    {
        public DataTable EmployeeProfile(LoginModel login)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserId", login.UserId));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetProfile", sqlparam);
            return dt;
        }

        public DataTable FetchSideMenu(Int32 UserID, Int32 ParentID)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            if (ParentID == 0)
            {
                sqlparam.Add(new SqlParameter("@ParentId", null));
            }
            else
            {
                sqlparam.Add(new SqlParameter("@ParentId", ParentID));
            }
            
            sqlparam.Add(new SqlParameter("@UserId", UserID));
            
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetSideMenu", sqlparam);
            return dt;
        }

        public String FetchUserName(Int32 UserID)
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("Select (FirstName + ' ' + LastName) as Name  from vw_GetEmployeeDetails Where UserId = " + UserID);
            String UserName = Convert.ToString(dt.Rows[0][0]);
            return UserName;
        }
    }
}
