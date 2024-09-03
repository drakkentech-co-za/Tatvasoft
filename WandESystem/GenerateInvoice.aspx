<%@ Page Title="W & E - Generate Invoice" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="GenerateInvoice.aspx.cs" Inherits="GenerateInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").css("color", "black");
            $("body").css("font-size", "12px");
            $(".chzn-select").chosen();
        });             
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form_field top_pad">
        <div class="content_block" style="border-top: 1px solid #d4d4d4">
            <table border="0" cellpadding="2" cellspacing="5">
                <tr>
                    <td align="left">
                        Account No
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAccountNo" runat="server" CssClass="chzn-select" data-placeholder="Select Account Number">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="ddlAccountNo" Text="*"
                            runat="server" ID="reqAccount" InitialValue="0" ValidationGroup="vgGenInvoice"
                            ForeColor="Red" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="left">
                        Month-Year:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlPeriodId" runat="server" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button Text="Generate" runat="server" ID="btnGenerateInvoice" OnClick="btnGenerateInvoice_Click"
                            ValidationGroup="vgGenInvoice" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="display:inline; float:right;">
        <a href="Javascript:void(0);" onclick="return PrintChart();" id="aprintInvoice" runat="server"
            visible="false">
            <img src="App_Themes/Default/images/printButton.png" alt="Print" /></a>
    </div>
    <div id="dvInvoice" runat="server" visible="false">
        <table cellspacing="2" cellpadding="5" border="0">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="left" width="33%" valign="top">
                                RoshSkor
                                <br />
                                Township (Pty) Ltd
                            </td>
                            <td valign="bottom" align="center" width="34%" class="transformUpper">
                                TAX INVOICE
                            </td>
                            <td width="33%">
                                <table width="100%">
                                    <tr>
                                        <td align="left">
                                            P.O. Box40
                                        </td>
                                        <td align="left">
                                            <span class="textBoldUnderline">Tel:</span> &nbsp; -264(63) 290 070
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="transformUpper">
                                            ROSH PINAH
                                        </td>
                                        <td align="left">
                                            <span class="textBoldUnderline">Fax:</span> &nbsp; -264(63) 274 187
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="transformUpper">
                                            NAMIBIA
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <b>VAT Reg NO: 2684.567.015 </b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="transformUpper">
                    if account is not paid before or on due date, services will be discontinued without
                    any further notice
                </td>
            </tr>
            <tr>
                <td>
                    <div style="float: left; width: 48%; border: 1px solid black; padding: 5px;">
                        <p class="transformUpper">
                            Rosh Pinah Zinc Corporation
                        </p>
                        <p>
                            Private Bag 2001
                        </p>
                        <p>
                            Rosh Pinah
                        </p>
                    </div>
                    <div style="float: right; width: 49%; border: 1px solid black;">
                        <table width="100%" cellspacing="2" cellpadding="2" border="0">
                            <tr>
                                <td align="left" width="15%">
                                    Stand Nr:
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblStandNr"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Extension:
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblExtension"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Location:
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblLocation"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Zoning:
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblZoning">Rosh Pinah</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Portion:
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblPortion"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Area:
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblArea"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="clear">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="float: left; width: 48%; border: 1px solid black; padding: 5px; min-height: 35px;">
                        <b>Invoice No </b>&nbsp;
                        <asp:Label runat="server" ID="lblInvoiceNumber"></asp:Label>
                    </div>
                    <div style="float: right; width: 49%; min-height: 50px;">
                        <table width="68%" style="border: 1px solid black; float: left;" cellspacing="0"
                            cellpadding="1">
                            <tr>
                                <td align="left" width="20%" style="border-bottom: 1px solid black !important;">
                                    <b>Date</b>
                                </td>
                                <td align="left" width="30%" style="border-bottom: 1px solid black !important;">
                                    <asp:Label runat="server" ID="lblDate"></asp:Label>
                                </td>
                                <td align="left" width="20%" style="border-bottom: 1px solid black !important;">
                                    <b>Acc Nr</b>
                                </td>
                                <td align="left" width="30%" style="border-bottom: 1px solid black !important;">
                                    <asp:Label runat="server" ID="lblAccountNumber"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table width="28%" style="border: 1px solid black; float: right;" cellspacing="0"
                            cellpadding="3">
                            <tr>
                                <td align="left" style="border-bottom: 1px solid black !important;">
                                    Deposits
                                </td>
                                <td align="right" style="border-bottom: 1px solid black !important;">
                                    <asp:Label runat="server" ID="lblDeposits" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Terms
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblTerms" Text="Current"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="clear">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater ID="rptParentInvoice" runat="server" OnItemDataBound="rptParentInvoice_ItemDataBound">
                        <HeaderTemplate>
                            <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                <tr>
                                    <th width="150px" align="left">
                                        <u>Date</u>
                                    </th>
                                    <th width="200px" align="left">
                                        <u>Reference</u>
                                    </th>
                                    <th width="200px" align="left">
                                        <u>Description</u>
                                    </th>
                                    <th width="100px" align="right">
                                        <u>Amount (Excl)</u>
                                    </th>
                                    <th width="100px" align="right">
                                        <u>VAT</u>
                                    </th>
                                    <th width="100px" align="right">
                                        <u>Amount (Incl)</u>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th colspan="6" align="left">
                                    <asp:Label runat="server" ID="lblCategory"></asp:Label>
                                </th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptChildInvoice">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Created_TS", "{0:yyyy/MM/dd}") %>
                                        </td>
                                        <td>
                                            <%# Eval("cReference") %>
                                        </td>
                                        <td>
                                            <%# Eval("Description") %>
                                        </td>
                                        <td align="right">
                                            <%# Eval("SAmountEx")%>
                                        </td>
                                        <td align="right">
                                            <%# Eval("SVAT")%>
                                        </td>
                                        <td align="right">
                                            <%# Eval("SAmountInc")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="6">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                </td>
                                <td colspan="2" align="right">
                                    <b>Total Amount Due</b>
                                </td>
                                <td style="border: 1px solid black !important;" align="right">
                                    <asp:Label runat="server" ID="lblTotal"></asp:Label>
                                </td>
                            </tr>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" style="border: 1px solid black;" cellspacing="2" cellpadding="2">
                        <tr>
                            <td align="left" width="60%">
                                <b>Banking Details:</b>
                            </td>
                            <td width="40%">
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 30px;">
                                <p>
                                    <b>First National Bank </b>
                                    <br />
                                    Branch: Rosh Pinah
                                    <br />
                                    Branch Code: 280-177
                                    <br />
                                    Cheque AccountNumber: 62021392824
                                    <br />
                                    <b>Please use your Account Number as Reference </b>
                                </p>
                            </td>
                            <td style="font-weight: bold; padding-left: 30px;">
                                <p class="transformUpper">
                                    !!! PLEASE FAX PROOF OF PAYMENT !!!
                                    <br />
                                    Duplicate INV. Will be Charged N$5.00
                                    <br />
                                    Due Date - 21ST OF Each Month
                                </p>
                                <br />
                                Interest of 2% will be charged on all overdue accounts
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <iframe id="printf" name="printf" style="width: 0; height: 0; text-align: center;
        display: none;"></iframe>
    <script type="text/javascript" language="javascript">
        function PrintChart() {
            $("#printf").show();
            var content = document.getElementById("ctl00_ContentPlaceHolder1_dvInvoice");
            if (navigator.appName == "Microsoft Internet Explorer") {
                var pri = document.getElementById('printf').contentWindow || document.getElementById('printf').contentDocument;
                Popup(content.innerHTML);
            } else {
                var pri = document.getElementById("printf").contentWindow;
                pri.document.open();
                pri.document.write(content.innerHTML);
                pri.document.close();
                pri.focus();
                pri.print();
            }
            $("#printf").hide();
            return false;
        }

        function Popup(data) {
            var newWin = window.frames["printf"];
            newWin.document.write('<head>  <link rel="stylesheet" type="text/css" href="App_Themes/Default/css/style.css" /></style> </head><body onload="window.print()">' + data + '</body>');
            newWin.focus();
            newWin.document.close();
        }
    </script>
</asp:Content>
