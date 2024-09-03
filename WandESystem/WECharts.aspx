<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="WECharts.aspx.cs" Inherits="WECharts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Kendo/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="Kendo/kendo.default.min.css" rel="stylesheet" type="text/css" />
    <script src="Kendo/kendo.all.min.js" type="text/javascript"></script>
    <script src="Kendo/common.js" type="text/javascript"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form_field top_pad">
        <div class="content_block" style="border-top: 1px solid #d4d4d4">
            <table border="0" cellpadding="0" cellspacing="5">
                <tr>
                    <td align="left">
                        Year:
                    </td>
                    <td align="left">
                        <asp:DropDownList runat="server" ID="ddlYear" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <input type="submit" onclick="loadChart();return false;" value="Show Chart" />
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
                            <h2 style="width: 100%">
                                Total Roshkor Cost N$<span id="spnYTDTotal"></span>
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
        function GetCharts() {
            GetMainFrontChart();
        }

        function GetMainFrontChart() {
            //alert($("select[id*=ddlYear]").val());
            callwebservice('WECharts.aspx', 'GetMainFrontChart', "{onSeriesClickMethod: 'loadsubCharts',year:'" + $("select[id*=ddlYear]").val() + "'}", MainFrontChartBindComplte, true);
        }

        function MainFrontChartBindComplte(result) {
            
            var YTDTotal = 0

            for (var i = 0; i < result.dataSource.data.length; i++) {
                if (result.dataSource.data[i] != undefined) {
                    YTDTotal += result.dataSource.data[i]["STotal"];
                }
            }

            YTDTotal = parseFloat(YTDTotal).toFixed(2);
            
            $("#spnYTDTotal").html(addCommas(YTDTotal));
            if (isDataAvailable(result, "dvMainChart"))
                createChart("dvMainChart", result);
        }

        function loadsubCharts(e) {
            window.location.href = "WESubCharts.aspx?year=" + $("select[id*=ddlYear]").val();
        }

        function loadChart() {
            GetCharts();
            return false;
        }

        $(document).ready(function () {
            GetCharts();
        });
    </script>
</asp:Content>
