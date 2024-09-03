using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

/// <summary>
/// Summary of WaterRateLimit - Search WaterRateLimit , Delete WaterRateLimit
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 13-Sep-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 13-Sep-2013 </ModifiedDate>
public partial class WaterRateLimits : BasePageHome 
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
            SetRights(PageRole.SystemPages.WaterRateLimits.GetHashCode());

            int actionType = 0;
            if (int.TryParse(GetPlainQueryString("at"), out actionType))
            {
                if (actionType == ActionType.Save.GetHashCode())
                    ShowMessage("Water Rate Limit Created Successfully.", lblMessage, MessageBoxType.Success);
                else if (actionType == ActionType.Update.GetHashCode())
                    ShowMessage("Water Rate Limit Updated Successfully.", lblMessage, MessageBoxType.Success);
            }

            SortBy = "Rate1";
            OrderBy = SystemEnum.SortDirection.asc.ToString();
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
    protected void gvWaterRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWaterRate.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    /// <summary>
    /// Handle Row Command Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWaterRate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditWaterRate")
        {
            int waterScaleId = int.Parse(e.CommandArgument.ToString());

            if (waterScaleId > 0)
                Response.Redirect("WaterRateLimitsDetail.aspx?ID=" + EncryptionDecryption.GetEncrypt(waterScaleId.ToString()));
        }
        else if (e.CommandName == "DeleteWaterRate")
        {
            string deleteMessage = WaterRate_Limit.Delete(ConvertTo.Integer(e.CommandArgument));
            if (string.IsNullOrEmpty(deleteMessage))
                ShowMessage("Water Rate Limit has been deleted successfully.", lblMessage, MessageBoxType.Information);
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
    protected void gvWaterRate_Sorting(object sender, GridViewSortEventArgs e)
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
    /// Bind waterscale Grid
    /// </summary>
    private void BindGrid()
    {
        string searchText = txtSearch.Text.Trim();

        DataSet ds = WaterRate_Limit.GetGridData(searchText);

        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            if (!string.IsNullOrEmpty(SortBy))
            {
                dv.Sort = SortBy + " " + OrderBy;
            }

            gvWaterRate.DataSource = dv;
        }
        gvWaterRate.DataBind();
    }
    #endregion
}