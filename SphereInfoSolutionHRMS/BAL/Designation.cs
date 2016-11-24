using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;
namespace BAL
{
    public class Designation
    {
         DataTable dt = new DataTable();
       

        //Retrieve Department List
        public DataTable DepartmentList()
        {
            dt = DAL.SQLHelp.ExecuteSelect("select * from Mst_Department");
            return dt;
        }

        //Retrieve Role List
        public DataTable RoleList()
       {
           dt = DAL.SQLHelp.ExecuteSelect("select * from Mst_Role");
           return dt;
              
       }
        //Get Designation From Master
        public DataTable GetDesigantion()
        {
            dt = DAL.SQLHelp.ExecuteSelect("select * from vw_GetDesignation");
            return dt;

        }

        //Get Designation From Temp Master
        public DataTable TempDesigantion()
        {
            dt = DAL.SQLHelp.ExecuteSelect("select * from vw_GetPendingDesignation");
            return dt;
        }

        // Add Role
        public int AddDesignation(Models.DesignationModel designation)
        {
            int count = 0;
            List<SqlParameter> sqldesignation = new List<SqlParameter>();
            sqldesignation.Add(new SqlParameter("@DesignationName", designation.DesignationName));
            sqldesignation.Add(new SqlParameter("@Role_Id", designation.RoleId));
            sqldesignation.Add(new SqlParameter("@Dept_Id", designation.DeptId));
            sqldesignation.Add(new SqlParameter("@CreatedBy", designation.CreatedBy));
            sqldesignation.Add(new SqlParameter("@Operation", 1));
            SqlParameter IsSuccessful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            sqldesignation.Add(IsSuccessful);
            //Add Desigantion In 
            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_UpdateDesignation", sqldesignation);
            count = Convert.ToInt32(IsSuccessful.Value.ToString());
            return count;
        }


        //Remove Role From DesignationMaster
        public int RemoveDesignation(Models.DesignationModel  designation)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@Desig_id", designation.DesigId));
            sqlparam.Add(new SqlParameter("@CreatedBy", designation.UpdatedBy));
            sqlparam.Add(new SqlParameter("@Operation", 2));
            SqlParameter IsSuccessful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            sqlparam.Add(IsSuccessful);
            // 2 is to Remove Role
            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_UpdateDesignation", sqlparam);
            int value = Convert.ToInt32(IsSuccessful.Value.ToString());
            return value;
        }


        //Update Designation From Temp Table 
        public int UpdateTempDesignation(Models.DesignationModel designation)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@TempDesignId", designation.TempDesigId));
            sqlparam.Add(new SqlParameter("@UserId", designation.CreatedBy));
            sqlparam.Add(new SqlParameter("@Operation", designation.Operation));
            SqlParameter IsSuccessful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            sqlparam.Add(IsSuccessful);
            // 1 is to Approve Designation
            // 2 is to Reject Designation
            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_ApproveOrRejectDesignation", sqlparam);
            int value = Convert.ToInt32(IsSuccessful.Value.ToString());
            return value;
        }

        //Filter Desigantion
        public DataTable FilterDesignation(string designationname)
        {

            dt = DAL.SQLHelp.ExecuteSelect("select * from vw_GetDesignation where Designation_Name='" + designationname + "'");
            return dt;
        }
    }
}
