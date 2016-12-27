<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="LeaveDetails.aspx.cs" Inherits="SphereInfoSolutionHRMS.Attendance.LeaveDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function DeleteConfirm() {
            var Ans = confirm("Do you want to Remove Selected Employee Leave Details?");
            if (Ans) {
                return true;
            }
            else {
                return false;
            }
        }
        function AddConfirm() {
            var Ans = confirm("Do you want to Add Selected Employee Leave Details?");
            if (Ans) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <%--<div class="row">
        <div class="col-sm-4">
            <asp:Label ID="lblAbsentDays" runat="server" Text="No. of Days Absent: " Font-Bold="true"></asp:Label>
            <asp:Label ID="lblDaysAbsent" runat="server" Text="02"></asp:Label>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="lblLeaveTaken" runat="server" Text="No. of Leaves Taken: " Font-Bold="true"></asp:Label>
            <asp:Label ID="lblTakenLeaves" runat="server" Text="04"></asp:Label>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="lblLeavesAvailable" runat="server" Text="No. of Leaves Available: " Font-Bold="true"></asp:Label>
            <asp:Label ID="lblAvailableLeaves" runat="server" Text="15"></asp:Label>
        </div>
    </div>--%>
    <div class="row">
        <%-- Apply Leave  --%>
        <div class="col-lg-4 col-sm-4 text-center">

            <h3 class="text-primary">Apply Leave</h3>
            <hr class="small" />
            <asp:Panel ID="PanelLeaveRequsition" runat="server">
                <table class="table-condensed">
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="form-control" Width="250px">
                                <asp:ListItem Text="--Select Leave Type--" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFLeaveType" InitialValue="0" ControlToValidate="ddlLeaveType" runat="server" ForeColor="Red"
                                ErrorMessage="Enter From Date" ValidationGroup="grpLeave"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" AutoPostBack="True" Width="250px" placeholder="From Date" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                            <cc1:CalendarExtender ID="ceFromDate" runat="server" TargetControlID="txtFromDate" Format="yyyy/M/dd"></cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RFFromDate" ControlToValidate="txtFromDate" runat="server" ForeColor="Red"
                                ErrorMessage="Enter From Date" ValidationGroup="grpLeave"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" AutoPostBack="True" Width="250px" placeholder="To Date" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                            <cc1:CalendarExtender ID="ceToDate" runat="server" TargetControlID="txtToDate" Format="yyyy/M/dd"></cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RfToDate" ControlToValidate="txtToDate" runat="server" ForeColor="Red"
                                ErrorMessage="Enter To Date" ValidationGroup="grpLeave"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtContactNo" runat="server" MaxLength="10" CssClass="form-control" Width="250px" placeholder="Contact"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFContact" ControlToValidate="txtContactNo"
                                runat="server" ForeColor="Red" ValidationGroup="grpLeave" ErrorMessage="Enter Only Digit"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" Width="250px" placeholder="Reason"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="grpLeave" ControlToValidate="txtReason" runat="server" ForeColor="Red" ErrorMessage="Enter Reason"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <!--Start - get halfday details-->
                <div>
                    <asp:Label ID="lblIsHalfDay" runat="server" Text="Is Halfday ?" Visible="false"></asp:Label>
                </div>
                <asp:GridView ID="gvHalfdayDetails" runat="server" AutoGenerateColumns="false"
                    Visible="false"
                    ShowHeader="true"
                    CssClass="table table-hover table-bordered table-condensed"
                    HeaderStyle-CssClass="gvHeader" OnPageIndexChanging="gvHalfdayDetails_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Day" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                            <HeaderTemplate>
                                <asp:Label ID="lblHeaderDate" runat="server" Text="Date"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Halfday" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                            <HeaderTemplate>
                                <%--<asp:CheckBox ID="cbHeaderIsHalfday" runat="server" />--%>
                                <%--<asp:Label ID="lblHeadIsHalfday" runat="server" Text="Is Halfday"></asp:Label>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbIsHalfday" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <!--End - get halfday details-->
                <br />
                <div>
                    <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-success btn-sm"
                        OnClientClick="if (typeof(Page_ClientValidate) == 'function') { Page_ClientValidate(); if(Page_IsValid) { return confirm('Do You Want To Apply Leave?'); } };" OnClick="btnApply_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btnCancel_Click" />
                </div>
                <div>
                    <asp:Label ID="lblAppliedStatus" runat="server" Visible="false"></asp:Label>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-8 col-sm-8 text-center border-right">
            <%-- <div>
                <br />

                <div class="row">
                    <div class="col-sm-6 col-xs-12" style="padding: 5px;">
                        <asp:TextBox ID="txtSearchLeave" runat="server" CssClass="form-control input-md" placeholder="Enter Date"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-5" style="padding: 5px;">
                        <asp:Button ID="btnSearchLeave" runat="server" Text="Search" CssClass="btn btn-primary btn-md" />
                    </div>
                    <div class="col-sm-2 col-xs-2" style="padding: 5px;">
                        <asp:Button ID="btnShowAllLeaves" runat="server" Text="Show All Leaves" CssClass="btn btn-primary btn-md" />
                    </div>
                </div>

            </div>
            <hr class="small" />--%>

            <h3 class="text-primary">Leaves Taken</h3>

            <asp:GridView ID="gvLeaveDetails" runat="server" AutoGenerateColumns="False"
                ShowHeader="true"
                DataKeyNames="LeaveId"
                CssClass="table table-hover table-bordered" OnPageIndexChanging="gvLeaveDetails_PageIndexChanging"
                HeaderStyle-CssClass="gvHeader" OnRowDataBound="gvLeaveDetails_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="LeaveId" HeaderText="Leave ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="UserId" HeaderText="UserId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Applied By" />
                    <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" />
                    <asp:BoundField DataField="ContactNo" HeaderText="Contact" />
                    <asp:BoundField DataField="Reason" HeaderText="Reason" />
                    <asp:BoundField DataField="FromDate" HeaderText="From Date" DataFormatString="{0:yyyy-M-dd}" />
                    <asp:BoundField DataField="ToDate" HeaderText="To Date" DataFormatString="{0:yyyy-M-dd}" />
                    <asp:BoundField DataField="AppliedOn" HeaderText="AppliedOn" DataFormatString="{0:yyyy-M-dd}" />

                    <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="gridviewHeader" ItemStyle-CssClass="gridviewIcon">
                        <ItemTemplate>
                            <asp:GridView
                                ID="gvLeaveChild"
                                runat="server"
                                ShowHeader="true"
                                AutoGenerateColumns="false"
                                DataKeyNames="LeaveId"
                                CssClass="table table-hover table-bordered table-condensed"
                                HeaderStyle-CssClass="gvHeader"
                                OnRowCommand="gvLeaveChild_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="LeaveId" HeaderText="LeaveID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-M-dd}" />

                                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblId" Text='<%# Eval("LeaveId") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblFrom" Text='<%# Eval("Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Half Day ?" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsHalfDay" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsHalfDay")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="LeaveStatus" HeaderText="Leave Status" />

                                    <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:Button ID="btnCancel" runat="server" Text="X" CommandName="CancelLeave" CssClass="btn btn-danger btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMessageLeaveDetails" runat="server" Visible="false"></asp:Label>
            <br />
            <br />
            <hr class="small" />

            <%--Waiting for approval grid view--%>
            <h3 class="text-primary">Pending Requests</h3>
            <br />

            <asp:GridView ID="gvApprovalLeave" runat="server" AutoGenerateColumns="False"
                ShowHeader="true" OnPageIndexChanging="gvApprovalLeave_PageIndexChanging"
                DataKeyNames="LeaveId"
                CssClass="table table-hover table-bordered table-condensed"
                HeaderStyle-CssClass="gvHeader" OnRowDataBound="gvApprovalLeave_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="LeaveId" HeaderText="Leave ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="UserId" HeaderText="UserId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Applied By" />
                    <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" />
                    <asp:BoundField DataField="ContactNo" HeaderText="Contact" />
                    <asp:BoundField DataField="Reason" HeaderText="Reason" />
                    <asp:BoundField DataField="FromDate" HeaderText="From Date" DataFormatString="{0:yyyy-M-dd}" />
                    <asp:BoundField DataField="ToDate" HeaderText="To Date" DataFormatString="{0:yyyy-M-dd}" />
                    <asp:BoundField DataField="AppliedOn" HeaderText="AppliedOn" DataFormatString="{0:yyyy-M-dd}" />

                    <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="gridviewHeader" ItemStyle-CssClass="gridviewIcon">
                        <ItemTemplate>
                            <asp:GridView
                                ID="gvApprovalLeaveChild"
                                runat="server"
                                ShowHeader="true"
                                AutoGenerateColumns="false"
                                DataKeyNames="LeaveId"
                                CssClass="table table-hover table-bordered table-condensed"
                                HeaderStyle-CssClass="gvHeader"
                                OnRowCommand="gvApprovalLeaveChild_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="LeaveId" HeaderText="LeaveID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-M-dd}" />

                                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblId" Text='<%# Eval("LeaveId") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblFrom" Text='<%# Eval("Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Half Day ?" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsHalfDay" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsHalfDay")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="LeaveStatus" HeaderText="Leave Status" />

                                    <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:Button ID="btnApprove" runat="server" Text="✓" CommandName="Approve" CssClass="btn btn-success btn-sm" />
                                            <asp:Button ID="btnReject" runat="server" Text="X" CommandName="Reject" CssClass="btn btn-danger btn-sm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblMessageApprovalLeave" runat="server" Visible="false"></asp:Label>
        </div>


    </div>
</asp:Content>
