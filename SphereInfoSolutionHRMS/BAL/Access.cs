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
    }
}
