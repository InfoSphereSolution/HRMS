<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="SphereInfoSolutionHRMS.Login.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
   
   
        <div role="tabpanel" class="tab-pane" id="PersonalDetails" style="font-weight:bold;">
                    <div id="MyProfile">
                        <div class="col-lg-12 well">
                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="row">
                                        <div class="col-lg-8 col-sm-8 col-xs-12 form-group">
                                        </div>
                                        <div class="col-lg-4 col-sm-4 col-xs-12 form-group">
                                            <asp:Image ID="imgEmployeePicture" CssClass="img-responsive img-circle center-block" Style="padding: 5px" AlternateText="Profile Image" ImageAlign="Middle"  runat="server" />
                                       </div>
                                    </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblname" runat="server" Text="EmployeeName"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="Empname" runat="server"></asp:Label>
                                    </div>
                                   </div>
                                </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-12">
                                  <div class="col-lg-3">
                                        <asp:Label ID="lblcontact" runat="server" Text="Contact No"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblmobile" runat="server"></asp:Label>
                                    </div>
                                            <br /><br />
                                              <div class="row">
                                    <div class="col-lg-12">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lbldes" runat="server" Text="Designation"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lbldesigantion" runat="server"></asp:Label>
                                    </div>
                                   </div>
                                </div>
                                            <br />
                                    <div class="row">
                                        <div class="col-lg-12">
                                  <div class="col-lg-3">
                                        <asp:Label ID="lblclient" runat="server" Text="Client Name"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblclientname" runat="server"></asp:Label>
                                    </div>
                                  </div>
                                    </div>
                                        </div>
                                </div>
                            </div>
                        </div>
            </div>
    
</asp:Content>
