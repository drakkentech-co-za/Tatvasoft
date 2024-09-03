using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Globalization;
using System.Data;
using DataAccess;
using System.Collections;


public partial class WECharts : BasePageHome
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkCharts');");
        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.WECharts.GetHashCode());
            General.BindYearDropDown(ref ddlYear, 2000, DateTime.Now.Year);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
        }
    }

    #region "Private Methods"

    #endregion

    [WebMethod]
    public static string GetMainFrontChart(string onSeriesClickMethod, int year)
    {
        DataTable dt = clsReport.GetMainFrontChart(year);
        List<pGetMainFrontChart> lstMainChart = Common.ToCollection<pGetMainFrontChart>(dt);
        return ChartHelper.CreateColumnChart("", lstMainChart, "Month", "STotal", true, 0, "yyyy MM dd", onSeriesClickMethod, false,"Cost N$");
    }
}

public class pGetMainFrontChart
{
    public int PeriodId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public decimal STotal { get; set; }
    public decimal RTotal { get; set; }
}