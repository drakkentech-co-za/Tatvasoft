using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

/// <summary>
/// Summary Manage Rights - Set rights by rolewise
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 26-SEP-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 26-SEP-2013 </ModifiedDate>
public partial class ManageRights : BasePageHome
{
    #region "Page Events"

    /// <summary>
    /// Handles Page Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkAdmin');");

        if (!IsPostBack)
        {
            SetRights();
            General.BindRole(ref ddlRole, false);            
            General.BindPages(ref chkPages, false);
            FillControls();
        }
    }

    #endregion

    #region "Control Events"

    /// <summary>
    /// handles ddlRole SelectedIndexChanged - Rebind rights
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        FillControls();
    }

    /// <summary>
    /// Handles btnUpdate Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string selectedRghts = GetSelectedRights();

            PageRole.SetRights(selectedRghts, ConvertTo.Integer(ddlRole.SelectedValue));

            ShowMessage("Rights Updated Successfully.", lblMessage, MessageBoxType.Success);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Set Rights
    /// </summary>
    private void SetRights()
    {
        SetRights(PageRole.SystemPages.ManageRights.GetHashCode());
    }

    /// <summary>
    /// Returns selected rights as comma separated string
    /// </summary>
    /// <returns></returns>
    private string GetSelectedRights()
    {
        string selectedRights = string.Empty;

        foreach (ListItem li in chkPages.Items)
        {
            if (li.Selected)
            {
                selectedRights = selectedRights + ConvertTo.String(li.Value) + ",";
            }
        }

        return selectedRights;
    }

    /// <summary>
    /// Checked checkbox controls by role selected
    /// </summary>
    private void FillControls()
    {
        foreach (ListItem li in chkPages.Items)
            li.Selected = false;

        PageRoleList lstRoles = PageRole.SelectByRoleId(ConvertTo.Integer(ddlRole.SelectedValue));

        for (int i = 0; i < lstRoles.Count; i++)
        {
            ListItem chk = chkPages.Items.FindByValue(ConvertTo.String((lstRoles[i].PageId)));

            if (chk != null)
                chk.Selected = true;
        }
    }

    #endregion
}