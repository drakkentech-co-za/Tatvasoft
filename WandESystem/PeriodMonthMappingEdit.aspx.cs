using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

public partial class PeriodMonthMappingEdit : BasePageHome
{
    #region Variable/Property Declaration
    /// <summary>
    /// Period Month Id
    /// </summary>
    public int PeriodMonthId
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
            SetRights(PageRole.SystemPages.PeriodMonthMappingEdit.GetHashCode());
            BindDropDowns();

            if (PeriodMonthId > 0)
            {
                btnSave.Text = "Update";
                FillControls();
            }
            else
            {
                if (ddlPeriod.Items.Count > 0)
                {
                    SetDefaultDate();
                }
                else
                {
                    ShowMessage("No period avilable to be mapped", lblMessage, MessageBoxType.Information);
                    trControls.Visible = false;
                }
            }
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

            Period_Month objPeriodMonth;

            if (PeriodMonthId > 0)
            {
                actionType = ActionType.Update.GetHashCode();
                objPeriodMonth = new Period_Month(PeriodMonthId);
            }
            else
            {
                objPeriodMonth = new Period_Month();
            }

            FillObject(ref objPeriodMonth);

            if (Period_Month.CheckConflict(objPeriodMonth))
            {
                ShowMessage("Selected Month and Year is aleady mapped", lblMessage, MessageBoxType.Warning);
            }
            else
            {
                objPeriodMonth.Save();
                Response.Redirect("PeriodMonthMappingList.aspx?at=" + actionType);
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
        Response.Redirect("PeriodMonthMappingList.aspx");
    }

    #endregion

    #region Methods/Events

    /// <summary>
    /// set default date (sets from date less than one month from today.set To date as today's date)
    /// </summary>
    private void SetDefaultDate()
    {
        DateTime dtTemp = DateTime.Now;
        ddlDateMonth.SelectedValue = Convert.ToString(dtTemp.Month);
        ddlDateYear.SelectedValue = Convert.ToString(dtTemp.Year);
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
    private void FillObject(ref Period_Month objPeriodMonth)
    {
        objPeriodMonth.PeriodMonthId = this.PeriodMonthId;

        if (PeriodMonthId > 0)
            objPeriodMonth.PeriodId = ConvertTo.Integer(lblPeriod.Text);
        else
            objPeriodMonth.PeriodId = ConvertTo.Integer(ddlPeriod.SelectedValue);

        objPeriodMonth.Month = ConvertTo.Integer(ddlDateMonth.SelectedValue);

        objPeriodMonth.Year = ConvertTo.Integer(ddlDateYear.SelectedValue);

        if (PeriodMonthId > 0)
        {
            objPeriodMonth.Updated_TS = DateTime.Now;
            objPeriodMonth.Updated_UserId = ProjectSession.UserID;
        }
        else
        {
            objPeriodMonth.Created_TS = DateTime.Now;
            objPeriodMonth.Created_UserId = ProjectSession.UserID;
        }
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        Period_Month objPeriodMonth = new Period_Month(this.PeriodMonthId);
        ddlPeriod.SelectedValue = ConvertTo.String(objPeriodMonth.PeriodId);
        lblPeriod.Text = ConvertTo.String(objPeriodMonth.PeriodId);
        ddlDateMonth.SelectedValue = Convert.ToString((objPeriodMonth.Month));
        ddlDateYear.SelectedValue = Convert.ToString((objPeriodMonth.Year));

        ddlPeriod.Visible = false;
        lblPeriod.Visible = true;
    }

    /// <summary>
    /// Bind DropDowns
    /// </summary>
    private void BindDropDowns()
    {
        DateTime dtTemp = DateTime.Now;
        General.BindMonthDropDown(ref ddlDateMonth);
        General.BindYearDropDown(ref ddlDateYear, 2000, dtTemp.Year + 5);
        General.BindPeriodIds(ref ddlPeriod);
    }

    #endregion
}