using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DAL;

namespace BAL
{
    public class Login
    {


        public DataTable checkUser(Models.LoginModel loginModel)
        {
            List<SqlParameter> sqParam = new List<SqlParameter>();
            sqParam.Add(new SqlParameter("@UserName", loginModel.Username));
            sqParam.Add(new SqlParameter("@Password", loginModel.Password));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_AuthenticateUser", sqParam);
            return dt;
        }
    }
}
