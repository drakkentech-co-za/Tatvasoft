<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>W & E - Log In </title>
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="App_Themes/Default/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function noBack() { window.history.forward() }
        noBack();
        window.onload = noBack;
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }
    </script>
</head>
<body>
    <div class="login_area" id="login">
        <!--Login Area Start-->
        <div class="login_top">
        </div>
        <div class="login_middle">
            <form id="form1" runat="server">
            <div class="login_header">
                <div class="login_logo">
                    <img src="App_Themes/default/images/RPZCLogo.png" alt="" /></div>
                <div class="login_title">
                    <span>Water Electricity & Token System</span>
                </div>
                <div class="key_img">
                    <img src="App_Themes/default/images/key.png" alt="" /></div>
            </div>
            <div id="dvLogin" class="login_content">
                <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnLogin">
                    <table width="100%" border="0" cellspacing="10" cellpadding="0">
                        <tr>
                            <td width="120" align="right">
                                &nbsp;
                            </td>
                            <td>
                                <h2>
                                    Login
                                </h2>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Username:
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="width225" MaxLength="250"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqtxtUserName" CssClass="required" ControlToValidate="txtUserName"
                                    ValidationGroup="Submit" ErrorMessage="User Name is Mandatory." runat="server"
                                    Display="Dynamic" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Password:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="width225"
                                    MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqtxtPassword" CssClass="required" ControlToValidate="txtPassword"
                                    ValidationGroup="Submit" ErrorMessage="Password is Mandatory." runat="server"
                                    Display="Dynamic" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnLogIn" runat="server" Text="LOGIN" ValidationGroup="Submit" CausesValidation="true"
                                    OnClick="btnLogIn_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            </form>
        </div>
        <div class="login_bottom">
        </div>
    </div>
</body>
</html>
