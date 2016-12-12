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

        public DataTable FetchSideMenu(int ParentID)
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("Select * from Mst_Menu where ParentMenu_Id = " + ParentID);
            return dt;
        }
    }
}
