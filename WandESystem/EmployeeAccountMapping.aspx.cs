using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using System.Collections;

/// <summary>
/// EmployeeAccountmapping summary - maps employee with house 
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDare>03-Sep-2013</CreatedDare>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 03-Sep-2013 </ModifiedDate>
public partial class EmployeeAccountMapping : BasePageHome
{
    #region Variable/Property Declaration

    /// <summary>
    /// dtAccount - contains all account records not contains mapped records
    /// </summary>
    public DataTable dtAccount
    {
        get
        {
            if (ViewState["dtAccount"] != null)
                return (DataTable)ViewState["dtAccount"];
            else
            {
                ViewState["dtAccount"] = House.GetAll();
                return (DataTable)ViewState["dtAccount"];
            }
        }
        set
        {
            ViewState["dtAccount"] = value;
        }
    }

    /// <summary>
    /// dtMapAccount - table for mapped account records 
    /// </summary>
    public DataTable dtMapAccount
    {
        get
        {
            if (ViewState["dtMapAccount"] != null)
                return (DataTable)ViewState["dtMapAccount"];
            else
                return null;
        }
        set
        {
            ViewState["dtMapAccount"] = value;
        }
    }

    /// <summary>
    /// IsEditMode - returns mode whether edit or not
    /// </summary>
    public Boolean IsEditMode
    {
        get
        {
            if (ViewState["IsEditMode"] != null)
                return ConvertTo.Boolean(ViewState["IsEditMode"]);
            else
                return false;
        }
        set
        {
            ViewState["IsEditMode"] = value;
        }
    }

    /// <summary>
    /// Employee details
    /// </summary>
    public DataTable dtEmployee
    {
        get
        {
            if (ViewState["dtEmployee"] == null)
            {
                DataTable dt = Employee.SelectAllEmployee();

                ViewState["dtEmployee"] = dt;
            }

            return (DataTable)ViewState["dtEmployee"];
        }
    }

    #endregion

    #region "Page Events"

