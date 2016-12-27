<%@ Page Title="Role" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="role.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.role" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">



    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <script type="text/javascript">
        function DeleteConfirm() {
            var Ans = confirm("Do you want to Delete Selected Role Record?");
            if (Ans) {
                return true;
            }
            else {
                return false;
            }
        }
        function AddConfirm() {
            var Ans = confirm("Do you want to Add Selected Role Record?");
            if (Ans) {
                return true;
            }
            else {
                return false;
            }
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

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <asp:UpdatePanel ID="RolePanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-8 col-sm-8 text-center border-right">
                    <asp:Panel ID="pnListRole" runat="server" Visible="false">
                        <div>
                            <br />
                            <table class="table-condensed">
                                <tr>

                                    <td>

                                        <asp:TextBox ID="txtSearchRole" runat="server" CssClass="form-control input-md" placeholder="Enter Role"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RFSearch" ValidationGroup="grpsearch" ForeColor="Red" runat="server" ControlToValidate="txtSearchRole" ErrorMessage="Enter Role"></asp:RequiredFieldValidator>

                                    </td>
                                    <td>

                                        <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="grpsearch" CssClass="btn btn-primary btn-md" OnClick="btnSearch_Click" OnClientClick="return myfunction();" />
                                    </td>




                                    <td>
                                        <asp:Button ID="btnShowAllRoles" runat="server" Text="Show All Roles" CssClass="btn btn-primary btn-md" OnClick="btnShowAllRoles_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <hr class="small" />
                        <h3 class="text-primary">List of Roles</h3>
                        <asp:GridView ID="gvRole" runat="server" DataKeyNames="RoleId" AutoGenerateColumns="False" OnRowCommand="gvRole_RowCommand"
                            OnRowDataBound="gvRole_RowDataBound" CssClass="table table-hover table-bordered table-condensed"
                            HeaderStyle-CssClass="gvHeader" OnPageIndexChanging="gvRole_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="RoleId" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="RoleName" HeaderText="Name" />
                                <asp:BoundField DataField="Role_Level" HeaderText="Level" />
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                                <asp:TemplateField HeaderText="Remove">
                                    <ItemTemplate>
                                        <asp:Button ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("RoleId") %>' Text="X" CssClass="btn btn-danger btn-xs" ValidationGroup="grpremove"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblMessageRole" runat="server"></asp:Label>
                        <br />
                        <br />
                        <hr class="small" />
                    </asp:Panel>

                    <asp:Panel ID="pnPendingRole" runat="server" Visible="false">

                        <%--Waiting for approval grid view--%>
                        <h3 class="text-primary">Pending Requests</h3>
                        <br />

                        <asp:GridView ID="gvTempRole" OnPageIndexChanging="gvTempRole_PageIndexChanging" runat="server" AutoGenerateColumns="False" OnRowDataBound="RowDataBound"
                            DataKeyNames="TempRoleId" CssClass="table table-bordered table-condensed" HeaderStyle-CssClass="gvHeader">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkboxSelectRole" runat="server" onclick="Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TempRoleId" HeaderText="Temp Role Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="RoleId" HeaderText="Role Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="RoleName" HeaderText="Name" />
                                <asp:BoundField DataField="Role_Level" HeaderText="Level" />
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                                <asp:BoundField DataField="Operation" HeaderText="Operation" />
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="btnapprove" runat="server" Text="Approve" OnClick="btnapprove_Click" CssClass="btn btn-success btn-md" Width="100px" ValidationGroup="grpapprove" OnClientClick="return AddConfirm();" />&nbsp;&nbsp;
                <asp:Button ID="btnreject" runat="server" Text="Reject" OnClick="btnreject_Click" CssClass="btn btn-danger btn-md" Width="100px" ValidationGroup="grpreject" OnClientClick="return DeleteConfirm();" />
                        <br />
                        <asp:Label ID="lblMessageTempRole" runat="server"></asp:Label>
                    </asp:Panel>
                </div>

                <asp:Panel ID="pnAddRole" runat="server" Visible="false">
                    <%-- Add New Role --%>
                    <div class="col-lg-4 col-sm-4 text-center">
                        <hr class="small" />
                        <h3 class="text-primary">Add Role</h3>


                        <table class="table-condensed">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtrolename" runat="server" placeholder="Enter Role" Width="250px" CssClass="form-control input-md" onkeypress="javascript:return AlfaWithSpace(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator1" ValidationGroup="grpRole" runat="server" ControlToValidate="txtrolename" ErrorMessage="Please Enter Role"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddllevel" runat="server" Width="250px" CssClass="form-control input-md" AutoPostBack="false"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="reqValQuestionType" ValidationGroup="grpRole" ForeColor="Red" InitialValue="Select Level" runat="server" ErrorMessage="Please Select Level" ControlToValidate="ddllevel">
                                    </asp:RequiredFieldValidator>
                                    <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="*" runat="server" ControlToValidate="ddllevel" InitialValue="-1" ErrorMessage="Please Select Role Level"></asp:RequiredFieldValidator>
                                    --%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnAddRole" runat="server" ValidationGroup="grpRole" Text="Add" Width="250px" OnClick="btnAddRole_Click" CssClass="btn btn-success btn-md"
                                        OnClientClick="if (typeof(Page_ClientValidate) == 'function') { Page_ClientValidate(); if(Page_IsValid) { return confirm('Do You Want To Save This Role?'); } };"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>

                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
