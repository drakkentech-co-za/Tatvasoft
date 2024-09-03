using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using DataAccess;

/// <summary>
/// Summary Login - Gets login and sets related session utility.
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 01-Oct-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 01-Oct-2013 </ModifiedDate>
public partial class Login : BasePage
{
    #region "Page Events"

    /// <summary>
    /// Handles page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    #endregion

    #region "Control Events"

    /// <summary>
    /// handles btnLogin Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        if (ValidateUser(txtUserName.Text.Trim(), txtPassword.Text.Trim()))
            Response.Redirect("UserProfile.aspx",false);
    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Validate User for get login into system.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    private bool ValidateUser(string username, string password)
    {
        bool result = false;
        try
        {
            DataTable dtUser = DataAccess.User.GetUserByUserName(username);

            if (dtUser.Rows.Count > 0)
            {
                if (dtUser.Columns.Count > 1)
                {
                    if (string.Compare(dtUser.Rows[0]["Password"].ToString(), password, false) == 0)
                    {

                        FormsAuthentication.SetAuthCookie(username, false);
                        ProjectSession.UserName = ConvertTo.String(dtUser.Rows[0]["UserName"]);
                        ProjectSession.UserID = ConvertTo.Integer(dtUser.Rows[0]["UserId"]);
                        int roleId = ConvertTo.Integer(dtUser.Rows[0]["RoleId"]);

                        SetRole(roleId);

                        List<int> lstRights = new List<int>();
                        foreach (DataRow drUser in dtUser.Rows)
                            lstRights.Add(DataAccess.ConvertTo.Integer(drUser["PageId"]));
                        
                        //if (ProjectSession.IsPayRoll)
                        //{
                        //    PageRoleList lstPageRole = PageRole.SelectByRoleId(DataAccess.User.UserRoles.Employee.GetHashCode());
                        //    foreach (PageRole p in lstPageRole)
                        //        lstRights.Add(p.PageId);
                        //}

                        ProjectSession.PageAccessRights = lstRights;

                        result = true;
                    }
                    else
                        ShowMessage("Invalid  Username / Password", lblErrorMessage, MessageBoxType.Error);
                }
                else if (dtUser.Columns.Count == 1)
                {
                    string name = ConvertTo.String(dtUser.Rows[0]["UserName"]);
                    if (string.Compare(name, password, false) == 0)
                    {
                        DataAccess.User objUser = new DataAccess.User();
                        objUser.UserName = name;
                        objUser.Password = name;
                        objUser.RoleId = DataAccess.User.UserRoles.Employee.GetHashCode();
                        objUser.Save();

                        FormsAuthentication.SetAuthCookie(username, false);
                        ProjectSession.UserName = objUser.UserName;
                        ProjectSession.UserID = objUser.UserId;
                        ProjectSession.IsEmployee = true;

                        SetRole(objUser.RoleId);

                        PageRoleList lstPageRights = PageRole.SelectByRoleId(DataAccess.User.UserRoles.Employee.GetHashCode());

                        if (lstPageRights != null)
                            ProjectSession.PageAccessRights = lstPageRights.Select(x => x.PageId).ToList();

                        result = true;
                    }
                    else
                        ShowMessage("Invalid  Username / Password", lblErrorMessage, MessageBoxType.Error);
                }
                else
                    ShowMessage("Invalid  Username / Password", lblErrorMessage, MessageBoxType.Error);
            }
            else
                ShowMessage("Invalid  Username / Password", lblErrorMessage, MessageBoxType.Error);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblErrorMessage, MessageBoxType.Error);
        }

        return result;
    }

    /// <summary>
    /// Set user has which role
    /// </summary>
    /// <param name="roleId"></param>
    private void SetRole(int roleId)
    {
        if (roleId == DataAccess.User.UserRoles.Admin.GetHashCode())
            ProjectSession.IsAdmin = true;
        else if (roleId == DataAccess.User.UserRoles.Payroll.GetHashCode())
            ProjectSession.IsPayRoll = true;
        else if (roleId == DataAccess.User.UserRoles.Employee.GetHashCode())
            ProjectSession.IsEmployee = true;
        else if (roleId == DataAccess.User.UserRoles.Maint.GetHashCode())
            ProjectSession.IsMaint = true;
    }

    #endregion
}