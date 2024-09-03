using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

/// <summary>
/// Summary of teriffs - Search teriffs , Delete teriffs
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 30-Aug-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 30-Aug-2013 </ModifiedDate>
public partial class Teriffs : BasePageHome
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
            SetRights(PageRole.SystemPages.Tariffs.GetHashCode());

            int actionType = 0;
            if (int.TryParse(GetPlainQueryString("at"), out actionType))
            {
                if (actionType == ActionType.Save.GetHashCode())
                    ShowMessage("Tariff Created Successfully.", lblMessage, MessageBoxType.Success);
                else if (actionType == ActionType.Update.GetHashCode())
                    ShowMessage("Tariff Updated Successfully.", lblMessage, MessageBoxType.Success);
            }

            SortBy = "CategoryName";
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
    protected void gvTeriffs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTeriffs.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    /// <summary>
    /// Handle Row Command Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvTeriffs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditTeriffs")
        {
            int periodId = int.Parse(e.CommandArgument.ToString());

            if (periodId > 0)
                Response.Redirect("TeriffsDetail.aspx?ID=" + EncryptionDecryption.GetEncrypt(periodId.ToString()));
        }
        else if (e.CommandName == "DeleteTeriffs")
        {
            string deleteMessage = Teriff.Delete(ConvertTo.Integer(e.CommandArgument));
            if (string.IsNullOrEmpty(deleteMessage))
                ShowMessage("Tariff has been deleted successfully.", lblMessage, MessageBoxType.Information);
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
    protected void gvTeriffs_Sorting(object sender, GridViewSortEventArgs e)
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
    /// Bind periods Grid
    /// </summary>
    private void BindGrid()
    {
        string searchText = txtSearch.Text.Trim();

        DataSet ds = Teriff.GetGridData(searchText);

        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            if (!string.IsNullOrEmpty(SortBy))
            {
                dv.Sort = SortBy + " " + OrderBy;
            }

            gvTeriffs.DataSource = dv;
        }
        gvTeriffs.DataBind();
    }
    #endregion
}