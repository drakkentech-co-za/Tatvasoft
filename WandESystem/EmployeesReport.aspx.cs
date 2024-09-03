using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;

/// <summary>
/// Employees Detail Report
/// </summary>
/// <CreatedBY> Darpan Khandhar </CreatedBY>
/// <CreatedDate> 17-Sep-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 17-Sep-2013 </ModifiedDate>
public partial class EmployeesReport : BasePageHome
{
    #region "Page Event"

    /// <summary>
    /// handles page laod
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkReports');");

        if (!IsPostBack)
        {
            SetRights();
            LoadEmployeesReport();
        }
    }

    #endregion

    #region "Private methods"

    /// <summary>
    /// Set Rights 
    /// </summary>
    private void SetRights()
    {
    }

    /// <summary>
    /// Load Report
    /// </summary>
    private void LoadEmployeesReport()
    {
        DataSet dsEmployee = Employee.GetEmployees(string.Empty);

        rptEmployees.LocalReport.DataSources.Clear();
        rptEmployees.LocalReport.EnableExternalImages = true;
        rptEmployees.LocalReport.EnableHyperlinks = true;


        rptEmployees.LocalReport.ReportPath = Server.MapPath("~/RDLC/EmployeeReport.rdlc");
        rptEmployees.LocalReport.DisplayName = "Employees";
        rptEmployees.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("dsEmployee", dsEmployee.Tables[0]));

        rptEmployees.LocalReport.Refresh();
    }

    #endregion
}