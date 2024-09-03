using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;

/// <summary>
/// Summary userProfile - displays user information
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 01-Oct-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 01-Oct-2013 </ModifiedDate>
public partial class UserProfile : BasePageHome
{
    #region "Page Event"

    /// <summary>
    /// handles page laod - loads data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadProfile();
    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Loads profile data
    /// </summary>
    private void LoadProfile()
    {
        try
        {
            if (!string.IsNullOrEmpty(ProjectSession.UserName))
            {
                DataTable dtProfile = Employee.GetEmployeeDetails(ProjectSession.UserName);

                if (dtProfile != null && dtProfile.Rows.Count > 0)
                {
                    lblEmployeeNumber.Text = ConvertTo.String(dtProfile.Rows[0]["EmployeeNumber"]);
                    lblFullName.Text = ConvertTo.String(dtProfile.Rows[0]["FullName"]);
                    lblEmail.Text = ConvertTo.String(dtProfile.Rows[0]["Email"]);

                    repAccountDetails.DataSource = dtProfile;
                    repAccountDetails.DataBind();
                }
                else
                {
                    //ShowMessage("No Record Found.", lblMessage, MessageBoxType.Information);
                    lblEmployeeNumber.Text = "NA";
                    if (ProjectSession.IsAdmin)
                    {
                        lblFullName.Text = "Admin";
                    }
                    else if (ProjectSession.IsMaint)
                    {                    
                        lblFullName.Text = "Maint";
                    }
                    else if (ProjectSession.IsPayRoll)
                    {
                        lblFullName.Text = "Payroll";
                    }
                }
                 
            }
            else
                ShowMessage("Username not found.", lblMessage, MessageBoxType.Information);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    #endregion
}