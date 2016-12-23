<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="AccessControl.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">

    <div class="row">
        <div class="col-sm-4">
            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RFDesignation" runat="server" InitialValue="0" ControlToValidate="ddlDesignation" ForeColor="Red" ErrorMessage="Select Designation" ValidationGroup="grpaccess"></asp:RequiredFieldValidator>
            <asp:TreeView ID="tvAccessControl" runat="server" ShowCheckBoxes="All" ShowLines="True" ExpandDepth="0">
            </asp:TreeView>

            <asp:Button ID="btnGiveAccess" ValidationGroup="grpaccess" runat ="server" Text="Grant Access" CssClass="btn btn-primary btn-sm pull-right" OnClick="btnGiveAccess_Click"/>
        </div>
    </div>

</asp:Content>
