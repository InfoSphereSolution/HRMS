<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="Designation.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.Designation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">
    <div class="row text-center">
        <h1>Designation</h1>
    </div>
    <div class="row">
        <div class="col-lg-8 col-sm-8 text-center">
            <h2>List of Designations</h2>
        </div>
        <div class="col-lg-4 col-sm-4 text-center">
            <h2>Add Designation</h2>
            <center>
             <table>
                  <tr>
                    <td>
                         <asp:DropDownList ID="ddlDepartment" runat="server" Width="250px">
                        <asp:ListItem Value="-1">Select Department</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>

                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:DropDownList ID="ddlrole" runat="server" Width="250px">
                        <asp:ListItem Value="-1">Select Role</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>

                    </td>
                </tr>    
                 <tr>
                     <td>
                   <asp:TextBox ID="txtdesignation" ReadOnly="true" runat="server" Width="250px"></asp:TextBox>  
                    </td>      
                    </tr>       
                <tr>
                    <td colspan="2">
                    <asp:Button ID="btnDesignation" runat="server" Text="Add" Width="250px"></asp:Button>
                    </td>
                </tr>
            </table>
                </center>
        </div>
    </div>
</asp:Content>
