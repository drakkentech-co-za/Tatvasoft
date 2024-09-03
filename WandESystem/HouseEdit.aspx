<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="HouseEdit.aspx.cs" Inherits="HouseEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <link href="greybox/gb_styles.css" rel="stylesheet" type="text/css" />
    <script src="greybox/AJS.js" type="text/javascript" defer="defer"></script>
    <script src="greybox/AJS_fx.js" type="text/javascript" defer="defer"></script>
    <script src="greybox/gb_scripts.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
        var GB_ROOT_DIR = "greybox/";
        $(document).ready(function () {
            choosen();
            showhidePrepaidPanel();
        });

        function choosen() {
            $(".chzn-select").chosen();
        }

        function openPopupForHouseTeriffMapping(id) {

            var url = "../PopUpBasicHouseTeriffMapping.aspx?ID=" + id;
            GB_showCenter("Account - Tariff Mapping", url, 500, 950, function () {
                window.location.href = "HouseList.aspx?at=0";
            });

        }

        function showhidePrepaidPanel() {
            var assetType = $('#' + '<%= ddlAssetType.ClientID %>')[0].value;
            var ElecType = $("input:radio[id*='rdElecType']:checked").val();

            if (assetType == "1" || ElecType == "0") {
                $("#ctl00_ContentPlaceHolder1_trPrepaid").show();
            }
            else {
                $("#ctl00_ContentPlaceHolder1_trPrepaid").hide();
            }
        }
   
    </script>
    <style type="text/css">
        .padtopbottom
        {
            padding: 3px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upHouseEdit" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h1>
                Account Detail
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
                            <tr id="trRental" runat="server">
                                <td align="left" style="width: 15%">
                                    Rental:
                                </td>
                                <td align="left" style="width: 85%">
                                    <asp:checkbox ID="chkRental" runat="server" OnCheckedChanged="chkRental_CheckedChanged" AutoPostBack="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%">
                                    Account No:
                                </td>
                                <td align="left" style="width: 85%">
                                    <asp:DropDownList ID="ddlAccountNo" runat="server" Width="300" data-placeholder="Select Account Number"
                                        class="chzn-select" DataTextField="Account" DataValueField="Account" AutoPostBack="false">
                                    </asp:DropDownList>
                                    <asp:Label runat="server" ID="lblAccountNo" Visible="false" />
                                    <asp:RequiredFieldValidator ID="rfvAccountNo" runat="server" ControlToValidate="ddlAccountNo"
                                        ErrorMessage="Enter Account Number" Text="*" ForeColor="red" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%">
                                    Address1:
                                </td>
                                <td align="left" style="width: 85%">
                                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="width200" MaxLength="150">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1"
                                        ErrorMessage="Enter Address1" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%">
                                    Address2:
                                </td>
                                <td align="left" style="width: 85%">
                                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="width200" MaxLength="150">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%">
                                    ERF No:
                                </td>
                                <td align="left" style="width: 85%">
                                    <asp:TextBox ID="txtErfNo" runat="server" CssClass="width200" MaxLength="50">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvErfNo" runat="server" ControlToValidate="txtErfNo"
                                        ErrorMessage="Enter ERF No" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%">
                                    ERF Size:
                                </td>
                                <td align="left" style="width: 85%">
                                    <asp:TextBox ID="txtERFSize" runat="server" CssClass="width200" MaxLength="8" onkeypress="return BlockNonNumbers(this,event,true,false)">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvERFSize" runat="server" ControlToValidate="txtERFSize"
                                        ErrorMessage="Enter ERF Size" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                <td align="left" style="width: 15%">
                                    Asset Type:
                                </td>
                                <td align="left" style="width: 85%">
                                    <asp:DropDownList runat="server" ID="ddlAssetType" CssClass="width210" onchange="showhidePrepaidPanel();">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%">
                                    Electricity Type:
                                </td>
                                <td align="left" style="width: 85%">
                                    <asp:RadioButtonList runat="server" ID="rdElecType" RepeatDirection="Horizontal"
                                        onclick="showhidePrepaidPanel();">
                                        <asp:ListItem Text="Pre-Paid" Value="0" Selected="True" />
                                        <asp:ListItem Text="Post-Paid" Value="1" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr id="trPrepaid" runat="server">
                                <td valign="top" colspan="2">
                                    <table width="100%" cellpadding="2" cellspacing="2">
                                        <tr class="padtopbottom">
                                            <td align="left" style="width: 15%">
                                                Prepaid Allowed:
                                            </td>
                                            <td align="left" style="width: 85%">
                                                <asp:CheckBox Checked="true" runat="server" ID="chkPrepaidAllowed" />
                                            </td>
                                        </tr>
                                        <tr class="padtopbottom">
                                            <td align="left" style="width: 15%" valign="top">
                                                Reason:
                                            </td>
                                            <td align="left" style="width: 85%">
                                                <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" CssClass="width200"
                                                    onkeypress="return CheckMaxLength(event,this,200);" onblur="return CheckMaxLength(event,this,200);"
                                                    onkeyup="return CheckMaxLength(event,this,200);" />
                                            </td>
                                        </tr>
                                        <tr class="padtopbottom">
                                            <td align="left" style="width: 15%">
                                                Electric Units:
                                            </td>
                                            <td align="left" style="width: 85%">
                                                <asp:TextBox runat="server" ID="txtElectricUnits" CssClass="width200" MaxLength="8"
                                                    onkeypress="return BlockNonNumbers(this,event,true,false)" />
                                            </td>
                                        </tr>
                                        <tr class="padtopbottom">
                                            <td align="left" style="width: 15%">
                                                Meter No:
                                            </td>
                                            <td align="left" style="width: 85%">
                                                <asp:TextBox runat="server" ID="txtMeterNo" CssClass="width200" MaxLength="100" />
                                            </td>
                                        </tr>
                                        <tr class="padtopbottom" id="trRentalAmount" runat="server">
                                            <td align="left" style="width: 15%">
                                                Rental Amount:
                                            </td>
                                            <td align="left" style="width: 85%">
                                                <asp:TextBox runat="server" ID="txtRentalAmount" CssClass="width200" MaxLength="100" onkeypress="return BlockNonNumbers(this,event,true,false)" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="chkRental" EventName="CheckedChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
