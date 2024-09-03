<%@ Page Title="W & E - Period" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="PeriodMonthMappingEdit.aspx.cs" Inherits="PeriodMonthMappingEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Period Detail
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
        <tr id="trControls" runat="server">
            <td align="center">
                <table border="0" cellspacing="2" width="100%" cellpadding="2">
                    <tr>
                        <td align="left" style="width: 15%">
                            Period :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:DropDownList runat="server" ID="ddlPeriod" CssClass="width130">
                            </asp:DropDownList>
                            <asp:Label runat="server" Visible="false" ID="lblPeriod"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Month
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlDateMonth" Width="100">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Year
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlDateYear" Width="100">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="true" OnClick="btnSave_Click">
                            </asp:Button>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
