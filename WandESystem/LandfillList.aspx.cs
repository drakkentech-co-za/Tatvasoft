using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;

public partial class LandfillList : BasePageHome
{
    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkAdmin');");
        
        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.LandfillList.GetHashCode());
            int actionType = 0;
            if (int.TryParse(GetPlainQueryString("at"), out actionType))
            {
                if (actionType == ActionType.Save.GetHashCode())
                    ShowMessage("Landfill Amount Saved Successfully.", lblMessage, MessageBoxType.Success);
                else if (actionType == ActionType.Update.GetHashCode())
                    ShowMessage("Landfill Amount Updated Successfully.", lblMessage, MessageBoxType.Success);
            }

            SortBy = "Period";
            BindLandfillDetails();
        }
    }
    #endregion

    #region privateMethods

    private void BindLandfillDetails()
    {        
        DataSet ds = Landfill.GetLandfill();

        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            if (!string.IsNullOrEmpty(SortBy))
            {
                dv.Sort = SortBy + " " + OrderBy;
            }

            gvLandfill.DataSource = dv;
        }
        gvLandfill.DataBind();


    }
    #endregion

    #region Events
    /// <summary>
    /// Perform Search
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindLandfillDetails();
        lblMessage.Visible = false;
    }
    #endregion

    #region GridEvents    

    /// <summary>
    /// Handle Paging Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLandfill_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLandfill.PageIndex = e.NewPageIndex;
        BindLandfillDetails();
    }

    /// <summary>
    /// Handle Row Command Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLandfill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditLandfill")
        {
            int landfillId = int.Parse(e.CommandArgument.ToString());

            if (landfillId > 0)
                Response.Redirect("LandfillEdit.aspx?ID=" + EncryptionDecryption.GetEncrypt(landfillId.ToString()));
        }
        else if (e.CommandName == "DeleteLandfill")
        {
            string deleteMessage = Landfill.Delete(ConvertTo.Integer(e.CommandArgument));
            if (string.IsNullOrEmpty(deleteMessage))
                ShowMessage("Landfill amount has been deleted successfully.", lblMessage, MessageBoxType.Information);
            else
                ShowMessage(deleteMessage, lblMessage, MessageBoxType.Warning);
            lblMessage.Visible = true;
            BindLandfillDetails();
        }               
    }

    /// <summary>
    /// Handle Sorting Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLandfill_Sorting(object sender, GridViewSortEventArgs e)
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
        BindLandfillDetails();
    }
    #endregion
}