﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Test.aspx.cs" Inherits="Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Kendo/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="Kendo/kendo.default.min.css" rel="stylesheet" type="text/css" />
    <script src="Kendo/kendo.all.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="chart">
    </div>
    <script type="text/javascript">
        function createChart() {
            $("#chart").kendoChart({
                title: {
                    text: "Market Value of Major Banks"
                },
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: "radarLine"
                },
                series: [{
                    name: "Market value as of 2007",
                    data: [116, 165, 215, 75, 100, 49, 80, 116, 108, 90, 67, 76, 91, 255, 120]
                }, {
                    name: "Market value as of 2009",
                    data: [64, 85, 97, 27, 16, 26, 35, 32, 26, 17, 10, 7, 19, 5]
                }],
                categoryAxis: {
                    categories: ["Santander", "JP Morgan", "HSBC", "Credit Suisse",
                        "Goldman Sachs", "Morgan Stanley", "Societe Generale", "UBS",
                        "BNP Paribas", "Unicredit", "Credit Agricole", "Deutsche Bank",
                        "Barclays", "Citigroup", "RBS"]
                },
                valueAxis: {
                    labels: {
                        format: "${0}"
                    }
                },
                tooltip: {
                    visible: true,
                    format: "${0} bln"
                }
            });
        }

        $(document).ready(createChart);
        $(document).bind("kendo:skinChange", createChart);
    </script>
</asp:Content>
