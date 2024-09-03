using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

public partial class HouseTeriffMapping : BasePageHome
{
    #region Variable/Property Declaration

    /// <summary>
    /// HouseId
    /// </summary>
    public int HouseId
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
    /// House View
    /// </summary>
    private DataTable HouseView
    {
        get
        {
            return ViewState["HouseView"] as DataTable;
        }
        set
        {
            ViewState["HouseView"] = value;
        }

    }

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
        if (!Page.IsPostBack)
        {
            SetRights(PageRole.SystemPages.HouseTariffMapping.GetHashCode());
            fillCombo();
            BindAccountTeriff();
        }
    }
    #endregion

    #region Methods

    /// <summary>
    /// Fill All DropDownlist
    /// </summary>
    private void fillCombo()
    {
        DataTable dtHouse = House.GetAccounts("").Tables[0];
        HouseView = dtHouse.Copy();
        General.DropDownListBind(ref ddlAccountNo, dtHouse, true, "HouseId", "AccountNo", "", "0");
        if (HouseId > 0)
        {
            ddlAccountNo.SelectedValue = HouseId.ToString();
        }
    }

    /// <summary>
    /// Bind Account Teriff
    /// </summary>
    private void BindAccountTeriff()
    {
        DataTable dtHouseTeriff = House_Teriff.BindHouseTariffMapping(Int32.Parse(ddlAccountNo.SelectedValue));
        lblAccountAddress.Visible = false;
        if (ddlAccountNo.SelectedValue != "0")
        {
            DataRow[] drHouse = HouseView.Select("HouseId='" + ddlAccountNo.SelectedValue.ToString() + "'");
            if (drHouse.Length == 1)
            {
                lblAccountAddress.Text = "Address: " + ConvertTo.String(drHouse[0]["Address1"]) + " " + ConvertTo.String(drHouse[0]["Address2"]);
                lblAccountAddress.Visible = true;
            }
        }
        rptHouseTeriff.DataSource = dtHouseTeriff;
        rptHouseTeriff.DataBind();
    }

    #endregion

    #region Events

    /// <summary>
    /// Selected Index Change event for ddlAccountNo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAccountTeriff();
        lblMessage.Visible = false;
    }
    /// <summary>
    /// btnSave Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strTeriffIds = "";
        string strAddTeriffs = "";
        foreach (RepeaterItem Ri in rptHouseTeriff.Items)
        {
            if (Ri.ItemType == ListItemType.Item || Ri.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkSelect = (CheckBox)Ri.FindControl("chkSelect");
                TextBox txtAdditionalTeriff = (TextBox)Ri.FindControl("txtAdditionalTeriff");
                if (chkSelect.Checked)
                {
                    strTeriffIds = strTeriffIds + chkSelect.Attributes["value"].ToString() + ",";
                    strAddTeriffs = strAddTeriffs + (txtAdditionalTeriff.Text == "" ? "0" : txtAdditionalTeriff.Text) + ",";
                }
            }
        }

        strTeriffIds = strTeriffIds.Trim(',');
        strAddTeriffs = strAddTeriffs.TrimEnd(',');

        if (strTeriffIds.Length > 0 && Convert.ToInt32(ddlAccountNo.SelectedValue) > 0)
        {
            House_Teriff.updateMapping(Convert.ToInt32(ddlAccountNo.SelectedValue), 0, strTeriffIds, strAddTeriffs);
            ShowMessage("Tariff Categories saved for Account:" + ddlAccountNo.SelectedItem, lblMessage, MessageBoxType.Success);
            lblMessage.Visible = true;
        }
    }
    /// <summary>
    /// btnCancel Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HouseList.aspx");
    }

    /// <summary>
    /// ItemDataBound event for rptHouseTeriff
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptHouseTeriff_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox chkSelect = (CheckBox)e.Item.FindControl("chkSelect");

            chkSelect.Checked = Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "Checked")) == 1 ? true : false;
        }
    }
    #endregion


}