    /// <summary>
    /// Page Events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkAdmin');");

        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.EmployeeAccountMapping.GetHashCode());
            General.BindEmployeeNumber(ref ddlEmployeeNo);
            SetEmployeeNumber();
            GetTablesForMappedRecord(true);

            BindControls();
        }

        lblMessage.Text = string.Empty;
        lblMessage.CssClass = string.Empty;

        ScriptManager.RegisterStartupScript(upContent, upContent.GetType(), Guid.NewGuid().ToString(), "choosen();", true);
    }

    #endregion

    #region "Control Events"

    /// <summary>
    /// handles btnSave Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int employeeNumber = ConvertTo.Integer(ddlEmployeeNo.SelectedValue);
            if (ConvertTo.Integer(ddlEmployeeNo.SelectedValue) > 0)
            {
                Employee_House.DeleteByEmployeeId(employeeNumber);
                Employee_House.SaveMapRecords(dtMapAccount, employeeNumber, 0);

                ShowMessage("Accounts Mapped Successfully", lblMessage, MessageBoxType.Success);

                ViewState["dtAccount"] = House.GetAll();
                BindAccount();
                BindAccountGrid();
            }
            else
            {
                ShowMessage("Select Employee Number To Map Account", lblMessage, MessageBoxType.Warning);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    /// <summary>
    /// Handles btnsearch click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAccountGrid();
    }

    /// <summary>
    /// Handles btnBack Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string backtopage = GetPlainQueryString("from");
        if (backtopage == "emp")
            Response.Redirect("EmployeeList.aspx");
        else if (backtopage == "acc")
            Response.Redirect("HouseList.aspx");
    }

    /// <summary>
    /// Handles ddlEmployeeNo SelectedindexChanged event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEmployeeNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int employeeId = ConvertTo.Integer(ddlEmployeeNo.SelectedValue);
            ShowHideEmployeeName(employeeId);

            dtAccount = null;
            dtMapAccount = null;

            if (employeeId > 0)
            {
                GetTablesForMappedRecord();
                BindControls();
            }
            else
                ShowMessage("Select Employee Number To Map Account", lblMessage, MessageBoxType.Warning);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    #region "gvAccount Grid Control Event"

    /// <summary>
    /// handles gvAccounts RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddAccount")
        {
            int houseid = ConvertTo.Integer(e.CommandArgument.ToString());

            if (houseid > 0)
                MapRecord(houseid);
        }
    }

    /// <summary>
    /// Handles paging event of gvAccounts
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAccounts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAccounts.PageIndex = e.NewPageIndex;
        BindAccountGrid();
    }

    #endregion

    #region "gvMapAccount Grid Control Event"

    /// <summary>
    /// handles gvMapAccount RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMapACcount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveAccount")
        {
            int houseid = ConvertTo.Integer(e.CommandArgument.ToString());

            if (houseid > 0)
                UnMapRecord(houseid);
        }
    }

    /// <summary>
    /// handles paging event of gvMapAccount
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMapACcount_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMapACcount.PageIndex = e.NewPageIndex;
        BindMapAccountGrid();
    }

    #endregion

    #endregion

    #region "Methods"

    /// <summary>
    /// Bind AccountNumbet from tbl_house table in dropdown control
    /// </summary>
    /// <param name="ddlAccount"></param>
    /// <param name="selectNeed"></param>
    private void BindAccount()
    {
        DataTable dtAcc = dtAccount.Copy();
        DataView dvAcc = dtAcc.DefaultView;
        dvAcc.RowFilter = "Mapped=0";
    }

    /// <summary>
    /// Bind All grid controls of page
    /// </summary>
    private void BindControls()
    {
        BindAccountGrid();
        BindMapAccountGrid();
        BindAccount();
        ShowHideSave();
    }

    /// <summary>
    /// sho/hide save button if no account selected to map
    /// </summary>
    private void ShowHideSave()
    {
        if (!IsEditMode)
        {
            btnSave.Visible = false;
            if (dtMapAccount != null)
            {
                if (dtMapAccount.Rows.Count > 0)
                    btnSave.Visible = true;
            }
        }
        else
        {
            btnSave.Visible = true;
        }
    }

    /// <summary>
    /// Set employee number 
    /// </summary>
    private void SetEmployeeNumber()
    {
        try
        {
            string employeeNumber = GetPlainQueryString("eno");
            if (!string.IsNullOrEmpty(employeeNumber))
            {
                ListItem selecteLisItem = ddlEmployeeNo.Items.FindByText(employeeNumber);
                if (selecteLisItem != null)
                {
                    ddlEmployeeNo.SelectedValue = selecteLisItem.Value;
                    ShowHideEmployeeName(ConvertTo.Integer(selecteLisItem.Value));
                }
            }
            else
                ShowHideEmployeeName(0);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    /// <summary>
    /// Bind AccountGrid according to searchcriteria
    /// </summary>
    private void BindAccountGrid()
    {
        try
        {
            DataView dvAcc = dtAccount.Copy().DefaultView;
            dvAcc.RowFilter = "Mapped=0";

            DataTable dt = dvAcc.ToTable();

            string strHouseId = "";
            if (!string.IsNullOrEmpty(txtAccountNumber.Text.Trim()))
                strHouseId = txtAccountNumber.Text.Trim();

            if (dt != null)
            {
                string filter = string.Empty;

                if (!string.IsNullOrEmpty(strHouseId))
                    filter = "AccountNo LIKE '*" + strHouseId + "*'";
                if (!string.IsNullOrEmpty(txtAddress1.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter = filter + "AND Address1 LIKE '*" + txtAddress1.Text.Trim() + "*' ";
                    else
                        filter = "Address1 LIKE '*" + txtAddress1.Text.Trim() + "*' ";
                }
                if (!string.IsNullOrEmpty(txtAddress2.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter = filter + "AND Address2 LIKE '*" + txtAddress2.Text.Trim() + "*' ";
                    else
                        filter = "Address2 LIKE '*" + txtAddress2.Text.Trim() + "*' ";
                }

                DataView dv = dt.DefaultView;
                dv.RowFilter = filter;

                if (dv != null)
                    gvAccounts.DataSource = dv;
            }
            gvAccounts.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    /// <summary>
    /// Bind mapaccount grid - mapped account
    /// </summary>
    private void BindMapAccountGrid()
    {
        DataTable dt = dtMapAccount;

        if (dt != null)
            gvMapACcount.DataSource = dt;

        gvMapACcount.DataBind();
    }

    /// <summary>
    /// map record - add as mapped.
    /// </summary>
    /// <param name="houseId"></param>
    private void MapRecord(int houseId)
    {
        try
        {
            DataTable accountTable = dtAccount;

            if (dtMapAccount == null)
                dtMapAccount = dtAccount.Clone();

            DataTable mapTable = dtMapAccount;

            DataRow[] rowToRemove = accountTable.Select("HouseId=' " + houseId + " ' ");

            foreach (DataRow dr in rowToRemove)
            {
                dr["Mapped"] = "1";
                mapTable.ImportRow(dr);
                //accountTable.Rows.Remove(dr);
            }

            dtAccount = accountTable;
            dtMapAccount = mapTable;

            BindControls();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    /// <summary>
    /// unmap record - remove from mapped.
    /// </summary>
    /// <param name="houseId"></param>
    private void UnMapRecord(int houseId)
    {
        try
        {
            DataTable accountTable = dtAccount;
            DataTable mapTable = dtMapAccount;

            DataRow[] rowToRemove = mapTable.Select("HouseId=' " + houseId + " ' ");

            foreach (DataRow dr in rowToRemove)
            {
                DataRow[] drAccount = accountTable.Select("HouseId=" + dr["HouseId"]);
                if (drAccount.Length == 1)
                {
                    drAccount[0]["Mapped"] = "0";
                }
                mapTable.Rows.Remove(dr);
            }

            dtAccount = accountTable;
            dtMapAccount = mapTable;

            BindControls();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void GetTablesForMappedRecord(bool IsByEmployeeNumber = false)
    {
        IsEditMode = false;
        DataTable dtMappedRecord = null;

        if (IsByEmployeeNumber)
        {
            string employeeNumber = GetPlainQueryString("eno");
            if (!string.IsNullOrEmpty(employeeNumber))
                dtMappedRecord = Employee_House.GetByEmployeeNumber(employeeNumber);
            IsByEmployeeNumber = false;
        }
        else
        {
            dtMappedRecord = Employee_House.GetByEmployeeId(ConvertTo.Integer(ddlEmployeeNo.SelectedValue));
        }

        DataTable dtTempAccount = dtAccount;

        if (dtMappedRecord != null && dtMappedRecord.Rows.Count > 0)
        {
            if (dtMapAccount == null)
                dtMapAccount = dtMappedRecord.Clone();

            dtMapAccount = dtMappedRecord;

            foreach (DataRow dr in dtMappedRecord.Rows)
            {
                int houseId = ConvertTo.Integer(dr["HouseId"]);
                DataRow[] rows = dtTempAccount.Select("HouseId=" + houseId);
                foreach (DataRow r in rows)
                    dtTempAccount.Rows.Remove(r);
            }

            IsEditMode = true;

            dtAccount = dtTempAccount;
        }
    }

    /// <summary>
    /// Show / Hide Employee name 
    /// </summary>
    private void ShowHideEmployeeName(int employeeId)
    {
        if (employeeId > 0)
        {
            lblEName.Visible = true;
            lblEmployeeName.Visible = true;

            DataRow[] dr = dtEmployee.Select("EmployeeId=" + employeeId.ToString());
            lblEmployeeName.Text = Convert.ToString(dr[0]["EmployeeName"]) + " " + Convert.ToString(dr[0]["EmployeeSurname"]);
        }
        else
        {
            lblEName.Visible = false;
            lblEmployeeName.Visible = false;
        }
    }
    #endregion
}