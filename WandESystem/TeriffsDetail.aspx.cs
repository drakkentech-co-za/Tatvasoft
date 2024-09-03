using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

/// <summary>
/// Summary TeriffDetail - Add/Edit teriff
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 30-Aug-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 2-Sep-2013 </ModifiedDate>
public partial class TeriffsDetail : BasePageHome
{
    #region Variable/Property Declaration

    /// <summary>
    /// teriff Id
    /// </summary>
    public int TeriffId
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
            SetRights(PageRole.SystemPages.TariffsDetail.GetHashCode());
            BindDropDown();
            if (TeriffId > 0)
            {
                btnSave.Text = "Update";
                FillControls();
            }

            txtCategoryName.Focus();
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

            Teriff objTeriff;

            if (TeriffId > 0)
            {
                actionType = ActionType.Update.GetHashCode();
                objTeriff = new Teriff(TeriffId);
            }
            else
            {
                objTeriff = new Teriff();
            }

            FillObject(ref objTeriff);

            objTeriff.Save();
            Response.Redirect("Teriffs.aspx?at=" + actionType);
        }
    }

    /// <summary>
    /// handles Cancel Click.Redirect to teriffs page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Teriffs.aspx");
    }

    #endregion

    #region Methods/Events

    /// <summary>
    /// Bind Dropdown controls
    /// </summary>
    private void BindDropDown()
    {
        General.BindCategoryType(ref ddlCategoryType);
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
    private void FillObject(ref Teriff  objTeriff)
    {
        objTeriff.TeriffId = this.TeriffId;
        objTeriff.CategoryName = Server.HtmlEncode(txtCategoryName.Text.Trim());
        objTeriff.CategoryValue = Server.HtmlEncode(txtCategoryValue.Text.Trim());
        objTeriff.CategoryType = ConvertTo.Integer(ddlCategoryType.SelectedValue);
        objTeriff.BIncludeVAT = chkIncludeVAT.Checked;

        if (TeriffId > 0)
        {
            objTeriff.Updated_TS = DateTime.Now;
            objTeriff.Updated_UserId = ProjectSession.UserID;
        }
        else
        {
            objTeriff.Created_TS = DateTime.Now;
            objTeriff.Created_UserId = ProjectSession.UserID;
        }
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        Teriff  objTeriff = new Teriff(this.TeriffId);

        txtCategoryName.Text = objTeriff.CategoryName;
        txtCategoryValue.Text = objTeriff.CategoryValue;
        chkIncludeVAT.Checked =Convert.ToBoolean(objTeriff.BIncludeVAT);
        if (objTeriff.CategoryType == 0)
            ddlCategoryType.SelectedIndex = -1;
        else
            ddlCategoryType.SelectedValue = ConvertTo.String(objTeriff.CategoryType);            
    }

    #endregion
}