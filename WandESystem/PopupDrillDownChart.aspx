<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupDrillDownChart.aspx.cs"
    Inherits="PopupDrillDownChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.2.js" type="text/javascript"></script>
    <link href="Kendo/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="Kendo/kendo.default.min.css" rel="stylesheet" type="text/css" />
    <script src="Kendo/kendo.all.min.js" type="text/javascript"></script>
    <script src="Kendo/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 5px;">
        <table border="0" cellpadding="3" cellspacing="3" width="100%">
            <tr>
                <td width="50%">
                    <div class="form_field top_pad">
                        <div class="titlebg">
                            <h2 id="h2txtBasic" style="width:100%">
                            </h2>
                        </div>
                        <div id="dvBasic" class="content_block">
                        </div>
                    </div>
                </td>
                <td>
                    <div class="form_field top_pad">
                        <div class="titlebg">
                            <h2 id="h2txtConsumption" style="width:100%">
                            </h2>
                        </div>
                        <div id="dvConsumption" class="content_block">
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        var seriesType = '<%=Type%>';
        var year = '<%=year%>';
        $(document).ready(function () {
            GetCharts();
        });

        function GetCharts() {
            GetBasicCharges();
            GetConsumptionCharges();
        }

        function GetBasicCharges() {
            callwebservice('PopupDrillDownChart.aspx', 'GetBasicChargeChart', "{seriesType: '" + seriesType + "',year:" + year + "}", BasicChargesChartBindComplte, true);
        }

        function BasicChargesChartBindComplte(result) {
            //setWidth("chartDepartmentDaysWithoutLTI", totalWidth * .48);
            if (isDataAvailable(result, "dvBasic"))
                createChart("dvBasic", result);

            if (seriesType == "Water") {
                $("#h2txtBasic").html("WATER BASIC");
            } else if (seriesType == "Electricity") {
                $("#h2txtBasic").html("ELECTRICITY BASIC");
            }

        }

        function GetConsumptionCharges() {
            callwebservice('PopupDrillDownChart.aspx', 'GetConsumptionChargeChart', "{seriesType: '" + seriesType + "',year:" + year + "}", ConsumptionChargesChartBindComplte, true);
        }

        function ConsumptionChargesChartBindComplte(result) {
            //setWidth("chartDepartmentDaysWithoutLTI", totalWidth * .48);
            if (isDataAvailable(result, "dvConsumption"))
                createChart("dvConsumption", result);

            if (seriesType == "Water") {
                $("#h2txtConsumption").html("WATER CONSUMPTION");
            } else if (seriesType == "Electricity") {
                $("#h2txtConsumption").html("ELECTRICITY CONSUMPTION");
            }

        }
    </script>
</body>
</html>
