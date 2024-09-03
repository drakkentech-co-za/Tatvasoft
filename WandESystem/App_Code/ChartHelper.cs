using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Services;
using System.Reflection;
using System.Text;
using System.Collections;
using System.Data;


/// <summary>
/// Summary description for ChartHelper
/// </summary>
public class ChartHelper
{

    #region Constant
    public const string BackgroundColor = "transparent";
    public const string DateFormatYouth = "yyyy MM dd";
    #endregion

    public enum ColumnType
    {
        Column,
        Line,
        Area,
        Donut       
    }

    public enum AxisLocation
    {
        Left,
        Right
    }


    public ChartHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string ConvertToCamelCase(string text)
    {
        try
        {
            TextInfo myTi = new CultureInfo("en-GB", false).TextInfo;
            text = text.ToLower();

            string[] stringArray = text.Split(' ');
            string foramttedText = "";
            foreach (string s in stringArray)
            {
                if (s.Length > 2)
                    foramttedText += myTi.ToTitleCase(s) + " ";
                else
                    foramttedText += s + " ";
            }

            return string.IsNullOrWhiteSpace(text) ? text : foramttedText.Trim();
        }
        catch
        {
            return text;
        }
    }

    /// <summary>
    /// Function to create column chart
    /// </summary>
    /// <typeparam name="T">Generic Collection</typeparam>
    /// <param name="title">Chart title</param>
    /// <param name="objects">Data collection list from which chart will be plotted</param>
    /// <param name="xAxisPropertyName">Property name from which X axis will be calculated</param>
    /// <param name="yAxisPropertyName">Property name from which Y axis will be calculated</param>
    /// <param name="isDataAvailable"> </param>
    /// <param name="rotation">Rotation angle for axis values</param>
    /// <param name="dateForamtString">Format string for DateTime</param>
    /// <param name="onSeriesClickMethod">Java script method on series click</param>
    /// <param name="chartType">Chart</param>
    /// <returns></returns>    
    public static string CreateMultiSeriesChart<T>(string title, IEnumerable<T> objects, string xAxisPropertyName, List<string> yAxisPropertyName, bool isDataAvailable = true, int rotation = -30, string dateForamtString = DateFormatYouth, string onSeriesClickMethod = "", string chartType = "column", string valueAxisTitle = "", string valueAxisFormat = "{0:#,##0.00}", string plotBand = "", string yAxisMin = "0", bool setYAxisMax = false, string yAxisMax = "0", string theme = "silver")
    {
        string json = string.Empty;
        string dataX = string.Empty;
        List<string> dataY = new List<string>();
        for (int i = 0; i < yAxisPropertyName.Count; i++)
        {
            dataY.Add(string.Empty);
        }

        PropertyInfo[] properties = typeof(T).GetProperties();

        var enumerable = objects as T[] ?? objects.ToArray();
        for (int i = 0; i < enumerable.Count(); i++)
        {
            for (int k = 0; k < properties.Count(); k++)
            {
                var info = properties[k].GetValue(enumerable.ElementAt(i), null);
                string columnValue;

                if ((info is DateTime) && !string.IsNullOrEmpty(dateForamtString))
                    columnValue = ((DateTime)info).ToString(dateForamtString);
                else
                    columnValue = ((info == null) ? "" : info.ToString());

                if (properties[k].Name.ToLower().Equals(xAxisPropertyName.ToLower()))
                {
                    if (dataX == string.Empty)
                        dataX = "'" + ConvertToCamelCase(columnValue) + "'";
                    else
                        dataX += ", '" + ConvertToCamelCase(columnValue) + "'";
                }

                for (int j = 0; j < yAxisPropertyName.Count; j++)
                {

                    if (properties[k].Name.ToLower().Equals(yAxisPropertyName[j].ToLower()))
                    {
                        if (dataY[j] == string.Empty)
                            dataY[j] = columnValue;
                        else
                            dataY[j] += "," + columnValue;
                    }
                }
            }
        }

        StringBuilder sb = new StringBuilder();       
        sb.Append(@"[{theme:" + @"""" + theme + @""",chartArea: { background: ""transparent""},  ");
        sb.Append(@"chartArea:{background:""" + BackgroundColor + @""",margin:0}, ");
        sb.Append("isDataAvailable :" + isDataAvailable.ToString().ToLower() + ",");
        sb.Append(@"title: { text: """ + title + @""",font: '20px arial',visible: true}, ");
        sb.Append(@"legend: {position: ""bottom""}, ");
        sb.Append(@"seriesDefaults: {type: """ + chartType + @""",spacing:0, labels: {visible: false,template: ""#=value#"",background: ""transparent"", rotation: -90}}, ");
        sb.Append(@"crosshair:{visible: false}, ");

        if (!string.IsNullOrEmpty(onSeriesClickMethod))
            sb.Append(@"seriesClick:" + onSeriesClickMethod + ",");

        sb.Append(@"series: [");
        for (int i = 0; i < dataY.Count; i++)
        {
            sb.Append(@"{name:""" + yAxisPropertyName[i] + @""",data: [" + dataY[i] + "]},");
        }

        if (dataY.Any())
            sb.Length--;

        sb.Append("],");
        sb.Append(@"valueAxis: {min:" + yAxisMin + (setYAxisMax == true ? ",max:" + yAxisMax : "") + @", labels: {format: """ + valueAxisFormat + @"""}," + plotBand + @" majorGridLines:{ visible: true},title:{text:""" + valueAxisTitle + @""",padding:{right:3}}}, ");
        sb.Append(@"categoryAxis: {categories: [" + dataX + @"],majorGridLines:{ visible: false}, justified :true,baseUnit :""fit"",labels: {font: ""12px arial"", rotation: " + rotation + ",margin: 0,padding: { top: 0, left: 0, right :0 }} }, ");
        sb.Append(@"tooltip: {color:""white"",visible: true,format: """ + valueAxisFormat + @"""}}]");

        json = sb.ToString();
        return json;
    }


    /// <summary>
    /// Function to create column chart
    /// </summary>
    /// <typeparam name="T">Generic Collection</typeparam>
    /// <param name="title">Chart title</param>
    /// <param name="objects">Data collection list from which chart will be plotted</param>
    /// <param name="xAxisPropertyName">Property name from which X axis will be calculated</param>
    /// <param name="yAxisPropertyName">Property name from which Y axis will be calculated</param>
    /// <param name="rotation">Rotation angle for axis values</param>
    /// <param name="dateForamtString">Format string for DateTime</param>
    /// <param name="onSeriesClickMethod">Java script method on series click</param>
    /// <param name="isDataAvailable"> </param>
    /// <param name="isScrollable"> </param>
    /// <returns></returns>
    public static string CreateColumnChart<T>(string title, IEnumerable<T> objects, string xAxisPropertyName, string yAxisPropertyName, bool isDataAvailable = true, int rotation = -30, string dateForamtString = DateFormatYouth, string onSeriesClickMethod = "", bool isScrollable = false, string valueAxisTitle = "", string valueAxisFormat = "{0:#,##0.00}")
    {
        string json = string.Empty;

        PropertyInfo[] properties = typeof(T).GetProperties();
        StringBuilder dataSource = new StringBuilder();
        var enumerable = objects as T[] ?? objects.ToArray();
        for (int i = 0; i < enumerable.Count(); i++)
        {
            dataSource.Append("{");
            for (int k = 0; k < properties.Count(); k++)
            {
                dataSource.Append(properties[k].Name + ":");
                if (properties[k].GetValue(enumerable.ElementAt(i), null) is string)
                    dataSource.Append(@"""" + properties[k].GetValue(enumerable.ElementAt(i), null) + @""",");
                else
                    dataSource.Append(properties[k].GetValue(enumerable.ElementAt(i), null) + ",");
            }
            dataSource.Append("},");
        }

        StringBuilder sb = new StringBuilder();
        sb.Append(@"[{theme: ""blueopal"",chartArea: { background: ""transparent""}, ");
        sb.Append(@"chartArea:{background:""" + BackgroundColor + @""",margin:0}, ");
        sb.Append("isDataAvailable :" + isDataAvailable.ToString().ToLower() + ",");
        sb.Append(@"title: { text: """ + title + @""",font: '20px arial',visible: true}, ");
        sb.Append(@"legend: {position: ""bottom"",visible: true}, ");
        if (!isScrollable)
            sb.Append(@"dataSource: {data: [" + dataSource + "]}, ");
        else
        {
            sb.Append(@"dataSource: {data: [" + dataSource + "], filter: getFilter(rangeStart, rangeLength)}, ");
        }
        sb.Append(@"seriesDefaults: {type: ""column"", labels: {visible: false,template: ""#=value#"",background: ""transparent""}}, ");

        if (!string.IsNullOrEmpty(onSeriesClickMethod))
            sb.Append(@"seriesClick:" + onSeriesClickMethod + ",");

        sb.Append(@"series: [{ field: " + @"""" + yAxisPropertyName + @"""}], ");
        sb.Append(@"valueAxis: {labels: {format: """ + valueAxisFormat + @"""},majorGridLines:{visible: true},title:{text:""" + valueAxisTitle + @""",padding:{right:3}}}, ");
        sb.Append(@"categoryAxis: {field:" + @"""" + xAxisPropertyName + @"""" + ",majorGridLines:{ visible: false}" + @", justified :true,baseUnit :""fit"",labels: {font: ""12px arial"", rotation: " + rotation + ",margin: 0,padding: { top: 0, left: 0, right :0 }} }, ");
        sb.Append(@"tooltip: {color:""white"",visible: true,format: """ + valueAxisFormat + @"""}}]");

        json = sb.ToString();
        return json;
    }


    /// <summary>
    /// Function to create multi axis chart
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="categoryAxisValue"></param>
    /// <param name="chartTile"></param>
    /// <param name="rotation"> </param>
    /// <param name="isMultiValue"> </param>
    /// <param name="theme"> </param>
    /// <param name="isTitleVisible"> </param>
    /// <returns></returns>
    public static string CreateMultiAxis(List<ColumnInfo> columns, string categoryAxisValue, string chartTile, int rotation, bool isMultiValue = false, string theme = "blueopal", bool isTitleVisible = true)
    {
        StringBuilder series = new StringBuilder();
        StringBuilder axis = new StringBuilder();
        StringBuilder axisLocation = new StringBuilder();
        foreach (ColumnInfo columnInfo in columns)
        {
            if (!isMultiValue)
            {
                StringBuilder tempSeries = new StringBuilder();
                tempSeries.Append("{");
                tempSeries.Append(@"type: """ + columnInfo.ColumnType.ToString().ToLower() + @"""");
                // tempSeries.Append(",  highlight: {visible: false}");
                tempSeries.Append(", data: [" + columnInfo.ColumnData + "]");

                tempSeries.Append(",  markers:{visible:false}");

                tempSeries.Append(columnInfo.IsStack ? ", stack: true" : ", spacing: 0");

                if (columnInfo.ColumnName.Length > 0)
                    tempSeries.Append(@", name: """ + columnInfo.ColumnName + @"""");

                if (columnInfo.Color.Length > 0)
                    tempSeries.Append(@", color: """ + columnInfo.Color + @"""");

                if (columnInfo.ShowAxis)
                    tempSeries.Append(@", axis: """ + columnInfo.AxisName + @"""");

                tempSeries.Append("}, ");
                series.Append(tempSeries);
                series.Length--;
            }
            else
                series.Append(columnInfo.ColumnData);

            if (columnInfo.ShowAxis)
            {
                StringBuilder tempAxis = new StringBuilder();
                tempAxis.Append("{");

                if (columnInfo.AxisName.Length > 0) tempAxis.Append(@"name: """ + columnInfo.AxisName + @""", ");
                if (!isMultiValue)
                    if (columnInfo.AxisTitle.Length > 0) tempAxis.Append(@"title: { text: """ + columnInfo.AxisTitle + @""" }, ");
                if (columnInfo.Color.Length > 0) tempAxis.Append(@"color: """ + columnInfo.AxisColor + @""", ");
                if (columnInfo.AxisMin != null) tempAxis.Append("min: " + columnInfo.AxisMin.ToString() + ", ");
                if (columnInfo.AxisMax != null) tempAxis.Append("max: " + columnInfo.AxisMax.ToString() + ", ");
                if (columnInfo.AxisMajorUnit != null) tempAxis.Append("majorUnit: " + columnInfo.AxisMajorUnit.ToString() + ", ");
                tempAxis.Length--;
                tempAxis.Append("}, ");
                axis.Append(tempAxis);
                axis.Length--;
            }

            // Align the first two value axes to the left and the last two to the right.
            // Right alignment is done by specifying a crossing value greater than or equal to the number of categories.
            axisLocation.Append(columnInfo.AxisLocation != AxisLocation.Left ? "999, " : "0, ");
            axisLocation.Length--;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append(@"[{theme:" + @"""" + theme + @""",");
        if (isTitleVisible)
            sb.Append(@"title:{text:""" + chartTile + @"""}, ");
        else
            sb.Append(@"title:{text:""" + chartTile + @""",visible: false},");
        sb.Append(@"legend: {position: ""top""}, ");
        sb.Append("series: [" + series + "], ");
        sb.Append("valueAxes:[" + axis + "], ");
        sb.Append("categoryAxis: {categories: [" + categoryAxisValue + "], axisCrossingValues: [" + axisLocation + "], ");

        sb.Append(" majorGridLines: {visible: false},");
        sb.Append(@"justified :false,baseUnit :""fit"", ");
        sb.Append("labels:{");
        sb.Append(@"rotation: " + rotation + ", ");
        sb.Append(@"margin: 0," + @"fontSize: 8, ");
        sb.Append(@"padding:{top:0,left:0,right:0}");
        sb.Append("}}");
        sb.Append(@",tooltip: {visible: true,color:""white"",format: ""{0}"",template:""#= series.name#: #= value#""}}]");        
        return sb.ToString();
    }
}

#region ValueAxes
public class ValueAxes
{
    public int? Min { get; set; }
    public int? Max { get; set; }
    public int? MajorUnit { get; set; }
}
#endregion

#region ColumnInfo
public class ColumnInfo
{
    public ChartHelper.ColumnType ColumnType { get; set; }
    public string ColumnName { get; set; }
    public string ColumnData { get; set; }
    public string AxisName { get; set; }
    public string AxisTitle { get; set; }
    public int? AxisMin { get; set; }
    public int? AxisMax { get; set; }
    public int? AxisMajorUnit { get; set; }
    public string Color { get; set; }
    public string AxisColor { get; set; }
    public ChartHelper.AxisLocation AxisLocation { get; set; }
    public bool ShowAxis { get; set; }
    public bool IsStack { get; set; }
    public string ValuAxisLableTemplate { get; set; }
    public string TooltipTemplate { get; set; }

    public ColumnInfo(ChartHelper.ColumnType columnType, bool showAxis)
    {
        ColumnType = columnType;
        AxisLocation = ChartHelper.AxisLocation.Left;
        ShowAxis = showAxis;
    }
}
#endregion