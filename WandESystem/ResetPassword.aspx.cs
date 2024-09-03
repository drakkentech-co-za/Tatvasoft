using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

/// <summary>
/// Summary Reset Password - Only for admin role. Can Reset password for all employee.
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 02-Oct-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 02-Oct-2013 </ModifiedDate>
public partial class ResetPassword : BasePageHome
{
    #region "Variables/Property Declaration"

    #endregion

    #region "Page Load"

    /// <summary>
    /// page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RunScript("ChangeMenuCSSOnClick('lnkAdmin');");

            SetRights(PageRole.SystemPages.ResetPassword.GetHashCode());

            trCurrentPassword.Visible = false;

            General.BindUsers(ref ddlUserName);
        }
    }

    #endregion

    #region "Control Events"

    /// <summary>
    /// Handles ddlUserName SelectedIndexChanged event - Set current password of selected employee.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ConvertTo.Integer(ddlUserName.SelectedValue) > 0)
        {
            DataAccess.User objUser = new DataAccess.User(ConvertTo.Integer(ddlUserName.SelectedValue));
            lblCurrentPassword.Text = objUser.Password;
            trCurrentPassword.Visible = true;
        }
        else
        {
            trCurrentPassword.Visible = true;
            ShowMessage("Please Select employee number to reset password.", lblMessage, MessageBoxType.Warning);
        }
    }

    /// <summary>
    /// handle btnSubmit Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidatePage())
            {
                
                DataAccess.User objUser = new DataAccess.User(ConvertTo.Integer(ddlUserName.SelectedValue));

                objUser.Password = txtNewPassword.Text.Trim();
                objUser.Save();

                ShowMessage("Password reset Successfully.", lblMessage, MessageBoxType.Success);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString(), lblMessage, MessageBoxType.Error);
        }
    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Validate page.
    /// </summary>
    /// <returns></returns>
    private bool ValidatePage()
    {
        bool result = true;

        if (!Page.IsValid)
            result = false;

        if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            result = false;

        return result;
    }

    #endregion
}