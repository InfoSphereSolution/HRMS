<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="Leave.aspx.cs" Inherits="SphereInfoSolutionHRMS.Attendance.Leave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

    <div class="row">
        <div class="col-lg-8 col-sm-8 text-center border-right">
            <div>
                <br />
                <table class="table-condensed">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSearchLeave" runat="server" CssClass="form-control input-md" placeholder="Enter Date"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearchLeave" runat="server" Text="Search" CssClass="btn btn-primary btn-md" />
                        </td>
                        <td>
                            <asp:Button ID="btnShowAllLeaves" runat="server" Text="Show All Leaves" CssClass="btn btn-primary btn-md" />
                        </td>
                    </tr>
                </table>
            </div>
            <hr class="small" />

            <h3 class="text-primary">Leaves Taken</h3>

            <asp:GridView ID="gvLeaveDetails" runat="server" AutoGenerateColumns="False"
                ShowHeader="true"
                DataKeyNames="Id"
                CssClass="table table-hover table-bordered table-condensed"
                HeaderStyle-CssClass="gvHeader">
                <Columns>
                    <asp:BoundField DataField="LeaveID" HeaderText="Leave ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                    <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                    <asp:BoundField DataField="Reason" HeaderText="Reason" />

                    <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="gridviewHeader" ItemStyle-CssClass="gridviewIcon">
                        <ItemTemplate>
                            <asp:GridView
                                ID="gvLeaveChild"
                                runat="server"
                                ShowHeader="true"
                                AutoGenerateColumns="false"
                                DataKeyNames="LeaveID"
                                CssClass="table table-hover table-bordered table-condensed"
                                HeaderStyle-CssClass="gvHeader">
                                <Columns>
                                    <asp:BoundField DataField="LeaveID" HeaderText="LeaveID" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" />

                                    <asp:TemplateField HeaderText="Half Day ?" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsHalfDay" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsHalfDay")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="LeaveStatus" HeaderText="Leave Status" />

                                    <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="CancelLeave" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMessageLeave" runat="server"></asp:Label>
            <br />
            <br />
            <hr class="small" />

            <%--Waiting for approval grid view--%>
            <h3 class="text-primary">Pending Requests</h3>
            <br />

            <asp:GridView ID="gvApprovalLeave" runat="server" AutoGenerateColumns="False"
                ShowHeader="true"
                DataKeyNames="Id"
                CssClass="table table-hover table-bordered table-condensed"
                HeaderStyle-CssClass="gvHeader">
                <Columns>
                  <asp:BoundField DataField="LeaveID" HeaderText="Leave ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                    <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                    <asp:BoundField DataField="Contact" HeaderText="Contact" />
                    <asp:BoundField DataField="Reason" HeaderText="Reason" />

                    <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="gridviewHeader" ItemStyle-CssClass="gridviewIcon">
                        <ItemTemplate>
                            <asp:GridView
                                ID="gvApprovalLeaveChild"
                                runat="server"
                                ShowHeader="true"
                                AutoGenerateColumns="false"
                                DataKeyNames="LeaveID"
                                CssClass="table table-hover table-bordered table-condensed"
                                HeaderStyle-CssClass="gvHeader">
                                <Columns>
                                     <asp:BoundField DataField="LeaveID" HeaderText="LeaveID" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" />

                                    <asp:TemplateField HeaderText="Half Day ?" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsHalfDay" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsHalfDay")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="LeaveStatus" HeaderText="Leave Status" />

                                    <asp:TemplateField HeaderText="" ItemStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" />
                                            <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblMessageApprovalLeave" runat="server"></asp:Label>
        </div>

        <%--  --%>
        <div class="col-lg-4 col-sm-4 text-center">
            <hr class="small" />
            <h3 class="text-primary">Apply Leave</h3>
            <asp:Panel ID="PanelLeaveRequsition" runat="server">
                <table class="table-condensed">
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="form-control" Width="250px">
                                <asp:ListItem Text="--Select Leave Type--"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" AutoPostBack="True" Width="250px" placeholder="From Date"></asp:TextBox>
                            <cc1:CalendarExtender ID="ceFromDate" runat="server" TargetControlID="txtFromDate" Format="yyyy/M/dd"></cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" AutoPostBack="True" Width="250px" placeholder="To Date"></asp:TextBox>
                            <cc1:CalendarExtender ID="ceToDate" runat="server" TargetControlID="txtToDate" Format="yyyy/M/dd"></cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" Width="250px" placeholder="Contact"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" Width="250px" placeholder="Reason"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <!--Start - get halfday details-->
                <div>
                    <asp:Label ID="lblIsHalfDay" runat="server" Text="Is Halfday ?"></asp:Label>
                </div>
                <asp:GridView ID="gvHalfdayDetails" runat="server" AutoGenerateColumns="false" Visible="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Day">
                            <HeaderTemplate>
                                <asp:Label ID="lblHeaderDate" runat="server" Text="Date"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Halfday">
                            <HeaderTemplate>
                                <asp:CheckBox ID="cbHeaderIsHalfday" runat="server" />
                                <asp:Label ID="lblHeadIsHalfday" runat="server" Text="Is Halfday"></asp:Label>
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
                    <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-primary btn-sm" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
