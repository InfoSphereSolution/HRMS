<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="ClientDetails.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.ClientDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">

    <cc1:toolkitscriptmanager runat="server"></cc1:toolkitscriptmanager>
    <div class="panel panel-default" style="padding: 10px; margin: 10px">
        <div id="Tabs" role="tabpanel">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li class="active"><a href="#viewAllClients" aria-controls="viewAllClients" role="tab" data-toggle="tab">View Clients</a></li>
                <li><a href="#clientDetails" aria-controls="clientDetails" role="tab" data-toggle="tab">Client Details</a></li>
                <li><a href="#shiftDetails" aria-controls="shiftDetails" role="tab" data-toggle="tab">Shift Timings</a></li>
                <li><a href="#holidayList" aria-controls="holidayList" role="tab" data-toggle="tab">Holiday List</a></li>
            </ul>
            <!-- Tab panes -->
            <div class="tab-content" style="padding-top: 20px">

                <%--View All Client--%>
                <div role="tabpanel" class="tab-pane active" id="viewAllClients">
                    <div id="listOfClients">
                        <h3 class="text-primary">List of Clients</h3>

                        <asp:GridView ID="gvClientList" runat="server" DataKeyNames="ClientId" AutoGenerateColumns="False"
                            CssClass="table table-hover table-bordered table-condensed"
                            HeaderStyle-CssClass="gvHeader">
                            <Columns>
                                <asp:BoundField DataField="ClientId" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ClientName" HeaderText="Name" />
                                <asp:BoundField DataField="Location" HeaderText="Level" />
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                                <asp:TemplateField HeaderText="Remove" ItemStyle-CssClass="center-block text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewClient" runat="server" CommandName="View" CommandArgument='<%# Eval("ClientId") %>' Text="View" CssClass="btn btn-primary btn-xs"></asp:Button>
                                        <asp:Button ID="btnEditClient" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ClientId") %>' Text="Edit" CssClass="btn btn-primary btn-xs"></asp:Button>
                                        <asp:Button ID="btnRemoveClient" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ClientId") %>' Text="X" CssClass="btn btn-danger btn-xs"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblMessageClientList" runat="server"></asp:Label>


                        <%--Waiting for approval grid--%>
                        <h3 class="text-primary">Pending Requests</h3>
                        <br />

                        <asp:GridView ID="gvPendingClientList" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="PendingClientId" CssClass="table table-bordered table-condensed" HeaderStyle-CssClass="gvHeader">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAllClientList" runat="server" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkboxSelectClientList" runat="server" onclick="Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PendingClientId" HeaderText="Pending Client Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ClientId" HeaderText="Client Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ClientName" HeaderText="Name" />
                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                <asp:BoundField DataField="Contact" HeaderText="Contact" />
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                                <asp:BoundField DataField="Operation" HeaderText="Operation" />
                                <asp:TemplateField HeaderText="Remove" ItemStyle-CssClass="center-block text-center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnPendingClientView" runat="server" CommandName="View" CommandArgument='<%# Eval("PendingClientId") %>' Text="View" CssClass="btn btn-primary btn-xs"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="btnApproveClient" runat="server" Text="Approve" CssClass="btn btn-success btn-md" Width="100px" />&nbsp;&nbsp;
                        <asp:Button ID="btnRejectClient" runat="server" Text="Reject" CssClass="btn btn-danger btn-md" Width="100px" />
                        <br />
                        <asp:Label ID="lblMessagePendingClientList" runat="server"></asp:Label>

                    </div>
                </div>

                <%--Client General Details--%>
                <div role="tabpanel" class="tab-pane" id="clientDetails">
                    <div id="ClientDetailsForm">
                        <div class="col-lg-12 well">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12 form-group">
                                            <asp:Label ID="lblNote" runat="server" Text="Enter the client Details Below.." Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-8 col-xs-12 form-group">
                                            <asp:Label ID="lblClientName" runat="server" Text="Name" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtClientName" CssClass="form-control" runat="server" placeholder="Enter Client Name Here.."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblDescription" runat="server" Text="Description" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Enter Description Here.." runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblAddress" runat="server" Text="Address" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtAddress" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Enter Address Here.." runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblCity" runat="server" Text="City" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>

                                            <div style="padding: 2px;">
                                                <asp:Button ID="btnAddCityPanel" runat="server" CssClass="btn btn-primary btn-md form-control pull-right" Text="+" Width="15%" />
                                            </div>

                                            <cc1:modalpopupextender id="mpeAddCity" runat="server"
                                                popupcontrolid="pnlAddCity"
                                                targetcontrolid="btnAddCityPanel"
                                                cancelcontrolid="btnCancelCity"
                                                backgroundcssclass="modal-backdrop">
                                            </cc1:modalpopupextender>

                                            <asp:Panel ID="pnlAddCity" runat="server" CssClass="modal-dialog">

                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h3 class="modal-title">
                                                            <asp:Label ID="lblHeaderAddCity" runat="server" Text="Enter Details" Font-Bold="true"></asp:Label>
                                                        </h3>
                                                    </div>
                                                    <div class="modal-body" style="padding: 5px;">

                                                        <asp:DropDownList ID="ddlCityState" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <asp:TextBox ID="txtCityName" CssClass="form-control" placeholder="Enter City Name Here.." runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <asp:Button ID="btnCancelCity" runat="server" class="btn btn-md btn-primary" Text="Cancel" />
                                                        <asp:Button ID="btnAddNewCity" runat="server" class="btn btn-md btn-primary" Text="Add City" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblState" runat="server" Text="State" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"></asp:DropDownList>

                                            <div style="padding: 2px;">
                                                <asp:Button ID="btnAddStatePanel" runat="server" CssClass="btn btn-primary btn-md form-control pull-right" Text="+" Width="15%" />
                                            </div>

                                            <cc1:modalpopupextender id="mpeAddState" runat="server"
                                                popupcontrolid="pnlAddState"
                                                targetcontrolid="btnAddStatePanel"
                                                cancelcontrolid="btnCancelState"
                                                backgroundcssclass="modal-backdrop">
                                            </cc1:modalpopupextender>

                                            <asp:Panel ID="pnlAddState" runat="server" CssClass="modal-dialog">

                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h3 class="modal-title">
                                                            <asp:Label ID="lblHeaderAddState" runat="server" Text="Enter Details" Font-Bold="true"></asp:Label>
                                                        </h3>
                                                    </div>
                                                    <div class="modal-body" style="padding: 5px;">
                                                        <asp:TextBox ID="txtStateName" CssClass="form-control" placeholder="Enter State Name Here.." runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <asp:Button ID="btnCancelState" runat="server" class="btn btn-md btn-primary" Text="Cancel" />
                                                        <asp:Button ID="btnAddNewState" runat="server" class="btn btn-md btn-primary" Text="Add State" />
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <asp:Label ID="lblPincode" runat="server" Text="Pincode" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtPincode" CssClass="form-control" runat="server" placeholder="Enter Pincode Here.."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 form-group">
                                            <asp:Label ID="lblContact" runat="server" Text="Contact" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtContact" CssClass="form-control" runat="server" placeholder="Enter Phone Number Here.."></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6 form-group">
                                            <asp:Label ID="lblSite" runat="server" Text="Website" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtSite" CssClass="form-control" runat="server" placeholder="Enter Website Address Here.."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblIP" runat="server" Text="IP Address" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtIP" CssClass="form-control" runat="server" placeholder="Enter IP Adress Here.."></asp:TextBox>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 form-group">
                                            <asp:CheckBox ID="cbIsSaturdayWorking" CssClass="checkbox-inline" Text="Saturday working?" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Panel ID="pnlISSaturdayWorking" runat="server">
                                            <div class="col-sm-12 form-group">
                                                <asp:CheckBox ID="cbFirstSaturday" CssClass="checkbox-inline" Text="First" runat="server" />
                                                <asp:CheckBox ID="cbSecondSaturday" CssClass="checkbox-inline" Text="Second" runat="server" />
                                                <asp:CheckBox ID="cbThirdSaturday" CssClass="checkbox-inline" Text="Third" runat="server" />
                                                <asp:CheckBox ID="cbFourthSaturday" CssClass="checkbox-inline" Text="Fourth" runat="server" />
                                                <asp:CheckBox ID="cbFifthSaturday" CssClass="checkbox-inline" Text="Fifth" runat="server" />
                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <asp:Button ID="btnToShift" href="#shiftDetails" aria-controls="shiftDetails" role="tab" data-toggle="tab" runat="server" class="btn btn-lg btn-primary pull-right" Text="Next" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <%--Client Shift Timing--%>
                <div role="tabpanel" class="tab-pane" id="shiftDetails">
                    <div id="ClientShiftForm">
                        <div class="col-lg-12 well">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12 form-group">
                                            <asp:Label ID="lblShift" runat="server" Text="Shift" Font-Bold="true"></asp:Label>
                                            <div class="row">
                                                <div class="col-sm-6 form-group">
                                                    <asp:DropDownList ID="ddlGeneralShift" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-6 form-group">
                                                    <asp:Label ID="lblGeneralShiftDetails" runat="server" Text="Label">
                            Note:- <br />
                            General-1 : (Timing: 08.30 to 17.30 | Hours: 09) <br />
                            General-2 : (Timing: 09.00 to 18.00 | Hours: 09) <br />
                            General-3 : (Timing: 09.30 to 18.30 | Hours: 09)
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 form-group">
                                            <asp:CheckBox ID="cbSecondShift" CssClass="checkbox-inline" Text="Second Shift(Timing: 02.30 to 23.00 | Hours: 09)" runat="server" />
                                        </div>
                                        <div class="col-sm-6 form-group">
                                            <asp:CheckBox ID="cbNightShift" CssClass="checkbox-inline" Text="Night Shift(Timing: 23.00 to 09.00 | Hours: 09)" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 form-group">
                                            <asp:CheckBox ID="cbFlexibleShift" CssClass="checkbox-inline" Text="Flexible Timing(Hours: 09)" runat="server" />
                                        </div>
                                        <div class="col-sm-6 form-group">
                                            <asp:CheckBox ID="cbCustomShift" CssClass="checkbox-inline" Text="Custom Shift" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 form-group">
                                            <asp:Panel ID="pnlCustomShift" runat="server">
                                                <asp:Label ID="lblHeaderCustomShift" runat="server" Text="Custom Shift" Font-Bold="true"></asp:Label>
                                                <div class="row" style="padding: 5px;">
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtCustomShiftName" runat="server" placeholder="Enter Shift Name Here.." CssClass="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="txtCustomShiftStart" runat="server" placeholder="Enter Start Time.." CssClass="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="txtCustomShiftEnd" runat="server" placeholder="Enter End Time.." CssClass="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="txtCustomShiftHours" runat="server" placeholder="Enter Hours.." CssClass="form-control"></asp:TextBox>
                                                        <asp:Button ID="btnAddCustomShift" runat="server" Text="Add" class="btn btn-sm btn-primary pull-right" />
                                                        <asp:Button ID="btnCancelCustomShift" runat="server" Text="Cancel" class="btn btn-sm btn-danger pull-right" />
                                                    </div>
                                                    <div class="col-sm-6" style="padding: 5px;">
                                                        <asp:Label ID="lblCustomShiftDetails" runat="server" Text="Label">
                                        Note:- Enter either start time and end time or no. of hours, <br />
                                        To add custom flexible shift only enter no. of hours
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>


                                    <asp:Button ID="btnBackClientDetails" href="#clientDetails" aria-controls="clientDetails" role="tab" data-toggle="tab" runat="server" class="btn btn-lg btn-primary pull-left" Text="Back" />
                                    <asp:Button ID="btnSaveClient" runat="server" class="btn btn-lg btn-primary pull-right" Text="Save" />
                                    <%--<asp:Button ID="btnToHoliday" href="#holidayList" aria-controls="holidayList" role="tab" data-toggle="tab" runat="server" class="btn btn-lg btn-primary pull-right" Text="Next" />--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--Holiday List--%>
                <div role="tabpanel" class="tab-pane" id="holidayList">
                    <div id="ClientholidayList">
                        <div class="row">
                            <div class="col-lg-8 col-sm-8 text-center border-right">
                                <div>
                                    <br />
                                    <table class="table-condensed">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSearchHoliday" runat="server" CssClass="form-control input-md" placeholder="Enter Holiday"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSearchHoliday" runat="server" Text="Search" CssClass="btn btn-primary btn-md" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnShowAllHolidays" runat="server" Text="Show All Holidays" CssClass="btn btn-primary btn-md" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <hr class="small" />


                                <h3 class="text-primary">List of Holidays</h3>

                                <asp:GridView ID="gvHolidayList" runat="server" DataKeyNames="ClientHolidayId" AutoGenerateColumns="False" CssClass="table table-hover table-bordered table-condensed"
                                    HeaderStyle-CssClass="gvHeader">
                                    <Columns>
                                        <asp:BoundField DataField="ClientHolidayId" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="HolidayName" HeaderText="Name" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" />
                                        <asp:BoundField DataField="IsOptional" HeaderText="Is Optional" />
                                        <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                                        <asp:TemplateField HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:Button ID="btnRemoveHoliday" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ClientHolidayId") %>' Text="X" CssClass="btn btn-danger btn-xs"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lblMessageHolidayList" runat="server"></asp:Label>
                                <br />
                                <br />
                                <hr class="small" />

                                <%--Waiting for approval grid view--%>
                                <h3 class="text-primary">Pending Requests</h3>
                                <br />

                                <asp:GridView ID="gvTempHolidayList" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="PendingClientHolidayId" CssClass="table table-bordered table-condensed" HeaderStyle-CssClass="gvHeader">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAllHoliday" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkboxSelectHoliday" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PendingClientHolidayId" HeaderText="Pending Client Holiday Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="ClientHolidayId" HeaderText="Client Holiday Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="HolidayName" HeaderText="Name" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" />
                                        <asp:BoundField DataField="IsOptional" HeaderText="Is Optional" />
                                        <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                                        <asp:BoundField DataField="Operation" HeaderText="Operation" />
                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="btnApproveHoliday" runat="server" Text="Approve" CssClass="btn btn-success btn-md" Width="100px" />&nbsp;&nbsp;
                                <asp:Button ID="btnRejectHoliday" runat="server" Text="Reject" CssClass="btn btn-danger btn-md" Width="100px" />
                                <br />
                                <asp:Label ID="lblMessagePendingHoliday" runat="server"></asp:Label>
                            </div>

                            <%--  --%>
                            <div class="col-lg-4 col-sm-4 text-center">

                                <hr class="small" />
                                <h3 class="text-primary">Add Holiday</h3>
                                <table class="table-condensed">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtHolidayname" runat="server" placeholder="Enter Holiday.." Width="250px" CssClass="form-control input-md"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtHolidayDate" runat="server" placeholder="Enter Date.." Width="250px" CssClass="form-control input-md"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlIsOptional" runat="server" Width="250px" CssClass="form-control input-md"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlClientName" runat="server" Width="250px" CssClass="form-control input-md"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="btnAddHoliday" runat="server" Text="Add" Width="250px" CssClass="btn btn-success btn-md"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblMessageAddHoliday" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 form-group">
                                <asp:Button ID="btnBackShift" href="#shiftDetails" aria-controls="shiftDetails" role="tab" data-toggle="tab" runat="server" class="btn btn-lg btn-primary pull-left" Text="Back" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
