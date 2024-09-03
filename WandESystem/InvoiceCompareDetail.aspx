<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="InvoiceCompareDetail.aspx.cs" Inherits="InvoiceCompareDetail" %>

<%@ Register Src="~/UserControls/ctlPager.ascx" TagName="ctlPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();

            $("tr[id*=trDetails]").each(function () {
                if (parseFloat($(this).attr("diff")) != 0) {
                    $(this).css("background", "#f8dedc url(../images/ic-error.png) 10px 7px no-repeat");
                    $(this).css("color", "#9c413a");
                }
            });

        });

        //        function checkAll(cntrl) {
        //            $(".checkMe").children().prop("checked", cntrl.checked);
        //        }

        //        function checkMe() {
        //            var count = $(".checkMe").length;
        //            var inc = 0;
        //            $(".checkMe").each(function () {
        //                if ($(this).children().is(":checked")) {
        //                    inc++;
        //                }
        //            });

        //            if (count == inc) {
        //                $("#ctl00_ContentPlaceHolder1_chkSelectAll").prop("checked", true);
        //            }
        //            else {
        //                $("#ctl00_ContentPlaceHolder1_chkSelectAll").prop("checked", false);
        //            }
        //        }

        //        function checkStatus() {            
        //            if ($("input[id*=btnInnerAccept]").length == 0) {
        //                $("tr[id*=trSelectAll]").hide();
        //            }           
        //        }

        function confirmation() {
            if (confirm("RoshSkor Amount and System Calculated Amount does not match, this will  accpect the RoshSkor's Invoice Amount and will count this amount in Reports and Charts.\n\nAre you sure you want to continue?")) {
                if (confirm("Are you sure you want to continue?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }   
    </script>
    <style type="text/css">
        .row1 td, .row2 td, .grid th
        {
            border: solid thin #cfcfcf;
        }
        table
        {
            border-collapse: collapse;
        }
        .innerTable
        {
            padding: 0px;
        }
        .innerTable td
        {
            border: solid 1px #cfcfcf;
            vertical-align: top;
            text-align: center;
            padding: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <table border="0" cellspacing="2" width="100%" cellpadding="2">
                    <tr>
                        <td>
                            <div class="form_field top_pad">
                                <div class="titlebg">
                                    <h2>
                                        Search Criteria
                                    </h2>
                                </div>
                                <div class="content_block">
                                    <table border="0" cellspacing="2" width="100%" cellpadding="2">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="left">
                                                            Account:
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList runat="server" ID="ddlAccountNumber" CssClass="width200 chzn-select"
                                                                data-placeholder="Select Account">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            Filter By:
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList runat="server" ID="ddlFilterBy" CssClass="width130">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            Month-Year:
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlPeriodId" runat="server" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button runat="server" ID="btnCompare" Text="Compare" CausesValidation="false"
                                                                OnClick="btnCompare_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr id="trSelectAll" runat="server" visible="false">
                        <td>
                            <div style="background: #FFF; border: 1px solid #d4d4d4; margin: 8x; text-align: left;
                                padding: 5px !important; min-height: 35px;">
                                <table width="100%">
                                    <tr>
                                        <td align="left">
                                            <asp:CheckBox runat="server" ID="chkSelectAll" Text="Select All" onclick="checkAll(this);" />
                                        </td>
                                        <td align="right">
                                            <asp:Button runat="server" ID="btnApproveSelected" Text="Accept Selected" CssClass="btn_medium"
                                                OnClick="btnApproveSelected_Click" />
                                            &nbsp;
                                            <asp:Button runat="server" ID="btnRejectSelected" Text="Reject Selected" CssClass="btn_medium"
                                                OnClick="btnApproveSelected_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="lblMessage"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trTopPager">
                        <td>
                            <uc1:ctlPager ID="Pager1" runat="server" OnPageIndexChange="Pager1_Pageindexchange"
                                CssClass="paging" PageSize="20" PagesToDisplay="5" />
                        </td>
                    </tr>
                    <tr id="trData">
                        <td>
                            <asp:Repeater runat="server" ID="rptParentInvoiceCompare" OnItemDataBound="rptParentInvoiceCompare_ItemDataBound"
                                OnItemCommand="rptParentInvoiceCompare_ItemCommand">
                                <ItemTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="8" class="g_grid">
                                        <tr class="g_main">
                                            <th runat="server" id="thHeadInfo" align="left" style="border-right: none">
                                                <asp:CheckBox runat="server" ID="chkSelect" class="checkMe" onclick="checkMe();"
                                                    value='<%# Eval("HouseId") %>' Visible="false" />
                                            </th>
                                            <th style="border: none">
                                                <asp:Label ID="Label1" Text='<%#"AccountNo: " + Eval("AccountNo")%>' runat="server" />
                                                <br />
                                                <asp:Label ID="Label2" Text='<%#"Address: " + Eval("HouseAddress")%>' runat="server" />
                                            </th>
                                            <th style="border: none">
                                            </th>
                                            <th>
                                                Roshskor Details
                                            </th>
                                            <th colspan="3">
                                                System Calculation
                                            </th>
                                            <th>
                                            </th>
                                        </tr>
                                        <tr class="g_sub">
                                            <th width="110px">
                                                Reference
                                            </th>
                                            <th width="185px">
                                                Tariff Category
                                            </th>
                                            <th width="160px">
                                                Description
                                            </th>
                                            <th width="100px">
                                                Amounnt(Incl)
                                            </th>
                                            <th width="75px">
                                                Amount(Excl)
                                            </th>
                                            <th width="25px">
                                                VAT
                                            </th>
                                            <th width="70px">
                                                Amount(Incl)
                                            </th>
                                            <th width="60px">
                                                Difference
                                            </th>
                                        </tr>
                                        <asp:Repeater runat="server" ID="rptInvoiceCompare">
                                            <ItemTemplate>
                                                <tr id="trDetails_<%# Eval("HouseId")%>_<%# Eval("TeriffId")%>" diff='<%# Eval("Difference")%>'>
                                                    <td>
                                                        <%# Eval("cReference") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CategoryName") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Description") %>
                                                    </td>
                                                    <td align="center">
                                                        <%# Eval("RAmountInc")%>
                                                    </td>
                                                    <td align="center">
                                                        <%# Eval("SAmountEx")%>
                                                    </td>
                                                    <td align="center">
                                                        <%# Eval("SVAT")%>
                                                    </td>
                                                    <td align="center">
                                                        <%# Eval("SAmountInc")%>
                                                    </td>
                                                    <td align="center">
                                                        <%# Eval("Difference")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr class="g_total">
                                                    <th colspan="3" align="right">
                                                        Total
                                                    </th>
                                                    <th id="thRAmt" runat="server" align="center">
                                                    </th>
                                                    <th id="thSAmtEx" runat="server" align="center">
                                                    </th>
                                                    <th id="thSVat" runat="server" align="center">
                                                    </th>
                                                    <th id="thSAmtIn" runat="server" align="center">
                                                    </th>
                                                    <th id="thSDiff" runat="server" align="center">
                                                    </th>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <tr id="trAccRej" runat="server" class="g_btn">
                                            <td colspan="8" style="border:none;">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="left"  valign="top" style="border:none;">
                                                            <asp:Label runat="server" ID="lblReason" Text="Comments :"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" Columns="100" Rows="2"
                                                                onkeypress="return CheckMaxLength(event,this,200);" onblur="return CheckMaxLength(event,this,1000);"
                                                                onkeyup="return CheckMaxLength(event,this,1000);" Visible="false"></asp:TextBox>
                                                            <asp:Label runat="server" ID="lblSavedReason" Visible="false"></asp:Label>
                                                        </td>
                                                        <td align="right" style="padding: 10px; border: none;">
                                                            <asp:Button runat="server" ID="btnInnerAccept" Text="Accept" CausesValidation="false"
                                                                CommandArgument='<%# Eval("HouseId") %>' CommandName="Accept" />
                                                            &nbsp;
                                                            <asp:Button runat="server" ID="btnAcceptRoshskorAmount" Text="Approve RoshSkor Amount"
                                                                CausesValidation="false" CommandArgument='<%# Eval("HouseId") %>' CommandName="AcceptRoshskorInvoice"
                                                                CssClass="btn_large" Visible="false" OnClientClick="return confirmation();"/>
                                                            <asp:Label runat="server" ID="lblStatus" Visible="false" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <tr>
                                        <td style="min-height: 5px; border: none;" colspan="8">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </SeparatorTemplate>
                            </asp:Repeater>
                            <asp:Label runat="server" Visible="false" ID="lblNodata" />
                        </td>
                    </tr>
                    <tr id="trBottomPager">
                        <td>
                            <uc1:ctlPager ID="Pager2" runat="server" OnPageIndexChange="Pager1_Pageindexchange"
                                CssClass="paging" PageSize="20" PagesToDisplay="5" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
