<%@ Page Title="" Language="C#" MasterPageFile="~/MasterMain.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SphereInfoSolutionHRMS.Login.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div id="loginForm">
                <%-- <%--loginForm--
            <asp:TextBox ID="txtUserName" runat="server" Placeholder="Username"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" Placeholder="Password" TextMode="Password"></asp:TextBox>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
            <asp:CheckBox ID="cbRememberMe" runat="server" />--%>

                <div class="card card-container">                    
                    <asp:Image ID="profileimg" class="profile-img-card" ImageUrl="~/Images/avatar_2x.png" alt="profile pic" runat="server" />
                    <p id="profile-name" class="profile-name-card"></p>
                    <div id="form1" class="form-signin">
                        <span id="reauth-email" class="reauth-email"></span>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Style="border-radius: 13px; height: 30px; font-weight: bold;" placeholder="Username"></asp:TextBox>
                        <br />
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Style="border-radius: 13px; height: 30px; font-weight: bold;" placeholder="Password" TextMode="Password"></asp:TextBox>
                        <br />
                        <center>                      
             <label class="checkbox pull-left">                    
        <asp:CheckBox id="cbRememberMe" type="checkbox" value="remember-me" runat="server" ForeColor="White"></asp:CheckBox><font color="White">Remember me</font></label>          
  <asp:Button id="btnLogin"  runat="server" Text="Login" class="btn btn-primary btn-md" width="100px" OnClick="btnLogin_Click" /> 
                            <br />
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                        <br />
                    <a href="#" class="forgot-password text-center">Forgot  Password?</a>
                       </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
