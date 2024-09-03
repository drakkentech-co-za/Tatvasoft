using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using DataAccess;

public partial class PopupDrillDownChart : BasePageHome
{

    /// <summary>
    /// Type
    /// </summary>
    public string Type
    {
        get
        {
            string type = Convert.ToString(Request.QueryString["Type"]);

            if (!string.IsNullOrEmpty(type))
                return Convert.ToString(type);
            else
                return "";
        }
    }

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

    #region "Page Event"

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            SetRights(PageRole.SystemPages.PopupDrillDownChart.GetHashCode());
    }

    #endregion 

    #region "Private Mehtods"

    #endregion

    [WebMethod]
    public static string GetBasicChargeChart(string seriesType,int year)
    {

        DataTable dt = new DataTable();

        if (seriesType.ToLower() == "water")
        {
            dt = clsReport.WaterBasicChargesChart(year);
        }
        else if (seriesType.ToLower() == "electricity")
        {
            dt = clsReport.ElectricBasicChargesChart(year);
        }

        List<string> lstYAxis = new List<string>();
        lstYAxis.Add("Core");
        lstYAxis.Add("NonCore");
        List<pBasicConsumptionChart> lstChart = Common.ToCollection<pBasicConsumptionChart>(dt);
        return ChartHelper.CreateMultiSeriesChart("", lstChart, "Month", lstYAxis, true, 0, "yyyy MM dd", "", "column", "Cost N$");
    }

    [WebMethod]
    public static string GetConsumptionChargeChart(string seriesType, int year)
    {

        DataTable dt = new DataTable();

        if (seriesType.ToLower() == "water")
        {
            dt = clsReport.WaterConsumptionChargesChart(year);
        }
        else if (seriesType.ToLower() == "electricity")
        {
            dt = clsReport.ElectricConsumptionChargesChart(year);
        }

        List<string> lstYAxis = new List<string>();
        lstYAxis.Add("Core");
        lstYAxis.Add("NonCore");
        List<pBasicConsumptionChart> lstChart = Common.ToCollection<pBasicConsumptionChart>(dt);
        return ChartHelper.CreateMultiSeriesChart("", lstChart, "Month", lstYAxis, true, 0, "yyyy MM dd", "", "column", "Cost N$");
    }
}

public class pBasicConsumptionChart
{
    public int PeriodId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public decimal Core { get; set; }
    public decimal NonCore { get; set; }
}