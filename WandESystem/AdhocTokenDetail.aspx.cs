using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;

public partial class AdhocTokenDetail : BasePageHome
{
    #region Properties
    /// <summary>
    /// 
    /// </summary>
    public int TokenRequestId
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

    /// <summary>
    /// Account View
    /// </summary>
    private DataTable AccountView
    {
        get
        {
            return (DataTable)ViewState["AccountView"];
        }
        set
        {
            ViewState["AccountView"] = value;
        }

    }
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkAdmin');");
        if (!IsPostBack)
        {
            GetAccountView();
            BindBropDown();
            if (TokenRequestId > 0)
                FillData();
        }
    }
    #endregion

    #region Private Methods
    private void FillData()
    {
        Extra_Token_Detail objTokenDetail = new Extra_Token_Detail(TokenRequestId);

        DataRow[] drAccount = AccountView.Select("HouseId='" + ConvertTo.String(objTokenDetail.HouseId) + "'");
        if (drAccount.Length > 0)
        {
            lblAddress.Text = Convert.ToString(drAccount[0]["Address"]);
            lblAccountNo.Text = Convert.ToString(drAccount[0]["AccountNo"]);
        }
        ddlAccountNo.Visible = false;
        rfvAccountNo.Visible = false;
        ddlEmployeeId.Visible = false;
        rfvEmployeeID.Visible = false;
        lblAccountNo.Visible = true;
        lblEmployeeName.Text = Employee.GetEmployeeName(Convert.ToInt32(objTokenDetail.EmployeeId));
        txtTokenUnits.Text = ConvertTo.String(objTokenDetail.NoOfUnits);
        txtTokenNumber.Text = objTokenDetail.TokenNumber;
        txtMeterNo.Text = objTokenDetail.MeterNumber;
        ddlPeriodId.SelectedValue = ConvertTo.String(objTokenDetail.PeriodId);
        if (objTokenDetail.DateIssue != DateTime.MinValue)
            txtIssueDate.Text = ConvertTo.Date(objTokenDetail.DateIssue, "MM/dd/yyyy");
    }

    private void BindBropDown()
    {
        ddlAccountNo.DataSource = AccountView;
        ddlAccountNo.DataBind();
        ddlAccountNo.Items.Insert(0, new ListItem(String.Empty, "0"));
        ddlAccountNo.SelectedIndex = 0;

        ddlEmployeeId.DataSource = Employee.SelectAll();
        ddlEmployeeId.DataBind();
        ddlEmployeeId.Items.Insert(0, new ListItem(String.Empty, "0"));
        ddlEmployeeId.SelectedIndex = 0;

        General.BindPeriodMonthYear(ref ddlPeriodId);
    }

    private void GetAccountView()
    {
        AccountView = House.GetAccountViewforToken();
    }
    #endregion

    #region Control Events
    protected void ddlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountNo.SelectedValue != "0")
        {
            DataRow[] drAccount = AccountView.Select("HouseId='" + ddlAccountNo.SelectedValue.ToString() + "'");
            if (drAccount.Length > 0)
            {
                lblAddress.Text = Convert.ToString(drAccount[0]["Address"]);
                txtTokenUnits.Text = Convert.ToString(drAccount[0]["ElectricUnits"]);
                txtTokenNumber.Text = "";
                txtMeterNo.Text = "";
            }
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            DateTime dtDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(txtIssueDate.Text))
                dtDate = DateTime.ParseExact(txtIssueDate.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            int retValue = Extra_Token_Detail.InsertExtraTokenDetails(ConvertTo.Integer(ddlAccountNo.SelectedValue), ConvertTo.Integer(ddlEmployeeId.SelectedValue), ConvertTo.Integer(txtTokenUnits.Text), txtTokenNumber.Text.Trim(), ConvertTo.Integer(ddlPeriodId.SelectedValue), txtMeterNo.Text.Trim(), dtDate, ProjectSession.UserID, TokenRequestId);

            switch (retValue)
            {
                case 1:
                    ShowMessage("This Account is already imported", lblMessage, MessageBoxType.Information);
                    break;
                case 2:
                    ShowMessage("Same token number is alloted to some other Account", lblMessage, MessageBoxType.Information);
                    break;
                case 3:
                    ShowMessage("Account imported successfully for Token Request", lblMessage, MessageBoxType.Success);
                    break;
                case 4:
                    ShowMessage("Account updated successfully for Token Request", lblMessage, MessageBoxType.Success);
                    break;
                case 5:
                    ShowMessage("Some error occured while Importing Account", lblMessage, MessageBoxType.Error);
                    break;
            }
        }
    }

    protected void ddlEmployeeId_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblEmployeeName.Text = Employee.GetEmployeeName(Convert.ToInt32(ddlEmployeeId.SelectedValue));
    }

    /// <summary>
    /// handles Cancel Click.Redirect to EmployeeList page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdhocTokenList.aspx");
    }
    #endregion
}