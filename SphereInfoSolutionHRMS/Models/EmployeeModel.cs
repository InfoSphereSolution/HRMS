using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EmployeeModel
    {
        public int Current_UserID { get; set; }
        

        //Employees Personal Details
        public int UserID { get; set; }
        public String EmpImgURL { get; set; }
        public String EmpFirstName { get; set; }
        public String EmpMiddleName { get; set; }
        public String EmpLastName { get; set; }
        public String EmpGender { get; set; }
        public DateTime? EmpDOB { get; set; }
        public String EmpMaritalStatus { get; set; }
        public String EmpContact { get; set; }
        public String EmpAltContact { get; set; }
        public String EmpPersonalEmailID { get; set; }
        public String EmpReligion { get; set; }
        public String EmpNationality { get; set; }
        public String EmpCurrentAddress { get; set; }
        public String EmpPermanentAddress { get; set; }
        public Int32 EmpState { get; set; }
        public Int32 EmpCity { get; set; }
        public Int32? EmpPincode { get; set; }
        public String EmpBAN { get; set; }
        public String EmpPAN { get; set; }
        public String EmpAdhaar { get; set; }
        public String EmpPassportNumber { get; set; }
        public String EmpPassportIssuePlace { get; set; }
        public String EmpPassportIssueCountry { get; set; }
        public DateTime? EmpPassportIssueDate { get; set; }        
        public DateTime? EmpPassportExpiryDate { get; set; }
        public String EmpECNRStatus { get; set; }

        //Employees Professional Details
        public DateTime? EmpDOJ { get; set; }
        public Int32 EmpDepartment { get; set; }
        public Int32 EmpDesignation { get; set; }
        public DateTime? EmpBondStart { get; set; }
        public DateTime? EmpBondEnd { get; set; }
        public String EmpOrganizationEmailID { get; set; }
        public Int32 EmpClientID { get; set; }
        public Int32 EmpShiftID { get; set; }
        public Int32 EmpReportingManagerID { get; set; }
        public Boolean EmpIsConfirm { get; set; }
        public DateTime? EmpConfirmDate { get; set; }
        public Boolean? EmpBackgroundVerification { get; set; }
        public Boolean? EmpAddressVerification { get; set; }
        public Boolean? EmpEducationVerification { get; set; }
        public Boolean? EmpEmploymentVerification { get; set; }
    }
}
