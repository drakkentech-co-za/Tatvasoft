using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

/// <summary>
/// Summary of HouseType - Search housetype , Delete housetype
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 29-Aug-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 29-Aug-2013 </ModifiedDate>
public partial class HouseTypes : BasePageHome 
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
            SetRights(PageRole.SystemPages.HouseTypes.GetHashCode());

            int actionType = 0;
            if (int.TryParse(GetPlainQueryString("at"), out actionType))
            {
                if (actionType == ActionType.Save.GetHashCode())
                    ShowMessage("House Type Created Successfully.", lblMessage, MessageBoxType.Success);
                else if (actionType == ActionType.Update.GetHashCode())
                    ShowMessage("House Type Updated Successfully.", lblMessage, MessageBoxType.Success);
            }

            SortBy = "HouseTypeName";
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
    protected void gvHouseType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHouseType.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    /// <summary>
    /// Handle Row Command Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHouseType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditHouseType")
        {
            int houseTypeId = int.Parse(e.CommandArgument.ToString());

            if (houseTypeId > 0)
                Response.Redirect("HouseTypesDetail.aspx?ID=" + EncryptionDecryption.GetEncrypt(houseTypeId.ToString()));
        }
        else if (e.CommandName == "DeleteHouseType")
        {
            string deleteMessage = HouseType.Delete(ConvertTo.Integer(e.CommandArgument));
            if (string.IsNullOrEmpty(deleteMessage))
                ShowMessage("House Type has been deleted successfully.", lblMessage, MessageBoxType.Information);
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
    protected void gvHouseType_Sorting(object sender, GridViewSortEventArgs e)
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
    /// Bind HouseType Grid
    /// </summary>
    private void BindGrid()
    {
        string searchText = txtSearch.Text.Trim();

        DataSet ds = HouseType.GetGridData(searchText);
        
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            if (!string.IsNullOrEmpty(SortBy))
            {
                dv.Sort = SortBy + " " + OrderBy;
            }

            gvHouseType.DataSource = dv;
        }
        gvHouseType.DataBind();
    }
    #endregion
}