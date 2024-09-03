<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="WESubCharts.aspx.cs" Inherits="WESubCharts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="greybox/gb_styles.css" rel="stylesheet" type="text/css" />
    <script src="greybox/AJS.js" type="text/javascript" defer="defer"></script>
    <script src="greybox/AJS_fx.js" type="text/javascript" defer="defer"></script>
    <script src="greybox/gb_scripts.js" type="text/javascript" defer="defer"></script>
    <link href="Kendo/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="Kendo/kendo.default.min.css" rel="stylesheet" type="text/css" />
    <script src="Kendo/kendo.all.min.js" type="text/javascript"></script>
    <script src="Kendo/common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upHouseEdit" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                    <tr>
                        <td width="100%">
                            <div class="form_field top_pad">
                                <div class="titlebg">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <h2 id="h2Txt" style="width: 100%">
                                                </h2>
                                            </td>
                                            <td align="right" width="100px" valign="baseline" style="padding-top: 5px; padding-right: 5px;">
                                                <input type="button" value="Back" style="background: white; border: none;" id="btnBack"
                                                    onclick="getPrevChart()" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="dvElectWaterOutside" class="content_block">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="2" cellspacing="" width="100%" style="border-collapse: collapse;">
                                <tr>
                                    <td width="50%">
                                        <div class="form_field top_pad">
                                            <div class="titlebg">
                                                <h2 style="width: 100%">
                                                    KWh Electricity Consumed
                                                </h2>
                                            </div>
                                            <div id="dvElecConsumed" class="content_block">
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="form_field top_pad">
                                            <div class="titlebg">
                                                <h2 style="width: 100%">
                                                    Electricity Unit Price (AVG)
                                                </h2>
                                            </div>
                                            <div id="dvElecUnitPrice" class="content_block">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        var GB_ROOT_DIR = "greybox/";
        var ChartCounter = 0;
        var currentSeries = '';
        var year = '<%=year%>';

        function GetCharts() {
            GetWaterElecOutsideChart();
            GetElecConsumedChart();
            GetElecUnitPriceChart();
        }

        function GetWaterElecOutsideChart() {
            callwebservice('WESubCharts.aspx', 'GetWaterElecOusideChart', "{onSeriesClickMethod: 'loadsubCharts',year:" + year + "}", WaterElecOutsideChartBindComplte, true);
        }

        function WaterElecOutsideChartBindComplte(result) {
            //setWidth("chartDepartmentDaysWithoutLTI", totalWidth * .48);            
            $("#btnBack").show();
            $('#btnBack').val("Back");
            $("#h2Txt").html("Water Electricity Outside Service charges");
            currentSeries = '';
            if (isDataAvailable(result, "dvElectWaterOutside"))
                createChart("dvElectWaterOutside", result);
        }


        function GetElecConsumedChart() {
            callwebservice('WESubCharts.aspx', 'GetElecConsumptionChart', "{year:" + year + "}", ElecConsumedChartBindComplte, true);
        }

        function ElecConsumedChartBindComplte(result) {
            //setWidth("chartDepartmentDaysWithoutLTI", totalWidth * .48);
            if (isDataAvailable(result, "dvElecConsumed"))
                createChart("dvElecConsumed", result);
        }

        function GetElecUnitPriceChart() {
            callwebservice('WESubCharts.aspx', 'GetElectricUnitPriceChart', "{year:" + year + "}", ElecUnitPriceChartBindComplte, true);
        }

        function ElecUnitPriceChartBindComplte(result) {
            //setWidth("chartDepartmentDaysWithoutLTI", totalWidth * .48);
            if (isDataAvailable(result, "dvElecUnitPrice"))
                createChart("dvElecUnitPrice", result);
        }

        function GetWaterCharges() {
            $("#dvElectWaterOutside").html("");
            $("#dvElectWaterOutside").append("<table width='100%'><tr><td width='50%'><div id='dvBasic' class='content_block' style='border-top:1px solid #d4d4d4;padding:10px'></div></td><td width='50%'><div id='dvConsumption' class='content_block' style='border-top:1px solid #d4d4d4;padding:10px'></div></td></tr></table>");
            callwebservice('WESubCharts.aspx', 'GetWaterBasicChargesChart', "{onSeriesClickMethod: 'PopupDrillDown',year:" + year + "}", WaterBasicChargesChartBindComplte, true);
            callwebservice('WESubCharts.aspx', 'GetWaterConsumptionChargesChart', "{onSeriesClickMethod: 'PopupDrillDown',year:" + year + "}", WaterConsumptionChargesChartBindComplte, true);
        }

        function WaterBasicChargesChartBindComplte(result) {
            //setWidth("chartDepartmentDaysWithoutLTI", totalWidth * .48);                       
            if (isDataAvailable(result, "dvBasic"))
                createChart("dvBasic", result);

            $("#btnBack").show();
            $('#btnBack').val("Back");
            $("#h2Txt").html("WATER (Basic & Consumption)");
            ChartCounter++;
            currentSeries = 'Water';
        }

        function WaterConsumptionChargesChartBindComplte(result) {
            if (isDataAvailable(result, "dvConsumption"))
                createChart("dvConsumption", result);
        }

        function GetElectricityCharges() {
            $("#dvElectWaterOutside").html("");
            $("#dvElectWaterOutside").append("<table width='100%'><tr><td width='50%'><div id='dvBasic' class='content_block' style='border-top:1px solid #d4d4d4;padding:10px'></div></td><td width='50%'><div id='dvConsumption' class='content_block' style='border-top:1px solid #d4d4d4;padding:10px'></div></td></tr></table>");

            callwebservice('WESubCharts.aspx', 'GetElectricityBasicChargesChart', "{onSeriesClickMethod: 'PopupDrillDown',year:" + year + "}", ElectricityBasicChargesChartBindComplte, true);
            callwebservice('WESubCharts.aspx', 'GetElectricityConsumptionChargesChart', "{onSeriesClickMethod: 'PopupDrillDown',year:" + year + "}", ElectricityConsumptionChargesChartBindComplte, true);
        }

        function ElectricityBasicChargesChartBindComplte(result) {
            //setWidth("chartDepartmentDaysWithoutLTI", totalWidth * .48);
            if (isDataAvailable(result, "dvBasic"))
                createChart("dvBasic", result);

            $("#btnBack").show();
            $('#btnBack').val("Back");
            $("#h2Txt").html("Electricity (Basic & Consumption)");
            ChartCounter++;
            currentSeries = 'Electricity';
        }

        function ElectricityConsumptionChargesChartBindComplte(result) {
            if (isDataAvailable(result, "dvConsumption"))
                createChart("dvConsumption", result);
        }

        function GetOutsideCharges() {
            callwebservice('WESubCharts.aspx', 'GetOutsideChargesChart', "{onSeriesClickMethod: '',year:" + year + "}", OutsideChargesChartBindComplte, true);
        }

        function OutsideChargesChartBindComplte(result) {
            //setWidth("chartDepartmentDaysWithoutLTI", totalWidth * .48);
            if (isDataAvailable(result, "dvElectWaterOutside"))
                createChart("dvElectWaterOutside", result);

            $("#btnBack").show();
            $('#btnBack').val("Back");
            $("#h2Txt").html("Outside Service Charges");
            ChartCounter++;
            currentSeries = 'OutsideService';
        }

        function loadsubCharts(e) {
            //debugger;
            if (e.series.name == "Water") {
                GetWaterCharges();
            } else if (e.series.name == "Electricity") {
                GetElectricityCharges();
                currentSeries = 'Electricity';
            } else if (e.series.name == "OutsideService") {
                GetOutsideCharges();
                currentSeries = 'OutsideService';
            }
        }

        function getPrevChart() {
            if (ChartCounter == 1) {
                GetWaterElecOutsideChart();
                ChartCounter--;
            } else if (ChartCounter == 0) {
                window.location.href = "WECharts.aspx";
            }
        }

        function PopupDrillDown(e) {
            var url = "../PopupDrillDownChart.aspx?Type=" + currentSeries + "&year=" + year;
            GB_showCenter(currentSeries + ' Charges', url, 550, 1100);
        }

        $(document).ready(function () {
            GetCharts();
        });        
    </script>
</asp:Content>
