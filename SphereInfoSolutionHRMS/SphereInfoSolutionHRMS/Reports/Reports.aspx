<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SphereInfoSolutionHRMS.Reports.Reports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <div class="row">
        
             <div class="col-md-3 text-center"  style="padding:5px">
            <asp:Label ID="lblEmployeeType" runat="server" Text="Select Employee Type: "></asp:Label>
        </div>
         <div class="col-md-1"  style="padding:5px">
             <asp:LinkButton ID="lnkbtnActive" runat="server" OnClick="lnkbtnActive_Click">Active</asp:LinkButton>
        </div>
         <div class="col-md-1"  style="padding:5px">
             <asp:LinkButton ID="lnkbtnNotActive" runat="server" OnClick="lnkbtnNotActive_Click">Not Active</asp:LinkButton>
        </div>
       
         
    </div>
    <div class="row">
        <div class="col-md-3 text-center"  style="padding:5px">
            <asp:Label ID="lblSelectReport" runat="server" Text="Select Report: "></asp:Label>
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:RadioButton ID="rbAttendance" Text="Attendance" runat="server" GroupName="reports" Checked="true" OnCheckedChanged="rbAttendance_CheckedChanged" AutoPostBack="true"/>
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:RadioButton ID="rbLeave" Text="Leave" runat="server" GroupName="reports" OnCheckedChanged="rbAttendance_CheckedChanged" AutoPostBack="true"/>
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:RadioButton ID="rbHoliday" Text="Holiday" runat="server" GroupName="reports" OnCheckedChanged="rbAttendance_CheckedChanged" AutoPostBack="true"/>
        </div>
       
    </div> 
    
    <hr class="small" />
    <div class="row">
        <div class="col-md-3 text-center""  style="padding:5px">
            <asp:Label ID="lblSelectClient" runat="server" Text="Select Client: "></asp:Label>
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:DropDownList ID="ddlClient" runat="server" Width="100%" CssClass="form-control input-md" AutoPostBack="True" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-3 text-center""  style="padding:5px">
            <asp:Label ID="lblSelectEmployee" runat="server" Text="Select Employee: "></asp:Label>
        </div>
        <div class="col-md-3"  style="padding:5px">
            <asp:DropDownList ID="ddlEmployee" runat="server" Width="100%" CssClass="form-control input-md" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"></asp:DropDownList>
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
            <asp:Button ID="btnDetails" runat="server" Text="View Details" CssClass="btn btn-primary btn-md" OnClick="btnDetails_Click" />
            <asp:Button ID="btnSummary" runat="server" Text="View Summary" CssClass="btn btn-primary btn-md" OnClick="btnSummary_Click" />
        </div>        
    </div>
    <hr class="small" />

    <div class="row">
         <div class="col-xs-12">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>
         <div class="col-xs-12">
             <rsweb:ReportViewer ID="ReportViewerAttendanceDetails" runat="server" Width="100%" 
                 Height="500px" ></rsweb:ReportViewer>
        </div>
       
    </div>

</asp:Content>
