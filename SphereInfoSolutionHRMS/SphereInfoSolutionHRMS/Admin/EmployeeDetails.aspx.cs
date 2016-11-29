using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using BAL;
using Models;
using System.Data;

namespace SphereInfoSolutionHRMS.Admin
{
    public partial class EmployeeDetails : System.Web.UI.Page
    {
        Employee employee = new Employee();
        EmployeeModel employeeModel = new EmployeeModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindEmployeeList();
                bindAllDDL();
                bindEmployeeID();
            }
        }

        //bind all the dropdownlist
        protected void bindAllDDL()
        {
            DataTable dt;
            dt = employee.FetchState();

            ddlState.DataSource = dt;
            ddlState.DataValueField = "State_Id";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();

            ddlCityState.DataSource = dt;
            ddlCityState.DataValueField = "State_Id";
            ddlCityState.DataTextField = "StateName";
            ddlCityState.DataBind();


            dt = employee.FetchCity();
            ddlCity.DataSource = dt;
            ddlCity.DataValueField = "City_Id";
            ddlCity.DataTextField = "CityName";
            ddlCity.DataBind();

            dt = employee.FetchDepartment();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "Dept_Id";
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataBind();

            dt = employee.FetchDesignation();
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Desig_Id";
            ddlDesignation.DataTextField = "Designation_Name";
            ddlDesignation.DataBind();

            dt = employee.FetchClient();
            ddlCLientName.DataSource = dt;
            ddlCLientName.DataValueField = "ClientId";
            ddlCLientName.DataTextField = "ClientName";
            ddlCLientName.DataBind();

            dt = employee.FetchReportingManager();
            ddlReportingManager.DataSource = dt;
            ddlReportingManager.DataValueField = "UserId";
            ddlReportingManager.DataTextField = "Reporting Manager";
            ddlReportingManager.DataBind();


            dt = employee.FetchShift();
            ddlShift.DataSource = dt;
            ddlShift.DataValueField = "ShiftId";
            ddlShift.DataTextField = "ShiftsName";
            ddlShift.DataBind();

        }

        //Bind employee List to the gridview
        protected void bindEmployeeList()
        {
            DataTable dt = employee.FetchEmployeeList();
            if (dt.Rows.Count > 0)
            {
                gvEmployeeList.DataSource = dt;
                gvEmployeeList.DataBind();
            }
            else
            {
                lblMessageEmployeeList.Visible = true;
                lblMessageEmployeeList.ForeColor = System.Drawing.Color.Red;
                lblMessageEmployeeList.Text = "No records found..";
            }

        }

        //Add new or Update employee details
        protected void btnSaveEmployee_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.UpdateEmployeeDetails(getEmployeeDetails());
            if (dt.Rows.Count > 0)
            {
                /*
                 * Issuccessful
                 * 1 : Inserted successful
                 * 2 : Inserted Unsuccessful
                 * 3 : Updated successful
                 * 4 : Updated Unsuccessful
                 */
                int IsSuccessful = Convert.ToInt32(dt.Rows[0][0]);


                if (IsSuccessful == 1)
                {
                    /*New user Username & Password*/
                    String Username = Convert.ToString(dt.Rows[0][1]);
                    String Password = Convert.ToString(dt.Rows[0][2]);
                }
                else if (IsSuccessful == 2)
                { }
                else if (IsSuccessful == 3)
                { }
                else if (IsSuccessful == 4)
                { }
                else { }

            }
            else
            {
                /*Not Inserted or Updated*/
            }

            bindEmployeeList();
        }

        //Get employee details from the page
        protected EmployeeModel getEmployeeDetails()
        {
            getEmpoyeePersonal();
            getEmployeePassport();
            getEmployeeProfessional();

            return employeeModel;
        }

        //Get employee personal details from the page
        protected void getEmpoyeePersonal()
        {
            //Employees Personal Details            
            employeeModel.UserID = Convert.ToInt32((txtEmployeeID.Text).Substring(3));
            employeeModel.Current_UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            /*File uploade code*/
            
            employeeModel.EmpImgURL = "";
            employeeModel.EmpFirstName = txtEmployeeFirstName.Text;
            employeeModel.EmpMiddleName = txtEmployeeMiddleName.Text;
            employeeModel.EmpLastName = txtEmployeeLastName.Text;
            employeeModel.EmpGender = Convert.ToString(ddlGender.SelectedItem);            
            employeeModel.EmpDOB = String.IsNullOrEmpty(txtDatefBirth.Text) ? (DateTime?)null : Convert.ToDateTime(txtDatefBirth.Text);
            employeeModel.EmpMaritalStatus = Convert.ToString(ddlMaritalStatus.SelectedItem);
            employeeModel.EmpContact = Convert.ToString(txtMobile.Text);
            employeeModel.EmpAltContact = Convert.ToString(txtAltMobile.Text);
            employeeModel.EmpPersonalEmailID = txtPersonalEmailID.Text;
            employeeModel.EmpReligion = txtReligion.Text;
            employeeModel.EmpNationality = txtNationality.Text;
            employeeModel.EmpCurrentAddress = txtCurrentAddress.Text;
            employeeModel.EmpPermanentAddress = txtPermanentAddress.Text;
            employeeModel.EmpState = Convert.ToInt32(ddlState.SelectedValue);
            employeeModel.EmpCity = Convert.ToInt32(ddlCity.SelectedValue);            
            employeeModel.EmpPincode = String.IsNullOrEmpty(txtPincode.Text) ? (Int32?)null : Convert.ToInt32(txtPincode.Text);
            employeeModel.EmpBAN = txtBankACC.Text;
            employeeModel.EmpPAN = txtPAN.Text;
            employeeModel.EmpAdhaar = txtAdhaarNumber.Text;
        }

        //Get employee passport details from the page
        protected void getEmployeePassport()
        {
            //Passport Details
            employeeModel.EmpPassportNumber = Convert.ToString(txtPassportNumber.Text);
            employeeModel.EmpPassportIssuePlace = txtPassportIssuePlace.Text;
            employeeModel.EmpPassportIssueCountry = txtPassportIssueCountry.Text;            
            employeeModel.EmpPassportIssueDate = String.IsNullOrEmpty(txtPassportIssueDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtPassportIssueDate.Text);            
            employeeModel.EmpPassportExpiryDate = String.IsNullOrEmpty(txtPassportExpiryDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtPassportExpiryDate.Text);
            employeeModel.EmpECNRStatus = ddlECNRStatus.SelectedValue;
        }

        //Get employee professional details from the page
        protected void getEmployeeProfessional()
        {
            //Employees Professional Details            
            employeeModel.EmpDOJ = String.IsNullOrEmpty(txtDateOfJoining.Text) ? (DateTime?)null : Convert.ToDateTime(txtDateOfJoining.Text);
            employeeModel.EmpDepartment = Convert.ToInt32(ddlDepartment.SelectedValue);
            employeeModel.EmpDesignation = Convert.ToInt32(ddlDesignation.SelectedValue);            
            employeeModel.EmpBondStart = String.IsNullOrEmpty(txtBondStart.Text) ? (DateTime?)null : Convert.ToDateTime(txtBondStart.Text);            
            employeeModel.EmpBondEnd = String.IsNullOrEmpty(txtBondEnd.Text) ? (DateTime?)null : Convert.ToDateTime(txtBondEnd.Text);
            employeeModel.EmpOrganizationEmailID = txtOrganizationEmailID.Text;
            employeeModel.EmpClientID = Convert.ToInt32(ddlCLientName.SelectedValue);
            employeeModel.EmpShiftID = Convert.ToInt32(ddlShift.SelectedValue);
            employeeModel.EmpReportingManagerID = Convert.ToInt32(ddlReportingManager.SelectedValue);
            employeeModel.EmpIsConfirm = cbIsConfirm.Checked;
            employeeModel.EmpConfirmDate = String.IsNullOrEmpty(txtConfirmationDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtConfirmationDate.Text);
            employeeModel.EmpBackgroundVerification = cbBackgroundVerification.Checked;
            employeeModel.EmpAddressVerification = cbAddressVerification.Checked;
            employeeModel.EmpEducationVerification = cbEducationVerification.Checked;
            employeeModel.EmpEmploymentVerification = cbEmploymentVerification.Checked;
        }

        //Check if adding new employee or updating existing employee
        protected int getSaveOperation()
        {
            //Operation:
            //1: Add New
            //2: Update Existing 
            return 1;
        }

        //Bind selected employee details to the page
        protected void bindEmployeeDetails(Int32 UserID)
        {
            EmployeeModel employeeModel = employee.FetchEmployeeDetails(UserID);
            try
            {
                bindEmpoyeePersonal(employeeModel);
                bindEmpoyeePassport(employeeModel);
                bindEmpoyeeProfessional(employeeModel);
            }
            catch (Exception)
            {

                throw;
            }

        }

        //Bind selected Personal employee details to the page
        protected void bindEmpoyeePersonal(EmployeeModel employeeModel)
        {
            //Employees Personal Details            
            txtEmployeeID.Text = Convert.ToString(employeeModel.UserID);
            imgEmployeePicture.ImageUrl = Convert.ToString(employeeModel.EmpImgURL);
            txtEmployeeFirstName.Text = employeeModel.EmpFirstName;
            txtEmployeeMiddleName.Text = employeeModel.EmpMiddleName;
            txtEmployeeLastName.Text = employeeModel.EmpLastName;
            ddlGender.SelectedItem.Text = employeeModel.EmpGender;
            txtDatefBirth.Text = Convert.ToString(employeeModel.EmpDOB);
            ddlMaritalStatus.SelectedItem.Text = employeeModel.EmpMaritalStatus;
            txtMobile.Text = employeeModel.EmpContact;
            txtAltMobile.Text = employeeModel.EmpAltContact;
            txtPersonalEmailID.Text = employeeModel.EmpPersonalEmailID;
            txtReligion.Text = employeeModel.EmpReligion;
            txtNationality.Text = employeeModel.EmpNationality;
            txtCurrentAddress.Text = employeeModel.EmpCurrentAddress;
            txtPermanentAddress.Text = employeeModel.EmpPermanentAddress;
            ddlState.SelectedValue = Convert.ToString(employeeModel.EmpState);
            ddlCity.SelectedValue = Convert.ToString(employeeModel.EmpCity);
            txtPincode.Text = Convert.ToString(employeeModel.EmpPincode);
            txtBankACC.Text = Convert.ToString(employeeModel.EmpBAN);
            txtPAN.Text = Convert.ToString(employeeModel.EmpPAN);
            txtAdhaarNumber.Text = Convert.ToString(employeeModel.EmpAdhaar);
        }

        //Bind selected Passport employee details to the page
        protected void bindEmpoyeePassport(EmployeeModel employeeModel)
        {
            //Passport Details
            txtPassportNumber.Text = Convert.ToString(employeeModel.EmpPassportNumber);
            txtPassportIssuePlace.Text = employeeModel.EmpPassportIssuePlace;
            txtPassportIssueCountry.Text = employeeModel.EmpPassportIssueCountry;
            txtPassportIssueDate.Text = Convert.ToString(employeeModel.EmpPassportIssueDate);
            txtPassportExpiryDate.Text = Convert.ToString(employeeModel.EmpPassportExpiryDate);
            ddlECNRStatus.SelectedValue = employeeModel.EmpECNRStatus;
        }

        //Bind selected Professional employee details to the page
        protected void bindEmpoyeeProfessional(EmployeeModel employeeModel)
        {
            //Employees Professional Details
            txtDateOfJoining.Text = Convert.ToString(employeeModel.EmpDOJ);
            ddlDepartment.SelectedValue = Convert.ToString(employeeModel.EmpDepartment);
            ddlDesignation.SelectedValue = Convert.ToString(employeeModel.EmpDesignation);
            txtBondStart.Text = Convert.ToString(employeeModel.EmpBondStart);
            txtBondEnd.Text = Convert.ToString(employeeModel.EmpBondEnd);
            txtOrganizationEmailID.Text = employeeModel.EmpOrganizationEmailID;
            ddlCLientName.SelectedValue = Convert.ToString(employeeModel.EmpClientID);
            ddlShift.SelectedValue = Convert.ToString(employeeModel.EmpShiftID);
            ddlReportingManager.SelectedValue = Convert.ToString(employeeModel.EmpReportingManagerID);
            cbIsConfirm.Checked = employeeModel.EmpIsConfirm;
            txtConfirmationDate.Text = Convert.ToString(employeeModel.EmpConfirmDate);
            cbBackgroundVerification.Checked = employeeModel.EmpBackgroundVerification;
            cbAddressVerification.Checked = employeeModel.EmpAddressVerification;
            cbEducationVerification.Checked = employeeModel.EmpEducationVerification;
            cbEmploymentVerification.Checked = employeeModel.EmpEmploymentVerification;
        }

        //Remove employee i.e make the employee inactive 
        protected void removeEmployee(Int32 EmpID)
        {
            employee.RemoveEmployee(EmpID);
        }

        //Check which action button on the gridview is pressed
        protected void gvEmployeeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int UserID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Display")
            {
                bindEmployeeDetails(UserID);                                
            }
            else if (e.CommandName == "Change")
            {
                bindEmployeeDetails(UserID);
            }
            else if (e.CommandName == "Remove")
            {
                removeEmployee(UserID);
            }
            else { }
        }

        //Search for employee in employeelist gridview
        protected void btnSearchEmployee_Click(object sender, EventArgs e)
        {

        }

        //Show all employees in employeelist gridview
        protected void btnShowAllEmployee_Click(object sender, EventArgs e)
        {
            //bindEmployeeList();
        }

        //Get the shift list of the selected client
        protected void ddlCLientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = employee.FetchShift(Convert.ToInt32(ddlCLientName.SelectedValue));
            ddlShift.DataSource = dt;
            ddlShift.DataValueField = "ShiftId";
            ddlShift.DataTextField = "ShiftsName";
            ddlShift.DataBind();
        }

        //Get the new userID
        protected void bindEmployeeID()
        {
            Int32 NewUserID = employee.FetchNewUserID();
            txtEmployeeID.Text = "SIS" + Convert.ToString(NewUserID);

        }

        protected void gvEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int IsActive = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IsActive"));
                Button btnRemove = (Button)e.Row.FindControl("btnRemoveEmployee");

                if (IsActive == 0)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5D8DC"); ;
                    btnRemove.Enabled = false;
                    btnRemove.CssClass = "btn btn-danger btn-xs disabled";

                }
                else if (IsActive == 1)
                {
                    //e.Row.BackColor = System.Drawing.Color.Honeydew;
                    btnRemove.Visible = true;
                    btnRemove.Enabled = true;

                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Blue;
                }
            }
        }


    }
}
