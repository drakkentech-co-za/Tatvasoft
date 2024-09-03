using System;
using System.Web.Services.Protocols;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using DataAccess;

/// <summary>
/// Summary Dynamic Reports - Generating ssrs reports dynamically
/// </summary>
/// <CreatedBY> Darpan Khandhar </CreatedBY>
/// <CreatedDate> 20-SEP-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifeidDate> 20-SEP-2013 </ModifeidDate>
public partial class DynamicReports : BasePageHome
{
    #region "Page events"

    /// <summary>
    /// Handles Page Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkReport');");

        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.DynamicReports.GetHashCode());
            BindReportList();
            SetDefaultValue();
        }
    }

    #endregion

    #region "Control Methods"

    /// <summary>
    /// Handles btnGenerate Click - Generate Report
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        rptViewer.ProcessingMode = ProcessingMode.Remote;
        rptViewer.ShowCredentialPrompts = true;
        rptViewer.ServerReport.ReportServerCredentials = new WandEReportCredentials();

        rptViewer.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["SSRSReportServer"].ToString());
        rptViewer.ServerReport.ReportPath = System.Configuration.ConfigurationManager.AppSettings["DynamicReportPath"].ToString() + ddlReports.SelectedValue.ToString();
        rptViewer.ServerReport.Refresh();
    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Set Defualt Values
    /// </summary>
    private void SetDefaultValue()
    {
        if (!ConvertTo.Boolean(System.Configuration.ConfigurationManager.AppSettings["IsDefaultCredential"]))
            lblServerPath.Text = System.Configuration.ConfigurationManager.AppSettings["SSRSReportServer"].ToString();
        lblFolderPath.Text = System.Configuration.ConfigurationManager.AppSettings["DynamicReportPath"].ToString();
    }

    /// <summary>
    /// Bind List of Reports from server
    /// </summary>
    private void BindReportList()
    {
        try
        {
            ReportingService2010 rs = new ReportingService2010();

            if (ConvertTo.Boolean(System.Configuration.ConfigurationManager.AppSettings["IsDefaultCredential"]))
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            else
            {
                string username = System.Configuration.ConfigurationManager.AppSettings["SSRSUsername"].ToString();
                string password = System.Configuration.ConfigurationManager.AppSettings["SSRSPassword"].ToString();
                string domainId = System.Configuration.ConfigurationManager.AppSettings["SSRSDomain"].ToString();

                rs.Credentials = new System.Net.NetworkCredential(username, password, domainId);
            }

            string reportFolder = System.Configuration.ConfigurationManager.AppSettings["DynamicReportPath"].ToString();
            reportFolder = reportFolder.Substring(0, reportFolder.Length - 1);

            CatalogItem[] items = null;

            // Retrieve a list of all items from the report server database. 
            items = rs.ListChildren(reportFolder, true);


            if (items != null)
            {
                foreach (CatalogItem i in items)
                {
                    if (i.TypeName == "Report")
                        ddlReports.Items.Add(new ListItem(i.Name.ToString(), i.Name.ToString()));
                }
            }

            ddlReports.DataBind();
        }
        catch (SoapException ee)
        {
            ShowMessage(ee.Detail.OuterXml.ToString(), lblMessage, MessageBoxType.Error);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    #endregion

}