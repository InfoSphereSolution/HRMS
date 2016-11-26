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
        public DataTable FetchEmployeeList()
        {
            DataTable dt = DAL.SQLHelp.ExecuteSelect("select * from vv_EmployeesList");
            return dt;
        }

        public EmployeeModel FetchEmployeeDetails(Int32 EmpID)
        {

            DataTable dt = DAL.SQLHelp.ExecuteSelect("select * from vv_EmployeesDetails where EmpID=" + EmpID);
            EmployeeModel employeeModel = new EmployeeModel();

            // Personal Details
            employeeModel.EmpID = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpImgURL = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpFirstName = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpMiddleName = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpLastName = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpGender = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpDOB = Convert.ToDateTime(dt.Rows[0][0]);
            employeeModel.EmpMaritalStatus = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpContact = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpAltContact = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpPersonalEmailID = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpReligion = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpNationality = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpCurrentAddress = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpPermanentAddress = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpState = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpCity = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpPincode = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpBAN = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpPAN = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpAdhaar = Convert.ToInt32(dt.Rows[0][0]);



            //Passport Details
            employeeModel.EmpPassportNumber = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpPassportIssuePlace = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpPassportIssueCountry = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpPassportIssueDate = Convert.ToDateTime(dt.Rows[0][0]);
            employeeModel.EmpPassportExpiryDate = Convert.ToDateTime(dt.Rows[0][0]);
            employeeModel.EmpECNRStatus = Convert.ToString(dt.Rows[0][0]);


            //Employees Professional Details
            employeeModel.EmpDOJ = Convert.ToDateTime(dt.Rows[0][0]);
            employeeModel.EmpDepartment = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpDesignation = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpBondStart = Convert.ToDateTime(dt.Rows[0][0]);
            employeeModel.EmpBondEnd = Convert.ToDateTime(dt.Rows[0][0]);
            employeeModel.EmpOrganizationEmailID = Convert.ToString(dt.Rows[0][0]);
            employeeModel.EmpClientID = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpShiftID = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpReportingManagerID = Convert.ToInt32(dt.Rows[0][0]);
            employeeModel.EmpIsConfirm = Convert.ToBoolean(dt.Rows[0][0]);
            employeeModel.EmpConfirmDate = Convert.ToDateTime(dt.Rows[0][0]);
            employeeModel.EmpBackgroundVerification = Convert.ToBoolean(dt.Rows[0][0]);
            employeeModel.EmpAddressVerification = Convert.ToBoolean(dt.Rows[0][0]);
            employeeModel.EmpEducationVerification = Convert.ToBoolean(dt.Rows[0][0]);
            employeeModel.EmpEmploymentVerification = Convert.ToBoolean(dt.Rows[0][0]);


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
