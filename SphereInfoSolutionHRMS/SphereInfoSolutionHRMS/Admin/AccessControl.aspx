<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="AccessControl.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">

    <div class="row">
        <div class="col-lg-12">
            <asp:TreeView ID="tvAccessControl" runat="server" ShowCheckBoxes="All" ShowLines="True" ExpandDepth="1">
            </asp:TreeView>

            <asp:Button ID="btnGiveAccess" runat ="server" Text="Grant Access" CssClass="btn btn-primary btn-sm"/>
        </div>
    </div>

</asp:Content>
