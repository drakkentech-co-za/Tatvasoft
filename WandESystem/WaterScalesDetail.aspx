﻿<%@ Page Title="W & E - Water Scale Detail" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="WaterScalesDetail.aspx.cs" Inherits="WaterScalesDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function ValidateDate() {
            var fromMonth = parseInt($("[id$='ddlFromDateMonth']").val()) - 1;
            var fromYear = parseInt($("[id$='ddlFromDateYear']").val());
            var toMonth = parseInt($("[id$='ddlToDateMonth']").val());
            var toYear = parseInt($("[id$='ddlToDateYear']").val());

            var dateFrom = new Date(fromYear, fromMonth, 1);
            var dateTo = new Date(toYear, toMonth, 1);
            dateTo.setDate(dateTo.getDate() - 1);

            if (dateFrom > dateTo) {
                alert("To Date must be gretaer than From Date.");
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Water Scale Detail
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
                        <td align="left">
                            Date From :
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlFromDateMonth" Width="100">
                            </asp:DropDownList>
                            <asp:DropDownList runat="server" ID="ddlFromDateYear" Width="100">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Date To:
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlToDateMonth" Width="100">
                            </asp:DropDownList>
                            <asp:DropDownList runat="server" ID="ddlToDateYear" Width="100">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Limit :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtLimit" runat="server" CssClass="width200" MaxLength="8" onkeyup="ExtractNumber(this,4,false);"
                                onblur="ExtractNumber(this,4,false);" onkeypress="return BlockNonNumbers(this, event, true, false);"
                                onpaste="return false;">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLimit" runat="server" ControlToValidate="txtLimit"
                                ErrorMessage="Enter Limit" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Multiplication factor :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtMultiplicationFactor" runat="server" CssClass="width200" MaxLength="8"
                                onkeyup="ExtractNumber(this,4,false);" onblur="ExtractNumber(this,4,false);"
                                onkeypress="return BlockNonNumbers(this, event, true, false);" onpaste="return false;">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvmultiplicationFactor" runat="server" ControlToValidate="txtMultiplicationFactor"
                                ErrorMessage="Enter Multiplication Factor" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="true" OnClick="btnSave_Click"
                                OnClientClick="return ValidateDate();"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
