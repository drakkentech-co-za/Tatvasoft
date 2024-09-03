<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="UserControls_Header" %>
<script type="text/javascript">
    function Security() {
        window.history.forward(1);
    }
</script>
<div id="header">
    <!--Header start-->
    <div class="hd-top">
    <div class="logo">
        <a href='<%= ResolveClientUrl("~/UserProfile.aspx")%>'>
            <img src='<%= ResolveClientUrl("~/App_Themes/Default/images/RPZCLogo.png")%>' alt="" /></a>
            <div class="title">
        <span>Water Electricity & Token System</span>
    </div>
            
            </div>
    
    <div class="user_info">
        <table border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <img src='<%= ResolveClientUrl("~/App_Themes/Default/images/ic_user.png")%>' alt="" />
                            </td>
                            <td class="text_pad">
                                Welcome<br />
                                <span><a href='<%= ResolveClientUrl("~/UserProfile.aspx")%>'>
                                    <%= ProjectSession.UserName%></a> </span>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkLogOut" runat="server" OnClientClick="javascript:Security();"
                                    CausesValidation="false" OnClick="lnkLogOut_Click"><img src='<%= ResolveClientUrl("~/App_Themes/Default/images/btn_logout.png")%>' alt=""/></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </div>
    <div class="clear">
    </div>
    <div id="navigation">
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <ul id="nav">
                        <li class="top" id="liInvoice" runat="server"><a id="lnkInvoice" href='<%= ResolveClientUrl("~/GenerateInvoice.aspx")%>'
                            class="nav_link"><span>Invoice </span></a></li>
                        <li class="top" id="liAdministrator" runat="server"><a id="lnkAdmin" href="#" class="nav_link">
                            <span>Administration
                                <img src='<%= ResolveClientUrl("~/App_Themes/Default/images/arrow.png")%>' alt="" /></span></a>
                            <ul class="sub" id="ulAdministrator" runat="server">
                                <li id="liEmployee" runat="server"><a href='<%= ResolveClientUrl("~/EmployeeList.aspx")%>'>
                                    Employee </a></li>
                                <li id="liHouseList" runat="server"><a href='<%= ResolveClientUrl("~/HouseList.aspx")%>'>
                                    Account </a></li>
                                <li id="liHouseType" runat="server"><a href='<%= ResolveClientUrl("~/HouseTypes.aspx")%>'>
                                    House Types</a></li>
                                <li id="liEmployeeAccountMapping" runat="server"><a href='<%= ResolveClientUrl("~/EmployeeAccountMapping.aspx")%>'>
                                    Employee-Account Mapping </a></li>
                                <li id="liTeriff" runat="server"><a href='<%= ResolveClientUrl("~/Teriffs.aspx")%>'>
                                    Tariffs </a></li>
                                <li id="liHouseTeriffMapping" runat="server"><a href='<%= ResolveClientUrl("~/HouseTeriffMapping.aspx")%>'>
                                    Account-Tariff Mapping </a></li>
                                <li id="liTeriffRate" runat="server"><a href='<%= ResolveClientUrl("~/TeriffRate.aspx")%>'>
                                    Tariff Rates </a></li>
                                <li id="liWaterScale" runat="server"><a href='<%= ResolveClientUrl("~/WaterScales.aspx")%>'>
                                    Water Scales </a></li>
                                <li id="liAssetType" runat="server"><a href='<%= ResolveClientUrl("~/AssetTypes.aspx")%>'>
                                    Asset Types </a></li>
                                <li id="liWaterRateLimit" runat="server"><a href='<%= ResolveClientUrl("~/WaterRateLimits.aspx") %>'>
                                    Water Rate Limits</a></li>
                                <li id="liElectricUnitPrice" runat="server"><a href='<%= ResolveClientUrl("~/ElectricUnitPrices.aspx") %>'>
                                    Electric Unit Prices</a></li>
                                <li id="liPeriodMonthMapping" runat="server"><a href='<%= ResolveClientUrl("~/PeriodMonthMappingList.aspx") %>'>
                                    Period Month Mapping</a></li>
                                <li id="liTokenSheetManagement" runat="server"><a href='<%= ResolveClientUrl("~/TokenSheetManagement.aspx") %>'>
                                    Token Sheet Management </a></li>
                                <li id="liManualToken" runat="server"><a href='<%= ResolveClientUrl("~/ManualTokenImport.aspx") %>'>
                                    Manual Token Import </a></li>
                                <li id="liAdhocToken" runat="server"><a href='<%= ResolveClientUrl("~/AdhocTokenList.aspx") %>'>
                                    Adhoc Token List </a></li>
                                <li id="liResetPassword" runat="server"><a href='<%= ResolveClientUrl("~/ResetPassword.aspx")%>'>
                                    <span>Reset Password </span></a></li>
                                <li id="liManageRights" runat="server"><a href='<%= ResolveClientUrl("~/ManageRights.aspx")%>'>
                                    <span>Manage User Permission</span></a></li>
                                <li id="liLandfill" runat="server"><a href='<%= ResolveClientUrl("~/LandfillList.aspx")%>'>
                                    <span>Landfill</span></a></li>
                            </ul>
                        </li>
                        <li class="top" id="liInvoiceCompare" runat="server"><a id="lnkInvoiceCompare" href='<%= ResolveClientUrl("~/InvoiceCompare.aspx")%>'
                            class="nav_link"><span>Invoice Compare </span></a></li>
                        <li class="top" id="liTokenRequest" runat="server"><a id="lnkTokenRequest" href='<%= ResolveClientUrl("~/TokenRequest.aspx")%>'
                            class="nav_link"><span>Token Request </span></a></li>
                        <li class="top" id="liReport" runat="server"><a id="lnkReport" href="#" class="nav_link">
                            <span>Reports
                                <img src='<%= ResolveClientUrl("~/App_Themes/Default/images/arrow.png")%>' alt="" /></span></a>
                            <ul class="sub">
                                <li id="liPayrollSubmission" runat="server"><a href='<%= ResolveClientUrl("~/Reports/rptPayrollSubmission.aspx")%>'>
                                    Payroll Submission </a></li>
                                <li id="liDynamicReports" runat="server"><a href='<%= ResolveClientUrl("~/Reports/DynamicReports.aspx")%>'>
                                    Dynamic Reports </a></li>
                                <li id="lirptWaterExcessUsageReport" runat="server"><a href='<%= ResolveClientUrl("~/Reports/WaterExcessUsageReport.aspx")%>'>
                                    Water Excess Usage Report</a></li>
                                <li id="liPrepaidElecAndOther" runat="server"><a href='<%= ResolveClientUrl("~/PrepaidElecAndOther.aspx")%>'>
                                    Prepaid Electricity and Other</a></li>
                            </ul>
                        </li>
                        <li class="top" id="liCharts" runat="server"><a id="lnkWECharts" href='<%= ResolveClientUrl("~/WECharts.aspx")%>'
                            class="nav_link"><span>Charts </span></a></li>
                        <li class="top" id="liChangePassword" runat="server"><a id="lnkChangePassword" href='<%= ResolveClientUrl("~/ChangePassword.aspx")%>'
                            class="nav_link"><span>Change Password </span></a></li>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
    <!--Header end-->
</div>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        if ($("#<%=liAdministrator.ClientID%>") != null && $("#<%=liAdministrator.ClientID%>") != 'undefined') {
            if ($("#<%=liAdministrator.ClientID%>").find("li").length == 0) {
                $("#<%=liAdministrator.ClientID%>").hide();
            }
        }
        if ($("#<%=liReport.ClientID%>") != null && $("#<%=liReport.ClientID%>") != 'undefined') {
            if ($("#<%=liReport.ClientID%>").find("li").length == 0) {
                $("#<%=liReport.ClientID%>").hide();
            }
        }
    });
   
</script>
