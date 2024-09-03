<%@ Page Title="W & E - Change Password" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Change Password</h1>
    <table width="100%" border="0" cellspacing="10" cellpadding="0">
        <tr>
            <td align="left" colspan="2">
                <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 20%;">
                UserName :
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblUserName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                Old Password :
            </td>
            <td align="left">
                <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server" CssClass="width200"
                    autocomplete="off" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqtxtOldPassword" CssClass="required" ControlToValidate="txtOldPassword"
                    ValidationGroup="Submit" ErrorMessage="Password is Mandatory." runat="server"
                    Display="Dynamic" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left">
                New Password :
            </td>
            <td>
                <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" CssClass="width200"
                    autocomplete="off" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqtxtNewPassword" CssClass="required" ControlToValidate="txtNewPassword"
                    ValidationGroup="Submit" ErrorMessage="Password is Mandatory." runat="server"
                    Display="Dynamic" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left">
                Confirm Password :
            </td>
            <td align="left">
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="width200" TextMode="Password"
                    autocomplete="off" MaxLength="50">
                </asp:TextBox>
                <asp:CompareValidator ID="cvConfirmPassword" runat="server" ErrorMessage="CompareValidator"
                    ValidationGroup="Submit" Operator="Equal" Text="*" ForeColor="Red" ControlToCompare="txtConfirmPassword"
                    ControlToValidate="txtNewPassword"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
            <td align="left">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                    ValidationGroup="Submit" CausesValidation="true" OnClick="btnSubmit_Click" OnClientClick="return confirmChange();" />
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        var confirmMsg = 'It will change your password. Are you sure?';
        function confirmChange() {
            if (Page_ClientValidate())
                return confirm(confirmMsg);
            else
                return false;
        }
    </script>
</asp:Content>
