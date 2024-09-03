using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DataAccess;

public partial class UserControls_Header : System.Web.UI.UserControl
{
    #region Variable/Property Declaration
    #endregion

    #region Page Events

    /// <summary>
    /// Handles Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetMenuByRights();
        }

    }
    #endregion

    #region Control Events

    /// <summary>
    /// btnLogout Click - logout users from system
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        lnkLogOut.Visible = false;
        ProjectSession.UserID = 0;
        ProjectSession.UserName = null;
        ProjectSession.IsAdmin = false;
        ProjectSession.IsEmployee = false;
        ProjectSession.IsPayRoll = false;
        ProjectSession.PageAccessRights = null;
        FormsAuthentication.SignOut();
        Session.Abandon();
        Session.Clear();

        Response.Redirect(URLHelper.GetPath("login.aspx"));
    }

    #endregion

    #region Methods/Functions

    /// <summary>
    /// Check rights and display Menu to users
    /// </summary>
    private void SetMenuByRights()
    {
        if (!ProjectSession.IsAdmin)
        {
            liInvoice.Visible = false;
            liEmployee.Visible = false;
            liHouseList.Visible = false;
            liHouseType.Visible = false;
            liEmployeeAccountMapping.Visible = false;
            liTeriff.Visible = false;
            liHouseTeriffMapping.Visible = false;
            liTeriffRate.Visible = false;
            liWaterScale.Visible = false;
            liAssetType.Visible = false;
            liWaterRateLimit.Visible = false;
            liElectricUnitPrice.Visible = false;
            liPeriodMonthMapping.Visible = false;
            liTokenSheetManagement.Visible = false;
            liInvoiceCompare.Visible = false;
            liTokenRequest.Visible = false;
            liPayrollSubmission.Visible = false;
            liDynamicReports.Visible = false;
            lirptWaterExcessUsageReport.Visible = false;
            liResetPassword.Visible = false;
            liCharts.Visible = false;
            liManageRights.Visible = false;
            liLandfill.Visible = false;
            liPrepaidElecAndOther.Visible = false;
            liManualToken.Visible = false;
            liAdhocToken.Visible = false;

            if (ProjectSession.PageAccessRights != null)
            {
                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.GenerateInvoice.GetHashCode()))
                    liInvoice.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.EmployeeList.GetHashCode()))
                    liEmployee.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.HouseList.GetHashCode()))
                    liHouseList.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.HouseTypes.GetHashCode()))
                    liHouseType.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.EmployeeAccountMapping.GetHashCode()))
                    liEmployeeAccountMapping.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.Tariffs.GetHashCode()))
                    liTeriff.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.HouseTariffMapping.GetHashCode()))
                    liHouseTeriffMapping.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.TariffRate.GetHashCode()))
                    liTeriffRate.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.WaterScales.GetHashCode()))
                    liWaterScale.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.AssetTypes.GetHashCode()))
                    liAssetType.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.WaterRateLimits.GetHashCode()))
                    liWaterRateLimit.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.ElectricUnitPrices.GetHashCode()))
                    liElectricUnitPrice.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.PeriodMonthMappingList.GetHashCode()))
                    liPeriodMonthMapping.Visible = false;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.TokenSheetManagement.GetHashCode()))
                    liTokenSheetManagement.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.InvoiceCompare.GetHashCode()))
                    liInvoiceCompare.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.TokenRequest.GetHashCode()))
                    liTokenRequest.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.PayrollSubmissionReport.GetHashCode()))
                    liPayrollSubmission.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.DynamicReports.GetHashCode()))
                    liDynamicReports.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.WaterExcessUsageReport.GetHashCode()))
                    lirptWaterExcessUsageReport.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.ResetPassword.GetHashCode()))
                    liResetPassword.Visible = true;

                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.WECharts.GetHashCode()))
                    liCharts.Visible = true;
                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.WECharts.GetHashCode()))
                    liCharts.Visible = true;
                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.ManageRights.GetHashCode()))
                    liManageRights.Visible = true;
                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.LandfillList.GetHashCode()))
                    liLandfill.Visible = true;
                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.LandfillEdit.GetHashCode()))
                    liLandfill.Visible = true;
                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.PrepaidElecAndOther.GetHashCode()))
                    liPrepaidElecAndOther.Visible = true;
                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.ManualTokenImport.GetHashCode()))
                    liManualToken.Visible = true;
                if (ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.AdhocTokenDetail.GetHashCode()))
                    liAdhocToken.Visible = true;
            }
        }

    }

    #endregion
}