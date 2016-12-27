<%@ Page Title="Designation" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="Designation.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.Designation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">

    <script type="text/javascript">
        function DeleteConfirm() {
            var Ans = confirm("Do you want to Delete Selected Designation Record?");
            if (Ans) {
                return true;
            }
            else {
                return false;
            }
        }
        function AddConfirm() {
            var Ans = confirm("Do you want to Add Selected Designation Record?");
            if (Ans) {
                return true;
            }
            else {
                return false;
            }
        }

        function isSpace(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 32) {
                return false;
            }
            return true;
        }

        function AlfaWithSpace(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && charCode != 32 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)) {
                return false;
            }

            return true;
        }
    </script>

    <div class="row text-center">
    </div>
    <div class="row">
        <div class="col-lg-8 col-sm-8 text-center border-right">
            <div>
                <br />
                <center>
                <table class="table-condensed">
                    <tr>                        
                        <td>
                            <asp:TextBox ID="txtSearchDesignation" runat="server" CssClass="form-control input-md" placeholder="Enter Designation"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtSearchDesignation" runat="server" ID="RFSearchDes" ErrorMessage="Enter Designation"  ForeColor="Red" ValidationGroup="grpdes"></asp:RequiredFieldValidator>   
                             </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-md" OnClick="btnSearch_Click" ValidationGroup="grpdes"/>
                        </td>
                        <td>
                            <asp:Button ID="btnShowAllDesignation" runat="server" Text="Show All Designation" CssClass="btn btn-primary btn-md" OnClick="btnShowAllDesignation_Click"/>
                        </td>
                    </tr>
                </table>
                </center>
            </div>
            <hr class="small" />
            <asp:Panel ID="pnlListDesignation" runat="server" Visible="false">
                <h3 class="text-primary">List of Designations</h3>
                <asp:GridView ID="gvDesignation" runat="server" DataKeyNames="Desig_Id" AutoGenerateColumns="False"
                    OnRowCommand="gvDesignation_RowCommand" OnRowDataBound="gvDesignation_RowDataBound" CssClass="table table-hover table-bordered table-condensed"
                    HeaderStyle-CssClass="gvHeader" OnPageIndexChanging="gvDesignation_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Desig_Id" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="Department_Name" HeaderText="Department Name" />
                        <%--      <asp:BoundField DataField="RoleName" HeaderText="RoleName"/>
                        --%>
                        <asp:BoundField DataField="Designation_Name" HeaderText="Desigantion" />
                        <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                        <asp:TemplateField HeaderText="Remove" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Button ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("Desig_Id") %>' Text="X" CssClass="btn btn-danger btn-xs"></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblMessageDesignation" runat="server"></asp:Label>
                <br />
                <br />
            </asp:Panel>
            <hr class="small" />

            <%--Waiting for approval grid view--%>
            <asp:Panel ID="pnlPendingDesignation" runat="server" Visible="false">
                <h3 class="text-primary">Pending Requests</h3>
                <br />

                <asp:GridView ID="gvTempDesignation" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="RowDataBound" OnPageIndexChanging="gvTempDesignation_PageIndexChanging" DataKeyNames="TempDesigId" CssClass="table table-bordered table-condensed" HeaderStyle-CssClass="gvHeader">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkboxSelectDesignation" runat="server" onclick="Check_Click(this)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TempDesigId" HeaderText="Temp Designation ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="Desig_Id" HeaderText="Designation ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="Department_Name" HeaderText="Department" />
                        <asp:BoundField DataField="Designation_Name" HeaderText="Designation" />
                        <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                        <asp:BoundField DataField="Operation" HeaderText="Operation" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnapprove" runat="server" Text="Approve" CssClass="btn btn-success btn-md" Width="100px" OnClick="btnapprove_Click" OnClientClick="return AddConfirm();" />&nbsp;&nbsp;
                <asp:Button ID="btnreject" runat="server" Text="Reject" CssClass="btn btn-danger btn-md" Width="100px" OnClick="btnreject_Click" OnClientClick="return DeleteConfirm();" />
                <br />
                <asp:Label ID="lblMessageTempDesignation" runat="server"></asp:Label>
            </asp:Panel>
        </div>


        <div class="col-lg-4 col-sm-4 text-center">

            <hr class="small" />
            <asp:Panel ID="pnlAddDesignation" runat="server" Visible="false">
                <h3 class="text-primary">Add Designation</h3>
                <table class="table-condensed">
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlDepartmentName" runat="server" Width="250px" CssClass="form-control input-md"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqValQuestionType" ValidationGroup="grpdsignation" ForeColor="Red" InitialValue="-Select Department-" runat="server" ErrorMessage="Please Select Level" ControlToValidate="ddlDepartmentName">
                            </asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlRole" runat="server" Width="250px" CssClass="form-control input-md"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="grpdsignation" ForeColor="Red" runat="server" ControlToValidate="ddlRole" InitialValue="-Select Role-" ErrorMessage="Select Role"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDesignationname" runat="server" placeholder="Enter Designation" Width="250px" CssClass="form-control input-md" onkeypress="javascript:return AlfaWithSpace(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="grpdsignation" runat="server" ForeColor="Red" ControlToValidate="txtDesignationname" ErrorMessage="Enter Designation"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnAddDesignation" ValidationGroup="grpdsignation" runat="server" Text="Add" Width="250px" CssClass="btn btn-success btn-md" OnClick="btnAddDesignation_Click" 
                                OnClientClick="if (typeof(Page_ClientValidate) == 'function') { Page_ClientValidate(); if(Page_IsValid) { return confirm('Do You Want To Save this Designation?'); } };"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="text-center">

                            <asp:Label ID="lblMessage" runat="server"></asp:Label>

                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
