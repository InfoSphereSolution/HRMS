using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace BAL
{
   public class Client
    {
       DataTable dt = new DataTable();
       //Retrive City
       public DataTable City()
       {
           dt = DAL.SQLHelp.ExecuteSelect("select * from Tbl_City");
           return dt;

       }

       //Retrive State 
       public DataTable State()
       {

           dt = DAL.SQLHelp.ExecuteSelect("select * from Tbl_State");
           return dt;
       }

       //Add State 
       public int AddState(Models.ClientModel clientmodel)
       {
           int value = 0;
           List<SqlParameter> sqlparam = new List<SqlParameter>();
           sqlparam.Add(new SqlParameter("@StateName", clientmodel.StateName));
           SqlParameter Issuccesfull = new SqlParameter("@StateResult", SqlDbType.Int)
           {

               Direction = ParameterDirection.Output
           };
           sqlparam.Add(Issuccesfull);
           int i = DAL.SQLHelp.ExecuteNonQuery("Usp_InsertState", sqlparam);
           value = Convert.ToInt32(Issuccesfull.Value.ToString());
           return value;
       }

       //Add City
       public int AddCity(Models.ClientModel clientmodel)
       {

           int value = 0;
           List<SqlParameter> sqlparam = new List<SqlParameter>();
           sqlparam.Add(new SqlParameter("@CityName", clientmodel.CityName));
           sqlparam.Add(new SqlParameter("@StateId", clientmodel.StateId));
           SqlParameter Issuccesfull=new SqlParameter("@CityResult",SqlDbType.Int)
           {

               Direction=ParameterDirection.Output
           };
           sqlparam.Add(Issuccesfull);
           int i = DAL.SQLHelp.ExecuteNonQuery("Usp_InsertCity", sqlparam);
           value = Convert.ToInt32(Issuccesfull.Value.ToString());
           return value;
       }

       //Add Client Details
       public int AddClient(Models.ClientModel clientmodel)
       {
           int value = 0;
           List<SqlParameter> sqlparam = new List<SqlParameter>();
           sqlparam.Add(new SqlParameter("@ClientName", clientmodel.ClientName));
           sqlparam.Add(new SqlParameter("@StateId", clientmodel.StateId));
           sqlparam.Add(new SqlParameter("@CityId", clientmodel.CityId));
           sqlparam.Add(new SqlParameter("@ClientAddress", clientmodel.ClientAddress));
           sqlparam.Add(new SqlParameter("@PinCode", clientmodel.pinCode));
           sqlparam.Add(new SqlParameter("@WebSite", clientmodel.WebSite));
           sqlparam.Add(new SqlParameter("@ContactNo", clientmodel.ContactNo));
           sqlparam.Add(new SqlParameter("@IP_Address", clientmodel.IP_Address));
           sqlparam.Add(new SqlParameter("@Is_Sat_Working", clientmodel.Is_Sat_Working));
           sqlparam.Add(new SqlParameter("@AddedBy", clientmodel.AddedBy));
           sqlparam.Add(new SqlParameter("@GeneralShift_1",clientmodel.General_Shift1));
           sqlparam.Add(new SqlParameter("@GeneralShift_2",clientmodel.General_Shift2));
           sqlparam.Add(new SqlParameter("@GeneralShift_3", clientmodel.General_Shift3));
           sqlparam.Add(new SqlParameter("@FirstShift", clientmodel.FirstShift));
           sqlparam.Add(new SqlParameter("@SecondShift", clientmodel.SecondShift));
           sqlparam.Add(new SqlParameter("@NightShift", clientmodel.NightShift));
           sqlparam.Add(new SqlParameter("@IsCustomShift", clientmodel.IsCustomShift));
           sqlparam.Add(new SqlParameter("@FlexibleTime", clientmodel.FlexibleTime));
           sqlparam.Add(new SqlParameter("@NoOfOptionalHoliday", clientmodel.optionalholiday));
           sqlparam.Add(new SqlParameter("@Operation", 1));
           if (clientmodel.Is_Sat_Working == true)
           {
               sqlparam.Add(new SqlParameter("@IsSat_1", clientmodel.sat1));
               sqlparam.Add(new SqlParameter("@IsSat_2", clientmodel.sat2));
               sqlparam.Add(new SqlParameter("@IsSat_3", clientmodel.sat3));
               sqlparam.Add(new SqlParameter("@IsSat_4", clientmodel.sat4));
               sqlparam.Add(new SqlParameter("@IsSat_5", clientmodel.sat5));

           }
           //SqlParameter Issuccesfull = new SqlParameter("@ReturnClientId", SqlDbType.Int)
           //{

           //    Direction = ParameterDirection.Output
           //};
           SqlParameter succesful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
           {
               Direction = ParameterDirection.Output

           };
           //sqlparam.Add(Issuccesfull);
           sqlparam.Add(succesful);
     
           int i = DAL.SQLHelp.ExecuteNonQuery("Usp_UpdateClient", sqlparam);
           value = Convert.ToInt32(succesful.Value.ToString());
           //if (clientmodel.ClientId == -1)
           //{

           //    value = 0;
           //}
           //else
           //{
             
           //}
          return value;
  
       }
   }
   
}
