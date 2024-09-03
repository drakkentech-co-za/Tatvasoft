using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary Change Password - User Can Change password
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 01-OCT-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 01-Oct-2013 </ModifiedDate>
public partial class ChangePassword : BasePageHome 
{
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
            RunScript("ChangeMenuCSSOnClick('lnkChangePassword');");

            if (ProjectSession.UserID > 0)
                lblUserName.Text = ProjectSession.UserName;
            else
                Response.Redirect("Login.aspx");
        }
    }

    #endregion

    #region "Control Events"

    /// <summary>
    /// handle btnSubmit Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidatePage())
            {
                DataAccess.User objUser = new DataAccess.User(ProjectSession.UserID);

                if (objUser.Password == txtOldPassword.Text)
                {
                    objUser.Password = txtNewPassword.Text.Trim();
                    objUser.Save();

                    ShowMessage("Password Changed Successfully.", lblMessage, MessageBoxType.Success);
                }
                else
                {
                    ShowMessage("Old Password do not match", lblMessage, MessageBoxType.Error);
                }
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