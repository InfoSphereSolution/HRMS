<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="role.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.role" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">
    <style>
        .hidden
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Gainsboro
                row.style.backgroundColor = "D5D8DC";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "White";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
    </script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "#D5D8DC";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "White";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">
        function MouseEvents(objRef, evt) {
            var checkbox = objRef.getElementsByTagName("input")[0];
            if (evt.type == "mouseover") {
                objRef.style.backgroundColor = "#ABB2B9";
            }
            else {
                if (checkbox.checked) {
                    objRef.style.backgroundColor = "#D5D8DC";
                }
                else if (evt.type == "mouseout") {
                    if (objRef.rowIndex % 2 == 0) {
                        //Alternating Row Color
                        objRef.style.backgroundColor = "White";
                    }
                    else {
                        objRef.style.backgroundColor = "white";
                    }
                }
            }
        }
    </script>

    <div class="row text-center">
    </div>
    <div class="row">
        <div class="col-lg-8 col-sm-8 text-center border-right">
            <div>                
                <br />
                <center>
                <table class="table-condensed">
                    <tr>                        
                        <td>
                            <asp:TextBox ID="txtSearchRole" runat="server" CssClass="form-control input-md" placeholder="Enter Role"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-md" OnClick="btnSearch_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnShowAllReoles" runat="server" Text="Show All Roles" CssClass="btn btn-primary btn-md" OnClick="btnShowAllReoles_Click" />
                        </td>
                    </tr>
                </table>
                </center>
            </div>
            <hr class="small" />

            <h3 class="text-primary">List of Roles</h3>

            <center>
            <asp:GridView ID="gvRole" runat="server" DataKeyNames="RoleId" AutoGenerateColumns="False"  OnRowCommand="gvRole_RowCommand" 
                OnRowDataBound="gvRole_RowDataBound" CssClass="table table-hover table-bordered table-condensed"
                HeaderStyle-CssClass="gvHeader">
                <Columns>
                    <asp:BoundField DataField="RoleId" HeaderText="RoleId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="RoleName" HeaderText="RoleName"/>
                    <asp:BoundField DataField="Role_Level" HeaderText="RoleLevel"/>
                    <asp:BoundField DataField="IsActive" HeaderText="IsActive" />
                    <asp:TemplateField HeaderText="Remove">
                        <ItemTemplate>
                            <center>
                            <asp:Button ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("RoleId") %>' Text="X" CssClass="btn btn-danger btn-xs"></asp:Button>
                                </center>
                        </ItemTemplate>
                    </asp:TemplateField>                       
                </Columns>
            </asp:GridView>
                <asp:Label ID="lblMessageRole" runat="server"></asp:Label>
                <br />
                <br />
                <hr class="small" />
                <%--Waiting for approval grid view--%>                 
                <h3 class="text-primary">Pending Requests</h3>
                <br />               

                   <asp:GridView ID="gvTempRole" runat="server" AutoGenerateColumns="False" OnRowDataBound = "RowDataBound"
                       DataKeyNames="TempRoleId" CssClass="table table-bordered table-condensed" HeaderStyle-CssClass="gvHeader">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">                        
                        <HeaderTemplate>                            
                                <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />                            
                        </HeaderTemplate>
                        <ItemTemplate>                            
                                 <asp:CheckBox ID="chkboxSelectRole" runat="server" onclick = "Check_Click(this)" />                            
                        </ItemTemplate>                        
                    </asp:TemplateField>
                    <asp:BoundField DataField="TempRoleId" HeaderText="TempRoleId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="RoleId" HeaderText="RoleId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:BoundField DataField="RoleName" HeaderText="RoleName"/>
                    <asp:BoundField DataField="Role_Level" HeaderText="RoleLevel"/>
                    <asp:BoundField DataField="IsActive" HeaderText="IsActive" />  
                   <asp:BoundField DataField="Operation" HeaderText="Operation" />  
                </Columns>
</asp:GridView>
                <asp:Button ID="btnapprove" runat="server" Text="Approve" OnClick="btnapprove_Click" CssClass="btn btn-success btn-md" Width="100px"/>&nbsp;&nbsp;
                <asp:Button ID="btnreject" runat="server" Text="Reject" OnClick="btnreject_Click" CssClass="btn btn-danger btn-md" Width="100px"/>
                <br />
                <asp:Label ID="lblMessageTempRole" runat="server"></asp:Label>
            </center>
        </div>


        <div class="col-lg-4 col-sm-4 text-center">
            <center>
                <hr class="small" />
                <h3 class="text-primary">Add Role</h3>
            <table class="table-condensed">
                <tr>
                    <td>
                        <asp:TextBox ID="txtrolename" runat="server" placeholder="Enter Role" Width="250px" CssClass="form-control input-md"></asp:TextBox>
                    </td>
                    <td>

                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:DropDownList ID="ddllevel" runat="server" Width="250px" CssClass="form-control input-md"></asp:DropDownList>
                    </td>
                    <td>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <asp:Button ID="btnAddRole" runat="server" Text="Add" Width="250px" OnClick="btnAddRole_Click" CssClass="btn btn-success btn-md"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <center>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </center>
                    </td>
                </tr>
            </table>
                </center>
        </div>
    </div>
</asp:Content>
