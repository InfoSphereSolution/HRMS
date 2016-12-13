using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class Access
    {
        public DataTable FetchAccess()
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("Select * from Mst_Menu where ParentMenu_Id = 0 ");            
            return dt;
        }

        public DataTable FetchAccessChild(String ParentID)
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select Menu_Id, MenuText from Mst_Menu where ParentMenu_Id = " + Convert.ToInt32(ParentID));
            return dt;        
        }

        public DataTable FetchFunctionalities(string PageID)
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select [Add], [Update], [Delete], Approve from Mst_Menu where Menu_Id = " + PageID);
            return dt;
        }

        public DataTable FetchDesignation()
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select Desig_Id, Designation_Name from Mst_Designation");
            return dt;
        }

        public Boolean GrantAccess(DataTable dtChecked)
        {
            Int32 DesignationId = Convert.ToInt32(dtChecked.Rows[0]["DesignationId"]);
            int delete = Convert.ToInt32(SQLHelp.ExecuteScalarValue("Delete from Mst_Access_Control where DesignationId = "+ DesignationId));            
            Boolean i = DAL.SQLHelp.CopyToServer("Mst_Access_Control", dtChecked);
            return i;
        }



        public DataTable FetchGivenAccess(Int32 DesignationID)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@DesignationId", DesignationID));
            DataTable dt = SQLHelp.ExecuteReader("Usp_GetGivenAccess", sqlparam);
            return dt;
        }

        public DataTable FetchGivenFunctionalities(String MenuID, Int32 DesignationID)
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select IsAdd, IsUpdate, IsDelete, IsApprove from Mst_Access_Control where DesignationId = " + DesignationID + "and MenuId = " + Convert.ToInt32(MenuID));
            return dt;
        }
    }
}
