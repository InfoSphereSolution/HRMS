<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterHome.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="SphereInfoSolutionHRMS.Admin.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="homeContentPlaceHolder" runat="server">

    <script type="text/javascript">
        function validate() {
            var txt1 = document.getElementById('<%=txtoldpwd.ClientID%>').value;
                var txt2 = document.getElementById('<%=txtnewpwd.ClientID%>').value;
                var txt3 = document.getElementById('<%=txtconpwd.ClientID%>').value;
                if (!txt1 && !txt2 && !txt3) {
                    true
                }
                else if ((txt1 && !txt2 && !txt3) || (!txt1 && !txt)) {
                    alert("Please fill both textbox");
                    false
                }
                true
            }
</script>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

            <div class="row text-center">

            </div>
    <div class="col-lg-8 col-sm-8 text-center">
            <div class="row">
                <hr class="small" />
                <h3 class="text-primary"> Change  Password</h3>

                <table class="table-responsive  table-condensed">
                    <tr>
                 <%--       <td>Enter Old Password&nbsp;&nbsp;</td>
                 --%>       <td><asp:TextBox ID="txtoldpwd" runat="server" Width="250px" CssClass="form-control input-mid" placeholder="Enter Old Password"></asp:TextBox></td>
                    </tr>
                    <tr>
               <%--         <td>Enter New Password&nbsp;&nbsp;</td>
               --%>         <td><asp:TextBox ID="txtnewpwd" runat="server" Width="250px" CssClass="form-control input-mid" placeholder="Enter New Password" TextMode="Password"></asp:TextBox></td>
                    </tr>
                   
                    <tr>
               <%--         <td>Enter Confirm Password</td>
               --%>         <td><asp:TextBox ID="txtconpwd" runat="server" Width="250px" CssClass="form-control input-mid" placeholder="Enter Confirm Password" TextMode="Password"></asp:TextBox> </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnsubmit" runat="server" Width="250px" CssClass="btn btn-success btn-md" Text="Submit" OnClientClick="validate();"  OnClick="btnsubmit_Click" />
                        </td>
                    </tr>
                </table>

            </div>
      </div>
                           
                            </ContentTemplate>

                        </asp:UpdatePanel>
</asp:Content>
