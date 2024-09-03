<%@ Page Title="W & E System - UserProfile" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Employee Profile
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
                <table border="0" cellspacing="2" width="100%" cellpadding="8" cellspacing="5">
                    <tr>
                        <td align="left" style="width: 15%">
                            <b>Employee Number :</b>
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:Label runat="server" ID="lblEmployeeNumber"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Full Name :</b>
                        </td>
                        <td align="left">
                            <asp:Label runat="server" ID="lblFullName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                           <b>Email :</b>
                        </td>
                        <td align="left">
                            <asp:Label runat="server" ID="lblEmail"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="repAccountDetails">
                        <ItemTemplate>
                            <tr>
                                <td align="left">
                                    <b>Account Number :</b>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblAccountNumber" Text='<%# Eval("AccountNumber") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>Address :</b>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblAddress" Text='<%# Eval("Address") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var backgorundImage = '<%= ResolveClientUrl("~/App_Themes/Default/images/RPZCwatermark.png")%>';
        $(document).ready(function () {
            $("div[id=middle]").css("background", "url(" + backgorundImage + ") no-repeat 250px");
        });
    </script>
</asp:Content>
