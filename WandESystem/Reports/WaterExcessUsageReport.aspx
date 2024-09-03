<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="WaterExcessUsageReport.aspx.cs" Inherits="WaterExcessUsageReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="../ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <link href="../Kendo/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="../Kendo/kendo.default.min.css" rel="stylesheet" type="text/css" />
    <script src="../Kendo/kendo.all.min.js" type="text/javascript"></script>
    <script src="../Kendo/common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form_field top_pad">
        <div class="content_block" style="border-top: 1px solid #d4d4d4">
            <table border="0" cellpadding="0" cellspacing="5">
                <tr>
                    <td align="left">
                        Employee No:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlEmployeeNo" runat="server" CssClass="chzn-select" data-placeholder="Select Employee Number">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Year
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlYear" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <input type="button" onclick="loadChart();return false;" value="Show Chart" class="inputbutton btn_small" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <div class="form_field top_pad">
                        <div class="titlebg">
                            <h2>
                                Water Excess Usage Report
                            </h2>
                        </div>
                        <div id="dvMainChart" class="content_block">
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".chzn-select").chosen();

            if ($("select[id*=ddlEmployeeNo]").val() > 0) {
                GetExcessUsageChart();
            }
        });

        function loadChart() {
            if ($("select[id*=ddlEmployeeNo]").val() == 0) {
                alert("Please select Employee No");
            }
            else {
                GetExcessUsageChart();
            }

            return false;
        }

        function GetExcessUsageChart() {
            var empId = $("select[id*=ddlEmployeeNo]").val();
            var year = $("select[id*=ddlYear]").val();
            var waterMonthlylimit = GetWaterMonthlyLimit(empId, year);
            callwebservice('WaterExcessUsageReport.aspx', 'GetExcessUsageChart', "{EmployeeId:" + empId + ",year:" + year + "}", ExcessUsageChartBindComplte, true);
            //callwebservice('WaterExcessUsageReport.aspx', 'GetExcessUsageChartBand', "{EmployeeId:" + empId + ",year:" + year + ",monthlyLimit:" + waterMonthlylimit + "}", ExcessUsageChartBindComplte, true);
        }

        function GetWaterMonthlyLimit(empId, year) {
            var waterMonthlyLimit = "0";

            $.ajax({
                type: "POST",
                url: "WaterExcessUsageReport.aspx/GetWaterMonthlyLimit",
                data: "{EmployeeId:" + empId + ",year:" + year + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    waterMonthlyLimit = msg.d;
                },
                error: function (msg) {
                    alert(msg.responseText);
                }
            });

            return waterMonthlyLimit;
        }

        function ExcessUsageChartBindComplte(result) {
            //setWidth("chartDepartmentDaysWithoutLTI", totalWidth * .48);
            if (isDataAvailable(result, "dvMainChart"))
                createChart("dvMainChart", result);
        }
    </script>
</asp:Content>
