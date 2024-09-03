using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class Reports_rptPayrollSubmission : BasePageHome
{
    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkReport');");
        
        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.PayrollSubmissionReport.GetHashCode());
            BindDropDowns();
        }
    }
    #endregion


    #region Methods
    private void BindDropDowns()
    {
        General.BindPeriodMonthYear(ref ddlPeriodId);
        General.BindEmployeeType(ref ddlEmpType, true, "--Select All--", "0");
    }

    private void loadReport()
    {
        DataSet dsReport = clsReport.GetPayrollSubmission(ConvertTo.Integer(ddlPeriodId.SelectedValue), ConvertTo.Integer(ddlEmpType.SelectedValue));
        if (dsReport != null && dsReport.Tables.Count > 0)
        {
            ReportDataSource datasource1 = new ReportDataSource("dspayroll",dsReport.Tables[0]);            
            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.AsyncRendering = true;
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            rptViewer.LocalReport.EnableExternalImages = true;
            rptViewer.PageCountMode = 0;

            rptViewer.LocalReport.ReportPath = Server.MapPath("~/RDLC/PayrollSubmission.rdlc");

            List<global::Microsoft.Reporting.WebForms.ReportParameter> paramList = new List<global::Microsoft.Reporting.WebForms.ReportParameter>();
            paramList.Add(new ReportParameter("EmpType", ddlEmpType.SelectedValue, false));

            rptViewer.LocalReport.SetParameters(paramList);

            rptViewer.LocalReport.DataSources.Add(datasource1);

            rptViewer.LocalReport.Refresh();
        }
    }

    #endregion

    #region ControlEvents
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        loadReport();
    }
    #endregion

}