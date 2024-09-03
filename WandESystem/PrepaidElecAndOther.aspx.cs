using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

public partial class PrepaidElecAndOther : BasePageHome
{
    #region "Variable/Private Decalarations"

    private decimal _invoiceTotal;

    /// <summary>
    /// Total of invoice
    /// </summary>
    public decimal InvoiceTotal
    {
        get
        {
            return ConvertTo.Decimal(_invoiceTotal);
        }
        set
        {
            _invoiceTotal = value;
        }
    }

    #endregion

    #region "Page Events"

    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkReport');");
        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.PrepaidElecAndOther.GetHashCode());
            if (!string.IsNullOrEmpty(ProjectSession.UserName))
            {               
                General.BindPeriodMonthYear(ref ddlPeriodId);                
            }
        }
    }

    #endregion

    #region "Control Events"

    protected void btnGenerateInvoice_Click(object sender, EventArgs e)
    {
        InvoiceGeneate();
    }

    /// <summary>
    /// Handles rptParentInvoice Itemdatabound  - Bind detail , generate total
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptParentInvoice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rptChildInvoice = (Repeater)e.Item.FindControl("rptChildInvoice");

            DataView dv = ((DataRowView)e.Item.DataItem).CreateChildView("InvoiceDetail");

            Label lblCategory = (Label)e.Item.FindControl("lblCategory");
            lblCategory.Text = ConvertTo.String(DataBinder.Eval(e.Item.DataItem, "CategoryType"));

            rptChildInvoice.DataSource = dv;
            rptChildInvoice.DataBind();
        }
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lblTotal = (Label)e.Item.FindControl("lblTotal");
            lblTotal.Text = ConvertTo.String(InvoiceTotal);
        }
    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Generate invoice - Get data from db and bind it to repeater
    /// </summary>
    private void InvoiceGeneate()
    {
        DataSet ds = Invoice_Detail.GetPrePaidElecAndOther(ConvertTo.Integer(ddlPeriodId.SelectedValue));

        if (ds != null)
        {
            DataTable dtChild = ds.Tables[0].Copy();
            DataTable dtAccountInvoiceInformation = ds.Tables[1].Copy();
            if (dtChild != null)
            {
                DataView dv = dtChild.DefaultView;
                DataTable dtParent = dv.ToTable(true, "CategoryType");
                dtParent.TableName = "ParentData";
                dtChild.TableName = "ChildData";
                DataSet dsInvoice = new DataSet();
                ds.Tables.Clear();
                ds.Tables.Add(dtParent);
                ds.Tables.Add(dtChild);
                ds.Relations.Add("InvoiceDetail", ds.Tables["ParentData"].Columns["CategoryType"], ds.Tables["ChildData"].Columns["CategoryType"]);
                InvoiceTotal = ConvertTo.Decimal(dtChild.Compute("SUM(SAmountInc)", ""));
            }

            if (dtAccountInvoiceInformation != null && dtAccountInvoiceInformation.Rows.Count > 0)
            {
                lblLocation.Text = ConvertTo.String(dtAccountInvoiceInformation.Rows[0]["Address"]);
                lblInvoiceNumber.Text = ConvertTo.String(dtAccountInvoiceInformation.Rows[0]["InvoiceNumber"]);
                lblDate.Text = ConvertTo.Date(dtAccountInvoiceInformation.Rows[0]["InvoiceDate"]).ToString("yyyy/MM/dd");
                lblAccountNumber.Text = ConvertTo.String(dtAccountInvoiceInformation.Rows[0]["AccountNo"]);
            }

            dvInvoice.Visible = true;
            aprintInvoice.Visible = true;
        }

        rptParentInvoice.DataSource = ds;
        rptParentInvoice.DataBind();
    }

    #endregion
}