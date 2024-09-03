<%@ Page Title="W & E - Water Rate Detail" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WaterRateLimitsDetail.aspx.cs" Inherits="WaterRateLimitsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />    
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
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

    $(document).ready(function () {
        $(".chzn-select").chosen();       
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>
        Water Rate Limit Detail
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
                            Start Date :
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
                            End Date:
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
                            Rate1 :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtRate1" runat="server" CssClass="width200" MaxLength="8" onkeyup="ExtractNumber(this,4,false);"
                                onblur="ExtractNumber(this,4,false);" onkeypress="return BlockNonNumbers(this, event, true, false);"
                                onpaste="return false;">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRate1" runat="server" ControlToValidate="txtRate1"
                                ErrorMessage="Enter Rate1" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Rate2 :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtRate2" runat="server" CssClass="width200" MaxLength="8"
                                onkeyup="ExtractNumber(this,4,false);" onblur="ExtractNumber(this,4,false);"
                                onkeypress="return BlockNonNumbers(this, event, true, false);" onpaste="return false;">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRate2" runat="server" ControlToValidate="txtRate2"
                                ErrorMessage="Enter Rate2" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 15%">
                            Rate3 :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtRate3" runat="server" CssClass="width200" MaxLength="8"
                                onkeyup="ExtractNumber(this,4,false);" onblur="ExtractNumber(this,4,false);"
                                onkeypress="return BlockNonNumbers(this, event, true, false);" onpaste="return false;">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRate3" runat="server" ControlToValidate="txtRate3"
                                ErrorMessage="Enter Rate3" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td align="left" style="width: 15%">
                            House Type:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:DropDownList ID="ddlHouseType" runat="server" Width="300" data-placeholder="Select House Type"
                                class="chzn-select">
                            </asp:DropDownList>
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

