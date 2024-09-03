using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

/// <summary>
/// Summary of ElectricUnitPrice - Search Electric Unit Price , Delete  Electric Unit Price
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 16-Sep-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 16-Sep-2013 </ModifiedDate>
public partial class ElectricUnitPrices : BasePageHome 
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
            SetRights(PageRole.SystemPages.ElectricUnitPrices.GetHashCode());

            int actionType = 0;
            if (int.TryParse(GetPlainQueryString("at"), out actionType))
            {
                if (actionType == ActionType.Save.GetHashCode())
                    ShowMessage("Electric Unit Price Created Successfully.", lblMessage, MessageBoxType.Success);
                else if (actionType == ActionType.Update.GetHashCode())
                    ShowMessage("Electric Unit Price Updated Successfully.", lblMessage, MessageBoxType.Success);
            }

            SortBy = "StartDate";
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
    protected void gvUnitPrice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUnitPrice.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    /// <summary>
    /// Handle Row Command Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUnitPrice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditUnitPrice")
        {
            int unitPriceId = int.Parse(e.CommandArgument.ToString());

            if (unitPriceId > 0)
                Response.Redirect("ElectricUnitPricesDetail.aspx?ID=" + EncryptionDecryption.GetEncrypt(unitPriceId.ToString()));
        }
        else if (e.CommandName == "DeleteUnitPrice")
        {
            string deleteMessage = ElectricUnitPrice.Delete(ConvertTo.Integer(e.CommandArgument));
            if (string.IsNullOrEmpty(deleteMessage))
                ShowMessage("Electric Unit Price has been deleted successfully.", lblMessage, MessageBoxType.Information);
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
    protected void gvUnitPrice_Sorting(object sender, GridViewSortEventArgs e)
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
    /// Bind electricunitprice Grid
    /// </summary>
    private void BindGrid()
    {
        string searchText = txtSearch.Text.Trim();

        DataSet ds = ElectricUnitPrice.GetGridData(searchText);

        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            if (!string.IsNullOrEmpty(SortBy))
            {
                dv.Sort = SortBy + " " + OrderBy;
            }

            gvUnitPrice.DataSource = dv;
        }
        gvUnitPrice.DataBind();
    }
    #endregion
}