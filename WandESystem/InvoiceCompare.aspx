<%@ Page Title="W & E - Invoice Compare" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="InvoiceCompare.aspx.cs" Inherits="InvoiceCompare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
        });
    </script>
    <script type="text/javascript">

        function checkStatus() {
            //        if ($("a[id*=lnkApprove]").length == 0) {
            //            // $(".col1").hide();
            //            $("input[id*=chkHeader]").hide();
            //            $("tr[id*=trSelectAll]").hide();
            //        }
        }

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            <tr id="Tr1" runat="server">
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
        <tr>
            <td>
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </td>
        </tr>
        <tr id="trSelectAll" runat="server">
            <td>
                <div style="background: #FFF; border: 1px solid #d4d4d4; margin: 8x; text-align: left;
                    padding: 5px !important; min-height: 35px;">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnAcceptMatched" Text="Accept All Matched" CssClass="btn_medium"
                                    OnClick="btnAcceptMatched_Click" />
                            </td>
                            <%--<td align="right">
                                <asp:Button runat="server" ID="btnApproveSelected" Text="Accept Selected" CssClass="btn_medium"
                                    OnClientClick="return checkValidate();" OnClick="btnApproveSelected_Click" />
                                &nbsp;
                                <asp:Button runat="server" ID="btnRejectSelected" Text="Reject Selected" CssClass="btn_medium"
                                    OnClientClick="return checkValidate();" OnClick="btnApproveSelected_Click" />
                            </td>--%>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvInvoice" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="false" SkinID="GridView" OnPageIndexChanging="gvInvoice_PageIndexChanging"
                    OnRowCommand="gvInvoice_RowCommand" OnRowDataBound="gvInvoice_RowDataBound" OnSorting="gvInvoice_Sorting">
                    <Columns>                       
                        <asp:TemplateField HeaderText="Account  No" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            ItemStyle-Width="10%" SortExpression="AccountNo">
                            <ItemTemplate>
                                <asp:LinkButton Text='<% #Eval("AccountNo") %>' runat="server" ID="lnkAccountNo"
                                    CommandName="EditInvoice" CommandArgument='<%#Eval("HouseId") %>' />
                                <asp:Label ID="lblAccountNo" runat="server" Text='<% #Eval("AccountNo") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Roshskor Amount" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-VerticalAlign="Middle" ItemStyle-Width="15%" SortExpression="RAmountInc">
                            <ItemTemplate>
                                <%# String.Format("{0:0.00}", Eval("RAmountInc"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--                        <asp:TemplateField HeaderText="Amount(Excl)" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            ItemStyle-Width="12%" SortExpression="AmountEx">
                            <ItemTemplate>
                                <%# String.Format("{0:0.0000}", Eval("AmountEx"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VAT" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            ItemStyle-Width="12%" SortExpression="VAT">
                            <ItemTemplate>
                                <%# String.Format("{0:0.0000}", Eval("VAT"))%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="System Amount" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            ItemStyle-Width="12%" SortExpression="SAmountInc">
                            <ItemTemplate>
                                <%# String.Format("{0:0.00}", Eval("SAmountInc"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Difference Amount" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-VerticalAlign="Middle" ItemStyle-Width="12%" SortExpression="Difference">
                            <ItemTemplate>
                                <%# String.Format("{0:0.00}", Eval("Difference"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                            ItemStyle-Width="15%" SortExpression="HouseAddress">
                            <ItemTemplate>
                                <%# Eval("HouseAddress")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblApproveReject" Visible="false"></asp:Label>
                                <asp:LinkButton runat="server" ID="lnkApprove" Text="Approve" CommandName="ApproveInvoice"
                                    CommandArgument='<%#Eval("HouseId") %>' />
                                &nbsp;
                                <asp:LinkButton runat="server" ID="lnkAcceptRoshskorAmount" Text="Approve RoshSkor Amount" CommandName="AcceptRoshskorInvoice"
                                    CommandArgument='<%#Eval("HouseId") %>' Visible="false" OnClientClick ="return confirmation();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
