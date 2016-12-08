using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BAL
{
    public class Employee
    {
        List<SqlParameter> sqParam = new List<SqlParameter>();
        public DataTable FetchState()
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select * from Tbl_State");
            return dt;
        }

        public DataTable FetchCity()
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select City_Id, CityName from Tbl_City");
            return dt;
        }

        public DataTable FetchDepartment()
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select * from Mst_Department");
            return dt;
        }

        public DataTable FetchDesignation()
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select Desig_Id, Designation_Name from Mst_Designation");
            return dt;
        }

        public DataTable FetchClient()
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("Select ClientId, ClientName from Mst_Client");
            return dt;
        }

        public DataTable FetchShift(Int32 ClientID)
        {
            List<SqlParameter> sqParam = new List<SqlParameter>();
            sqParam.Add(new SqlParameter("@ClientId", ClientID));
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetShiftList", sqParam);
            return dt;
        }

        public DataTable FetchShift()
        {
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetShiftList", null);
            return dt;
        }

        public DataTable FetchReportingManager()
        {
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetReportingManager", null);
            return dt;
        }

        public Int32 FetchNewUserID()
        {
            DataTable dt = DAL.SQLHelp.ExecuteReader("Usp_GetNewUserID", null);
            Int32 NewUserID = Convert.ToInt32(dt.Rows[0][0]);
            return NewUserID;
        }

        public DataTable FetchEmployeeList()
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select * from vw_GetEmployeeList");
            return dt;
        }

        public EmployeeModel FetchEmployeeDetails(Int32 UserID)
        {

            DataTable dt = DAL.SQLHelp.ExecuteSelect("select * from vw_GetEmployeeDetails where UserId = " + UserID);
            EmployeeModel employeeModel = new EmployeeModel();

            // Personal Details
            employeeModel.UserID = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpImgURL = Convert.ToString(dt.Rows[0][1]);            
            employeeModel.EmpFirstName = Convert.ToString(dt.Rows[0][2]);
            employeeModel.EmpMiddleName = Convert.ToString(dt.Rows[0][3]);
            employeeModel.EmpLastName = Convert.ToString(dt.Rows[0][4]);
            employeeModel.EmpGender = Convert.ToString(dt.Rows[0][5]);
            //employeeModel.EmpDOB = Convert.ToDateTime(dt.Rows[0][6]);
            employeeModel.EmpDOB = String.IsNullOrEmpty((dt.Rows[0][6].ToString())) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0][6]);
            employeeModel.EmpMaritalStatus = Convert.ToString(dt.Rows[0][7]);
            employeeModel.EmpContact = Convert.ToString(dt.Rows[0][8]);
            employeeModel.EmpAltContact = Convert.ToString(dt.Rows[0][9]);
            employeeModel.EmpPersonalEmailID = Convert.ToString(dt.Rows[0][10]);
            employeeModel.EmpReligion = Convert.ToString(dt.Rows[0][11]);
            employeeModel.EmpNationality = Convert.ToString(dt.Rows[0][12]);
            employeeModel.EmpCurrentAddress = Convert.ToString(dt.Rows[0][13]);
            employeeModel.EmpPermanentAddress = Convert.ToString(dt.Rows[0][14]);
            employeeModel.EmpState = Convert.ToInt32(dt.Rows[0][15]);
            employeeModel.EmpCity = Convert.ToInt32(dt.Rows[0][16]);
            //employeeModel.EmpPincode = Convert.ToInt32(dt.Rows[0][17]);
            employeeModel.EmpPincode = String.IsNullOrEmpty((dt.Rows[0][17]).ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[0][17]);
            employeeModel.EmpBAN = Convert.ToString(dt.Rows[0][18]);
            employeeModel.EmpPAN = Convert.ToString(dt.Rows[0][19]);
            employeeModel.EmpAdhaar = Convert.ToString(dt.Rows[0][20]);



            //Passport Details
            employeeModel.EmpPassportNumber = Convert.ToString(dt.Rows[0][21]);
            employeeModel.EmpPassportIssuePlace = Convert.ToString(dt.Rows[0][22]);
            employeeModel.EmpPassportIssueCountry = Convert.ToString(dt.Rows[0][23]);
            //employeeModel.EmpPassportIssueDate = Convert.ToDateTime(dt.Rows[0][24]);
            employeeModel.EmpPassportIssueDate = String.IsNullOrEmpty((dt.Rows[0][24].ToString())) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0][24]);
            //employeeModel.EmpPassportExpiryDate = Convert.ToDateTime(dt.Rows[0][25]);
            employeeModel.EmpPassportExpiryDate = String.IsNullOrEmpty((dt.Rows[0][25].ToString())) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0][25]);
            employeeModel.EmpECNRStatus = Convert.ToString(dt.Rows[0][26]);


            //Employees Professional Details
            //employeeModel.EmpDOJ = Convert.ToDateTime(dt.Rows[0][27]);
            employeeModel.EmpDOJ = String.IsNullOrEmpty((dt.Rows[0][27].ToString())) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0][27]);
            employeeModel.EmpDepartment = Convert.ToInt32(dt.Rows[0][28]);
            employeeModel.EmpDesignation = Convert.ToInt32(dt.Rows[0][29]);
            //employeeModel.EmpBondStart = Convert.ToDateTime(dt.Rows[0][30]);
            employeeModel.EmpBondStart = String.IsNullOrEmpty((dt.Rows[0][30].ToString())) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0][30]);
            //employeeModel.EmpBondEnd = Convert.ToDateTime(dt.Rows[0][31]);
            employeeModel.EmpBondEnd = String.IsNullOrEmpty((dt.Rows[0][31].ToString())) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0][31]);
            employeeModel.EmpOrganizationEmailID = Convert.ToString(dt.Rows[0][32]);
            employeeModel.EmpClientID = Convert.ToInt32(dt.Rows[0][33]);
            employeeModel.EmpShiftID = Convert.ToInt32(dt.Rows[0][34]);
            employeeModel.EmpReportingManagerID = Convert.ToInt32(dt.Rows[0][35]);
            employeeModel.EmpIsConfirm = Convert.ToBoolean(dt.Rows[0][36]);
            //employeeModel.EmpConfirmDate = Convert.ToDateTime(dt.Rows[0][37]);
            employeeModel.EmpConfirmDate = String.IsNullOrEmpty((dt.Rows[0][37].ToString())) ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0][37]);
            employeeModel.EmpBackgroundVerification = Convert.ToBoolean(dt.Rows[0][38]);
            employeeModel.EmpAddressVerification = Convert.ToBoolean(dt.Rows[0][39]);
            employeeModel.EmpEducationVerification = Convert.ToBoolean(dt.Rows[0][40]);
            employeeModel.EmpEmploymentVerification = Convert.ToBoolean(dt.Rows[0][41]);


            return employeeModel;
        }

        public Int32 RemoveEmployee(Int32 UserID, Int32 CurrentUserID)
        {
            List<SqlParameter> sqParam = new List<SqlParameter>();
            sqParam.Add(new SqlParameter("@UserId", UserID));
            sqParam.Add(new SqlParameter("@UpdatedBy", CurrentUserID));
            Int32 i = SQLHelp.ExecuteNonQuery("Usp_DeleteEmployee", sqParam);
            return i;
        }

        public DataTable UpdateEmployeeDetails(Models.EmployeeModel employeeModel)
        {
            DataTable dt;
            //Update Employee Details
            try
            {
                UpdatedEmployeePersonal(employeeModel);
                UpdatedEmployeePassport(employeeModel);
                UpdatedEmployeeProfessional(employeeModel);

                dt = DAL.SQLHelp.ExecuteReader("Usp_UpdateEmployee", sqParam);                

            }
            catch (Exception)
            {   
                throw;
            }

            return dt;
        }

        protected void UpdatedEmployeePersonal(Models.EmployeeModel employeeModel)
        {
            //Current UserId
            sqParam.Add(new SqlParameter("@Created_By", employeeModel.Current_UserID));            

            // Personal details
            sqParam.Add(new SqlParameter("@UserId", employeeModel.UserID));
            sqParam.Add(new SqlParameter("@PhotoUrl", employeeModel.EmpImgURL));
            sqParam.Add(new SqlParameter("@FirstName", employeeModel.EmpFirstName));
            sqParam.Add(new SqlParameter("@MiddleName", employeeModel.EmpMiddleName));
            sqParam.Add(new SqlParameter("@LastName", employeeModel.EmpLastName));
            sqParam.Add(new SqlParameter("@Gender", employeeModel.EmpGender));
            sqParam.Add(new SqlParameter("@DOB", employeeModel.EmpDOB));
            sqParam.Add(new SqlParameter("@Marital_Status", employeeModel.EmpMaritalStatus));
            sqParam.Add(new SqlParameter("@MobileNo", employeeModel.EmpContact));
            sqParam.Add(new SqlParameter("@Emergency_ContactNo", employeeModel.EmpAltContact));
            sqParam.Add(new SqlParameter("@EmailId", employeeModel.EmpPersonalEmailID));
            sqParam.Add(new SqlParameter("@Religion", employeeModel.EmpReligion));
            sqParam.Add(new SqlParameter("@Nationality", employeeModel.EmpNationality));
            sqParam.Add(new SqlParameter("@Current_Address", employeeModel.EmpCurrentAddress));
            sqParam.Add(new SqlParameter("@Perm_Address", employeeModel.EmpPermanentAddress));
            sqParam.Add(new SqlParameter("@Perm_State", employeeModel.EmpState));
            sqParam.Add(new SqlParameter("@Perm_City", employeeModel.EmpCity));
            sqParam.Add(new SqlParameter("@Perm_Pincode", employeeModel.EmpPincode));
            sqParam.Add(new SqlParameter("@Bank_AccountNo", employeeModel.EmpBAN));
            sqParam.Add(new SqlParameter("@PAN_No", employeeModel.EmpPAN));
            sqParam.Add(new SqlParameter("@Adhar_No", employeeModel.EmpAdhaar));     
        }

        protected void UpdatedEmployeePassport(Models.EmployeeModel employeeModel)
        {            
            // Passport Details
            sqParam.Add(new SqlParameter("@PassportNo", employeeModel.EmpPassportNumber));
            sqParam.Add(new SqlParameter("@PlaceOfIssue", employeeModel.EmpPassportIssuePlace));
            sqParam.Add(new SqlParameter("@CountryOfIssue", employeeModel.EmpPassportIssueCountry));
            sqParam.Add(new SqlParameter("@DateOfIssue", employeeModel.EmpPassportIssueDate));
            sqParam.Add(new SqlParameter("@DateOfExpiry", employeeModel.EmpPassportExpiryDate));
            sqParam.Add(new SqlParameter("@ECNR_Status", employeeModel.EmpECNRStatus));
        }

        protected void UpdatedEmployeeProfessional(Models.EmployeeModel employeeModel)
        {            
            // Professional details
            sqParam.Add(new SqlParameter("@DOJ", employeeModel.EmpDOJ));
            sqParam.Add(new SqlParameter("@Department", employeeModel.EmpDepartment));
            sqParam.Add(new SqlParameter("@Designation", employeeModel.EmpDesignation));
            sqParam.Add(new SqlParameter("@Bond_Start_Date", employeeModel.EmpBondStart));
            sqParam.Add(new SqlParameter("@Bond_End_Date", employeeModel.EmpBondEnd));
            sqParam.Add(new SqlParameter("@Official_Email_Id", employeeModel.EmpOrganizationEmailID));
            sqParam.Add(new SqlParameter("@Client_Id", employeeModel.EmpClientID));
            sqParam.Add(new SqlParameter("@ShiftTimeId", employeeModel.EmpShiftID));
            sqParam.Add(new SqlParameter("@ReportingManager_Id", employeeModel.EmpReportingManagerID));
            sqParam.Add(new SqlParameter("@Confirmation_Status", employeeModel.EmpIsConfirm));
            sqParam.Add(new SqlParameter("@Confirmation_Date", employeeModel.EmpConfirmDate));
            sqParam.Add(new SqlParameter("@Background_VerificationCheck", employeeModel.EmpBackgroundVerification));
            sqParam.Add(new SqlParameter("@Address_Check", employeeModel.EmpAddressVerification));
            sqParam.Add(new SqlParameter("@Educational_Check", employeeModel.EmpEducationVerification));
            sqParam.Add(new SqlParameter("@Employment_Check", employeeModel.EmpEmploymentVerification));
        }

        public Int32 encryptPassword(String Username, String EncryptedPassword)
        {
            List<SqlParameter> sqParam = new List<SqlParameter>();
            sqParam.Add(new SqlParameter("@Username", Username));
            sqParam.Add(new SqlParameter("@Password", EncryptedPassword));
            Int32 i = SQLHelp.ExecuteNonQuery("Usp_EncryptEmployeePassword", sqParam);
            return i;
        }
    }
}
