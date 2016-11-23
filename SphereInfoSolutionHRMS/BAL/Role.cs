using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
namespace BAL
{
   public class Role
    {
        public DataTable DisplayLevel()
        {
         
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select * from Mst_Role_Level");
            return dt;
        }
        public int AddRole(Models.RoleModel rolemodel)
        {
            int value=0;
            List<SqlParameter> sqlparam=new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@RoleName",rolemodel.RoleName));
            sqlparam.Add(new SqlParameter("@Level",rolemodel.RoleLevel));
            sqlparam.Add(new SqlParameter("@CreatedBy", rolemodel.CreatedBy));
            sqlparam.Add(new SqlParameter("@Operation", 1));
            SqlParameter IsSuccessful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            sqlparam.Add(IsSuccessful);
            // 1 is to Insert Role
            int i  = DAL.SQLHelp.ExecuteNonQuery("Usp_UpdateRole", sqlparam);
            value=Convert.ToInt32(IsSuccessful.Value.ToString());

            return value;
            
        }
     
       //Fetch Role Details from master table
        public DataTable Displaydata()
        {
            DataTable dt = new DataTable();            
            dt = DAL.SQLHelp.ExecuteSelect("select * from vw_GetRoles");
            return dt;
        }

       //Fetch Role details from temp table wating for approval
        public DataTable DisplayTempdata()
        {
            DataTable dt = new DataTable();            
            dt = DAL.SQLHelp.ExecuteSelect("select * from vw_GetPendingRoles");
            return dt;


        }
       //Remove Role From RoleMst Table
        public int RemoveRole(Models.RoleModel rolemodel)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@RoleId", rolemodel.RoleId));
            sqlparam.Add(new SqlParameter("@CreatedBy", rolemodel.UpdatedBy));
            sqlparam.Add(new SqlParameter("@Operation", 2));
            SqlParameter IsSuccessful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            sqlparam.Add(IsSuccessful);
            // 2 is to Remove Role
            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_UpdateRole", sqlparam);
            int value = Convert.ToInt32(IsSuccessful.Value.ToString());
            return value;
        }
      //Update Role From Temp Table 
        public int UpdateTempRole(Models.RoleModel rolemodel)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@TempRoleId", rolemodel.TempRoleId));
            sqlparam.Add(new SqlParameter("@UserId", rolemodel.CreatedBy));
            sqlparam.Add(new SqlParameter("@Operation", rolemodel.Operation));
            SqlParameter IsSuccessful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            sqlparam.Add(IsSuccessful);
            // 1 is to Approve Role
            // 2 is to Reject Role
            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_ApproveOrRejectRoles", sqlparam);
            int value = Convert.ToInt32(IsSuccessful.Value.ToString());
            return value;
        }

       public DataTable SearchRole(String SearchRole)
       {
           DataTable dt = new DataTable();
           dt = DAL.SQLHelp.ExecuteSelect("select * from vw_GetRoles where RoleName='" + SearchRole + "'");
           return dt;
       }

    }
}
