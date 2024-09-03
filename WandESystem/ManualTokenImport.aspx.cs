using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;

public partial class ManualTokenImport : BasePageHome
{
    #region Properties
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
        }
    }
    #endregion

    #region Private Methods
    private void BindBropDown()
    {
        ddlAccountNo.DataSource = AccountView;
        ddlAccountNo.DataBind();
        ddlAccountNo.Items.Insert(0, new ListItem(String.Empty, "0"));
        ddlAccountNo.SelectedIndex = 0;
    }

    private void GetAccountView()
    {
        AccountView = House.GetAccountViewforToken();
    }
    #endregion

    #region Control Events
    protected void ddlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlAccountNo.SelectedValue != "0")
        {
            DataRow[] drAccount = AccountView.Select("HouseId='" + ddlAccountNo.SelectedValue.ToString() + "'");
            if (drAccount.Length > 0)
            {
                lblAddress.Text = Convert.ToString(drAccount[0]["Address"]);
                lblTokenUnits.Text = Convert.ToString(drAccount[0]["ElectricUnits"]);
                txtTokenNumber.Text = "";
                txtMeterNo.Text = "";
            }
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        int retValue = Token_Detail.InsertManualToken(ConvertTo.Integer(ddlAccountNo.SelectedValue), ConvertTo.Integer(lblTokenUnits.Text), txtTokenNumber.Text.Trim(), ProjectSession.UserID,txtMeterNo.Text.Trim());

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
                ShowMessage("Some error occured while Importing Account", lblMessage, MessageBoxType.Success);
                break;
        }
    }
    #endregion   
}