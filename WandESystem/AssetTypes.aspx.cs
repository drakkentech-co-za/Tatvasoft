using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

/// <summary>
/// Summary of AssetType - Search AssetType , Delete AssetType
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 11-Sep-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 11-Sep-2013 </ModifiedDate>
public partial class AssetTypes : BasePageHome 
{
    #region Variable/Property Declaration
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
        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.AssetTypes.GetHashCode());

            int actionType = 0;
            if (int.TryParse(GetPlainQueryString("at"), out actionType))
            {
                if (actionType == ActionType.Save.GetHashCode())
                    ShowMessage("Asset Type Created Successfully.", lblMessage, MessageBoxType.Success);
                else if (actionType == ActionType.Update.GetHashCode())
                    ShowMessage("Asset Type Updated Successfully.", lblMessage, MessageBoxType.Success);
            }

            SortBy = "AssetTypeName";
            BindGrid();
        }
    }

    #endregion

    #region Control Events

    /// <summary>
    /// Perform Search
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    /// <summary>
    /// Handle Paging Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAssetType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAssetType.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    /// <summary>
    /// Handle Row Command Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAssetType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditAssetType")
        {
            int assetTypeId = int.Parse(e.CommandArgument.ToString());

            if (assetTypeId > 0)
                Response.Redirect("AssetTypesDetail.aspx?ID=" + EncryptionDecryption.GetEncrypt(assetTypeId.ToString()));
        }
        else if (e.CommandName == "DeleteAssetType")
        {
            string deleteMessage = AssetType.Delete(ConvertTo.Integer(e.CommandArgument));
            if (string.IsNullOrEmpty(deleteMessage))
                ShowMessage("Asset Type has been deleted successfully.", lblMessage, MessageBoxType.Information);
            else
                ShowMessage(deleteMessage, lblMessage, MessageBoxType.Warning);
            BindGrid();
        }
    }

    /// <summary>
    /// Handle Sorting Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAssetType_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (SortBy == e.SortExpression)
        {
            if (OrderBy == SystemEnum.SortDirection.asc.ToString())
                OrderBy = SystemEnum.SortDirection.desc.ToString();
            else
                OrderBy = SystemEnum.SortDirection.asc.ToString();
        }
        else
        {
            SortBy = e.SortExpression;
            OrderBy = SystemEnum.SortDirection.asc.ToString();
        }
        BindGrid();
    }

    #endregion

    #region Methods/Functions

    /// <summary>
    /// Bind AssetType Grid
    /// </summary>
    private void BindGrid()
    {
        string searchText = txtSearch.Text.Trim();

        DataSet ds = AssetType.GetGridData(searchText);

        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            if (!string.IsNullOrEmpty(SortBy))
            {
                dv.Sort = SortBy + " " + OrderBy;
            }

            gvAssetType.DataSource = dv;
        }
        gvAssetType.DataBind();
    }
    #endregion
}