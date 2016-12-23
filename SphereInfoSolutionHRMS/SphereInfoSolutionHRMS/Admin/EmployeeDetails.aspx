<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.EmployeeDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">
    <script type="text/javascript">

        function isNumber(evt) {

            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }

        function isAlfa(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)) {
                return false;
            }
            return true;
        }

        function isSpace(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 32) {
                return false;
            }
            return true;
        }




    </script>

      <script type="text/javascript">
          function DeleteConfirm() {
              var Ans = confirm("Do you want to Remove Selected Employee Details?");
              if (Ans) {
                  return true;
              }
              else {
                  return false;
              }
          }
          function AddConfirm() {
              var Ans = confirm("Do you want to Add Selected Employee Details?");
              if (Ans) {
                  return true;
              }
              else {
                  return false;
              }
          }
          
</Script>


    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <cc1:ToolkitScriptManager runat="server"></cc1:ToolkitScriptManager>
    <div class="panel panel-default" style="padding: 10px; margin: 10px">
        <div id="Tabs" role="tabpanel">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li class="active"><a href="#viewAllEmployees" aria-controls="viewAllEmployees" role="tab" data-toggle="tab">Employees</a></li>
                <li><a href="#PersonalDetails" aria-controls="PersonalDetails" role="tab" data-toggle="tab">Personal</a></li>
                <li><a href="#ProfessionalDetails" aria-controls="ProfessionalDetails" role="tab" data-toggle="tab">Professional</a></li>
                <li><a href="#EducationalDetails" aria-controls="EducationalDetails" role="tab" data-toggle="tab">Education</a></li>
                <li><a href="#WorkExperienceDetails" aria-controls="WorkExperienceDetails" role="tab" data-toggle="tab">Experience</a></li>
                <li><a href="#HealthDetails" aria-controls="HealthDetails" role="tab" data-toggle="tab">Health</a></li>
                <li><a href="#FamilyDetails" aria-controls="FamilyDetails" role="tab" data-toggle="tab">Family</a></li>
                <li><a href="#References" aria-controls="References" role="tab" data-toggle="tab">References</a></li>
                <li><a href="#Documents" aria-controls="Documents" role="tab" data-toggle="tab">Documents</a></li>
            </ul>
            <!-- Tab panes -->
            <div class="tab-content" style="padding-top: 20px">

                <%--View All Employee--%>
                <div role="tabpanel" class="tab-pane active" id="viewAllEmployees">
                    <div id="listOfEmployees">
                        <table class="table-condensed">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSearchEmployeeName" runat="server" CssClass="form-control input-md" placeholder="Enter Employee Name Here.."></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFSearchEmp" ControlToValidate="txtSearchEmployeeName" runat="server" ErrorMessage="Enter Employee Name" ValidationGroup="grpsearchemp" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearchEmployee" ValidationGroup="grpsearchemp" runat="server" Text="Search" CssClass="btn btn-primary btn-md" OnClick="btnSearchEmployee_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnShowAllEmployee" runat="server" Text="Show All Employee" CssClass="btn btn-primary btn-md" OnClick="btnShowAllEmployee_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:LinkButton ID="lbAddNewEmployee" CssClass="pull-right" OnClick="lbAddNewEmployee_Click" runat="server" CausesValidation="false">Add New Employee</asp:LinkButton>
                        <h3 class="text-primary text-center">List of Employees</h3>

                        <asp:GridView ID="gvEmployeeList" runat="server" DataKeyNames="UserId" AutoGenerateColumns="False"
                            CssClass="table table-hover table-bordered table-condensed"
                            HeaderStyle-CssClass="gvHeader" OnRowCommand="gvEmployeeList_RowCommand" OnRowDataBound="gvEmployeeList_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="UserId" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ReportingManager_Id" HeaderText="ReportingManager_Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="Employee_Name" HeaderText="Employee_Name" />
                                <asp:BoundField DataField="Designation_Name" HeaderText="Designation" />
                                <asp:BoundField DataField="Reporting_Manager" HeaderText="Reporting Manager" />
                                <asp:BoundField DataField="ClientName" HeaderText="Client" />
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                                <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="center-block text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDisplayEmployeeDetails" runat="server" CausesValidation="false" CommandName="Display" CommandArgument='<%# Eval("UserId") %>' Text="View" CssClass="btn btn-primary btn-xs"></asp:Button>
                                        <asp:Button ID="btnEditEmployee" runat="server" CommandName="Change" CausesValidation="false" CommandArgument='<%# Eval("UserId") %>' Text="Edit" CssClass="btn btn-primary btn-xs"></asp:Button>
                                        <asp:Button ID="btnRemoveEmployee" runat="server" CommandName="Remove" CausesValidation="false" CommandArgument='<%# Eval("UserId") %>' Text="X" CssClass="btn btn-danger btn-xs"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblMessageEmployeeList" runat="server"></asp:Label>
                    </div>
                </div>

                <%--Employee Personal Details--%>
                <div role="tabpanel" class="tab-pane" id="PersonalDetails">
                    <div id="EmployeePersonalDetailsForm">
                        <div class="col-lg-12 well">
                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="row">
                                        <div class="col-lg-8 col-sm-8 col-xs-12 form-group">
                                        </div>
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Image ID="imgEmployeePicture" CssClass="img-responsive img-rounded center-block" Style="padding: 5px; max-width: 200px" AlternateText="Profile Image" ImageAlign="Middle" ImageUrl="../ProfilePhoto/defaultPhoto.png" runat="server" />
                                            <asp:FileUpload ID="fuEmployeePicture" runat="server" CssClass="form-control center-block" />
                                           <%-- <asp:RequiredFieldValidator ID="RfPicture" ControlToValidate="fuEmployeePicture" runat="server" ForeColor="Red" ErrorMessage="Upload Photo"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblEmployeeID" runat="server" Text="ID" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtEmployeeID" CssClass="form-control" runat="server" placeholder="Employee ID" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblEmployeeFirstName" runat="server" Text="First Name" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtEmployeeFirstName" CssClass="form-control" runat="server" onkeypress="javascript:return isAlfa(event)" placeholder="Enter First Name Here.."></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFFirstName" runat="server" ErrorMessage="Enter First Name" ControlToValidate="txtEmployeeFirstName" ForeColor="Red" ValidationGroup="grpAddEmp"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblEmployeeMiddleName" runat="server" Text="Middle Name" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtEmployeeMiddleName" CssClass="form-control" runat="server" placeholder="Enter Middle Name Here.."></asp:TextBox>
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="grpAddEmp" runat="server" ErrorMessage="Enter Middle Name" ControlToValidate="txtEmployeeMiddleName" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                        </div>
                                        <div class="col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblEmployeeLastName" runat="server" Text="Last Name" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtEmployeeLastName" CssClass="form-control" runat="server" placeholder="Enter Last Name Here.."></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFLastName" ValidationGroup="grpAddEmp" runat="server" ErrorMessage="Enter Last Name" ControlToValidate="txtEmployeeLastName" ForeColor="Red"></asp:RequiredFieldValidator>


                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblGender" runat="server" Text="Gender" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Select Gender" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Male" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="grpAddEmp" ControlToValidate="ddlGender" InitialValue="-1" ErrorMessage="Select Gender" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblDatefBirth" runat="server" Text="Date of Birth" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtDatefBirth" CssClass="form-control" runat="server" placeholder="Enter Date Of Birth Here.."></asp:TextBox>
                                            <cc1:CalendarExtender ID="ceDateofBirth" runat="server" TargetControlID="txtDatefBirth" Format="yyyy/M/dd"></cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RFDOb" runat="server" ValidationGroup="grpAddEmp"
                                                ErrorMessage="Enter Date of birth" ControlToValidate="txtDatefBirth" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblMaritalStatus" runat="server" Text="Marital Status" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Select Status" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Married" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Unmarried" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RFMarriedStatus" InitialValue="-1" runat="server" ValidationGroup="grpAddEmp" ControlToValidate="ddlMaritalStatus" ErrorMessage="Select Married Status" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblMobile" runat="server" Text="Contact" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" placeholder="Enter Contact Number Here.." MaxLength="10" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFContactNo" ValidationGroup="grpAddEmp" ForeColor="Red" runat="server" ErrorMessage="Enter Contact Number" ControlToValidate="txtMobile"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REContNo" ValidationGroup="grpAddEmp" runat="server" ErrorMessage="Contact No should be of 10 digits"
                                                ControlToValidate="txtMobile" ValidationExpression="[0-9]{10}" ForeColor="Red"></asp:RegularExpressionValidator>

                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblAltMobile" runat="server" Text="Alt-Contact" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtAltMobile" CssClass="form-control" runat="server" placeholder="Enter Alternate Contact Number Here.." MaxLength="10" onkeypress="javascript:return isNumber(event)"></asp:TextBox>

                                            <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Contact Number" ControlToValidate="txtAltMobile" ValidationGroup="grpAddEmp"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Contact No should be of 10 digits" ValidationGroup="grpAddEmp"
                                                ControlToValidate="txtAltMobile" ValidationExpression="[0-9]{10}" ForeColor="Red"></asp:RegularExpressionValidator>

                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPersonalEmailID" runat="server" Text="Personal Email-ID" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPersonalEmailID" CssClass="form-control" runat="server" placeholder="Enter Personal Email-ID Here.."></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFEnailId" runat="server" ErrorMessage="Enter Email id"
                                                ControlToValidate="txtPersonalEmailID" ForeColor="Red" ValidationGroup="grpAddEmp"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REemailId" runat="server" ErrorMessage="Email id is not valid" ValidationGroup="grpAddEmp"
                                                ControlToValidate="txtPersonalEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblReligion" runat="server" Text="Religion" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtReligion" CssClass="form-control" runat="server" placeholder="Enter Religion Here.."></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFtxtReligion" ForeColor="Red" runat="server" ControlToValidate="txtReligion" ValidationGroup="grpAddEmp" ErrorMessage="Enter Religion"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblNationality" runat="server" Text="Nationality" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtNationality" CssClass="form-control" runat="server" placeholder="Enter Nationality Here.."></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFNationality" runat="server" ForeColor="Red" ControlToValidate="txtNationality" ValidationGroup="grpAddEmp" ErrorMessage="Enter Nationality"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblCurrentAddress" runat="server" Text="Current Address" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtCurrentAddress" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Enter Current Address Here.." runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFCurrAddr" runat="server" ErrorMessage="Enter Current Address"
                                            ControlToValidate="txtCurrentAddress" ForeColor="Red" ValidationGroup="grpAddEmp"></asp:RequiredFieldValidator>

                                    </div>
                                    <asp:UpdatePanel ID="upSameAddress" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <asp:Label ID="lblPermanentAddress" runat="server" Text="Permanent Address" Font-Bold="true"></asp:Label>
                                                (&nbsp;<asp:CheckBox ID="chSameAddress" CssClass="checkbox-inline" Text="Same as above" runat="server" OnCheckedChanged="chSameAddress_CheckedChanged" AutoPostBack="true" />&nbsp;)
                                        <asp:TextBox ID="txtPermanentAddress" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Enter Permanent Address Here.." runat="server"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="ErrMsg" ErrorMessage="Enter Permanent Address"
                                                    ControlToValidate="txtPermanentAddress" ForeColor="Red"></asp:RequiredFieldValidator>--%>


                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="row">

                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblState" runat="server" Text="State" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlState" ErrorMessage="Select State" ForeColor="Red" InitialValue="Select State"></asp:RequiredFieldValidator>
                                            <div style="padding: 2px;">
                                                <asp:Button ID="btnAddStatePanel" runat="server" CssClass="btn btn-primary btn-md form-control pull-right" Text="+" Width="15%" />
                                            </div>

                                            <cc1:ModalPopupExtender ID="mpeAddState" runat="server"
                                                PopupControlID="pnlAddState"
                                                TargetControlID="btnAddStatePanel"
                                                CancelControlID="btnCancelState"
                                                BackgroundCssClass="modal-backdrop">
                                            </cc1:ModalPopupExtender>

                                            <asp:Panel ID="pnlAddState" runat="server" CssClass="modal-dialog">

                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h3 class="modal-title">
                                                            <asp:Label ID="lblHeaderAddState" runat="server" Text="Enter Details" Font-Bold="true"></asp:Label>
                                                        </h3>
                                                    </div>
                                                    <div class="modal-body" style="padding: 5px;">
                                                        <asp:TextBox ID="txtStateName" CssClass="form-control" placeholder="Enter State Name Here.." runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="Grpaddemp" ID="RFCity" runat="server"
                                                            ErrorMessage="Enter State" ControlToValidate="txtStateName"></asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="modal-footer">
                                                        <asp:Button ID="btnCancelState" runat="server" class="btn btn-md btn-primary" Text="Cancel" />
                                                        <asp:Button ID="btnAddNewState" runat="server" class="btn btn-md btn-primary" Text="Add State" />
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </div>

                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblCity" runat="server" Text="City" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Grpaddemp"
                                                ErrorMessage="Select City" ControlToValidate="ddlCity"></asp:RequiredFieldValidator>

                                            <div style="padding: 2px;">
                                                <asp:Button ID="btnAddCityPanel" runat="server" CssClass="btn btn-primary btn-md form-control pull-right" Text="+" Width="15%" />
                                            </div>

                                            <cc1:ModalPopupExtender ID="mpeAddCity" runat="server"
                                                PopupControlID="pnlAddCity"
                                                TargetControlID="btnAddCityPanel"
                                                CancelControlID="btnCancelCity"
                                                BackgroundCssClass="modal-backdrop">
                                            </cc1:ModalPopupExtender>

                                            <asp:Panel ID="pnlAddCity" runat="server" CssClass="modal-dialog">

                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h3 class="modal-title">
                                                            <asp:Label ID="lblHeaderAddCity" runat="server" Text="Enter Details" Font-Bold="true"></asp:Label>
                                                        </h3>
                                                    </div>
                                                    <div class="modal-body" style="padding: 5px;">

                                                        <asp:DropDownList ID="ddlCityState" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <asp:TextBox ID="txtCityName" CssClass="form-control" placeholder="Enter City Name Here.." runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <asp:Button ID="btnCancelCity" runat="server" class="btn btn-md btn-primary" Text="Cancel" />
                                                        <asp:Button ID="btnAddNewCity" runat="server" class="btn btn-md btn-primary" Text="Add City" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>


                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPincode" runat="server" Text="Pincode" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPincode" CssClass="form-control" runat="server" placeholder="Enter Pincode Here.." MaxLength="6" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFPincode" CssClass="ErrMsg" runat="server"
                                                ErrorMessage="Enter pin code" ControlToValidate="txtPincode" ValidationGroup="grpAddEmp" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegExpPinCode" ValidationGroup="grpAddEmp" runat="server" ErrorMessage="Pincode should be of 6 digits"
                                                ControlToValidate="txtPincode" ValidationExpression="[0-9]{6}" ForeColor="Red"></asp:RegularExpressionValidator>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblBankACC" runat="server" Text="Bank Account Number" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtBankACC" CssClass="form-control" runat="server" placeholder="Enter Bank Account Number Here.."></asp:TextBox>

                                            <%--<asp:RequiredFieldValidator ControlToValidate="txtBankACC" ValidationGroup="grpAddEmp" runat="server" ErrorMessage="Enter Account Numbar" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPAN" runat="server" Text="PAN" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPAN" CssClass="form-control" runat="server"  placeholder="Enter PAN Here.."  MaxLength="10" onkeypress="javascript:return isSpace(event)"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegExpPanNo" runat="server" ValidationGroup="grpAddEmp" ForeColor="Red" ErrorMessage="Invalid Pan card number" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}" ControlToValidate="txtPAN"></asp:RegularExpressionValidator>
                                        </div>

                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblAdhaarNumber" runat="server" Text="Adhaar Number" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtAdhaarNumber" CssClass="form-control" runat="server" placeholder="Enter Adhaar Number Here.." MaxLength="12"
                                                onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="grpAddEmp" ErrorMessage="Adhar card number should be of 12 digits"
                                                ControlToValidate="txtAdhaarNumber" ValidationExpression="[0-9]{12}"></asp:RegularExpressionValidator>


                                        </div>
                                    </div>

                                    <hr class="small" />

                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPassportNumber" runat="server" Text="Passport Number" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPassportNumber" CssClass="form-control" runat="server" placeholder="Enter Passport Number Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPassportIssuePlace" runat="server" Text="Place of Issue" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPassportIssuePlace" CssClass="form-control" runat="server" placeholder="Enter Place Of Issue Number Here.."></asp:TextBox>

                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPassportIssueCountry" runat="server" Text="Country of Issue" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPassportIssueCountry" CssClass="form-control" runat="server" placeholder="Enter Country Of Issue Here.."></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPassportIssueDate" runat="server" Text="Date of Issue" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPassportIssueDate" CssClass="form-control" runat="server" placeholder="Enter Date of Issue Here.."></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPassportIssueDate" Format="yyyy/M/dd"></ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPassportExpiryDate" runat="server" Text="Date of Expiry" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPassportExpiryDate" CssClass="form-control" runat="server" placeholder="Enter Place Of Issue Number Here.."></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPassportExpiryDate" Format="yyyy/M/dd"></ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblECNRStatus" runat="server" Text="ECNR Status" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlECNRStatus" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Select Status" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="ECR" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="ECNR" Value="0"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <asp:Button ID="btnToProfessional" href="#ProfessionalDetails"  aria-controls="ProfessionalDetails" role="tab" data-toggle="tab" runat="server" class="btn btn-lg btn-primary pull-right" Text="Next" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--Employee Professional Details--%>
                <div role="tabpanel" class="tab-pane" id="ProfessionalDetails">
                    <div id="EmployeeProfessionalDetailsForm">
                        <div class="col-lg-12 well">
                            <div class="row">
                                <div class="col-sm-12 form-group">

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblDateOfJoining" runat="server" Text="Date of Joining" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtDateOfJoining" CssClass="form-control" runat="server" placeholder="Enter Date of Joining Here.."></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDateofJoining" runat="server" TargetControlID="txtDateOfJoining" Format="yyyy/M/dd"></ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="grpAddEmp" ControlToValidate="txtDateOfJoining" ID="dtjoin" ForeColor="Red" ErrorMessage="Enter Date of Joining"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblDepartment" runat="server" Text="Department" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RFDepartment" ValidationGroup="grpAddEmp" ControlToValidate="ddlDepartment" ErrorMessage="Select Department" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblDesignation" runat="server" Text="Designation" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RFDesignation" runat="server" ValidationGroup="grpAddEmp" ControlToValidate="ddlDesignation" ForeColor="Red" InitialValue="0" ErrorMessage="Select Designation"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblBondStart" runat="server" Text="Bond Start Date" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtBondStart" CssClass="form-control" runat="server" placeholder="Enter Start Date Here.."></asp:TextBox>
                                            <cc1:CalendarExtender ID="ceBondStart" runat="server" TargetControlID="txtBondStart" Format="yyyy/M/dd"></cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfbondstart" ValidationGroup="grpAddEmp" runat="server" ControlToValidate="txtBondStart" ForeColor="Red" ErrorMessage="Enter Bond Start Date"></asp:RequiredFieldValidator>


                                        </div>
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblBondEnd" runat="server" Text="Bond End Date" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtBondEnd" CssClass="form-control" runat="server" placeholder="Enter End Date Here.."></asp:TextBox>
                                            <cc1:CalendarExtender ID="ceBondEnd" runat="server" TargetControlID="txtBondEnd" Format="yyyy/M/dd"></cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RFBondEnd" ValidationGroup="grpAddEmp" runat="server" ControlToValidate="txtBondEnd" ForeColor="Red" ErrorMessage="Enter Bond End Date"></asp:RequiredFieldValidator>


                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblOrganizationEmailID" runat="server" Text="Organization Email-ID" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtOrganizationEmailID" CssClass="form-control" runat="server" placeholder="Enter Organization Email-ID  Here.."></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFEmailId" ValidationGroup="grpAddEmp" runat="server" ControlToValidate="txtOrganizationEmailID" ForeColor="Red" ErrorMessage="Enter Organization Email Id"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                    <asp:UpdatePanel ID="upShiftClient" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                                    <asp:Label ID="lblClientName" runat="server" Text="Client Name" Font-Bold="true"></asp:Label>
                                                    <asp:DropDownList ID="ddlCLientName" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCLientName_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="grpAddEmp" runat="server" ControlToValidate="ddlCLientName" InitialValue="0" ForeColor="Red" ErrorMessage="Select Client Name"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                                    <asp:Label ID="lblShift" runat="server" Text="Shift(Timing)" Font-Bold="true"></asp:Label>
                                                    <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFShift" runat="server" ValidationGroup="grpAddEmp" ControlToValidate="ddlShift" InitialValue="0" ErrorMessage="Select Shift" ForeColor="Red"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                                    <asp:Label ID="lblReportingManager" runat="server" Text="Reporting Manager" Font-Bold="true"></asp:Label>
                                                    <asp:DropDownList ID="ddlReportingManager" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFReportingMgr" runat="server" ValidationGroup="grpAddEmp" ControlToValidate="ddlReportingManager" InitialValue="0" ErrorMessage="Select Reporting Manager" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="upConfirm" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                                    <asp:CheckBox ID="cbIsConfirm" CssClass="checkbox-inline" Text="Confirmed?" runat="server" AutoPostBack="True" OnCheckedChanged="cbIsConfirm_CheckedChanged" />
                                                    <asp:TextBox ID="txtConfirmationDate" CssClass="form-control" runat="server" Enabled="false" placeholder="Enter Confirmation Date Here.."></asp:TextBox>
                                                    <cc1:CalendarExtender ID="ceConfirmationDate" TargetControlID="txtConfirmationDate" runat="server"></cc1:CalendarExtender>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <hr class="small" />

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-6 col-xs-12 form-group">
                                            <asp:Label ID="lblUAN" runat="server" Text="UAN" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtUAN" CssClass="form-control" runat="server" placeholder="Enter UAN Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12 form-group text-center">
                                            <asp:Label ID="lblOR" runat="server" Text="OR" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12 form-group">
                                            <asp:Label ID="lblPFID" runat="server" Text="PF Member ID" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPFID" CssClass="form-control" runat="server" placeholder="Enter PF-ID Here.."></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-6 col-xs-12 form-group">
                                            <asp:Label ID="lblPreviousDateExit" runat="server" Text="Date Of Exit For Previous" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPreviousDateExit" CssClass="form-control" runat="server" placeholder="Enter Exit Date Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12 form-group">
                                            <asp:Label ID="lblSchemaCertificate" runat="server" Text="Schema Certificate Number" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtSchemaCertificate" CssClass="form-control" runat="server" placeholder="Enter Schema Certificate Number Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12 form-group">
                                            <asp:Label ID="lblPPO" runat="server" Text="Pension Payment Order Number" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPPO" CssClass="form-control" runat="server" placeholder="Enter PPO Number Here.."></asp:TextBox>

                                        </div>
                                    </div>

                                    <hr class="small" />

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-6 col-xs-12 form-group">
                                            <asp:CheckBox ID="cbBackgroundVerification" CssClass="checkbox-inline" Text="Background Verification?" runat="server" />
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12 form-group">
                                            <asp:CheckBox ID="cbAddressVerification" CssClass="checkbox-inline" Text="Address Verification?" runat="server" />
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12 form-group">
                                            <asp:CheckBox ID="cbEducationVerification" CssClass="checkbox-inline" Text="Education Verification?" runat="server" />
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12 form-group">
                                            <asp:CheckBox ID="cbEmploymentVerification" CssClass="checkbox-inline" Text="Employment Verification?" runat="server" />
                                        </div>
                                    </div>

                                    <asp:Button ID="btnBackPersonalDetails" href="#PersonalDetails" aria-controls="PersonalDetails" role="tab" data-toggle="tab" runat="server" class="btn btn-lg btn-primary pull-left" Text="Back" CausesValidation="false"/>
                                    <asp:Button ID="btnSaveEmployee" runat="server" class="btn btn-lg btn-primary pull-right" Text="Save" OnClick="btnSaveEmployee_Click" 
                                        OnClientClick="if (typeof(Page_ClientValidate) == 'function') { Page_ClientValidate(); if(Page_IsValid) { return confirm('Do You Want To Save Employee?'); } };"/>
                                    <%--<asp:Button ID="btnToHoliday" href="#holidayList" aria-controls="holidayList" role="tab" data-toggle="tab" runat="server" class="btn btn-lg btn-primary pull-right" Text="Next" />--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--Educational Details--%>
                <div role="tabpanel" class="tab-pane" id="EducationalDetails">
                    Educational Details
                </div>

                <%--Work Experience Details--%>
                <div role="tabpanel" class="tab-pane" id="WorkExperienceDetails">
                    Work Experience
                </div>

                <%--Health Details--%>
                <div role="tabpanel" class="tab-pane" id="HealthDetails">
                    Health Details
                </div>

                <%--Family Details--%>
                <div role="tabpanel" class="tab-pane" id="FamilyDetails">
                    Familty Details
                </div>

                <%--References Details--%>
                <div role="tabpanel" class="tab-pane" id="References">
                    References
                </div>

                <%--Documents Details--%>
                <div role="tabpanel" class="tab-pane" id="Documents">
                    Documents
                </div>

            </div>
            <!-- End Tab panes -->
        </div>
        <!-- End Nav tabs -->
    </div>



</asp:Content>
