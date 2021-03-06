﻿<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="SphereInfoSolutionHRMS.Attendance.Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
  <%--  <div class="row">
        <div class="col-sm-12">
            <asp:LinkButton ID="lbtnApplyLeave" runat="server" CssClass="pull-right">Apply Leave</asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-2">
            <asp:Label ID="lblTotalDays" runat="server" Text="Total Days" Visible="false"></asp:Label>
        </div>
        <div class="col-sm-2">
            <asp:Label ID="lblPresentDays" runat="server" Text="Present" Visible="false"></asp:Label>
        </div>
        <div class="col-sm-2">
            <asp:Label ID="lblAbsentDays" runat="server" Text="Absent" Visible="false"></asp:Label>
        </div>
        <div class="col-sm-2">
            <asp:Label ID="lblLeaveTaken" runat="server" Text="Leave Taken" Visible="false"></asp:Label>
        </div>
        <div class="col-sm-2">
            <asp:Label ID="lblTotalHours" runat="server" Text="Total Hours" Visible="false"></asp:Label>
        </div>
        <div class="col-sm-2">
            <asp:Label ID="lblAvailableLeave" runat="server" Text="Available Leave" Visible="false"></asp:Label>
        </div>        
    </div>--%>
    <div class="row">
        <div class="col-sm-12">
            <table class="table-condensed">
                <tr>
                    <td>
                        <asp:Label ID="lblRange" runat="server" Font-Bold="true" Text="Range:"></asp:Label>
                    </td>
                    <td>
                        From Date: <asp:TextBox ID="txtFromDate" CssClass="form-control" ReadOnly="true" runat="server" placeholder="From Date.."></asp:TextBox>
                        <cc1:CalendarExtender ID="ceFromDate" runat="server" TargetControlID="txtFromDate" Format="yyyy/M/dd"></cc1:CalendarExtender>
                    </td>
                    <td>
                        To Date<asp:TextBox ID="txtToDate" CssClass="form-control" ReadOnly="true" runat="server" placeholder="To Date.."></asp:TextBox>                       
                        <cc1:CalendarExtender ID="ceToDate" runat="server" TargetControlID="txtToDate" Format="yyyy/M/dd"></cc1:CalendarExtender> 
                    </td>
                    <td>
                        <asp:Button ID="btnViewAttendance" CssClass="btn btn-primary" runat="server" Text="View" OnClick="btnViewAttendance_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    <div class="row">
        <div class="col-sm-12">
            <asp:GridView ID="gvEmployeeAttendance" runat="server" DataKeyNames="Attendance_Id" AutoGenerateColumns="False"
                CssClass="table table-hover table-bordered table-condensed" OnPageIndexChanging="gvEmployeeAttendance_PageIndexChanging"
                HeaderStyle-CssClass="gvHeader">
                <Columns>
                    <asp:BoundField DataField="Attendance_Id" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="Day" HeaderText="Day" />
                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-M-dd}"/>
                    <asp:BoundField DataField="ShiftStartTime" HeaderText="Shift Start" />
                    <asp:BoundField DataField="InTime" HeaderText="In Time"/>
                    <asp:BoundField DataField="ShiftEndTime" HeaderText="Shift Close" />
                    <asp:BoundField DataField="OutTime" HeaderText="Out Time" />
                    <asp:BoundField DataField="Late" HeaderText="Late" />
                    <asp:BoundField DataField="OverTime" HeaderText="Over Time"/>
                    <asp:BoundField DataField="TotalHrs" HeaderText="Total Hours" />
                    <asp:BoundField DataField="AttendanceStatus" HeaderText="Status" />                    
                </Columns>
            </asp:GridView>
        </div>

        <div class="col-sm-12">
            <asp:Label ID="lblMessageAttendance" runat="server" Visible="false"></asp:Label>
        </div>

    </div>
</asp:Content>
