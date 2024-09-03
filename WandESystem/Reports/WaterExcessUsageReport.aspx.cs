using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using DataAccess;
using System.Text;

public partial class WaterExcessUsageReport : BasePageHome
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkReport');");
        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.WaterExcessUsageReport.GetHashCode());
            General.BindEmployeeNumber(ref ddlEmployeeNo);
            if (!ProjectSession.IsAdmin)
            {                
                if (ddlEmployeeNo.Items.Count > 1)
                {
                    if (!string.IsNullOrEmpty(ProjectSession.UserName))
                    {
                        if (ddlEmployeeNo.Items.FindByText(ProjectSession.UserName) != null)
                        {
                            ddlEmployeeNo.Items.FindByText(ProjectSession.UserName).Selected = true;
                            ddlEmployeeNo.Enabled = false;
                        }
                        
                        
                    }
                }
            }            
            General.BindYearDropDown(ref ddlYear, 2000, DateTime.Now.Year);
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }
    }

    [WebMethod]
    public static string GetWaterMonthlyLimit(int EmployeeId, int year)
    {
        decimal dWaterMonthlyLimit = DataAccess.General.GetWaterMonthlyLimit(EmployeeId, year);
        return Convert.ToString(dWaterMonthlyLimit);
    }

    [WebMethod]
    public static string GetExcessUsageChart(int EmployeeId, int year)
    {
        DataTable dt = clsReport.ExcessWaterUsage(EmployeeId, year);

        List<pGetExcessUsageChart> lstChart = Common.ToCollection<pGetExcessUsageChart>(dt);

        StringBuilder sbCategoryAxisValue = new StringBuilder();
        StringBuilder sbLimit = new StringBuilder();
        StringBuilder sbConsumption = new StringBuilder();


        foreach (pGetExcessUsageChart excessUsage in lstChart)
        {

            sbCategoryAxisValue.Append(@"""" + excessUsage.Month.ToString() + @""",");
            sbLimit.Append(excessUsage.QuotaLimit.ToString() + ",");
            sbConsumption.Append(excessUsage.WaterUsage.ToString() + ",");
        }

        if (dt.Rows.Count > 0)
        {
            sbCategoryAxisValue.Length--;
            sbLimit.Length--;
            sbConsumption.Length--;
        }

        List<ColumnInfo> lstColumnInfo = new List<ColumnInfo>
         {
                    new ColumnInfo(ChartHelper.ColumnType.Column,true) { ColumnData = sbConsumption.ToString(), ColumnName = "WaterUsage", AxisName = "WaterUsage", AxisTitle = "WaterUsage",AxisColor="#4e4141", Color = "#3BB9FF",AxisLocation = ChartHelper.AxisLocation.Left,IsStack = false,ShowAxis=false},                                        
                    new ColumnInfo(ChartHelper.ColumnType.Line,true) { ColumnData = sbLimit.ToString(), ColumnName = "QuotaLimit", AxisName = "QuotaLimit", AxisTitle = "Water Reading In KL", AxisColor="#4e4141",Color = "red" ,ShowAxis=true}
         };

        return ChartHelper.CreateMultiAxis(lstColumnInfo, sbCategoryAxisValue.ToString(), "", 0, false, "Silver");
    }

    [WebMethod]
    public static string GetExcessUsageChartBand(int EmployeeId, int year, decimal monthlyLimit)
    {
        DataTable dt = clsReport.ExcessWaterUsage(EmployeeId, year);
        List<pGetExcessUsageChart> lstChart = Common.ToCollection<pGetExcessUsageChart>(dt);


        StringBuilder sbBand = new StringBuilder();

        decimal limit = monthlyLimit;

        decimal limitTo = 0;

        if (limit > 0)
        {
            limitTo = limit + (limit * 1) / 100;
            sbBand.Append("plotBands: [");
            sbBand.Append(@"{from:" + limit + @",to:" + limitTo + @",color:""#c00"",opacity:0.8}");
            sbBand.Append("],");
        }

        List<string> lstYAxis = new List<string>();
        lstYAxis.Add("Reading");
        decimal yMax = limitTo * 2;
        return ChartHelper.CreateMultiSeriesChart("", lstChart, "PeriodId", lstYAxis, true, 0, "yyyy MM dd", "", "column", "Water Reading in KL", "{0}", sbBand.ToString(), "0", true, yMax.ToString());
    }


}

public class pGetExcessUsageChart
{
    public int PeriodId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public decimal QuotaLimit { get; set; }
    public decimal ActualWaterAmount { get; set; }
    public int WaterUsage { get; set; }
}