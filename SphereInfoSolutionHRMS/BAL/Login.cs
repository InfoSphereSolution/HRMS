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
        //Update Password
        public bool UpdatePassword(Models.LoginModel login)
        {
            List<SqlParameter> changepwd = new List<SqlParameter>();
            changepwd.Add(new SqlParameter("@Password", login.Password));
            changepwd.Add(new SqlParameter("@UserId", login.UserId));
            int rows = DAL.SQLHelp.ExecuteNonQuery("SP_ChangePwd ", changepwd);
            return true;
        }
        //Check Pwd Exist
        public DataSet CheckPassword(Models.LoginModel login)
        {

            DataSet ds = new DataSet();
            SqlParameter[] sp ={
                                new SqlParameter("@UserId",SqlDbType.Int){Value= login.UserId},
                               };
            ds = DAL.SQLHelp.DSExecuteQuery1("SP_Chkpwdexist", CommandType.StoredProcedure, sp);
            return ds;

        }
    }
}
