<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="HouseTeriffMapping.aspx.cs" Inherits="HouseTeriffMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
        });

        function validateCategories() {
            if ($("#ctl00_ContentPlaceHolder1_ddlAccountNo").val() == 0) {
                alert("Please select Account");
                $(".chosen-search input[type=text]").focus();
                return false;
            }

            var count = 0;
            $('.checkme').each(function () {
                if ($(this).children().is(":checked")) {
                    count++;
                }
            });

            if (count == 0) {
                alert("Please select Tariff Categories")
                return false;
            }
            else {
                return true;
            }
        }

        function setDefaultValue(cntrl) {

            var selected = $(cntrl).parent().parent().find("input[id*=chkSelect]").is(":checked");
            if (selected) {
                if (parseInt($(cntrl).val()) == 0) {
                    $(cntrl).val("1");
                }
            }            

            if ($(cntrl).val() == "") {
                if (selected) {
                    $(cntrl).val("1");
                }
                else {
                    $(cntrl).val("0");
                }                
            }
        }

        function checkTariff(e) {
            if ($(e).is(":checked")) {
                $(e).parent().parent().parent().find("input[id*=txtAdditionalTeriff]").val("1");
            }
            else {
                $(e).parent().parent().parent().find("input[id*=txtAdditionalTeriff]").val("0");
            }
        }

        function txtOnblur(e) {
            
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Account - Tariff Mapping</h1>
        <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <table border="0" cellspacing="2" width="98%" cellpadding="2">
                    <tr>
                        <td align="left" style="width: 86%">
                            Account No: &nbsp;
                            <asp:DropDownList ID="ddlAccountNo" runat="server" Width="300" data-placeholder="Select Account Number"
                                class="chzn-select" DataTextField="Account" DataValueField="HouseId" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAccountNo_SelectedIndexChanged">
                            </asp:DropDownList>&nbsp;
                            <asp:Label runat="server" id="lblAccountAddress"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div class="titlebg">
                                <h2>
                                    Tariff Category
                                </h2>
                            </div>
                            <div class="content_block" style="padding: 0px; margin: 0px;">
                                <asp:Repeater runat="server" ID="rptHouseTeriff" OnItemDataBound="rptHouseTeriff_ItemDataBound">
                                    <HeaderTemplate>
                                        <table cellpadding="2" cellspacing="0" class="grid" width="973px">
                                            <tr>
                                                <th width="70px">
                                                    Select
                                                </th>
                                                <th align="left">
                                                    Tariff Id
                                                </th>
                                                <th align="left">
                                                    Tariff Name
                                                </th>
                                                <th align="left">
                                                    Units
                                                </th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="row1">
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="chkSelect" value='<%# Eval("TeriffId")%>' CssClass="checkme" onclick="checkTariff(this)"/>
                                            </td>
                                            <td>
                                                <%# Eval("CategoryValue")%>
                                            </td>
                                            <td>
                                                <%# Eval("CategoryName")%>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtAdditionalTeriff"  onkeypress="return OnlyNumeric(event)" onblur="setDefaultValue(this)" Text='<%# Eval("AdditionalTeriff")%>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="row2">
                                            <td align="center">
                                                <asp:CheckBox runat="server" ID="chkSelect" value='<%# Eval("TeriffId")%>' CssClass="checkme" onclick="checkTariff(this)"/>
                                            </td>
                                            <td>
                                                <%# Eval("CategoryValue")%>
                                            </td>
                                            <td>
                                                <%# Eval("CategoryName")%>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtAdditionalTeriff"  onkeypress="return OnlyNumeric(event)" onblur="setDefaultValue(this)" Text='<%# Eval("AdditionalTeriff")%>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnSearch" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateCategories();" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
