using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

public partial class PeriodMonthMappingList :BasePageHome
{
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
            SetRights(PageRole.SystemPages.PeriodMonthMappingList.GetHashCode());
            int actionType = 0;
            if (int.TryParse(GetPlainQueryString("at"), out actionType))
            {
                if (actionType == ActionType.Save.GetHashCode())
                    ShowMessage("Period Mapped Successfully.", lblMessage, MessageBoxType.Success);
                else if (actionType == ActionType.Update.GetHashCode())
                    ShowMessage("Period Mapped Successfully.", lblMessage, MessageBoxType.Success);
            }

            SortBy = "PeriodId";
            OrderBy = SystemEnum.SortDirection.asc.ToString();
            BindGrid();
        }
    }

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
    protected void gvPeriods_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPeriods.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    /// <summary>
    /// Handle Row Command Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPeriods_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditPeriod")
        {
            int periodId = int.Parse(e.CommandArgument.ToString());

            if (periodId > 0)
                Response.Redirect("PeriodMonthMappingEdit.aspx?ID=" + EncryptionDecryption.GetEncrypt(periodId.ToString()));
        }
        else if (e.CommandName == "DeletePeriod")
        {
            string deleteMessage = Period_Month.Delete(ConvertTo.Integer(e.CommandArgument));
            if (string.IsNullOrEmpty(deleteMessage))
                ShowMessage("Period has been deleted successfully.", lblMessage, MessageBoxType.Information);
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
    protected void gvPeriods_Sorting(object sender, GridViewSortEventArgs e)
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

        DataSet ds = Period_Month.GetGridData(searchText);

        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            if (!string.IsNullOrEmpty(SortBy))
            {
                dv.Sort = SortBy + " " + OrderBy;
            }

            gvPeriods.DataSource = dv;
        }
        gvPeriods.DataBind();
    }
    #endregion
}