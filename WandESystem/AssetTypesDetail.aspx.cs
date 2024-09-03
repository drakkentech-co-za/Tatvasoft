using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

/// <summary>
/// Summary AssetTypeDetail - Add/Edit Asset Type
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 11-Sep-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 11-Sep-2013 </ModifiedDate>
public partial class AssetTypesDetail : BasePageHome 
{
    #region Variable/Property Declaration

    /// <summary>
    /// HouseTypeId
    /// </summary>
    public int AssetTypeId
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
            SetRights(PageRole.SystemPages.AssetTypesDetail.GetHashCode());
            if (AssetTypeId > 0)
            {
                btnSave.Text = "Update";
                FillControls();
            }

            txtAssetTypeName.Focus();
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

            AssetType objAssetType;

            if (AssetTypeId > 0)
            {
                actionType = ActionType.Update.GetHashCode();
                objAssetType = new AssetType(AssetTypeId);
            }
            else
            {
                objAssetType = new AssetType();
            }

            FillObject(ref objAssetType);
            objAssetType.Save();

            Response.Redirect("AssetTypes.aspx?at=" + actionType);
        }
    }

    /// <summary>
    /// handles Cancel Click.Redirect to assettypes page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AssetTypes.aspx");
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
            return false;
        if (DataAccess.General.CheckDuplicateRecords("tbl_AssetType", "AssetTypeName", txtAssetTypeName.Text.Trim(), "AssetTypeId", this.AssetTypeId.ToString()))
        {
            ShowMessage("Asset Type name already exists", lblMessage, MessageBoxType.Warning);
            return false;
        }
        return isValid;
    }

    /// <summary>
    /// Fill Object
    /// </summary>
    private void FillObject(ref AssetType objAssetType)
    {
        objAssetType.AssetTypeId = this.AssetTypeId;
        objAssetType.AssetTypeName = Server.HtmlEncode(txtAssetTypeName.Text.Trim());

        if (AssetTypeId > 0)
        {
            objAssetType.Updated_UserId = ProjectSession.UserID;
            objAssetType.Updated_TS = DateTime.Now;
        }
        else
        {
            objAssetType.Created_UserId = ProjectSession.UserID;
            objAssetType.Created_TS = DateTime.Now;
        }
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        AssetType objAssetType = new AssetType(this.AssetTypeId);

        txtAssetTypeName.Text = objAssetType.AssetTypeName;
    }

    #endregion
}