using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

/// <summary>
/// Summary ElectricUnitPriceDetail - Add/Edit electric unit price
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 16-Sep-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 16-Sep-2013 </ModifiedDate>
public partial class ElectricUnitPricesDetail : BasePageHome 
{
    #region Variable/Property Declaration

    /// <summary>
    /// Electric Unit Price Id
    /// </summary>
    public int ElectricUnitPriceId
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
            SetRights(PageRole.SystemPages.ElectricUnitPricesDetail.GetHashCode());
            BindDropDowns();

            if (ElectricUnitPriceId > 0)
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

            ElectricUnitPrice  objUnitPrice;

            if (ElectricUnitPriceId > 0)
            {
                actionType = ActionType.Update.GetHashCode();
                objUnitPrice = new ElectricUnitPrice(ElectricUnitPriceId);
            }
            else
            {
                objUnitPrice = new ElectricUnitPrice();
            }

            FillObject(ref objUnitPrice);

            objUnitPrice.Save();

            Response.Redirect("ElectricUnitPrices.aspx?at=" + actionType);
        }
    }

    /// <summary>
    /// handles Cancel Click.Redirect to electricunitprice page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ElectricUnitPrices.aspx");
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
    private void FillObject(ref ElectricUnitPrice  objUnitPrice)
    {
        objUnitPrice.ElectricUnitPriceId = this.ElectricUnitPriceId;

        objUnitPrice.UnitPrice  = ConvertTo.Decimal(txtElectricUnitPrice.Text.Trim());

        GetFromAndToDate(ref objUnitPrice);
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        ElectricUnitPrice objUnitPrice = new ElectricUnitPrice(this.ElectricUnitPriceId);

        txtElectricUnitPrice.Text = string.Format("{0:0.0000}", objUnitPrice.UnitPrice);
        ddlFromDateMonth.SelectedValue = Convert.ToString((objUnitPrice.StartDate).Month);
        ddlToDateMonth.SelectedValue = Convert.ToString((objUnitPrice.EndDate).Month);
        ddlFromDateYear.SelectedValue = Convert.ToString((objUnitPrice.StartDate).Year);
        ddlToDateYear.SelectedValue = Convert.ToString((objUnitPrice.EndDate).Year);
    }

    /// <summary>
    /// Get From Date To Date from dropdown selected value
    /// </summary>
    private void GetFromAndToDate(ref ElectricUnitPrice objUnitPrice)
    {
        DateTime dtFromDate = new DateTime(Convert.ToInt32(ddlFromDateYear.SelectedValue), Convert.ToInt32(ddlFromDateMonth.SelectedValue), 1);
        DateTime dtToDate = new DateTime(Convert.ToInt32(ddlToDateYear.SelectedValue), Convert.ToInt32(ddlToDateMonth.SelectedValue), 1);
        dtToDate = dtToDate.AddMonths(1).AddDays(-1);

        objUnitPrice.StartDate = dtFromDate;
        objUnitPrice.EndDate = dtToDate;
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