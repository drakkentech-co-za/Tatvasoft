using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;

public partial class HouseEdit : BasePageHome
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
            SetRights(PageRole.SystemPages.HouseEdit.GetHashCode());
            fillCombo();
            General.BindAssetType(ref ddlAssetType, false);

            if (HouseId > 0)
            {
                btnSave.Text = "Update";
                FillControls();
                trRentalAmount.Visible = chkRental.Checked;
                trRental.Visible = chkRental.Checked;
            }
            else
            {
                BindAccountNumber(chkRental.Checked);
            }
        }
        ScriptManager.RegisterStartupScript(upHouseEdit, upHouseEdit.GetType(), Guid.NewGuid().ToString(), "choosen();", true);
    }
    #endregion

    #region Control Events

    /// <summary>
    /// Save Page
    /// </summary>
    /// <param name=sender></param>
    /// <param name=e></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int actionType = ActionType.Save.GetHashCode();

        House objHouse;

        if (HouseId > 0)
        {
            actionType = ActionType.Update.GetHashCode();
            objHouse = new House(HouseId);
        }
        else
        {
            objHouse = new House();
        }

        FillObject(ref objHouse);
        objHouse.Save();


        if (HouseId > 0)
        {
            Response.Redirect("HouseList.aspx?at=" + actionType);
        }
        else
        {
            string id = EncryptionDecryption.GetEncrypt(objHouse.HouseId.ToString());
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "open Basic HouseTariffMapping", "openPopupForHouseTeriffMapping('" + id + "');", true);
        }
    }

    /// <summary>
    /// handles Cancel Click.Redirect to EmployeeList page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HouseList.aspx");
    }


    #endregion

    #region Methods

    /// <summary>
    /// Fill All DropDownlist
    /// </summary>
    private void fillCombo()
    {
        List<HouseType> lstHouseType = HouseType.SelectAll();
        General.DropDownListBind(ref ddlHouseType, lstHouseType, true, "HouseTypeId", "HouseTypeName", "--Select--", "0");
    }

    /// <summary>
    /// Fill Object
    /// </summary>
    private void FillObject(ref House objHouse)
    {
        objHouse.HouseId = this.HouseId;
        objHouse.AccountNo = ddlAccountNo.Visible == true ? ddlAccountNo.SelectedValue : lblAccountNo.Text;
        objHouse.Address1 = Server.HtmlEncode(txtAddress1.Text.Trim());
        objHouse.Address2 = Server.HtmlEncode(txtAddress2.Text.Trim());
        objHouse.ERFNo = Server.HtmlEncode(txtErfNo.Text.Trim());
        objHouse.ERFSize = float.Parse(txtERFSize.Text.Trim() == "" ? "0" : txtERFSize.Text.Trim());
        objHouse.HouseTypeId = Convert.ToInt32(ddlHouseType.SelectedValue);

        if (ConvertTo.Integer(ddlAssetType.SelectedValue) > 0)
            objHouse.AssetType = ConvertTo.Integer(ddlAssetType.SelectedValue);
        objHouse.ElectricityType = rdElecType.SelectedValue == "1" ? true : false;
        if (objHouse.AssetType > 0 || !objHouse.ElectricityType)
        {
            objHouse.PrepaidAllowed = (Convert.ToBoolean(chkPrepaidAllowed.Checked) ? true : false);
            objHouse.Reason = txtReason.Text;
            objHouse.ElectricUnits = (txtElectricUnits.Text == "" ? 0 : Convert.ToInt32(txtElectricUnits.Text));
            objHouse.MeterNo = txtMeterNo.Text.Trim();
        }
        else
        {
            objHouse.PrepaidAllowed = false;
            objHouse.Reason = "";
            objHouse.ElectricUnits = 0;
        }
        objHouse.IsRental = chkRental.Checked;
        objHouse.RentalAmount = ConvertTo.Decimal(txtRentalAmount.Text);
        if (HouseId > 0)
        {
            objHouse.Updated_UserId = ProjectSession.UserID;
            objHouse.Updated_TS = DateTime.Now;
        }
        else
        {
            objHouse.Created_UserId = ProjectSession.UserID;
            objHouse.Created_TS = DateTime.Now;
        }
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        House objHouse = new House(this.HouseId);
        lblAccountNo.Text = objHouse.AccountNo;
        //ddlAccountNo.SelectedValue = objHouse.AccountNo;
        txtAddress1.Text = objHouse.Address1;
        txtAddress2.Text = objHouse.Address2;
        ddlHouseType.SelectedValue = Convert.ToString(objHouse.HouseTypeId);
        txtErfNo.Text = objHouse.ERFNo;
        txtERFSize.Text = Convert.ToString(objHouse.ERFSize);
        ddlAssetType.SelectedValue = ConvertTo.String(objHouse.AssetType);
        rdElecType.SelectedValue = objHouse.ElectricityType == true ? "1" : "0";
        chkPrepaidAllowed.Checked = Convert.ToBoolean(objHouse.PrepaidAllowed);
        txtReason.Text = objHouse.Reason;
        txtElectricUnits.Text = Convert.ToString(objHouse.ElectricUnits);
        txtMeterNo.Text = Convert.ToString(objHouse.MeterNo);
        chkRental.Checked = objHouse.IsRental;
        txtRentalAmount.Text = Convert.ToString(objHouse.RentalAmount);

        ddlAccountNo.Visible = false;
        rfvAccountNo.Enabled = false;
        lblAccountNo.Visible = true;
        chkRental.Enabled = false;
    }


    /// <summary>
    /// Show/Hide Prepaid row
    /// </summary>
    private void ShowHidePrepaid()
    {
        if (ConvertTo.Integer(ddlAssetType.SelectedValue) == House.AssetTypeNames.Core.GetHashCode() || ConvertTo.Integer(rdElecType.SelectedValue) == ConvertTo.Integer(House.ElectricityTypes.PrePaid.GetHashCode()))
        {
            trPrepaid.Style.Add("display", "");
        }
        else
        {
            trPrepaid.Style.Add("display", "none");
        }
    }

    /// <summary>
    /// Bind accountno dropdown control
    /// </summary>
    private void BindAccountNumber(bool IsRental)
    {
        ddlAccountNo.DataSource = House.GetAccountView(IsRental);
        ddlAccountNo.DataBind();
        ddlAccountNo.Items.Insert(0, new ListItem(String.Empty, "0"));
        ddlAccountNo.SelectedIndex = 0;
    }

    #endregion
    protected void chkRental_CheckedChanged(object sender, EventArgs e)
    {
        BindAccountNumber(chkRental.Checked);
        trRentalAmount.Visible = chkRental.Checked;
        trRental.Visible = chkRental.Checked;
    }
}