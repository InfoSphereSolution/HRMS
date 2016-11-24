
<%@ Page Title="Designation" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="Designation.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.Designation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">
  
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
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-md" OnClick="btnSearch_Click"/>
                        </td>
                        <td>
                            <asp:Button ID="btnShowAllDesignation" runat="server" Text="Show All Designation" CssClass="btn btn-primary btn-md" OnClick="btnShowAllDesignation_Click"/>
                        </td>
                    </tr>
                </table>
                </center>
            </div>
            <hr class="small" />

            <center>
                <h3 class="text-primary">List of Designations</h3>
            <asp:GridView ID="gvDesignation" runat="server" DataKeyNames="Desig_Id" AutoGenerateColumns="False" 
               OnRowCommand="gvDesignation_RowCommand"  OnRowDataBound="gvDesignation_RowDataBound"  CssClass="table table-hover table-bordered table-condensed"
                HeaderStyle-CssClass="gvHeader">
                <Columns>
                      <asp:BoundField DataField="Desig_Id" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                     <asp:BoundField DataField="Department_Name" HeaderText="Department Name"/>
               <%--      <asp:BoundField DataField="RoleName" HeaderText="RoleName"/>
               --%>     <asp:BoundField DataField="Designation_Name" HeaderText="Desigantion"/>
                    <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                    <asp:TemplateField HeaderText="Remove">
                        <ItemTemplate>
                            <center>
                        <asp:Button ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("Desig_Id") %>' Text="X" CssClass="btn btn-danger btn-xs"></asp:Button>
                                </center>
                        </ItemTemplate>
                    </asp:TemplateField>                       
                </Columns>
            </asp:GridView>
                <asp:Label ID="lblMessageDesignation" runat="server"></asp:Label>
                <br />
                <br />
                <hr class="small" />
                <%--Waiting for approval grid view--%>                 
                <h3 class="text-primary">Pending Requests</h3>
                <br />               

                   <asp:GridView ID="gvTempDesignation" runat="server" AutoGenerateColumns="False" 
                    OnRowDataBound = "RowDataBound"   DataKeyNames="TempDesigId" CssClass="table table-bordered table-condensed" HeaderStyle-CssClass="gvHeader">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">                        
                        <HeaderTemplate>                            
                                <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />                            
                        </HeaderTemplate>
                        <ItemTemplate>                            
                                 <asp:CheckBox ID="chkboxSelectDesignation" runat="server" onclick = "Check_Click(this)" />                            
                        </ItemTemplate>                        
                    </asp:TemplateField>
                    <asp:BoundField DataField="TempDesigId" HeaderText="Temp Designation ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="Desig_Id" HeaderText="Designation ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="Department_Name" HeaderText="Department"/>
                    <asp:BoundField DataField="Designation_Name" HeaderText="Designation" />
                    <asp:BoundField DataField="IsActive" HeaderText="Is Active" />  
                   <asp:BoundField DataField="Operation" HeaderText="Operation" />  
                </Columns>
</asp:GridView>
                <asp:Button ID="btnapprove" runat="server" Text="Approve" CssClass="btn btn-success btn-md" Width="100px" OnClick="btnapprove_Click"/>&nbsp;&nbsp;
                <asp:Button ID="btnreject" runat="server" Text="Reject"  CssClass="btn btn-danger btn-md" Width="100px" OnClick="btnreject_Click"/>
                <br />
                <asp:Label ID="lblMessageTempDesignation" runat="server"></asp:Label>
            </center>
        </div>


        <div class="col-lg-4 col-sm-4 text-center">
            <center>
                <hr class="small" />
                <h3 class="text-primary">Add Designation</h3>
            <table class="table-condensed">
                <tr>
                    <td>                        
                        <asp:DropDownList ID="ddlDepartmentName" runat="server" Width="250px" CssClass="form-control input-md"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:DropDownList ID="ddlRole" runat="server" Width="250px" CssClass="form-control input-md"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:TextBox ID="txtDesignationname" runat="server" placeholder="Enter Designation" Width="250px" CssClass="form-control input-md"></asp:TextBox>
                    </td>
                </tr>                
                <tr>
                    <td colspan="2">
                    <asp:Button ID="btnAddDesignation" runat="server" Text="Add" Width="250px" CssClass="btn btn-success btn-md" OnClick="btnAddDesignation_Click"></asp:Button>
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