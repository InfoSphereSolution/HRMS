<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.EmployeeDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">

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
                                </td>
                                <td>
                                    <asp:Button ID="btnSearchEmployee" runat="server" Text="Search" CssClass="btn btn-primary btn-md" />
                                </td>
                                <td>
                                    <asp:Button ID="btnShowAllEmployee" runat="server" Text="Show All Employee" CssClass="btn btn-primary btn-md" />
                                </td>
                            </tr>
                        </table>
                        <h3 class="text-primary text-center">List of Employees</h3>

                        <asp:GridView ID="gvEmployeeList" runat="server" DataKeyNames="EmployeeId" AutoGenerateColumns="False"
                            CssClass="table table-hover table-bordered table-condensed"
                            HeaderStyle-CssClass="gvHeader">
                            <Columns>
                                <asp:BoundField DataField="EmployeeId" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                <asp:BoundField DataField="ClientName" HeaderText="Client" />
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                                <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="center-block text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewEmployee" runat="server" CommandName="View" CommandArgument='<%# Eval("EmployeeId") %>' Text="View" CssClass="btn btn-primary btn-xs"></asp:Button>
                                        <asp:Button ID="btnEditEmployee" runat="server" CommandName="Edit" CommandArgument='<%# Eval("EmployeeId") %>' Text="Edit" CssClass="btn btn-primary btn-xs"></asp:Button>
                                        <asp:Button ID="btnRemoveEmployee" runat="server" CommandName="Remove" CommandArgument='<%# Eval("EmployeeId") %>' Text="X" CssClass="btn btn-danger btn-xs"></asp:Button>
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
                                        <div class="col-lg-6 col-sm-6 col-xs-12 form-group">
                                            <asp:Label ID="lblEmployeeID" runat="server" Text="ID" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtEmployeeID" CssClass="form-control" runat="server" placeholder="Employee ID" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-6 col-sm-6 col-xs-12 form-group">
                                            <asp:Image ID="imgEmployeePicture" CssClass="img-responsive img-circle center-block" Style="padding: 5px" AlternateText="Profile Image" ImageAlign="Middle" ImageUrl="~/Images/spherelogo.jpg" runat="server" />
                                            <asp:FileUpload ID="fuEmployeePicture" runat="server" CssClass="form-control center-block" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblEmployeeFirstName" runat="server" Text="First Name" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtEmployeeFirstName" CssClass="form-control" runat="server" placeholder="Enter First Name Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblEmployeeMiddleName" runat="server" Text="Middle Name" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtEmployeeMiddleName" CssClass="form-control" runat="server" placeholder="Enter Middle Name Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblEmployeeLastName" runat="server" Text="Last Name" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtEmployeeLastName" CssClass="form-control" runat="server" placeholder="Enter Last Name Here.."></asp:TextBox>
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
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblDatefBirth" runat="server" Text="Date of Birth" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtDatefBirth" CssClass="form-control" runat="server" placeholder="Enter Date Of Birth Here.."></asp:TextBox>
                                            <cc1:CalendarExtender ID="ceDateofBirth" runat="server" TargetControlID="txtDatefBirth" Format="yyyy/M/dd"></cc1:CalendarExtender>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblMaritalStatus" runat="server" Text="Marital Status" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Select Status" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Married" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Unmarried" Value="1"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblMobile" runat="server" Text="Contact" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" placeholder="Enter Contact Number Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblAltMobile" runat="server" Text="Alt-Contact" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtAltMobile" CssClass="form-control" runat="server" placeholder="Enter Alternate Contact Number Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPersonalEmailID" runat="server" Text="Personal Email-ID" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPersonalEmailID" CssClass="form-control" runat="server" placeholder="Enter Personal Email-ID Here.."></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblReligion" runat="server" Text="Religion" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtReligion" CssClass="form-control" runat="server" placeholder="Enter Religion Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblNationality" runat="server" Text="Nationality" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtNationality" CssClass="form-control" runat="server" placeholder="Enter Nationality Here.."></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblCurrentAddress" runat="server" Text="Current Address" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtCurrentAddress" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Enter Current Address Here.." runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblPermanentAddress" runat="server" Text="Permanent Address" Font-Bold="true"></asp:Label>
                                        (&nbsp;<asp:CheckBox ID="chSameAddress" CssClass="checkbox-inline" Text="Same as above" runat="server" />&nbsp;)
                                        <asp:TextBox ID="txtPermanentAddress" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Enter Permanent Address Here.." runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row">

                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblState" runat="server" Text="State" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"></asp:DropDownList>

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
                                            <asp:TextBox ID="txtPincode" CssClass="form-control" runat="server" placeholder="Enter Pincode Here.."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblBankACC" runat="server" Text="Bank Account Number" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtBankACC" CssClass="form-control" runat="server" placeholder="Enter Bank Account Number Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPAN" runat="server" Text="PAN" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPAN" CssClass="form-control" runat="server" placeholder="Enter PAN Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblAdhaarNumber" runat="server" Text="Adhaar Number" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtAdhaarNumber" CssClass="form-control" runat="server" placeholder="Enter Adhaar Number Here.."></asp:TextBox>
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
                                            <asp:TextBox ID="txtPassportIssuePlace" CssClass="form-control" Enabled="false" runat="server" placeholder="Enter Place Of Issue Number Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPassportIssueCountry" runat="server" Text="Country of Issue" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPassportIssueCountry" CssClass="form-control" Enabled="false" runat="server" placeholder="Enter Country Of Issue Here.."></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPassportIssueDate" runat="server" Text="Date of Issue" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPassportIssueDate" CssClass="form-control" Enabled="false" runat="server" placeholder="Enter Date of Issue Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPassportExpiryDate" runat="server" Text="Date of Expiry" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPassportExpiryDate" CssClass="form-control" Enabled="false" runat="server" placeholder="Enter Place Of Issue Number Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblECNRStatus" runat="server" Text="ECNR Status" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlECNRStatus" CssClass="form-control" Enabled="false" runat="server">
                                                <asp:ListItem Text="ECR" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="ECNR" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <asp:Button ID="btnToProfessional" href="#ProfessionalDetails" aria-controls="ProfessionalDetails" role="tab" data-toggle="tab" runat="server" class="btn btn-lg btn-primary pull-right" Text="Next" />
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
                                            <cc1:CalendarExtender ID="ceDateofJoining" runat="server" TargetControlID="txtDateOfJoining" Format="yyyy/M/dd"></cc1:CalendarExtender>
                                        </div>
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblDepartment" runat="server" Text="Department" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblDesignation" runat="server" Text="Designation" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblBondStart" runat="server" Text="Bond Start Date" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtBondStart" CssClass="form-control" Enabled="false" runat="server" placeholder="Enter Start Date Here.."></asp:TextBox>
                                            <cc1:CalendarExtender ID="ceBondStart" runat="server" TargetControlID="txtBondStart" Format="yyyy/M/dd"></cc1:CalendarExtender>
                                        </div>
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblBondEnd" runat="server" Text="Bond End Date" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtBondEnd" CssClass="form-control" Enabled="false" runat="server" placeholder="Enter End Date Here.."></asp:TextBox>
                                            <cc1:CalendarExtender ID="ceBondEnd" runat="server" TargetControlID="txtBondEnd" Format="yyyy/M/dd"></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblOrganizationEmailID" runat="server" Text="Organization Email-ID" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtOrganizationEmailID" CssClass="form-control" runat="server" placeholder="Enter Organization Email-ID  Here.."></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblClientName" runat="server" Text="Client Name" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlCLientName" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblShift" runat="server" Text="Shift(Timing)" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Label ID="lblReportingManager" runat="server" Text="Reporting Manager" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlReportingManager" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:CheckBox ID="cbIsConfirm" CssClass="checkbox-inline" Text="Confirmed?" runat="server" />
                                            <asp:TextBox ID="txtConfirmationDate" CssClass="form-control" runat="server" Enabled="false" placeholder="Enter Confirmation Date Here.."></asp:TextBox>
                                        </div>
                                    </div>

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

                                    <asp:Button ID="btnBackPersonalDetails" href="#PersonalDetails" aria-controls="PersonalDetails" role="tab" data-toggle="tab" runat="server" class="btn btn-lg btn-primary pull-left" Text="Back" />
                                    <asp:Button ID="btnSaveEmployee" runat="server" class="btn btn-lg btn-primary pull-right" Text="Save" />
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
