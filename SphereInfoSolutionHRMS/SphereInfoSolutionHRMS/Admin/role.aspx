<%@ Page Title="Role" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="role.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.role" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">
    <style>
        .hidden
        {
            display: none;
        }
    </style>
  
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
                            <asp:TextBox ID="txtSearchRole" runat="server" CssClass="form-control input-md" placeholder="Enter Role"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-md" OnClick="btnSearch_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnShowAllReoles" runat="server" Text="Show All Roles" CssClass="btn btn-primary btn-md" OnClick="btnShowAllReoles_Click" />
                        </td>
                    </tr>
                </table>
                </center>
            </div>
            <hr class="small" />

            <h3 class="text-primary">List of Roles</h3>

            <center>
            <asp:GridView ID="gvRole" runat="server" DataKeyNames="RoleId" AutoGenerateColumns="False"  OnRowCommand="gvRole_RowCommand" 
                OnRowDataBound="gvRole_RowDataBound" CssClass="table table-hover table-bordered table-condensed"
                HeaderStyle-CssClass="gvHeader">
                <Columns>
                    <asp:BoundField DataField="RoleId" HeaderText="RoleId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="RoleName" HeaderText="RoleName"/>
                    <asp:BoundField DataField="Role_Level" HeaderText="RoleLevel"/>
                    <asp:BoundField DataField="IsActive" HeaderText="IsActive" />
                    <asp:TemplateField HeaderText="Remove">
                        <ItemTemplate>
                            <center>
                            <asp:Button ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("RoleId") %>' Text="X" CssClass="btn btn-danger btn-xs"></asp:Button>
                                </center>
                        </ItemTemplate>
                    </asp:TemplateField>                       
                </Columns>
            </asp:GridView>
                <asp:Label ID="lblMessageRole" runat="server"></asp:Label>
                <br />
                <br />
                <hr class="small" />
                <%--Waiting for approval grid view--%>                 
                <h3 class="text-primary">Pending Requests</h3>
                <br />               

                   <asp:GridView ID="gvTempRole" runat="server" AutoGenerateColumns="False" OnRowDataBound = "RowDataBound"
                       DataKeyNames="TempRoleId" CssClass="table table-bordered table-condensed" HeaderStyle-CssClass="gvHeader">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">                        
                        <HeaderTemplate>                            
                                <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />                            
                        </HeaderTemplate>
                        <ItemTemplate>                            
                                 <asp:CheckBox ID="chkboxSelectRole" runat="server" onclick = "Check_Click(this)" />                            
                        </ItemTemplate>                        
                    </asp:TemplateField>
                    <asp:BoundField DataField="TempRoleId" HeaderText="TempRoleId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="RoleId" HeaderText="RoleId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="RoleName" HeaderText="RoleName"/>
                    <asp:BoundField DataField="Role_Level" HeaderText="RoleLevel"/>
                    <asp:BoundField DataField="IsActive" HeaderText="IsActive" />  
                   <asp:BoundField DataField="Operation" HeaderText="Operation" />  
                </Columns>
</asp:GridView>
                <asp:Button ID="btnapprove" runat="server" Text="Approve" OnClick="btnapprove_Click" CssClass="btn btn-success btn-md" Width="100px"/>&nbsp;&nbsp;
                <asp:Button ID="btnreject" runat="server" Text="Reject" OnClick="btnreject_Click" CssClass="btn btn-danger btn-md" Width="100px"/>
                <br />
                <asp:Label ID="lblMessageTempRole" runat="server"></asp:Label>
            </center>
        </div>


        <div class="col-lg-4 col-sm-4 text-center">
            <center>
                <hr class="small" />
                <h3 class="text-primary">Add Role</h3>
            <table class="table-condensed">
                <tr>
                    <td>
                        <asp:TextBox ID="txtrolename" runat="server" placeholder="Enter Role" Width="250px" CssClass="form-control input-md"></asp:TextBox>
                    </td>
                    <td>

                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:DropDownList ID="ddllevel" runat="server" Width="250px" CssClass="form-control input-md"></asp:DropDownList>
                    </td>
                    <td>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <asp:Button ID="btnAddRole" runat="server" Text="Add" Width="250px" OnClick="btnAddRole_Click" CssClass="btn btn-success btn-md"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <center>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </center>
                    </td>
                </tr>
            </table>
                </center>
        </div>
    </div>
</asp:Content>
