<%@ Page Title="W & E - Manage Rights" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="ManageRights.aspx.cs" Inherits="ManageRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_chkPages").find("td").css("border", "solid 1px #CFCFCF");
            $("#ctl00_ContentPlaceHolder1_chkPages").find("input:checked").parent().css("background-color", "#CFCFCF");
            $("#ctl00_ContentPlaceHolder1_chkPages").find("input[type=checkbox]").bind("click", function () {
                if ($(this).is(":checked")) {
                    $(this).parent().css("background-color", "#CFCFCF");
                }
                else {
                    $(this).parent().css("background-color", "white");
                }
            });           
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Manage Rights
    </h1>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table border="0" cellspacing="2" width="100%" cellpadding="2">
                    <tr>
                        <td align="left" style="width: 15%">
                            Role :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:DropDownList runat="server" ID="ddlRole" CssClass="width200" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            Rights :
                        </td>
                        <td align="left">
                            <asp:CheckBoxList runat="server" ID="chkPages" RepeatColumns="3" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
