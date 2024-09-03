<%@ Page Title="W & E - Token Sheet Management" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="TokenSheetManagement.aspx.cs" Inherits="TokenSheetManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <link href="ChoosenJS/jquery-ui.css" rel="stylesheet" />
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var today = new Date();
            var fromdate = new Date(today.getFullYear(), today.getMonth() - 1, today.getDate());

            $(".datefrom").datepicker({
                dateFormat: "dd/mm/yy",
                defaultDate: fromdate,
                changeMonth: true,
                changeYear: true,
                yearRange: '1901:' + new Date().getFullYear,
                maxDate: new Date("31/12/" + parseInt(today.getFullYear())),
                numberOfMonths: 1
            });

            $(".dateto").datepicker({
                dateFormat: "dd/mm/yy",
                defaultDate: today,
                maxDate: new Date("31/12/" + parseInt(today.getFullYear())),
                yearRange: '1901:' + new Date().getFullYear,
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1
            });
        });

        function ValidateDate() {
            var dateFrom = $(".datefrom").datepicker("getDate");
            var dateTo = $(".dateto").datepicker("getDate");

            if (dateFrom <= dateTo || dateFrom == null || dateTo == null)
                return true;

            alert("Select Proper Time Interval");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Token Sheet Management
    </h1>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form_field top_pad">
                    <div class="titlebg">
                        <h2>
                            Token Period Settings
                        </h2>
                    </div>
                    <div class="content_block">
                        <table border="0" cellspacing="2" width="100%" cellpadding="2">
                            <tr>
                                <td align="left" width="10%">
                                    Start Day :
                                </td>
                                <td align="left" width="25%">
                                    <asp:DropDownList runat="server" ID="ddlStartDay" CssClass="width200">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" width="10%">
                                    End Day :
                                </td>
                                <td align="left" width="25%">
                                    <asp:DropDownList runat="server" ID="ddlEndDay" CssClass="width200">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" width="30%">
                                    <asp:Button ID="btnSave" runat="server" Text="Update" CausesValidation="true" OnClick="btnSave_Click"
                                        OnClientClick="return confirm('Are you sure you want to continue with your change?')">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form_field top_pad">
                    <div class="titlebg">
                        <h2>
                            Export Token Sheet
                        </h2>
                    </div>
                    <div class="content_block">
                        <table border="0" cellspacing="2" width="100%" cellpadding="2">
                            <tr>
                                <td align="right">
                                    <asp:Button runat="server" ID="btnGenerate" CausesValidation="false" Text="Generate Token Sheet"
                                        OnClick="btnGenerate_Click" CssClass="btn_large" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form_field top_pad">
                    <div class="titlebg">
                        <h2>
                            Import Token Sheet
                        </h2>
                    </div>
                    <div class="content_block">
                        <table border="0" cellspacing="2" width="100%" cellpadding="2">
                            <tr>
                                <td align="left" width="10%">
                                    Select File :
                                </td>
                                <td align="left" width="30%">
                                    <asp:FileUpload ID="fuTokenSheet" runat="server" CssClass="width225" />
                                    <asp:RequiredFieldValidator runat="server" ID="rfvTokenSheet" Display="Dynamic" ControlToValidate="fuTokenSheet"
                                        ErrorMessage="Please Select File to Import" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td width="20%">
                                    <asp:HyperLink runat="server" ID="hpSampleTokenSheet" NavigateUrl="~/Sample Files/TokenSheet.xlsx" Target="_blank" Text="Sample Token Sheet" ></asp:HyperLink>
                                </td>
                                <td align="right">
                                    <asp:Button runat="server" ID="btnImport" CausesValidation="true" Text="Import Token Sheet"
                                        OnClick="btnImport_Click" CssClass="btn_large" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form_field top_pad">
                    <div class="titlebg">
                        <h2>
                            Requested Token Details
                        </h2>
                    </div>
                    <div class="content_block">
                        <table border="0" cellspacing="2" width="100%" cellpadding="2">
                            <tr>
                                <td align="left" width="10%">
                                    From Date :
                                </td>
                                <td align="left" width="25%">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="width210 datepicker datefrom"
                                        onpaste="return false;" onkeypress='MakeReadOnly(event);' MaxLength="10"></asp:TextBox>
                                </td>
                                <td align="left" width="10%">
                                    To Date :
                                </td>
                                <td align="left" width="25%">
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="width210 datepicker dateto"
                                        onpaste="return false;" onkeypress='MakeReadOnly(event);' MaxLength="10"></asp:TextBox>
                                </td>
                                <td align="right" width="30%">
                                    <asp:Button runat="server" ID="btnDownloadRequestedToken" CausesValidation="true"
                                        Text="Download Request" OnClick="btnDownloadRequestedToken_Click" CssClass="btn_large" OnClientClick="return ValidateDate();"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
