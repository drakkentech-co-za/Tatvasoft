<%@ Page Title="W & E System - Reset Password" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Reset Password
    </h1>
    <table width="100%" border="0" cellspacing="10" cellpadding="0">
        <tr>
            <td align="left" colspan="2">
                <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 20%;">
                User Name :
            </td>
            <td align="left">
                <asp:DropDownList runat="server" ID="ddlUserName" CssClass="width200"  AutoPostBack="true"
                    onselectedindexchanged="ddlUserName_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="rfvUserName" ControlToValidate="ddlUserName"
                    InitialValue="0" ErrorMessage="Select User Name" Text="*" Display="Dynamic"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trCurrentPassword" runat="server">
            <td align="left">
                Current Password :
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblCurrentPassword"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                New Password :
            </td>
            <td>
                <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" CssClass="width200"
                    autocomplete="off" MaxLength="20"></asp:TextBox>
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
                    autocomplete="off" MaxLength="20">
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
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Submit"
                    CausesValidation="true" OnClick="btnSave_Click" OnClientClick="return confirmChange();" />
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        var confirmMsg = 'It will reset password of selected username. Are you sure to continue?';
        function confirmChange() {
            if (Page_ClientValidate())
                return confirm(confirmMsg);
            else
                return false;
        }
    </script>
</asp:Content>
