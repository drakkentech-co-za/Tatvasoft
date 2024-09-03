using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

/// <summary>
/// Invoice Compare Summary - Show Invoices. Can approve , reject invoice.
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CretaedDate> 17SEP2013 </CretaedDate>
/// <ModifiedBy> Shreyansh Thakkar </ModifiedBy>
/// <ModifiedDate> 17SEP2013 </ModifiedDate>
public partial class InvoiceCompare : BasePageHome
{
    #region "Variable/Property Declarataions"

    /// <summary>
    /// Contains Invoice
    /// </summary>
    public DataTable dtInvoice
    {
        get
        {
            if (ViewState["dtInvoice"] != null)
                return (DataTable)ViewState["dtInvoice"];
            return null;
        }
        set
        {
            ViewState["dtInvoice"] = value;
        }
    }

    /// <summary>
    /// Contains Invoice Details
    /// </summary>
    public DataTable dtInvoiceDetails
    {
        get
        {
            if (ViewState["dtInvoiceDetails"] != null)
                return (DataTable)ViewState["dtInvoiceDetails"];
            return null;
        }
        set
        {
            ViewState["dtInvoiceDetails"] = value;
        }
    }

    public int IPeriodId
    {
        get { return ConvertTo.Integer(ViewState["PeriodId"]); }
        set { ViewState["PeriodId"] = value; }
    }

    #endregion

    #region "Page Events'

