using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;

public partial class HouseList : BasePageHome
{
    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkAdmin');");
        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.HouseList.GetHashCode());
            int actionType = 0;
            if (int.TryParse(GetPlainQueryString("at"), out actionType))
            {
                if (actionType == ActionType.Save.GetHashCode())
                    ShowMessage("Account Created Successfully.", lblMessage, MessageBoxType.Success);
                else if (actionType == ActionType.Update.GetHashCode())
                    ShowMessage("Account Updated Successfully.", lblMessage, MessageBoxType.Success);
            }

            SortBy = "AccountNo";
            BindAccountDetails();
        }
    }
    #endregion

    #region privateMethods

    private void BindAccountDetails()
    {
        string searchText = txtSearch.Text.Trim();

        DataSet ds = House.GetAllAccounts(searchText);

        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            if (!string.IsNullOrEmpty(SortBy))
            {
                dv.Sort = SortBy + " " + OrderBy;
            }

            gvHouse.DataSource = dv;
        }
        gvHouse.DataBind();


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
        BindAccountDetails();
        lblMessage.Visible = false;
    }
    #endregion

    #region GridEvents
    protected void gvHouse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblElectricityType = (Label)e.Row.FindControl("lblElectricityType");
            Label lblPrePaidAllowed = (Label)e.Row.FindControl("lblPrePaidAllowed");
            bool ElectricityType = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "ElectricityType"));

            if (ElectricityType)
                lblElectricityType.Text = House.ElectricityTypes.PostPaid.ToString();
            else
                lblElectricityType.Text = House.ElectricityTypes.PrePaid.ToString();

            bool PrepaidAllowed = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "PrepaidAllowed"));
            if (PrepaidAllowed)
                lblPrePaidAllowed.Text = "Yes";
            else
                lblPrePaidAllowed.Text = "No";

            bool IsRental = ConvertTo.Boolean(DataBinder.Eval(e.Row.DataItem, "IsRental"));
            if (IsRental)
            {
                LinkButton lnkCategories = (LinkButton)e.Row.FindControl("lnkCategories");
                LinkButton lnkEmployee = (LinkButton)e.Row.FindControl("lnkEmployee");
                lnkCategories.Text = "";
                lnkEmployee.Text = "";
            }
            ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
            bool IsActive = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive"));
            if (!IsActive)
                imgEdit.Visible = false;
            //if (!ProjectSession.IsAdmin)
            //    gvHouse.Columns[12].Visible = false; 
        }
    }

    /// <summary>
    /// Handle Paging Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHouse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHouse.PageIndex = e.NewPageIndex;
        BindAccountDetails();
    }

    /// <summary>
    /// Handle Row Command Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHouse_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditHouse")
        {
            int houseId = int.Parse(e.CommandArgument.ToString());

            if (houseId > 0)
                Response.Redirect("HouseEdit.aspx?ID=" + EncryptionDecryption.GetEncrypt(houseId.ToString()));
        }
        //else if (e.CommandName == "DeleteHouse")
        //{
        //    string deleteMessage = House.Delete(ConvertTo.Integer(e.CommandArgument));
        //    if (string.IsNullOrEmpty(deleteMessage))
        //        ShowMessage("Account has been deleted successfully.", lblMessage, MessageBoxType.Information);
        //    else
        //        ShowMessage(deleteMessage, lblMessage, MessageBoxType.Warning);
        //    lblMessage.Visible = true;
        //    BindAccountDetails();
        //}
        else if (e.CommandName == "EditCategories")
        {
            int houseId = int.Parse(e.CommandArgument.ToString());

            if (houseId > 0)
                Response.Redirect("HouseTeriffMapping.aspx?ID=" + EncryptionDecryption.GetEncrypt(houseId.ToString()));
        }
        else if (e.CommandName == "DeActiveAccount")
        {
            int houseId = int.Parse(e.CommandArgument.ToString());
            if (houseId > 0)
            {
                int EmployeeId = House.GetForRelatedEmployee(houseId);
                if (EmployeeId > 0)
                    ShowMessage("Selected Account is linked with Employee. To de-active this account please de-active it's linked employee first.", lblMessage, MessageBoxType.Error);
                else
                {
                    string deleteMessage = House.DeActivate(ConvertTo.Integer(e.CommandArgument),false);
                    if (string.IsNullOrEmpty(deleteMessage))
                        ShowMessage("Account has been deleted successfully.", lblMessage, MessageBoxType.Success);
                    else
                        ShowMessage(deleteMessage, lblMessage, MessageBoxType.Warning);
                    lblMessage.Visible = true;
                }
            }
            BindAccountDetails();
        }
        else if (e.CommandName == "ActiveAccount")
        {
            int houseId = int.Parse(e.CommandArgument.ToString());
            if (houseId > 0)
            {
                int EmployeeId = House.GetForRelatedEmployee(houseId);
                if (EmployeeId > 0)
                    ShowMessage("Selected Account is linked with Employee. To de-active this account please de-active it's linked employee first.", lblMessage, MessageBoxType.Error);
                else
                {
                    string deleteMessage = House.DeActivate(ConvertTo.Integer(e.CommandArgument), true);
                    if (string.IsNullOrEmpty(deleteMessage))
                        ShowMessage("Account has been deleted successfully.", lblMessage, MessageBoxType.Success);
                    else
                        ShowMessage(deleteMessage, lblMessage, MessageBoxType.Warning);
                    lblMessage.Visible = true;
                }
            }
            BindAccountDetails();
        }
    }

    /// <summary>
    /// Handle Sorting Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHouse_Sorting(object sender, GridViewSortEventArgs e)
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
        BindAccountDetails();
    }
    #endregion
}