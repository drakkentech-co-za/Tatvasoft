using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

/// <summary>
/// Summary WaterRateLimitDetail - Add/Edit water rate limit
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 13-Sep-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 13-Sep-2013 </ModifiedDate>
public partial class WaterRateLimitsDetail : BasePageHome
{
    #region Variable/Property Declaration

    /// <summary>
    /// Water Rate Limit Id
    /// </summary>
    public int WaterRateLimitId
    {
        get
        {
            string id = GetQueryString("ID");

            if (!string.IsNullOrEmpty(id))
                return Convert.ToInt32(id);
            else
                return 0;
        }
    }

    #endregion

    #region Page Events

    /// <summary>
    /// Page Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkAdmin');");

        if (!Page.IsPostBack)
        {
            SetRights(PageRole.SystemPages.WaterRateLimitsDetail.GetHashCode());
            BindDropDowns();

            if (WaterRateLimitId > 0)
            {
                btnSave.Text = "Update";
                FillControls();
            }
            else
                SetDefaultDate();

            ddlFromDateMonth.Focus();
        }
    }

    #endregion

    #region Control Events

    /// <summary>
    /// Save Page
    /// </summary>
    /// <param name=sender></param>
    /// <param name=e></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidatePage())
        {
            int actionType = ActionType.Save.GetHashCode();

            WaterRate_Limit objWaterRate;

            if (WaterRateLimitId > 0)
            {
                actionType = ActionType.Update.GetHashCode();
                objWaterRate = new WaterRate_Limit(WaterRateLimitId);
            }
            else
            {
                objWaterRate = new WaterRate_Limit();
            }

            FillObject(ref objWaterRate);

            if (WaterRate_Limit.CheckTimeConflict(objWaterRate))
            {
                ShowMessage("Water Rate Limit for selected House Type exists. Select other time interval.", lblMessage, MessageBoxType.Warning);
            }
            else
            {
                objWaterRate.Save();
                Response.Redirect("WaterRateLimits.aspx?at=" + actionType);
            }
        }
    }

    /// <summary>
    /// handles Cancel Click.Redirect to waterratelimit page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("WaterRateLimits.aspx");
    }

    #endregion

    #region Methods/Events

    /// <summary>
    /// Validate Page
    /// </summary>
    /// <returns></returns>
    private bool ValidatePage()
    {
        bool isValid = true;
        if (!Page.IsValid)
            isValid = false;
        return isValid;
    }

    /// <summary>
    /// Fill Object
    /// </summary>
    private void FillObject(ref WaterRate_Limit objWaterRate)
    {
        objWaterRate.WaterRateLimitId = this.WaterRateLimitId;

        objWaterRate.Rate1 = ConvertTo.Decimal(txtRate1.Text.Trim());
        objWaterRate.Rate2 = ConvertTo.Decimal(txtRate2.Text.Trim());
        objWaterRate.Rate3 = ConvertTo.Decimal(txtRate3.Text.Trim());
        objWaterRate.HouseTypeId = ConvertTo.Integer(ddlHouseType.SelectedValue);
        GetFromAndToDate(ref objWaterRate);
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        WaterRate_Limit objWaterRate = new WaterRate_Limit(this.WaterRateLimitId);

        txtRate1.Text = string.Format("{0:0.0000}", objWaterRate.Rate1);
        txtRate2.Text = string.Format("{0:0.0000}", objWaterRate.Rate2);
        txtRate3.Text = string.Format("{0:0.0000}", objWaterRate.Rate3);
        ddlFromDateMonth.SelectedValue = Convert.ToString((objWaterRate.StartDate).Month);
        ddlToDateMonth.SelectedValue = Convert.ToString((objWaterRate.EndDate).Month);
        ddlFromDateYear.SelectedValue = Convert.ToString((objWaterRate.StartDate).Year);
        ddlToDateYear.SelectedValue = Convert.ToString((objWaterRate.EndDate).Year);
        ddlHouseType.SelectedValue = Convert.ToString(objWaterRate.HouseTypeId);
    }

    /// <summary>
    /// Get From Date To Date from dropdown selected value
    /// </summary>
    private void GetFromAndToDate(ref WaterRate_Limit objWaterRate)
    {
        DateTime dtFromDate = new DateTime(Convert.ToInt32(ddlFromDateYear.SelectedValue), Convert.ToInt32(ddlFromDateMonth.SelectedValue), 1);
        DateTime dtToDate = new DateTime(Convert.ToInt32(ddlToDateYear.SelectedValue), Convert.ToInt32(ddlToDateMonth.SelectedValue), 1);
        dtToDate = dtToDate.AddMonths(1).AddDays(-1);

        objWaterRate.StartDate = dtFromDate;
        objWaterRate.EndDate = dtToDate;
    }

    /// <summary>
    /// set default date (sets from date less than one month from today.set To date as today's date)
    /// </summary>
    private void SetDefaultDate()
    {
        DateTime dtTemp = DateTime.Now;
        ddlFromDateMonth.SelectedValue = Convert.ToString(dtTemp.Month);
        ddlToDateMonth.SelectedValue = Convert.ToString(dtTemp.Month - 1);

        ddlFromDateYear.SelectedValue = Convert.ToString(dtTemp.Year);
        ddlToDateYear.SelectedValue = Convert.ToString(dtTemp.Year + 1);
    }

    /// <summary>
    /// Bind DropDowns
    /// </summary>
    private void BindDropDowns()
    {
        DateTime dtTemp = DateTime.Now;
        General.BindMonthDropDown(ref ddlFromDateMonth);
        General.BindMonthDropDown(ref ddlToDateMonth);
        General.BindYearDropDown(ref ddlFromDateYear, 2000, dtTemp.Year + 5);
        General.BindYearDropDown(ref ddlToDateYear, 2000, dtTemp.Year + 5);

        List<HouseType> lstHouseType = HouseType.SelectAll();
        General.DropDownListBind(ref ddlHouseType, lstHouseType, false, "HouseTypeId", "HouseTypeName", "");
    }

    #endregion
}