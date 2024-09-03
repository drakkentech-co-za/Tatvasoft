using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using DataAccess;

public partial class WESubCharts : BasePageHome
{

    public int year
    {
        get
        {
            int year = ConvertTo.Integer(Request.QueryString["year"]);

            if (!string.IsNullOrEmpty(Convert.ToString(year)))
                return ConvertTo.Integer(year);
            else
                return DateTime.Now.Year;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkCharts');");

        if (!IsPostBack)
            SetRights(PageRole.SystemPages.WESubCharts.GetHashCode());
    }

    #region "Private Methods"

    #endregion

    [WebMethod]
    public static string GetWaterElecOusideChart(string onSeriesClickMethod,int year)
    {
        DataTable dt = clsReport.GetWaterElecOusideChart(year);
        List<string> lstYAxis = new List<string>();
        lstYAxis.Add("Water");
        lstYAxis.Add("Electricity");
        lstYAxis.Add("OutsideService");
        List<pWaterElecOusideChart> lstChart = Common.ToCollection<pWaterElecOusideChart>(dt);
        return ChartHelper.CreateMultiSeriesChart("", lstChart, "Month", lstYAxis, true, 0, "yyyy MM dd", onSeriesClickMethod,"column","Cost N$");
    }

    [WebMethod]
    public static string GetElecConsumptionChart(int year)
    {
        DataTable dt = clsReport.ElecConsumptionChart(year);
        List<pElecConsumptionChart> lstChart = Common.ToCollection<pElecConsumptionChart>(dt);
        return ChartHelper.CreateColumnChart("", lstChart, "Month", "Reading", true, 0,"yyyy MM dd","",false,"Kwh Consumed","{0:#,##0}");
    }

    [WebMethod]
    public static string GetElectricUnitPriceChart(int year)
    {
        DataTable dt = clsReport.ElectricUnitPriceChart(year);
        List<pElectricUnitPriceChart> lstChart = Common.ToCollection<pElectricUnitPriceChart>(dt);
        return ChartHelper.CreateColumnChart("", lstChart, "Month", "AvgUnitPrice", true, 0, "yyyy MM dd","",false,"Unit Price");
    }

    [WebMethod]
    public static string GetWaterBasicChargesChart(string onSeriesClickMethod, int year)
    {
        DataTable dt = clsReport.WaterChargesChart(year);
        List<string> lstYAxis = new List<string>();
        lstYAxis.Add("Basic");        
        List<pWaterChargesChart> lstChart = Common.ToCollection<pWaterChargesChart>(dt);
        return ChartHelper.CreateMultiSeriesChart("", lstChart, "Month", lstYAxis, true, -30, "yyyy MM dd", onSeriesClickMethod, "column", "Cost N$");
    }

    [WebMethod]
    public static string GetWaterConsumptionChargesChart(string onSeriesClickMethod, int year)
    {
        DataTable dt = clsReport.WaterChargesChart(year);
        List<string> lstYAxis = new List<string>();        
        lstYAxis.Add("Consumption");
        List<pWaterChargesChart> lstChart = Common.ToCollection<pWaterChargesChart>(dt);
        return ChartHelper.CreateMultiSeriesChart("", lstChart, "Month", lstYAxis, true, -30, "yyyy MM dd", onSeriesClickMethod, "column", "Cost N$", "{0:#,##0.00}", "", "0", false, "0", "metro");
    }

    [WebMethod]
    public static string GetElectricityBasicChargesChart(string onSeriesClickMethod, int year)
    {
        DataTable dt = clsReport.ElectricityChargesChart(year);
        List<string> lstYAxis = new List<string>();
        lstYAxis.Add("Basic");        
        List<pElectricityChargesChart> lstChart = Common.ToCollection<pElectricityChargesChart>(dt);
        return ChartHelper.CreateMultiSeriesChart("", lstChart, "Month", lstYAxis, true, -30, "yyyy MM dd", onSeriesClickMethod, "column", "Cost N$");
    }

    [WebMethod]
    public static string GetElectricityConsumptionChargesChart(string onSeriesClickMethod, int year)
    {
        DataTable dt = clsReport.ElectricityChargesChart(year);
        List<string> lstYAxis = new List<string>();        
        lstYAxis.Add("Consumption");
        List<pElectricityChargesChart> lstChart = Common.ToCollection<pElectricityChargesChart>(dt);
        return ChartHelper.CreateMultiSeriesChart("", lstChart, "Month", lstYAxis, true, -30, "yyyy MM dd", onSeriesClickMethod, "column", "Cost N$", "{0:#,##0.00}", "", "0", false, "0", "metro");
    }

    [WebMethod]
    public static string GetOutsideChargesChart(string onSeriesClickMethod, int year)
    {
        DataTable dt = clsReport.OutsideChargesChart(year);
        List<string> lstYAxis = new List<string>();
        lstYAxis.Add("Core");
        lstYAxis.Add("NonCore");
        List<pOutsideChargesChart> lstChart = Common.ToCollection<pOutsideChargesChart>(dt);
        return ChartHelper.CreateMultiSeriesChart("", lstChart, "Month", lstYAxis, true, 0, "yyyy MM dd", onSeriesClickMethod, "column", "Cost N$");
    } 
}

public class pWaterElecOusideChart
{
    public int PeriodId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public decimal Water { get; set; }
    public decimal Electricity { get; set; }
    public decimal OutsideService { get; set; }
}

public class pElecConsumptionChart
{
    public int PeriodId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public int Reading { get; set; }
}

public class pElectricUnitPriceChart
{
    public int PeriodId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public decimal AvgUnitPrice { get; set; }
}

public class pWaterChargesChart
{
    public int PeriodId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public decimal Basic { get; set; }
    public decimal Consumption { get; set; }
}

public class pElectricityChargesChart
{
    public int PeriodId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public decimal Basic { get; set; }
    public decimal Consumption { get; set; }
}

public class pOutsideChargesChart
{
    public int PeriodId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public decimal Core { get; set; }
    public decimal NonCore { get; set; }
}