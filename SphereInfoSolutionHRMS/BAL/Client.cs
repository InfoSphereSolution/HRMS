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

        //Checking Access
        public bool CheckAccess(int UserId)
        {
            bool value = false;
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@UserId", UserId));
            SqlParameter IsAdd = new SqlParameter("@IsAdd", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter IsUpdate = new SqlParameter("@IsUpdate", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter IsDelete = new SqlParameter("@IsDelete", SqlDbType.Bit)
            {

                Direction = ParameterDirection.Output
            };
            SqlParameter IsApprove = new SqlParameter("@IsApprove", SqlDbType.Bit)
            {

                Direction = ParameterDirection.Output
            };
            sqlparam.Add(IsAdd);
            sqlparam.Add(IsUpdate);
            sqlparam.Add(IsDelete);
            sqlparam.Add(IsApprove);
            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_Check_UserAccess", sqlparam);
            bool isAdd = Convert.ToBoolean(IsAdd.Value.ToString());
            bool isUpdate = Convert.ToBoolean(IsUpdate.Value.ToString());
            bool isDel = Convert.ToBoolean(IsDelete.Value.ToString());
            bool isApp = Convert.ToBoolean(IsApprove.Value.ToString());

            if (isApp == true)
            {
                value=true;
            }
            return value;
        }

       public DataTable retrieveshift(int shift)
       {
           List<SqlParameter> sqlparam = new List<SqlParameter>();
           sqlparam.Add(new SqlParameter("@ClientId", shift));
           dt = DAL.SQLHelp.ExecuteReader("[dbo].[Usp_GetGeneralShift]",sqlparam);
           return dt;
       }
        //Get saturday working Details 
       public DataTable RetriveSaturdayWorking(int clientId)
       {
           List<SqlParameter> sqlparam = new List<SqlParameter>();
           sqlparam.Add(new SqlParameter("@ClientId", clientId));
           dt = DAL.SQLHelp.ExecuteReader("Usp_GetSatWorkingOn", sqlparam);
           return dt;
       }

       //Get custom shift Details 
       public DataTable RetriveCustomShiftDetails(int clientId)
       {
           List<SqlParameter> sqlparam = new List<SqlParameter>();
           sqlparam.Add(new SqlParameter("@ClientId", clientId));
           dt = DAL.SQLHelp.ExecuteReader("Usp_GetCustomShiftDetail", sqlparam);
           return dt;
       }

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

        //Retrive Master Table
        public DataTable MasterClient()
        {

            dt = DAL.SQLHelp.ExecuteSelect("select * from [dbo].[vw_GetClientsDetail]");
            return dt;
        }
        //Retrive Master Table
        public DataTable MasterClient1(int Id)
        {

            dt = DAL.SQLHelp.ExecuteSelect("select * from [dbo].[vw_GetClientsDetail] where ClientId="+Id);
            return dt;
        }

        //Retrive Temp atble Data
        public DataTable TempClient()
        {
            dt = DAL.SQLHelp.ExecuteSelect("select * from [dbo].[vw_GetPendingClientsDetail]");
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
            SqlParameter Issuccesfull = new SqlParameter("@CityResult", SqlDbType.Int)
            {

                Direction = ParameterDirection.Output
            };
            sqlparam.Add(Issuccesfull);
            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_InsertCity", sqlparam);
            value = Convert.ToInt32(Issuccesfull.Value.ToString());
            return value;
        }

        //Add Client Details
        public int AddClient(Models.ClientModel clientmodel)
        {
            Int32 MasterClientID=0;
            int value = 0;
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@ClientId", clientmodel.ClientId));
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
            sqlparam.Add(new SqlParameter("@GeneralShift_1", clientmodel.General_Shift1));
            sqlparam.Add(new SqlParameter("@GeneralShift_2", clientmodel.General_Shift2));
            sqlparam.Add(new SqlParameter("@GeneralShift_3", clientmodel.General_Shift3));
            sqlparam.Add(new SqlParameter("@FirstShift", clientmodel.FirstShift));
            sqlparam.Add(new SqlParameter("@SecondShift", clientmodel.SecondShift));
            sqlparam.Add(new SqlParameter("@NightShift", clientmodel.NightShift));
            sqlparam.Add(new SqlParameter("@IsCustomShift", clientmodel.IsCustomShift));
            sqlparam.Add(new SqlParameter("@FlexibleTime", clientmodel.FlexibleTime));
            sqlparam.Add(new SqlParameter("@NoOfOptionalHoliday", clientmodel.optionalholiday));
            sqlparam.Add(new SqlParameter("@Operation",clientmodel.Operation));

            //if (clientmodel.Is_Sat_Working == true)
            //{
            sqlparam.Add(new SqlParameter("@IsSat_1", clientmodel.sat1));
            sqlparam.Add(new SqlParameter("@IsSat_2", clientmodel.sat2));
            sqlparam.Add(new SqlParameter("@IsSat_3", clientmodel.sat3));
            sqlparam.Add(new SqlParameter("@IsSat_4", clientmodel.sat4));
            sqlparam.Add(new SqlParameter("@IsSat_5", clientmodel.sat5));

           

            SqlParameter MasterClientId = new SqlParameter("@ReturnClientId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
          
            SqlParameter succesful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output

            };
            sqlparam.Add(MasterClientId);
           // sqlparam.Add(TempClientId);
            sqlparam.Add(succesful);

            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_UpdateClient", sqlparam);
            value = Convert.ToInt32(succesful.Value.ToString());
         
            if (i != -1 && i!=2)
            {
                 MasterClientID = Convert.ToInt32(MasterClientId.Value.ToString());
            }
            //Int32 TempClientID = Convert.ToInt32(TempClientId.Value.ToString());
            bool isApproveAccess=CheckAccess(clientmodel.AddedBy);

          
            return value;

        }


        ////Add Client Details
        //public int AddClient(Models.ClientModel clientmodel, DataTable dtcustom)
        //{
        //    Int32 MasterClientID = 0;
        //    int value = 0;
        //    List<SqlParameter> sqlparam = new List<SqlParameter>();
        //    sqlparam.Add(new SqlParameter("@ClientId", clientmodel.ClientId));
        //    sqlparam.Add(new SqlParameter("@ClientName", clientmodel.ClientName));
        //    sqlparam.Add(new SqlParameter("@StateId", clientmodel.StateId));
        //    sqlparam.Add(new SqlParameter("@CityId", clientmodel.CityId));
        //    sqlparam.Add(new SqlParameter("@ClientAddress", clientmodel.ClientAddress));
        //    sqlparam.Add(new SqlParameter("@PinCode", clientmodel.pinCode));
        //    sqlparam.Add(new SqlParameter("@WebSite", clientmodel.WebSite));
        //    sqlparam.Add(new SqlParameter("@ContactNo", clientmodel.ContactNo));
        //    sqlparam.Add(new SqlParameter("@IP_Address", clientmodel.IP_Address));
        //    sqlparam.Add(new SqlParameter("@Is_Sat_Working", clientmodel.Is_Sat_Working));
        //    sqlparam.Add(new SqlParameter("@AddedBy", clientmodel.AddedBy));
        //    sqlparam.Add(new SqlParameter("@GeneralShift_1", clientmodel.General_Shift1));
        //    sqlparam.Add(new SqlParameter("@GeneralShift_2", clientmodel.General_Shift2));
        //    sqlparam.Add(new SqlParameter("@GeneralShift_3", clientmodel.General_Shift3));
        //    sqlparam.Add(new SqlParameter("@FirstShift", clientmodel.FirstShift));
        //    sqlparam.Add(new SqlParameter("@SecondShift", clientmodel.SecondShift));
        //    sqlparam.Add(new SqlParameter("@NightShift", clientmodel.NightShift));
        //    sqlparam.Add(new SqlParameter("@IsCustomShift", clientmodel.IsCustomShift));
        //    sqlparam.Add(new SqlParameter("@FlexibleTime", clientmodel.FlexibleTime));
        //    sqlparam.Add(new SqlParameter("@NoOfOptionalHoliday", clientmodel.optionalholiday));
        //    sqlparam.Add(new SqlParameter("@Operation", clientmodel.Operation));

        //    //if (clientmodel.Is_Sat_Working == true)
        //    //{
        //    sqlparam.Add(new SqlParameter("@IsSat_1", clientmodel.sat1));
        //    sqlparam.Add(new SqlParameter("@IsSat_2", clientmodel.sat2));
        //    sqlparam.Add(new SqlParameter("@IsSat_3", clientmodel.sat3));
        //    sqlparam.Add(new SqlParameter("@IsSat_4", clientmodel.sat4));
        //    sqlparam.Add(new SqlParameter("@IsSat_5", clientmodel.sat5));



        //    SqlParameter MasterClientId = new SqlParameter("@ReturnClientId", SqlDbType.Int)
        //    {
        //        Direction = ParameterDirection.Output
        //    };

        //    SqlParameter succesful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
        //    {
        //        Direction = ParameterDirection.Output

        //    };
        //    sqlparam.Add(MasterClientId);
        //    // sqlparam.Add(TempClientId);
        //    sqlparam.Add(succesful);

        //    int i = DAL.SQLHelp.ExecuteNonQuery("Usp_UpdateClient", sqlparam);
        //    value = Convert.ToInt32(succesful.Value.ToString());

        //    if (i != -1 && i != 2)
        //    {
        //        MasterClientID = Convert.ToInt32(MasterClientId.Value.ToString());
        //    }
        //    //Int32 TempClientID = Convert.ToInt32(TempClientId.Value.ToString());
        //    bool isApproveAccess = CheckAccess(clientmodel.AddedBy);

        //    if (clientmodel.ClientId == -1)
        //    {

        //        return value;
        //    }
        //    else
        //    {

        //        if (dtcustom.Rows.Count >= 1)
        //        {
        //            if (clientmodel.IsCustomShift == true)
        //            {
        //                if (isApproveAccess == false)
        //                {
        //                    DataTable dt = new DataTable();
        //                    dt.Columns.Add(new DataColumn("TempCustomShiftId", typeof(Int32)));
        //                    dt.Columns.Add(new DataColumn("TempClientID", typeof(Int32)));
        //                    dt.Columns.Add(new DataColumn("CustomShiftName", typeof(string)));
        //                    dt.Columns.Add(new DataColumn("StartTime", typeof(string)));
        //                    dt.Columns.Add(new DataColumn("EndTime", typeof(string)));
        //                    dt.Columns.Add(new DataColumn("TotalHrs", typeof(Int32)));
        //                    for (int a = 0; a < dtcustom.Rows.Count; a++)
        //                    {
        //                        string ShiftName = dtcustom.Rows[a]["ShiftName"].ToString();
        //                        string StartTime = dtcustom.Rows[a]["StartTime"].ToString();
        //                        string EndTime = dtcustom.Rows[a]["EndTime"].ToString();
        //                        int hour = Convert.ToInt32(dtcustom.Rows[a]["Hours"].ToString());
        //                        dt.Rows.Add(1, MasterClientID, ShiftName, StartTime, EndTime, hour);

        //                    }
        //                    DAL.SQLHelp.CopyToServer("Tbl_Temp_Client_Custom_Shift", dt);
        //                }
        //                else
        //                {
        //                    DataTable dt = new DataTable();
        //                    dt.Columns.Add(new DataColumn("CustomShiftId", typeof(int)));
        //                    dt.Columns.Add(new DataColumn("CustomShiftName", typeof(string)));
        //                    dt.Columns.Add(new DataColumn("ClientId", typeof(string)));
        //                    dt.Columns.Add(new DataColumn("StartTime", typeof(string)));
        //                    dt.Columns.Add(new DataColumn("EndTime", typeof(string)));
        //                    dt.Columns.Add(new DataColumn("TotalHrs", typeof(int)));
        //                    for (int a = 0; a < dtcustom.Rows.Count; a++)
        //                    {
        //                        string ShiftName = dtcustom.Rows[a]["ShiftName"].ToString();
        //                        string StartTime = dtcustom.Rows[a]["StartTime"].ToString();
        //                        string EndTime = dtcustom.Rows[a]["EndTime"].ToString();
        //                        int hour = Convert.ToInt32(dtcustom.Rows[a]["Hours"].ToString());
        //                        dt.Rows.Add(1, ShiftName, MasterClientID, StartTime, EndTime, hour);

        //                    }
        //                    DAL.SQLHelp.CopyToServer("Tbl_Clients_Custom_Shift", dt);

        //                }
        //            }
        //        }

        //    }
        //    return value;

        //}


        //Update Client From Temp Table 
        public int UpdateTempClient(Models.ClientModel client)
        {
           
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@PendingClientId", client.tempclientId));

            sqlparam.Add(new SqlParameter("@UserId", client.CreatedBy));
            sqlparam.Add(new SqlParameter("@Operation", client.Operation));
            if (client.Operation == 1)
            {
                DataTable dtcustom = GetCustom(client.ClientId);
                if (dtcustom.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("CustomShiftId", typeof(int)));
                    dt.Columns.Add(new DataColumn("CustomShiftName", typeof(string)));
                    dt.Columns.Add(new DataColumn("ClientId", typeof(string)));
                    dt.Columns.Add(new DataColumn("StartTime", typeof(string)));
                    dt.Columns.Add(new DataColumn("EndTime", typeof(string)));
                    dt.Columns.Add(new DataColumn("TotalHrs", typeof(int)));
                    for (int a = 0; a < dtcustom.Rows.Count; a++)
                    {
                        string ShiftName = dtcustom.Rows[a]["CustomShiftName"].ToString();
                        string StartTime = dtcustom.Rows[a]["StartTime"].ToString();
                        string EndTime = dtcustom.Rows[a]["EndTime"].ToString();
                        int hour = Convert.ToInt32(dtcustom.Rows[a]["TotalHrs"].ToString());
                        dt.Rows.Add(1, ShiftName, client.ClientId, StartTime, EndTime, hour);

                    }
                    DAL.SQLHelp.CopyToServer("Tbl_Clients_Custom_Shift", dt);

                }
            }
            
            SqlParameter IsSuccessful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            sqlparam.Add(IsSuccessful);
            // 1 is to Approve Designation
            // 2 is to Reject Designation
      
            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_ApproveOrRejectClients", sqlparam);
            int value = Convert.ToInt32(IsSuccessful.Value.ToString());
           
            return value;
        }
        protected DataTable GetCustom(int Id)
        {

            dt = DAL.SQLHelp.ExecuteSelect("select * from [dbo].[Tbl_Temp_Client_Custom_Shift] where [TempClientID]=" + Id);
            return dt;
        }


        //Remove Client From ClientMaster
        public int RemoveClient(Models.ClientModel client)
        {
            List<SqlParameter> sqlparam = new List<SqlParameter>();
            sqlparam.Add(new SqlParameter("@ClientId", client.ClientId));
            sqlparam.Add(new SqlParameter("@AddedBy", client.UpdatedBy));
            sqlparam.Add(new SqlParameter("@Operation", 3));
            SqlParameter IsSuccessful = new SqlParameter("@IsSuccessful", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            sqlparam.Add(IsSuccessful);
            // 2 is to Remove Role
            int i = DAL.SQLHelp.ExecuteNonQuery("Usp_UpdateClient", sqlparam);
            int value = Convert.ToInt32(IsSuccessful.Value.ToString());
            return value;
        }
    }

}
