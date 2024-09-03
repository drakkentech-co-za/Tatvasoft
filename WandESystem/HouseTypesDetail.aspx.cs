using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

/// <summary>
/// Summary HouseTypeDetail - Add/Edit House Type
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 29-Aug-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 29-Aug-2013 </ModifiedDate>
public partial class HouseTypesDetail : BasePageHome 
{
    #region Variable/Property Declaration

    /// <summary>
    /// HouseTypeId
    /// </summary>
    public int HouseTypeId
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
            SetRights(PageRole.SystemPages.HouseTypesDetail.GetHashCode());
            if (HouseTypeId > 0)
            {
                btnSave.Text = "Update";
                FillControls();
            }

            txtHouseTypeName.Focus();
        }
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
        if (ValidatePage())
        {
            int actionType = ActionType.Save.GetHashCode();

            HouseType objHouseType;

            if (HouseTypeId > 0)
            {
                actionType = ActionType.Update.GetHashCode();
                objHouseType = new HouseType(HouseTypeId);
            }
            else
            {
                objHouseType = new HouseType();
            }

            FillObject(ref objHouseType);
            objHouseType.Save();

            Response.Redirect("HouseTypes.aspx?at=" + actionType);
        }
    }

    /// <summary>
    /// handles Cancel Click.Redirect to housetypes page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HouseTypes.aspx");
    }

    #endregion

    #region Methods/Events

    /// <summary>
    /// Validate Page
    /// </summary>
    /// <returns></returns>
    private bool ValidatePage()
    {
        bool isValid = true;
        if (!Page.IsValid)
            return false;
        if (DataAccess.General.CheckDuplicateRecords("tbl_HouseType", "HouseTypeName", txtHouseTypeName.Text.Trim(), "HouseTypeId", this.HouseTypeId.ToString()))
        {
            ShowMessage("HouseType name already exists", lblMessage, MessageBoxType.Warning);
            return false;
        }
        return isValid;
    }

    /// <summary>
    /// Fill Object
    /// </summary>
    private void FillObject(ref HouseType objHouseType)
    {
        objHouseType.HouseTypeId = this.HouseTypeId;
        objHouseType.HouseTypeName = Server.HtmlEncode(txtHouseTypeName.Text.Trim());
      
        if (HouseTypeId > 0)
        {
            objHouseType.Updated_UserId = ProjectSession.UserID;
            objHouseType.Updated_TS = DateTime.Now;
        }
        else
        {
            objHouseType.Created_UserId  = ProjectSession.UserID;
            objHouseType.Created_TS = DateTime.Now;
        }
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        HouseType objHouseType = new HouseType(this.HouseTypeId);

        txtHouseTypeName.Text = objHouseType.HouseTypeName;
    }

    #endregion
}