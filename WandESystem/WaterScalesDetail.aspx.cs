using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

/// <summary>
/// Summary WaterScaleDetail - Add/Edit water scale
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 29-Aug-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 02-Sep-2013 </ModifiedDate>
public partial class WaterScalesDetail : BasePageHome
{
    #region Variable/Property Declaration

    /// <summary>
    /// Water Scale Id
    /// </summary>
    public int WaterScaleId
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
            //General.BindPeriod(ref ddlPeriod);
            SetRights(PageRole.SystemPages.WaterScalesDetail.GetHashCode());
            BindDropDowns();

            if (WaterScaleId > 0)
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

            Water_Scale objWaterScale;

            if (WaterScaleId > 0)
            {
                actionType = ActionType.Update.GetHashCode();
                objWaterScale = new Water_Scale(WaterScaleId);
            }
            else
            {
                objWaterScale = new Water_Scale();
            }

            FillObject(ref objWaterScale);

            objWaterScale.Save();

            Response.Redirect("WaterScales.aspx?at=" + actionType);
        }
    }

    /// <summary>
    /// handles Cancel Click.Redirect to waterscale page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("WaterScales.aspx");
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
    private void FillObject(ref Water_Scale objWaterScale)
    {
        objWaterScale.WaterScaleId = this.WaterScaleId;

        objWaterScale.Limit = ConvertTo.Float(txtLimit.Text.Trim());
        objWaterScale.MultiplicationFactor = ConvertTo.Float(txtMultiplicationFactor.Text.Trim());

        GetFromAndToDate(ref objWaterScale);
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        Water_Scale objWaterScale = new Water_Scale(this.WaterScaleId);

        txtLimit.Text = string.Format("{0:0.0000}", objWaterScale.Limit);
        txtMultiplicationFactor.Text = string.Format("{0:0.0000}", objWaterScale.MultiplicationFactor);
        ddlFromDateMonth.SelectedValue = Convert.ToString((objWaterScale.DateFrom).Month);
        ddlToDateMonth.SelectedValue = Convert.ToString((objWaterScale.DateTo).Month);
        ddlFromDateYear.SelectedValue = Convert.ToString((objWaterScale.DateFrom).Year);
        ddlToDateYear.SelectedValue = Convert.ToString((objWaterScale.DateTo).Year);
    }

    /// <summary>
    /// Get From Date To Date from dropdown selected value
    /// </summary>
    private void GetFromAndToDate(ref Water_Scale objWaterScale)
    {
        DateTime dtFromDate = new DateTime(Convert.ToInt32(ddlFromDateYear.SelectedValue), Convert.ToInt32(ddlFromDateMonth.SelectedValue), 1);
        DateTime dtToDate = new DateTime(Convert.ToInt32(ddlToDateYear.SelectedValue), Convert.ToInt32(ddlToDateMonth.SelectedValue), 1);
        dtToDate = dtToDate.AddMonths(1).AddDays(-1);

        objWaterScale.DateFrom = dtFromDate;
        objWaterScale.DateTo = dtToDate;
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

    }

    #endregion
}