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
            employeeModel.EmpImgURL = Convert.ToString(dt.Rows[0][1]);
            employeeModel.EmpID = Convert.ToInt32(dt.Rows[0][2]);            
            employeeModel.EmpFirstName = Convert.ToString(dt.Rows[0][3]);
            employeeModel.EmpMiddleName = Convert.ToString(dt.Rows[0][4]);
            employeeModel.EmpLastName = Convert.ToString(dt.Rows[0][5]);
            employeeModel.EmpGender = Convert.ToString(dt.Rows[0][6]);
            employeeModel.EmpDOB = Convert.ToDateTime(dt.Rows[0][7]);
            employeeModel.EmpMaritalStatus = Convert.ToString(dt.Rows[0][8]);
            employeeModel.EmpContact = Convert.ToString(dt.Rows[0][9]);
            employeeModel.EmpAltContact = Convert.ToString(dt.Rows[0][10]);
            employeeModel.EmpPersonalEmailID = Convert.ToString(dt.Rows[0][11]);
            employeeModel.EmpReligion = Convert.ToString(dt.Rows[0][12]);
            employeeModel.EmpNationality = Convert.ToString(dt.Rows[0][13]);
            employeeModel.EmpCurrentAddress = Convert.ToString(dt.Rows[0][14]);
            employeeModel.EmpPermanentAddress = Convert.ToString(dt.Rows[0][15]);
            employeeModel.EmpState = Convert.ToInt32(dt.Rows[0][16]);
            employeeModel.EmpCity = Convert.ToInt32(dt.Rows[0][17]);
            employeeModel.EmpPincode = Convert.ToInt32(dt.Rows[0][18]);
            employeeModel.EmpBAN = Convert.ToString(dt.Rows[0][19]);
            employeeModel.EmpPAN = Convert.ToString(dt.Rows[0][20]);
            employeeModel.EmpAdhaar = Convert.ToString(dt.Rows[0][21]);



            //Passport Details
            employeeModel.EmpPassportNumber = Convert.ToString(dt.Rows[0][22]);
            employeeModel.EmpPassportIssuePlace = Convert.ToString(dt.Rows[0][23]);
            employeeModel.EmpPassportIssueCountry = Convert.ToString(dt.Rows[0][24]);
            employeeModel.EmpPassportIssueDate = Convert.ToDateTime(dt.Rows[0][25]);
            employeeModel.EmpPassportExpiryDate = Convert.ToDateTime(dt.Rows[0][26]);
            employeeModel.EmpECNRStatus = Convert.ToString(dt.Rows[0][27]);


            //Employees Professional Details
            employeeModel.EmpDOJ = Convert.ToDateTime(dt.Rows[0][28]);
            employeeModel.EmpDepartment = Convert.ToInt32(dt.Rows[0][29]);
            employeeModel.EmpDesignation = Convert.ToInt32(dt.Rows[0][30]);
            employeeModel.EmpBondStart = Convert.ToDateTime(dt.Rows[0][31]);
            employeeModel.EmpBondEnd = Convert.ToDateTime(dt.Rows[0][32]);
            employeeModel.EmpOrganizationEmailID = Convert.ToString(dt.Rows[0][33]);
            employeeModel.EmpClientID = Convert.ToInt32(dt.Rows[0][34]);
            employeeModel.EmpShiftID = Convert.ToInt32(dt.Rows[0][35]);
            employeeModel.EmpReportingManagerID = Convert.ToInt32(dt.Rows[0][36]);
            employeeModel.EmpIsConfirm = Convert.ToBoolean(dt.Rows[0][37]);
            employeeModel.EmpConfirmDate = Convert.ToDateTime(dt.Rows[0][38]);
            employeeModel.EmpBackgroundVerification = Convert.ToBoolean(dt.Rows[0][39]);
            employeeModel.EmpAddressVerification = Convert.ToBoolean(dt.Rows[0][40]);
            employeeModel.EmpEducationVerification = Convert.ToBoolean(dt.Rows[0][41]);
            employeeModel.EmpEmploymentVerification = Convert.ToBoolean(dt.Rows[0][42]);


            return employeeModel;
        }

        public void RemoveEmployee(Int32 EmpID)
        {
            List<SqlParameter> sqParam = new List<SqlParameter>();
            sqParam.Add(new SqlParameter("@UserName", EmpID));
            SQLHelp.ExecuteNonQuery("", sqParam);
        }

        public int UpdateEmployeeDetails(Models.EmployeeModel employeeModel, Int32 Operation)
        {
            int Status = 0;
            //Update Employee Details
            try
            {
                UpdatedEmployeePersonal(employeeModel, Operation);
                UpdatedEmployeePassport(employeeModel, Operation);
                UpdatedEmployeeProfessional(employeeModel, Operation);
            }
            catch (Exception)
            {

                throw;
            }




            return Status;
        }

        protected void UpdatedEmployeePersonal(Models.EmployeeModel employeeModel, Int32 Operation)
        {
            List<SqlParameter> sqParam = new List<SqlParameter>();
            // Personal details
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpID));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpImgURL));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpFirstName));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpMiddleName));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpLastName));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpGender));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpDOB));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpMaritalStatus));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpContact));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpAltContact));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpPersonalEmailID));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpReligion));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpNationality));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpCurrentAddress));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpPermanentAddress));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpState));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpCity));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpPincode));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpBAN));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpPAN));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpAdhaar));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpImgURL));
        }

        protected void UpdatedEmployeePassport(Models.EmployeeModel employeeModel, Int32 Operation)
        {
            List<SqlParameter> sqParam = new List<SqlParameter>();
            // Passport Details
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpPassportNumber));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpPassportIssuePlace));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpPassportIssueCountry));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpPassportIssueDate));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpPassportExpiryDate));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpECNRStatus));
        }

        protected void UpdatedEmployeeProfessional(Models.EmployeeModel employeeModel, Int32 Operation)
        {
            List<SqlParameter> sqParam = new List<SqlParameter>();
            // Professional details
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpDOJ));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpDepartment));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpDesignation));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpBondStart));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpBondEnd));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpOrganizationEmailID));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpClientID));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpShiftID));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpReportingManagerID));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpIsConfirm));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpConfirmDate));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpBackgroundVerification));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpAddressVerification));
            sqParam.Add(new SqlParameter("@Password", employeeModel.EmpEducationVerification));
            sqParam.Add(new SqlParameter("@UserName", employeeModel.EmpEmploymentVerification));


        }
    }
}
