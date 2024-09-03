using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

public partial class PopUpBasicHouseTeriffMapping :  BasePageHome
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
        if (!Page.IsPostBack)
        {
            SetRights(PageRole.SystemPages.PopUpBasicHouseTariffMapping.GetHashCode());
            BindAccountTeriff();
        }
    }

    #endregion

    #region Control Events

    /// <summary>
    /// Bind Account Teriff
    /// </summary>
    private void BindAccountTeriff()
    {
        DataTable dtHouseTeriff = House_Teriff.BindBasicHouseTariffMapping(HouseId);
        rptHouseTeriff.DataSource = dtHouseTeriff;
        rptHouseTeriff.DataBind();
    }

    #endregion

    #region Control Events

     /// <summary>
    /// btnSave Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int actionType = ActionType.Save.GetHashCode();

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

        if (strTeriffIds.Length > 0 )
        {
            House_Teriff.updateMapping(Convert.ToInt32(HouseId), 0, strTeriffIds, strAddTeriffs);
            ShowMessage("Tariff Categories saved for Account", lblMessage, MessageBoxType.Success);
            lblMessage.Visible = true;
           
        }

        string path = "HouseList.aspx?at=0";
        //ScriptManager.RegisterStartupScript(this.Page, GetType(), "open HouseList", "top.openHouseListPage('"+path+"');parent.parent.GB_hide();", true);            
      
       // Response.Redirect("HouseList.aspx?at=" + actionType);
    }

      /// <summary>
    /// btnAddMore Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        string path = "HouseTeriffMapping.aspx?ID=" + EncryptionDecryption.GetEncrypt(HouseId.ToString());
        ScriptManager.RegisterStartupScript(this.Page, GetType(), "open HouseTariffMapping", "openHouseTeriffMappingPage('" + path + "');", true);            
     
       // Response.Redirect("HouseTeriffMapping.aspx?ID=" + EncryptionDecryption.GetEncrypt(HouseId.ToString()));
    }
    
      
    #endregion

    #region "Private Methods"

    #endregion
}