<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="UpdateAttendance.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.UpdateAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-4 col-sm-4 text-center">
                    <h3 class="text-primary">Mark Attendance</h3>
                    <hr class="small" />
                    <asp:Panel ID="pnlMarkAttendance" runat="server">
                        <table class="table-condensed">
                            <tr>
                                <td>Select Employee : 
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Select Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Width="250px" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="ceTDate" runat="server" TargetControlID="txtDate" Format="yyyy/M/dd"></ajaxToolkit:CalendarExtender>

                                </td>
                            </tr>
                            <tr>
                                <td>Shift : </td>
                                <td>
                                    <asp:TextBox ID="txtShift" runat="server" CssClass="form-control" ReadOnly="true" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Shift Start Time:
                                    <asp:TextBox ID="txtShiftStartTime" runat="server" CssClass="form-control" Width="250px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>Shift End Time:
                                    <asp:TextBox ID="txtShiftEndTime" runat="server" CssClass="form-control" Width="250px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>Enter Start Time:
                                    <asp:TextBox ID="txtEnterST" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>

                                </td>
                                <td>Enter End Time:
                                    <asp:TextBox ID="txtEnterET" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnMark" runat="server" Text="Mark Attendance" CssClass="btn btn-success btn-md" OnClick="btnMark_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