    /// <summary>
    /// Handles Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkInvoiceCompare');");

        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.InvoiceCompare.GetHashCode());
            General.BindAccount(ref ddlAccountNumber);
            General.BindPeriodMonthYear(ref ddlPeriodId);
            IPeriodId = ConvertTo.Integer(ddlPeriodId.SelectedValue);
            BindFilterParameter();
            BindInvoiceCompare();
        }
    }

    #endregion

    #region "Control Events"

    /// <summary>
    /// Handles gvInvoice PageIndexChanging event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInvoice.PageIndex = e.NewPageIndex;
        BindInvoiceCompare();
    }

    /// <summary>
    /// Handles gvInvoice Sorting event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvInvoice_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (SortBy == e.SortExpression)
        {
            if (OrderBy == SystemEnum.SortDirection.asc.ToString())
                OrderBy = SystemEnum.SortDirection.desc.ToString();
            else
                OrderBy = SystemEnum.SortDirection.asc.ToString();
        }
        else
        {
            SortBy = e.SortExpression;
            OrderBy = SystemEnum.SortDirection.asc.ToString();
        }

        BindInvoiceCompare();
    }

    /// <summary>
    /// Handles gvInvoice RowCommand - Approve and reject invoice.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int houseId = ConvertTo.Integer(e.CommandArgument);

            if (e.CommandName == "ApproveInvoice" || e.CommandName == "AcceptRoshskorInvoice")
            {
                if (dtInvoiceDetails != null)
                {
                    DataTable dtInvoiceCompare = dtInvoiceDetails.Copy();
                    DataView dvInvoiceCompare = dtInvoiceCompare.DefaultView;
                    dvInvoiceCompare.RowFilter = "HouseId=" + houseId;

                    bool isAccepted = true;

                    bool isRAmountAccepted = e.CommandName == "AcceptRoshskorInvoice" ? true : false;

                    InsertInvoiceDetails(dvInvoiceCompare, isAccepted, isRAmountAccepted);

                    DataRow[] dr = dtInvoice.Select("HouseId=" + houseId);
                    if (dr.Length == 1)
                    {
                        dr[0]["Exist"] = true;
                        dr[0]["IsAccepted"] = isAccepted;
                        dr[0]["IsRAmountAccepted"] = isRAmountAccepted;
                    }

                    BindInvoiceCompare();

                    ShowMessage("Successfully Accepted selected Acccount", lblMessage, MessageBoxType.Success);
                }
            }
            else if (e.CommandName == "EditInvoice")
            {
                Response.Redirect("InvoiceCompareDetail.aspx?ID=" + EncryptionDecryption.GetEncrypt(houseId.ToString()) + "&PID=" + EncryptionDecryption.GetEncrypt(ConvertTo.String(IPeriodId)));
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString(), lblMessage, MessageBoxType.Error);
        }
    }

    /// <summary>
    /// Handles gvInvoice rowdatabound - Apply stylesheet as per filter parameter.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool isDifference = ConvertTo.Boolean(DataBinder.Eval(e.Row.DataItem, "isDiff"));
            LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
            LinkButton lnkAcceptRoshskorAmount = (LinkButton)e.Row.FindControl("lnkAcceptRoshskorAmount");
            int HouseID = ConvertTo.Integer(DataBinder.Eval(e.Row.DataItem, "HouseId"));
            LinkButton lnkAccountNo = (LinkButton)e.Row.FindControl("lnkAccountNo");
            Label lblAccountNo = (Label)e.Row.FindControl("lblAccountNo");

            if (isDifference)
            {
                e.Row.Style.Add("background", "#f8dedc url(../images/ic-error.png) 10px 7px no-repeat;");
                e.Row.Style.Add("color", "#9c413a");
                lnkApprove.Visible = false;
                checkAdminRights(lnkAcceptRoshskorAmount);
                if (string.IsNullOrWhiteSpace(ConvertTo.String(DataBinder.Eval(e.Row.DataItem, "HouseId"))))
                {
                    e.Row.Style.Add("background", "#F3DC02;");
                    e.Row.Style.Add("color", "#9c413a");
                    lnkApprove.Visible = false;
                    lnkAccountNo.Visible = false;
                    lblAccountNo.Visible = true;
                    lnkApprove.Visible = false;
                    lnkAcceptRoshskorAmount.Visible = false;
                }
            }

            //check tolerance

            double dblDiff = ConvertTo.Double(DataBinder.Eval(e.Row.DataItem, "Difference"));

            if ((dblDiff > 0 && dblDiff <= 0.03) || (dblDiff < 0 && dblDiff > -0.03))
            {
                lnkApprove.Visible = true;
            }

            if (ConvertTo.Boolean(DataBinder.Eval(e.Row.DataItem, "Exist")))
            {
                Label lblApproveReject = (Label)e.Row.FindControl("lblApproveReject");


                if (lblApproveReject != null)
                    lblApproveReject.Visible = true;
                if (lnkApprove != null)
                {
                    lnkApprove.Visible = false;
                    lnkAcceptRoshskorAmount.Visible = false;
                }

                if (ConvertTo.Boolean(DataBinder.Eval(e.Row.DataItem, "IsRAmountAccepted")))
                    lblApproveReject.Text = "Accepted RoshSkor Amount";
                else if (ConvertTo.Boolean(DataBinder.Eval(e.Row.DataItem, "IsAccepted")))
                    lblApproveReject.Text = "Accepted";
            }

        }
    }

    /// <summary>
    /// Handles btnCompare click - rebind grid 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCompare_Click(object sender, EventArgs e)
    {
        dtInvoice = null;
        IPeriodId = ConvertTo.Integer(ddlPeriodId.SelectedValue);
        BindInvoiceCompare();
    }

    protected void btnAcceptMatched_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtInvoiceCompare = dtInvoiceDetails.Copy();
            DataView dvInvoiceCompare = dtInvoiceCompare.DefaultView;
            dvInvoiceCompare.RowFilter = "Exist=False AND sDiff=0";
            InsertInvoiceDetails(dvInvoiceCompare, true, false);
            dtInvoice = null;
            BindInvoiceCompare();

            ShowMessage("Successfully Accepted Matched accounts", lblMessage, MessageBoxType.Success);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Bind Filter parameters into dropdown controls
    /// </summary>
    private void BindFilterParameter()
    {
        ddlFilterBy.Items.Add(new ListItem("Select All", "0"));
        ddlFilterBy.Items.Add(new ListItem(Invoice.InvoiceFilterParameter.Matched.ToString(), Invoice.InvoiceFilterParameter.Matched.GetHashCode().ToString()));
        ddlFilterBy.Items.Add(new ListItem(Invoice.InvoiceFilterParameter.Unmatched.ToString(), Invoice.InvoiceFilterParameter.Unmatched.GetHashCode().ToString()));
        ddlFilterBy.Items.Add(new ListItem(Invoice.InvoiceFilterParameter.Accepted.ToString(), Invoice.InvoiceFilterParameter.Accepted.GetHashCode().ToString()));
        ddlFilterBy.Items.Add(new ListItem(Invoice.InvoiceFilterParameter.Pending.ToString(), Invoice.InvoiceFilterParameter.Pending.GetHashCode().ToString()));
        ddlFilterBy.Items.Add(new ListItem(Invoice.InvoiceFilterParameter.Rental.ToString(), Invoice.InvoiceFilterParameter.Rental.GetHashCode().ToString()));
        ddlFilterBy.DataBind();
    }

    /// <summary>
    /// Bind Invoice Compare as per Crieteria.
    /// </summary>
    private void BindInvoiceCompare()
    {
        int houseId = ConvertTo.Integer(ddlAccountNumber.SelectedValue);
        int filterParameter = ConvertTo.Integer(ddlFilterBy.SelectedValue);

        DataTable dt = null;

        if (dtInvoice == null)
        {
            DataSet ds = Invoice.GetInvoice(houseId, IPeriodId, filterParameter);

            if (ds != null)
            {
                dt = ds.Tables[0];
                dtInvoice = dt;
                dtInvoiceDetails = ds.Tables[1];
            }
        }
        else
        {
            dt = dtInvoice;
        }

        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = new DataView(dt);

            if (!string.IsNullOrEmpty(SortBy))
            {
                dv.Sort = SortBy + " " + OrderBy;
            }

            gvInvoice.DataSource = dv;
        }

        gvInvoice.DataBind();

        //foreach (GridViewRow gr in gvInvoice.Rows)
        //{
        //    if (gr.RowType == DataControlRowType.DataRow)
        //    {
        //        LinkButton lnkAcceptRoshskorAmount = (LinkButton)gr.FindControl("lnkAcceptRoshskorAmount");
        //        if (lnkAcceptRoshskorAmount != null)
        //        {
        //            if (!ProjectSession.IsAdmin)
        //            {
        //                lnkAcceptRoshskorAmount.Visible = false;
        //            }
        //        }
        //    }
        //}

        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "checkStatus();", true);
    }

    private void InsertInvoiceDetails(DataView dvInvoiceCompare, bool isAccepted, bool isRAmountAccepted)
    {
        Invoice_Detail.InvoiceDetails(dvInvoiceCompare.ToTable(false, "HouseId", "TeriffId", "Description", "RAmountInc", "SAmountEx", "SVAT", "SAmountInc", "Difference", "sDiff", "cReference", "StartReading", "EndReading", "Reading", "CategoryType"), isAccepted, 0, IPeriodId, isRAmountAccepted);
    }

    /// <summary>
    /// Check admin rights
    /// </summary>
    /// <param name="c"></param>
    private void checkAdminRights(Control c)
    {
        if (!ProjectSession.IsAdmin)
            c.Visible = false;
        else
            c.Visible = true;
    }

    #endregion
}

