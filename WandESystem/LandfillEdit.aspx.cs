using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

public partial class LandfillEdit : BasePageHome
{
    #region Variable/Property Declaration

    /// <summary>
    /// LandfillId
    /// </summary>
    public int LandfillId
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
            SetRights(PageRole.SystemPages.LandfillEdit.GetHashCode());
            fillCombo();
            if (LandfillId > 0)
            {
                btnSave.Text = "Update";
                FillControls();
                ddlPeriodId.Enabled = false;
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
        int actionType = ActionType.Save.GetHashCode();

        Landfill objLandfill;

        if (LandfillId > 0)
        {
            actionType = ActionType.Update.GetHashCode();
            objLandfill = new Landfill(LandfillId);
        }
        else
        {
            objLandfill = new Landfill();
        }
        FillObject(ref objLandfill);
        objLandfill.Save();
        Response.Redirect("LandfillList.aspx?at=" + actionType);
    }

    /// <summary>
    /// handles Cancel Click.Redirect to EmployeeList page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("LandfillList.aspx");
    }


    #endregion

    #region Methods

    /// <summary>
    /// Fill All DropDownlist
    /// </summary>
    private void fillCombo()
    {
        if(LandfillId > 0)
            General.BindPeriodMonthYear(ref ddlPeriodId);
        else
            General.BindPeriodForLandfill(ref ddlPeriodId);
    }

    /// <summary>
    /// Fill Object
    /// </summary>
    private void FillObject(ref Landfill objLandfill)
    {
        objLandfill.LandfillId = this.LandfillId;
        objLandfill.PeriodId = Convert.ToInt32(ddlPeriodId.SelectedValue);
        objLandfill.LandfillAmount = ConvertTo.Decimal(txtLandfillAmount.Text.Trim());

        if (LandfillId > 0)
        {
            objLandfill.Updated_UserId = ProjectSession.UserID;
            objLandfill.Updated_TS = DateTime.Now;
        }
        else
        {
            objLandfill.Created_UserId = ProjectSession.UserID;
            objLandfill.Created_TS = DateTime.Now;
        }
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        Landfill objLandfill = new Landfill(this.LandfillId);
        ddlPeriodId.SelectedValue = Convert.ToString(objLandfill.PeriodId);
        txtLandfillAmount.Text = Convert.ToString(objLandfill.LandfillAmount);
    }

    #endregion
}