﻿<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SphereInfoSolutionHRMS.Reports.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>

    <div class="row">
        <div class="col-md-3 text-center"  style="padding:5px">
            <asp:Label ID="lblSelectReport" runat="server" Text="Select Report: "></asp:Label>
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:RadioButton ID="rbAttendance" Text="Attendance" runat="server" GroupName="reports" />
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:RadioButton ID="rbLeave" Text="Leave" runat="server" GroupName="reports" />
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:RadioButton ID="rvHoliday" Text="Holiday" runat="server" GroupName="reports" />
        </div>
    </div>
    <hr class="small" />

    <div class="row">
        <div class="col-md-3 text-center""  style="padding:5px">
            <asp:Label ID="lblSelectClient" runat="server" Text="Select Client: "></asp:Label>
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:DropDownList ID="ddlClient" runat="server" Width="100%" CssClass="form-control input-md"></asp:DropDownList>
        </div>
        <div class="col-md-3 text-center""  style="padding:5px">
            <asp:Label ID="lblSelectEmployee" runat="server" Text="Select Employee: "></asp:Label>
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:DropDownList ID="ddlEmployee" runat="server" Width="100%" CssClass="form-control input-md"></asp:DropDownList>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 text-center""  style="padding:5px">
            <asp:Label ID="lblFromDate" runat="server" Text="From Date: "></asp:Label>
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:TextBox ID="txtFromDate" CssClass="form-control" runat="server" placeholder="Enter From Date Here.."></asp:TextBox>
            <%--<cc1:calendarextender id="ceDateofBirth" runat="server" targetcontrolid="txtDatefBirth" format="yyyy/M/dd"></cc1:calendarextender>--%>
        </div>
        <div class="col-md-3 text-center""  style="padding:5px">
            <asp:Label ID="lblToDate" runat="server" Text="To Date: "></asp:Label>
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:TextBox ID="txtToDate" CssClass="form-control" runat="server" placeholder="Enter To Date Here.."></asp:TextBox>
            <%--<cc1:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtDatefBirth" format="yyyy/M/dd"></cc1:calendarextender>--%>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
            
        </div>
        <div class="col-md-3" style="padding:5px">
            <asp:Button ID="btnDetails" runat="server" Text="View Details" CssClass="btn btn-primary btn-md" />
            <asp:Button ID="btnSummary" runat="server" Text="View Summary" CssClass="btn btn-primary btn-md" />
        </div>        
    </div>
    <hr class="small" />
</asp:Content>