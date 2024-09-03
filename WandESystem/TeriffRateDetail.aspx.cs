using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

/// <summary>
/// Summary TeriffRateDetail - Add/Edit teriff rate
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 02-Sep-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 02-Sep-2013 </ModifiedDate>
public partial class TeriffRateDetail : BasePageHome 
{
    #region Variable/Property Declaration

    /// <summary>
    /// teriff rate Id
    /// </summary>
    public int TeriffRateId
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
            SetRights(PageRole.SystemPages.TariffRateDetail.GetHashCode());
            General.BindTeriff(ref ddlteriff);

            BindDropDowns();

            if (TeriffRateId > 0)
            {
                btnSave.Text = "Update";
                FillControls();
                
            }
            else
            {
                SetDefaultDate();
            }

            ddlteriff.Focus();
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

            Teriff_Rate objTeriffRate;

            if (TeriffRateId > 0)
            {
                actionType = ActionType.Update.GetHashCode();
                objTeriffRate = new Teriff_Rate(TeriffRateId);
            }
            else
            {
                objTeriffRate = new Teriff_Rate();
            }

            FillObject(ref objTeriffRate);

            if (Teriff_Rate.CheckTimeConflict(objTeriffRate))
            {
                ShowMessage("TariffRate for selected time interval exists. Select other time interval.", lblMessage, MessageBoxType.Warning);
            }
            else 
            {
                objTeriffRate.Save();
                Response.Redirect("TeriffRate.aspx?at=" + actionType);
            }
        }
    }

    /// <summary>
    /// handles Cancel Click.Redirect to teriff rate page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeriffRate.aspx");
    }

    #endregion

    #region Methods/Events

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
    private void FillObject(ref Teriff_Rate objTeriffRate)
    {
        objTeriffRate.TeriffId = this.TeriffRateId;

        if ( ConvertTo.Integer(ddlteriff.SelectedValue) > 0)
            objTeriffRate.TeriffId = ConvertTo.Integer(ddlteriff.SelectedValue);

        objTeriffRate.TeriffRate = ConvertTo.Float(txtTeriffRate.Text.Trim());
       // objTeriffRate.DateFrom = ConvertTo.Date(txtDateFrom.Text.Trim());
       // objTeriffRate.DateTo = ConvertTo.Date(txtDateTo.Text.Trim());

       // objTeriffRate.BIncludeVAT = chkIncludeVAT.Checked;
        GetFromAndToDate(ref objTeriffRate);

        if (TeriffRateId > 0)
        {
            objTeriffRate.Updated_TS = DateTime.Now;
            objTeriffRate.Updated_UserId = ProjectSession.UserID;
        }
        else
        {
            objTeriffRate.Created_TS = DateTime.Now;
            objTeriffRate.Created_UserId = ProjectSession.UserID;
        }
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        Teriff_Rate objTeriffRate = new Teriff_Rate(this.TeriffRateId);

        ddlteriff.SelectedValue = ConvertTo.String(objTeriffRate.TeriffId);
        txtTeriffRate.Text = string.Format("{0:0.0000}", objTeriffRate.TeriffRate);
        //txtDateFrom.Text = General.GetStringDate(ConvertTo.String(objTeriffRate.DateFrom));
        ddlFromDateMonth.SelectedValue = Convert.ToString((objTeriffRate.DateFrom).Month);
        ddlToDateMonth.SelectedValue = Convert.ToString((objTeriffRate.DateTo).Month);
        ddlFromDateYear.SelectedValue = Convert.ToString((objTeriffRate.DateFrom).Year);
        ddlToDateYear.SelectedValue = Convert.ToString((objTeriffRate.DateTo).Year);

        //txtDateTo.Text = General.GetStringDate(ConvertTo.String(objTeriffRate.DateTo));
        //chkIncludeVAT.Checked = ConvertTo.Boolean(objTeriffRate.BIncludeVAT);
    }

    /// <summary>
    /// Bind DropDowns
    /// </summary>
    private void BindDropDowns()
    {
        DateTime dtTemp=DateTime.Now;
        General.BindMonthDropDown(ref ddlFromDateMonth);
        General.BindMonthDropDown(ref ddlToDateMonth);
        General.BindYearDropDown(ref ddlFromDateYear, 2000, dtTemp.Year+5);
        General.BindYearDropDown(ref ddlToDateYear, 2000, dtTemp.Year + 5);

    }


    /// <summary>
    /// Get From Date To Date from dropdown selected value
    /// </summary>
    private void GetFromAndToDate(ref Teriff_Rate objTeriffRate)
    {
        DateTime dtFromDate=new DateTime(Convert.ToInt32(ddlFromDateYear.SelectedValue),Convert.ToInt32(ddlFromDateMonth.SelectedValue),1);
        DateTime dtToDate = new DateTime(Convert.ToInt32(ddlToDateYear.SelectedValue),  Convert.ToInt32(ddlToDateMonth.SelectedValue), 1);
        dtToDate = dtToDate.AddMonths(1).AddDays(-1);

        objTeriffRate.DateFrom = dtFromDate;
        objTeriffRate.DateTo = dtToDate;

    }
    #endregion
    
}