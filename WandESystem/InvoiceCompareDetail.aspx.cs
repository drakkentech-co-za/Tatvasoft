using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

public partial class InvoiceCompareDetail : BasePageHome
{
    #region "Variable/property declaration"
    /// <summary>
    /// HouseId
    /// </summary>
    public int HouseId
    {
        get
        {
            string id = GetQueryString("ID");

            if (!string.IsNullOrEmpty(id))
                return Convert.ToInt32(id);
            else
                return 0;
        }
    }

    /// <summary>
    /// PeriodId
    /// </summary>
    public int PeriodId
    {
        get
        {
            string id = GetQueryString("PID");

            if (!string.IsNullOrEmpty(id))
                return Convert.ToInt32(id);
            else
                return 0;
        }
    }

    public int IPeriodId
    {
        get { return ConvertTo.Integer(ViewState["PeriodId"]); }
        set { ViewState["PeriodId"] = value; }
    }

    #endregion

    #region "Page Event"

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkInvoiceCompare');");
        if (!IsPostBack)
        {
            SetRights();
            BindDropDown();
            ShowHideControls();
            if (HouseId > 0)
            {
                compare();
            }
        }
    }

    #endregion

    #region "Control Events"

    /// <summary>
    /// Handles btnCompare click 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCompare_Click(object sender, EventArgs e)
    {
        compare();
    }

    /// <summary>
    /// Handles rptParentInvoiceCompare ItemDataBound - bind child repeater control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptParentInvoiceCompare_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rptInvoiceCompare = (Repeater)e.Item.FindControl("rptInvoiceCompare");

            DataView dv = ((DataRowView)e.Item.DataItem).CreateChildView("Invoice");

            rptInvoiceCompare.DataSource = dv;
            rptInvoiceCompare.DataBind();

            Button btnInnerAccept = (Button)e.Item.FindControl("btnInnerAccept");
            Button btnAcceptRoshskorAmount = (Button)e.Item.FindControl("btnAcceptRoshskorAmount");
            btnInnerAccept.Visible = true;         

            if (rptInvoiceCompare.Items.Count > 0)
            {
                Control FooterTemplate = rptInvoiceCompare.Controls[rptInvoiceCompare.Controls.Count - 1].Controls[0];

                HtmlTableCell thRAmt = (HtmlTableCell)FooterTemplate.FindControl("thRAmt");
                HtmlTableCell thSAmtEx = (HtmlTableCell)FooterTemplate.FindControl("thSAmtEx");
                HtmlTableCell thSVat = (HtmlTableCell)FooterTemplate.FindControl("thSVat");
                HtmlTableCell thSAmtIn = (HtmlTableCell)FooterTemplate.FindControl("thSAmtIn");
                HtmlTableCell thSDiff = (HtmlTableCell)FooterTemplate.FindControl("thSDiff");

                DataTable dtForSum = dv.ToTable();

                thRAmt.InnerHtml = Convert.ToString(dtForSum.AsEnumerable().Sum(x => x.Field<double>("RAmountInc")));
                thSAmtEx.InnerHtml = Convert.ToString(dtForSum.AsEnumerable().Sum(x => x.Field<double>("SAmountEx")));
                thSVat.InnerHtml = Convert.ToString(dtForSum.AsEnumerable().Sum(x => x.Field<double>("SVAT")));
                thSAmtIn.InnerHtml = Convert.ToString(dtForSum.AsEnumerable().Sum(x => x.Field<double>("SAmountInc")));
                thSDiff.InnerHtml = Convert.ToString(dtForSum.AsEnumerable().Sum(x => x.Field<double>("Difference")));

                if (ConvertTo.Double(thSDiff.InnerHtml) != 0)
                {
                    btnInnerAccept.Visible = false;                    
                    checkAdminRights(btnAcceptRoshskorAmount);
                }

                Double dblDiff = ConvertTo.Double(thSDiff.InnerHtml);

                if ((dblDiff > 0 && dblDiff <= 0.03) || (dblDiff < 0 && dblDiff > -0.03))
                {
                    btnInnerAccept.Visible = true;
                    btnAcceptRoshskorAmount.Visible = false;
                }

            }

            Label lblSavedReason = (Label)e.Item.FindControl("lblSavedReason");
            Label lblReason = (Label)e.Item.FindControl("lblReason");
            TextBox txtReason = (TextBox)e.Item.FindControl("txtReason");

            lblSavedReason.Visible = false;
            lblReason.Visible = false;
            txtReason.Visible = false;

            bool exists = ConvertTo.Boolean(DataBinder.Eval(e.Item.DataItem, "Exist"));
            if (exists)
            {
                CheckBox chkSelect = (CheckBox)e.Item.FindControl("chkSelect");
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");

                chkSelect.Visible = false;
                btnInnerAccept.Visible = false;
                btnAcceptRoshskorAmount.Visible = false;
                bool status = ConvertTo.Boolean(DataBinder.Eval(e.Item.DataItem, "IsAccepted"));
                if (ConvertTo.Boolean(DataBinder.Eval(e.Item.DataItem, "IsRAmountAccepted")))
                    lblStatus.Text = "Accepted RoshSkor Amount";
                else
                    lblStatus.Text = status == true ? "Accepted" : "Rejected";

                if (status)
                {
                    lblStatus.Style.Add("color", "#528404");
                    ((HtmlTableRow)e.Item.FindControl("trAccRej")).Style.Add("background", "#f9f9e5 url(../images/ic_warning.png) 10px 7px no-repeat");
                }
                else
                {
                    lblStatus.Style.Add("color", "#9c413a");
                    ((HtmlTableRow)e.Item.FindControl("trAccRej")).Style.Add("background", "#f9f9e5 url(../images/ic_warning.png) 10px 7px no-repeat");
                }
                lblStatus.Visible = true;

                string reason = ConvertTo.String(DataBinder.Eval(e.Item.DataItem, "Reason"));

                if (!string.IsNullOrEmpty(reason))
                {
                    lblSavedReason.Text = reason.Trim();
                    lblSavedReason.Visible = true;
                    lblReason.Visible = true;
                }
            }
            else
            {
                lblReason.Visible = true;
                txtReason.Visible = true;
            }
        }
    }

    protected void Pager1_Pageindexchange(object sender, PagerCommandEventArgs e)
    {
        ctlPager pager = (ctlPager)sender;
        if ((pager != null))
        {
            switch (pager.ID)
            {
                case "Pager1":
                    Pager2.PageSize = Pager1.PageSize;
                    Pager2.CurrentPageNumber = Pager1.CurrentPageNumber;
                    break;
                case "Pager2":
                    Pager1.PageSize = Pager2.PageSize;
                    Pager1.CurrentPageNumber = Pager2.CurrentPageNumber;
                    break;
            }
        }

        BindInvoiceCompare();
    }

    protected void btnApproveSelected_Click(object sender, EventArgs e)
    {
        try
        {
            string sChecked = "";
            foreach (RepeaterItem ri in rptParentInvoiceCompare.Items)
            {
                if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkBox = (CheckBox)ri.FindControl("chkSelect");
                    if (chkBox.Checked)
                    {
                        sChecked = sChecked + chkBox.Attributes["value"].ToString() + ",";
                    }
                }
            }

            sChecked = sChecked.Trim(',');

            if (Session["InvoiceCompare"] != null && sChecked != "")
            {
                DataTable dtInvoiceCompare = (DataTable)Session["InvoiceCompare"];
                DataView dvInvoiceCompare = dtInvoiceCompare.DefaultView;
                dvInvoiceCompare.RowFilter = "HouseId in (" + sChecked + ")";

                bool isAccepted = ((Button)sender).ID == "btnApproveSelected" ? true : false;

                InsertInvoiceDetails(dvInvoiceCompare, isAccepted, false);

                compare();

                //ShowMessage("Successfully Accepted selected records.", lblMessage, MessageBoxType.Success);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    protected void rptParentInvoiceCompare_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Accept" || e.CommandName == "AcceptRoshskorInvoice")
            {
                if (Session["InvoiceCompare"] != null)
                {
                    bool isAccepted = true;

                    bool isRAmountAccepted = e.CommandName == "AcceptRoshskorInvoice" ? true : false;

                    DataTable dtInvoiceCompare = (DataTable)Session["InvoiceCompare"];
                    DataView dvInvoiceCompare = dtInvoiceCompare.DefaultView;
                    int houseId = ConvertTo.Integer(e.CommandArgument);
                    dvInvoiceCompare.RowFilter = "HouseId=" + houseId;

                    TextBox txtReason = (TextBox)e.Item.FindControl("txtReason");
                    string reason = string.Empty;
                    if (txtReason != null)
                        reason = txtReason.Text.Trim();

                    InsertInvoiceDetails(dvInvoiceCompare, isAccepted, isRAmountAccepted, reason);

                    CheckBox chkSelect = (CheckBox)e.Item.FindControl("chkSelect");
                    Button btnInnerAccept = (Button)e.Item.FindControl("btnInnerAccept");
                    Button btnAcceptRoshskorAmount = (Button)e.Item.FindControl("btnAcceptRoshskorAmount");
                    Label lblStatus = (Label)e.Item.FindControl("lblStatus");

                    chkSelect.Visible = false;
                    btnInnerAccept.Visible = false;
                    btnAcceptRoshskorAmount.Visible = false;                    

                    if (isRAmountAccepted == true)
                        lblStatus.Text = "Accepted RoshSkor Amount";
                    else
                        lblStatus.Text = "Accepted";

                    lblStatus.Visible = true;
                    if (isAccepted || isRAmountAccepted)
                    {
                        lblStatus.Style.Add("color", "#528404");
                        ((HtmlTableRow)e.Item.FindControl("trAccRej")).Style.Add("background", "#f9f9e5 url(../images/ic_warning.png) 10px 7px no-repeat");
                    }
                    else
                    {
                        lblStatus.Style.Add("color", "#9c413a");
                        ((HtmlTableRow)e.Item.FindControl("trAccRej")).Style.Add("background", "#f9f9e5 url(../images/ic_warning.png) 10px 7px no-repeat");
                    }

                    Label lblSavedReason = (Label)e.Item.FindControl("lblSavedReason");
                    Label lblReason = (Label)e.Item.FindControl("lblReason");
                    lblSavedReason.Visible = false;
                    lblReason.Visible = false;
                    txtReason.Visible = false;

                    if (!string.IsNullOrEmpty(reason))
                    {
                        lblSavedReason.Text = reason.Trim();
                        lblSavedReason.Visible = true;
                        lblReason.Visible = true;
                    }

                }

                ShowMessage("Successfully Accepted selected Acccount", lblMessage, MessageBoxType.Success);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    #endregion

    #region "PrivateMethods"

    /// <summary>
    /// Set Rights
    /// </summary>
    private void SetRights()
    {
        if (!ProjectSession.PageAccessRights.Contains(PageRole.SystemPages.InvoiceCompareDetail.GetHashCode()))
            Response.Redirect("UserProfile.aspx");
    }

    /// <summary>
    /// Show Hide Controls
    /// </summary>
    /// <param name="IsVisible"></param>
    private void ShowHideControls(bool IsVisible = false)
    {
        Pager1.Visible = IsVisible;
        Pager2.Visible = IsVisible;
        //trSelectAll.Visible = IsVisible;
        rptParentInvoiceCompare.Visible = IsVisible;

        if (IsPostBack)
        {
            if (IsVisible)
                lblNodata.Visible = false;
            else
                ShowMessage("No record found", lblNodata, MessageBoxType.Information);
        }
    }

    /// <summary>
    /// Bind dropdown controls
    /// </summary>
    private void BindDropDown()
    {
        General.BindAccount(ref ddlAccountNumber, true, "--SelectAll--");
        ddlAccountNumber.SelectedValue = Convert.ToString(HouseId);
        General.BindPeriodMonthYear(ref ddlPeriodId);
        if (PeriodId > 0)
            ddlPeriodId.SelectedValue = Convert.ToString(PeriodId);
        BindFilterParameter();
    }

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
    /// Bind parent invoice compare
    /// </summary>
    private void BindInvoiceCompare()
    {
        //Pager1.CurrentPageNumber = 1;
        //Pager1.TotalRecordsCount = 0;

        int totalCount = 0;

        int houseId = 0;
        if (ConvertTo.Integer(ddlAccountNumber.SelectedValue) > 0)
            houseId = ConvertTo.Integer(ddlAccountNumber.SelectedValue);

        int filterParameter = 0;
        if (ConvertTo.Integer(ddlFilterBy.SelectedValue) > 0)
            filterParameter = ConvertTo.Integer(ddlFilterBy.SelectedValue);

        DataSet ds = Invoice.GetInvoiceCompare(houseId, IPeriodId, filterParameter, Pager1.CurrentRowIndex, Pager1.PageSize, ref totalCount);

        Session["InvoiceCompare"] = ds.Tables[0];

        if (ds != null)
        {
            DataTable dtChild = ds.Tables[0].Copy();
            if (dtChild != null)
            {
                DataView dv = dtChild.DefaultView;
                DataTable dtParent = dv.ToTable(true, "HouseId", "AccountNo", "HouseAddress", "Exist", "IsAccepted", "IsRAmountAccepted", "Reason");
                dtParent.TableName = "ParentData";
                dtChild.TableName = "ChildData";
                ds.Tables.Clear();
                ds.Tables.Add(dtParent);
                ds.Tables.Add(dtChild);
                ds.Relations.Add("Invoice", ds.Tables["ParentData"].Columns["HouseId"], ds.Tables["ChildData"].Columns["HouseId"]);
            }
        }
        Pager1.TotalRecordsCount = totalCount;
        Pager2.TotalRecordsCount = totalCount;

        rptParentInvoiceCompare.DataSource = ds;
        rptParentInvoiceCompare.DataBind();

        if (totalCount > 0)
            ShowHideControls(true);
        else
            ShowHideControls();

    }

    private void compare()
    {
        Pager1.CurrentPageNumber = 1;
        Pager1.TotalRecordsCount = 0;
        Pager2.CurrentPageNumber = 1;
        Pager2.TotalRecordsCount = 0;
        IPeriodId = ConvertTo.Integer(ddlPeriodId.SelectedValue);
        BindInvoiceCompare();
    }

    private void InsertInvoiceDetails(DataView dvInvoiceCompare, bool isAccepted, bool isRAmountAccepted, string reason = "")
    {
        Invoice_Detail.InvoiceDetails(dvInvoiceCompare.ToTable(false, "HouseId", "TeriffId", "Description", "RAmountInc", "SAmountEx", "SVAT", "SAmountInc", "Difference", "sDiff", "cReference", "StartReading", "EndReading", "Reading", "CategoryType"), isAccepted, 0, IPeriodId, isRAmountAccepted, reason);
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