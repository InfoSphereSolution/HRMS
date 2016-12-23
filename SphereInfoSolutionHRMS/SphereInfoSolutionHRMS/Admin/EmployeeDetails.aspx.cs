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
using System.IO;
using System.Web.Security;
using System.Globalization;

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
                ((NestedMasterHome)this.Master).PageName = "Employee";
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
            ddlState.Items.Insert(0, new ListItem("Select State", "0"));

            ddlState.DataBind();

            ddlCityState.DataSource = dt;
            ddlCityState.DataValueField = "State_Id";
            ddlCityState.DataTextField = "StateName";
            ddlState.Items.Insert(0, new ListItem("Select State", "0"));

            ddlCityState.DataBind();


            dt = employee.FetchCity();
            ddlCity.DataSource = dt;
            ddlCity.DataValueField = "City_Id";
            ddlCity.DataTextField = "CityName";
            ddlCity.Items.Insert(0, new ListItem("Select City", "0"));
            ddlCity.DataBind();

            dt = employee.FetchDepartment();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "Dept_Id";
            ddlDepartment.DataTextField = "Department_Name";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0"));

            dt = employee.FetchDesignation();
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Desig_Id";
            ddlDesignation.DataTextField = "Designation_Name";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("Select Designation", "0"));

            dt = employee.FetchClient();
            ddlCLientName.DataSource = dt;
            ddlCLientName.DataValueField = "ClientId";
            ddlCLientName.DataTextField = "ClientName";
            ddlCLientName.DataBind();
            ddlCLientName.Items.Insert(0, new ListItem("Select Client", "0"));

            dt = employee.FetchReportingManager();
            ddlReportingManager.DataSource = dt;
            ddlReportingManager.DataValueField = "UserId";
            ddlReportingManager.DataTextField = "Reporting Manager";
            ddlReportingManager.DataBind();
            ddlReportingManager.Items.Insert(0, new ListItem("Select ReportManager", "0"));


            dt = employee.FetchShift();
            ddlShift.DataSource = dt;
            ddlShift.DataValueField = "ShiftId";
            ddlShift.DataTextField = "ShiftsName";
            ddlShift.DataBind();
            ddlShift.Items.Insert(0, new ListItem("Select Shift", "0"));

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
                 * -3 : Updated Unsuccessful
                 */
                int IsSuccessful = Convert.ToInt32(dt.Rows[0][0]);


                if (IsSuccessful == 1)
                {
                    /*New user Username & Password*/
                    String Username = Convert.ToString(dt.Rows[0][1]);
                    String Password = Convert.ToString(dt.Rows[0][2]);
                    string encryptedpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
                    int i = employee.encryptPassword(Username, encryptedpassword);
                    if (i == 1)
                    {
                        lblMessageEmployeeList.Text = "Username: " + Username + " & Password: " + Password;
                        lblMessageEmployeeList.Visible = true;
                    }
                    else
                    {
                        lblMessageEmployeeList.Text = "Error";
                        lblMessageEmployeeList.Visible = true;
                    }

                }
                else if (IsSuccessful == 2)
                {
                    lblMessageEmployeeList.Text = "Error";
                    lblMessageEmployeeList.Visible = true;
                }
                else if (IsSuccessful == 3)
                {
                    lblMessageEmployeeList.Text = "Updated Successfully";
                    lblMessageEmployeeList.Visible = true;
                }
                else if (IsSuccessful == -3)
                {
                    lblMessageEmployeeList.Text = "Error";
                    lblMessageEmployeeList.Visible = true;
                }
                else { }

            }
            else
            {
                /*Not Inserted or Updated*/
            }

            bindEmployeeList();
            clearEmployeeDetails();
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

            employeeModel.EmpImgURL = UploadPhoto();
            employeeModel.EmpFirstName = txtEmployeeFirstName.Text;
            employeeModel.EmpMiddleName = txtEmployeeMiddleName.Text;
            employeeModel.EmpLastName = txtEmployeeLastName.Text;
            employeeModel.EmpGender = Convert.ToString(ddlGender.SelectedItem);
            if (txtDatefBirth.Text!="")
            {
            employeeModel.EmpDOB = Convert.ToDateTime(txtDatefBirth.Text);
            }
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
            if (txtPassportIssueDate.Text != "")
            {
                employeeModel.EmpPassportIssueDate = String.IsNullOrEmpty(txtPassportIssueDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtPassportIssueDate.Text);
            }
            if (txtPassportExpiryDate.Text != "")
            {
                employeeModel.EmpPassportExpiryDate = String.IsNullOrEmpty(txtPassportExpiryDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtPassportExpiryDate.Text);
            }
            employeeModel.EmpECNRStatus = ddlECNRStatus.SelectedValue;
        }

        //Get employee professional details from the page
        protected void getEmployeeProfessional()
        {
            //Employees Professional Details   
            if (txtDateOfJoining.Text != "")
            {
                employeeModel.EmpDOJ = Convert.ToDateTime(txtDateOfJoining.Text);
            }
            employeeModel.EmpDepartment = Convert.ToInt32(ddlDepartment.SelectedValue);
            employeeModel.EmpDesignation = Convert.ToInt32(ddlDesignation.SelectedValue);
            if (txtBondStart.Text != "")
            {
                employeeModel.EmpBondStart = Convert.ToDateTime(txtBondStart.Text);
            }
            if (txtBondEnd.Text != "")
            {
                employeeModel.EmpBondEnd = Convert.ToDateTime(txtBondEnd.Text);
            }
            employeeModel.EmpOrganizationEmailID = txtOrganizationEmailID.Text;
            employeeModel.EmpClientID = Convert.ToInt32(ddlCLientName.SelectedValue);
            employeeModel.EmpShiftID = Convert.ToInt32(ddlShift.SelectedValue);
            employeeModel.EmpReportingManagerID = Convert.ToInt32(ddlReportingManager.SelectedValue);
            employeeModel.EmpIsConfirm = cbIsConfirm.Checked;
            employeeModel.EmpConfirmDate = Convert.ToDateTime(txtConfirmationDate.Text);
            employeeModel.EmpBackgroundVerification = cbBackgroundVerification.Checked;
            employeeModel.EmpAddressVerification = cbAddressVerification.Checked;
            employeeModel.EmpEducationVerification = cbEducationVerification.Checked;
            employeeModel.EmpEmploymentVerification = cbEmploymentVerification.Checked;
        }

        //Bind selected employee details to the page
        protected void bindEmployeeDetails(Int32 UserID,string Action)
        {
            EmployeeModel employeeModel = employee.FetchEmployeeDetails(UserID);
            try
            {
                if (Action == "View")
                {
                    //Disable controlls
                }
                else
                {

                }
                bindAllDDL();
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
            txtEmployeeID.Text = "SIS" + Convert.ToString(employeeModel.UserID);
            imgEmployeePicture.ImageUrl = Convert.ToString(employeeModel.EmpImgURL);
            txtEmployeeFirstName.Text = employeeModel.EmpFirstName;
            txtEmployeeMiddleName.Text = employeeModel.EmpMiddleName;
            txtEmployeeLastName.Text = employeeModel.EmpLastName;
            ddlGender.SelectedItem.Text = employeeModel.EmpGender;
            if (employeeModel.EmpDOB != null)
            {
                txtDatefBirth.Text = Convert.ToDateTime(employeeModel.EmpDOB).ToString("yyyy/M/dd");
            }
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
            if (employeeModel.EmpPassportIssueDate != null)
            {
                txtPassportIssueDate.Text = Convert.ToDateTime(employeeModel.EmpPassportIssueDate).ToString("yyyy/M/dd");
            }
            if (employeeModel.EmpPassportExpiryDate != null)
            {
                txtPassportExpiryDate.Text = Convert.ToDateTime(employeeModel.EmpPassportExpiryDate).ToString("yyyy/M/dd");
            }
            ddlECNRStatus.SelectedValue = employeeModel.EmpECNRStatus;
        }

        //Bind selected Professional employee details to the page
        protected void bindEmpoyeeProfessional(EmployeeModel employeeModel)
        {
            //Employees Professional Details
            txtDateOfJoining.Text = Convert.ToDateTime(employeeModel.EmpDOJ).ToString("yyyy/M/dd");
            ddlDepartment.SelectedValue = Convert.ToString(employeeModel.EmpDepartment);
            ddlDesignation.SelectedValue = Convert.ToString(employeeModel.EmpDesignation);
            if (employeeModel.EmpBondStart != null)
            {
                txtBondStart.Text = Convert.ToDateTime(employeeModel.EmpBondStart).ToString("yyyy/M/dd");
            }
            if (employeeModel.EmpBondEnd != null)
            {
                txtBondEnd.Text = Convert.ToDateTime(employeeModel.EmpBondEnd).ToString("yyyy/M/dd");
            }
            txtOrganizationEmailID.Text = employeeModel.EmpOrganizationEmailID;
            ddlCLientName.SelectedValue = Convert.ToString(employeeModel.EmpClientID);
            ddlShift.SelectedValue = Convert.ToString(employeeModel.EmpShiftID);
            ddlReportingManager.SelectedValue = Convert.ToString(employeeModel.EmpReportingManagerID);
            cbIsConfirm.Checked = employeeModel.EmpIsConfirm;
            if (employeeModel.EmpConfirmDate != null)
            {
                txtConfirmationDate.Text = Convert.ToDateTime(employeeModel.EmpConfirmDate).ToString("yyyy/M/dd");
            }
            //cbBackgroundVerification.Checked = employeeModel.EmpBackgroundVerification;
            cbBackgroundVerification.Checked = string.IsNullOrEmpty((employeeModel.EmpBackgroundVerification).ToString());

            cbAddressVerification.Checked = string.IsNullOrEmpty((employeeModel.EmpAddressVerification).ToString());
            cbEducationVerification.Checked = string.IsNullOrEmpty((employeeModel.EmpEducationVerification).ToString());
            cbEmploymentVerification.Checked = string.IsNullOrEmpty((employeeModel.EmpEmploymentVerification).ToString());
        }

        //Remove employee i.e make the employee inactive 
        protected void removeEmployee(Int32 UserID)
        {
            Int32 CurrentUserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            Int32 i = employee.RemoveEmployee(UserID, CurrentUserID);
            if (i > 0)
            {

                /*Employee Removed*/
            }
            else
            {
                /*Error*/
            }
            bindEmployeeList();
        }

        //Check which action button on the gridview is pressed
        protected void gvEmployeeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int UserID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Display")
            {
                bindEmployeeDetails(UserID,"View");
            }
            else if (e.CommandName == "Change")
            {
                bindEmployeeDetails(UserID,"Edit");
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
            if (Page.IsValid)
            {

            }

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
                Button btnView = (Button)e.Row.FindControl("btnDisplayEmployeeDetails");
                Button btnEdit = (Button)e.Row.FindControl("btnEditEmployee");
                btnRemove.Attributes["onclick"] = "if(!confirm('Do you want to Remove Selected Employee?')){ return false; };";

                if (IsActive == 0)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5D8DC"); ;
                    btnRemove.Enabled = false;
                    btnEdit.Enabled = false;
                    btnView.Enabled = false;

                    btnRemove.CssClass = "btn btn-danger btn-xs disabled";

                }
                else if (IsActive == 1)
                {
                    //e.Row.BackColor = System.Drawing.Color.Honeydew;
                    btnRemove.Visible = true;
                    btnRemove.Enabled = true;
                    btnEdit.Enabled = true;
                    btnView.Enabled = true;

                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Blue;
                }
            }
        }

        //link button to clear employee details and to bind new employee id
        protected void lbAddNewEmployee_Click(object sender, EventArgs e)
        {
            bindEmployeeID();
            clearEmployeeDetails();
        }

        protected void clearEmployeeDetails()
        {
            clearPersonalDetails();
            clearPassportDetails();
            clearProfessionalDetails();
            bindAllDDL();
        }

        protected void clearPersonalDetails()
        {
            //Employees Personal Details                        
            imgEmployeePicture.ImageUrl = "~/ProfilePhoto/defaultPhoto.png";
            txtEmployeeFirstName.Text = "";
            txtEmployeeMiddleName.Text = "";
            txtEmployeeLastName.Text = "";
            txtDatefBirth.Text = "";
            txtMobile.Text = "";
            txtAltMobile.Text = "";
            txtPersonalEmailID.Text = "";
            txtReligion.Text = "";
            txtNationality.Text = "";
            txtCurrentAddress.Text = "";
            txtPermanentAddress.Text = "";
            txtPincode.Text = "";
            txtBankACC.Text = "";
            txtPAN.Text = "";
            txtAdhaarNumber.Text = "";
        }

        protected void clearPassportDetails()
        {
            //Passport Details
            txtPassportNumber.Text = "";
            txtPassportIssuePlace.Text = "";
            txtPassportIssueCountry.Text = "";
            txtPassportIssueDate.Text = "";
            txtPassportExpiryDate.Text = "";
        }

        protected void clearProfessionalDetails()
        {
            //Employees Professional Details
            txtDateOfJoining.Text = "";
            txtBondStart.Text = "";
            txtBondEnd.Text = "";
            txtOrganizationEmailID.Text = "";
            cbIsConfirm.Checked = false;
            txtConfirmationDate.Text = "";
            cbBackgroundVerification.Checked = false;
            cbAddressVerification.Checked = false;
            cbEducationVerification.Checked = false;
            cbEmploymentVerification.Checked = false;
        }

        protected void cbIsConfirm_CheckedChanged(object sender, EventArgs e)
        {
            txtConfirmationDate.Enabled = cbIsConfirm.Checked;
        }

        protected void chSameAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chSameAddress.Checked)
            {
                txtPermanentAddress.Text = txtCurrentAddress.Text;
            }
            else
            {
                txtPermanentAddress.Text = "";
            }
        }

        protected String UploadPhoto()
        {
            String PhotoPath = "";
            if (fuEmployeePicture.HasFile)
            {
                string fileName = Path.GetFileName(fuEmployeePicture.PostedFile.FileName);
                fuEmployeePicture.PostedFile.SaveAs(Server.MapPath("~/ProfilePhoto/") + fileName);
                PhotoPath = "~/ProfilePhoto/" + fileName;
                return PhotoPath;
            }
            else
            {
                PhotoPath = "~/ProfilePhoto/defaultPhoto.png";
                return PhotoPath;
            }
        }

    }
}
