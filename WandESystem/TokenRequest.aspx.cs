using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using System.Configuration;

/// <summary>
/// TOkenRequest - Summary description for token request ( for user , to request from site)
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 23-Sep-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 23-Sep-2013 </ModifiedDate>
public partial class TokenRequest : BasePageHome
{
    #region "Page Load"

    /// <summary>
    /// Hanldes Page Load
    /// </summary>
    /// <param name="sender"></param>   
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkTokenRequest');");

        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.TokenRequest.GetHashCode());

            BindEmployeeNumber();
        }
    }

    #endregion

    #region "Control Events"

    /// <summary>
    /// Handles btnTokenRequest Click - Request token
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTokenRequest_Click(object sender, EventArgs e)
    {
        try
        {
            if (ConvertTo.Integer(ddlEmployeeNumber.SelectedValue) > 0)
            {

                DataTable dtCloseDates = Token_Request_Detail.CheckTokenRequestCloseDays();

                bool allow = false;
                DateTime dStartDate = DateTime.Now;

                if (dtCloseDates.Rows.Count > 0)
                {
                    allow = ConvertTo.Integer(dtCloseDates.Rows[0]["Result"]) == 0 ? false : true;
                    dStartDate = ConvertTo.Date(dtCloseDates.Rows[0]["StartDate"]);
                }

                if (allow)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("EployeeNo", System.Type.GetType("System.String"));
                    dt.Columns.Add("FromAddress", System.Type.GetType("System.String"));
                    dt.Columns.Add("RequestType", System.Type.GetType("System.Int32"));
                    dt.Columns.Add("UserId", System.Type.GetType("System.Int32"));

                    DataRow dr = dt.NewRow();
                    dr["EployeeNo"] = ConvertTo.String(ConvertTo.String(ddlEmployeeNumber.SelectedItem.Text));
                    dr["FromAddress"] = ConvertTo.String("System");
                    dr["RequestType"] = ConvertTo.Integer(Token_Request_Detail.RequestTypes.System.GetHashCode());
                    dr["UserId"] = ConvertTo.Integer(0);
                    dt.Rows.Add(dr);

                    DataSet ds = Token_Request_Detail.InsertTokenRequestDetail(dt, string.Empty, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

                    if (ds != null && ds.Tables.Count > 1)
                    {
                        DataTable dtSuccess = ds.Tables[0];
                        DataTable dtFailure = ds.Tables[1];

                        SetSuccessMessage(dtSuccess);
                        SetFailureMessage(dtFailure);
                    }
                }
                else
                {
                    ShowMessage("Accounts are closed for now you can request a token again on <b>" + dStartDate.ToString("dddd, MMMM dd, yyyy") + "</b>", lblMessage, MessageBoxType.Information);
                }
            }
            else
                ShowMessage("Select Employee Number to request token.", lblMessage, MessageBoxType.Warning);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Bind account number in dropdown control
    /// </summary>
    private void BindEmployeeNumber()
    {
        DataTable dt = Employee.GetEmployeeNumber(ProjectSession.IsEmployee == true ? ProjectSession.UserName : "0");

        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = "EmployeeNo ASC";

            ddlEmployeeNumber.DataSource = dv;
            ddlEmployeeNumber.DataValueField = "EmployeeNo";
            ddlEmployeeNumber.DataTextField = "EmployeeNo";
        }
        ddlEmployeeNumber.DataBind();
        ddlEmployeeNumber.Items.Insert(0, new ListItem("--Select Employee Number--", "0"));

        if (dt.Rows.Count == 0)
        {
            trEmployeeNumber.Visible = false;
            ShowMessage("Account not allowed for prepaid.", lblMessage, MessageBoxType.Information);
        }
    }

    /// <summary>
    /// Send mail on successfull token request
    /// </summary>
    /// <param name="dtSuccess"></param>
    private void SetSuccessMessage(DataTable dtSuccess)
    {
        foreach (DataRow drSuccess in dtSuccess.Rows)
        {
            string tokenNumber = ConvertTo.String(drSuccess["TokenNumber"]);
            string body = string.Format(ConfigurationManager.AppSettings["RequestSuccessBody"].ToString(), tokenNumber);

            ShowMessage(body, lblMessage, MessageBoxType.Success);
        }
    }

    /// <summary>
    /// Send mail on failure token request
    /// </summary>
    /// <param name="dtFailure"></param>
    private void SetFailureMessage(DataTable dtFailure)
    {
        foreach (DataRow drFailure in dtFailure.Rows)
        {
            string toAddress = ConvertTo.String(drFailure["FromAddress"]);
            string accountNumber = ConvertTo.String(drFailure["AccountNumber"]);

            string body = ConfigurationManager.AppSettings["RequestFailureBody"].ToString();

            ShowMessage(body, lblMessage, MessageBoxType.Information);
        }
    }

    #endregion
}