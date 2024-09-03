using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using DataAccess;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

/// <summary>
/// Summary TokenSheetImport - Import excel sheet 
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreateDate> 18SEP2013 </CreateDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate> 18SEP2013 </ModifiedDate>
public partial class TokenSheetManagement : BasePageHome
{
    #region "Page Event"

    /// <summary>
    /// handles page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //setrights

        RunScript("ChangeMenuCSSOnClick('lnkAdmin');");

        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.TokenSheetManagement.GetHashCode());
            BindDropdowns();
        }

        lblMessage.Text = string.Empty;
        lblMessage.CssClass = string.Empty;
    }

    #endregion

    #region "Control Events"

    /// <summary>
    /// handles btnImport Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (fuTokenSheet.HasFile)
            {
                DataTable dtTokenData = ImportFile();
                if (dtTokenData != null && dtTokenData.Rows.Count > 0)
                {
                    DataTable dtNewTokenData = new DataTable();
                    dtNewTokenData.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
                    dtNewTokenData.Columns.Add("NoOfUnits", System.Type.GetType("System.Int32"));
                    dtNewTokenData.Columns.Add("TokenNumber", System.Type.GetType("System.String"));
                    dtNewTokenData.Columns.Add("MeterNo", System.Type.GetType("System.String"));

                    foreach (DataRow dr in dtTokenData.Rows)
                    {
                        DataRow drNew = dtNewTokenData.NewRow();
                        drNew["AccountNumber"] = ConvertTo.String(dr["AccountNo"]);
                        drNew["NoOfUnits"] = ConvertTo.Integer(dr["RequiredUnit"]);
                        drNew["TokenNumber"] = ConvertTo.String(dr["TokenNo"]);
                        drNew["MeterNo"] = ConvertTo.String(dr["MeterNo"]);
                        dtNewTokenData.Rows.Add(drNew);
                    }
                    Token_Detail.BulkInsertToken(dtNewTokenData, ProjectSession.UserID);
                }

                ShowMessage("Token Sheet Imported Successfully.", lblMessage, MessageBoxType.Success);
            }
            else
                ShowMessage("Select file to import", lblMessage, MessageBoxType.Warning);

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    /// <summary>
    /// btnGenerate Click Handles - Export data to excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtAccountSheet = House.GetDataForTokenSheet();

            if (dtAccountSheet != null && dtAccountSheet.Rows.Count > 0)
            {
                ExportData(dtAccountSheet , "Emp_" + DateTime.Now.Month.ToString() + "_Token_" + DateTime.Now.Year.ToString() , false );
                ShowMessage("Token Sheet Generated Successfully.", lblMessage, MessageBoxType.Success);
            }
            else
                ShowMessage("No Records Found.", lblMessage, MessageBoxType.Information);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    /// <summary>
    /// Save Page
    /// </summary>
    /// <param name=sender></param>
    /// <param name=e></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int actionType = ActionType.Save.GetHashCode();

            Token_Period objTokenPeriod = new Token_Period();

            FillObject(ref objTokenPeriod);
            Token_Period.InsertTokenPeriod(objTokenPeriod);

            SetCurrentValue();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    /// <summary>
    /// Handles btnDownloadRequestedTokens Click - Download Requested Tokens
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDownloadRequestedToken_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MinValue;

            if (!string.IsNullOrEmpty(txtFromDate.Text.Trim()))
                fromDate = ConvertTo.Date(txtFromDate.Text.Trim());
            
            if (!string.IsNullOrEmpty(txtToDate.Text.Trim()))
                toDate = ConvertTo.Date(txtToDate.Text.Trim());

            DataTable dtRequestedTokens = Token_Request_Detail.GetRequestedTokens(fromDate , toDate );

            if ( dtRequestedTokens != null && dtRequestedTokens.Rows.Count > 0)
                ExportData(dtRequestedTokens, "TOkenRequest_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString(), true);
            else
                ShowMessage("No Requested Tokens Found." , lblMessage , MessageBoxType.Information);
            
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Import Excel file and get records as datatable
    /// </summary>
    /// <returns></returns>
    private DataTable ImportFile()
    {
        DataTable dtTokenData = new DataTable();
        string fname = Server.MapPath("Input\\");

        if (!Directory.Exists(fname))
            Directory.CreateDirectory(fname);

        string fileExtension = System.IO.Path.GetExtension(fuTokenSheet.PostedFile.FileName).ToLower();
        if (fileExtension.ToLower() == ".xls" || fileExtension.ToLower() == ".xlsx")
        {
            string fileName = fname + fuTokenSheet.FileName;
            fuTokenSheet.SaveAs(fileName);

            DataSet dsTokenData = DataAccess.General.GetExcelDataSet(fileName);

            if (dsTokenData != null)
            {
                dtTokenData = dsTokenData.Tables[0];
            }
            else
            {
                ShowMessage("No Data Found", lblMessage, MessageBoxType.Information);
            }
        }

        return dtTokenData;
    }

    /// <summary>
    /// Export Data to excel
    /// </summary>
    /// <param name="dtAccounts"></param>
    private void ExportData(DataTable dtAccounts, string fileName, bool isTokenRequestData)
    {
        MemoryStream mStream = DownloadAsExcel(dtAccounts, isTokenRequestData);

        if (mStream != null)
        {
            Context.Response.AppendHeader("content-disposition", "attachment; filename=" + fileName  + ".xls");
            Context.Response.AddHeader("Content-Length", mStream.Length.ToString());
            Context.Response.ContentType = "application/excel";
            Context.Response.BinaryWrite(mStream.ToArray());
        }
        else
        {
            Context.Response.ContentType = "text/plain";
            Context.Response.Write("No data found.");
        }
    }

    /// <summary>
    /// Download Excel File 
    /// </summary>
    /// <param name="imageUrl"></param>
    /// <param name="tableHtml"></param>
    /// <param name="reportHeader"></param>
    /// <returns></returns>
    private MemoryStream DownloadAsExcel(DataTable dt , bool isTokenRequestData)
    {
        HSSFWorkbook objExcelSheet = new HSSFWorkbook();

        ISheet excelSheet = null;
        IPrintSetup printSetup = null;
        excelSheet = objExcelSheet.CreateSheet();
        printSetup = excelSheet.PrintSetup;
        printSetup.Landscape = true;
        excelSheet.FitToPage = true;
        excelSheet.HorizontallyCenter = true;

        IRow dataRow;
        ICell dataCell;

        if ( !isTokenRequestData)
            dt.Columns.Add("TokenNo");

        dataRow = excelSheet.CreateRow(0);
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            dataCell = dataRow.CreateCell(i);

            string cellValue = ConvertTo.String(dt.Columns[i].ColumnName);
            dataCell.SetCellValue(cellValue);

        }

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            DataRow dr = dt.Rows[j];
            dataRow = excelSheet.CreateRow(j + 1);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dataCell = dataRow.CreateCell(i);

                string cellValue = ConvertTo.String(dr[i]);

                int val;
                if (int.TryParse(cellValue, out val))
                    dataCell.SetCellValue(ConvertTo.Integer(cellValue));
                else
                    dataCell.SetCellValue(cellValue);
            }
        }

        MemoryStream mStream = new MemoryStream();
        objExcelSheet.Write(mStream);
        mStream.Flush();
        return mStream;
    }

    /// <summary>
    /// Bind Start day and end day dropdown
    /// </summary>
    private void BindDropdowns()
    {
        for (int i = 1; i <= 31; i++)
        {
            ddlStartDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            ddlEndDay.Items.Add(new ListItem(i.ToString(), i.ToString()));

        }

        ddlStartDay.DataBind();
        ddlEndDay.DataBind();

        SetCurrentValue();
    }

    /// <summary>
    /// Set current value
    /// </summary>
    private void SetCurrentValue()
    {
        DataTable dt = Token_Period.GetCurrentTokenPeriod();

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlStartDay.SelectedValue = ConvertTo.String(dt.Rows[0]["StartDay"]);
            ddlEndDay.SelectedValue = ConvertTo.String(dt.Rows[0]["EndDay"]);
        }
    }

    /// <summary>
    /// Fill Object
    /// </summary>
    private void FillObject(ref Token_Period objTokenPeriod)
    {
        objTokenPeriod.StartDay = ConvertTo.Integer(ddlStartDay.SelectedValue);
        objTokenPeriod.EndDay = ConvertTo.Integer(ddlEndDay.SelectedValue);
        objTokenPeriod.IsCurrent = true;

        objTokenPeriod.Created_UserId = ProjectSession.UserID;
        objTokenPeriod.Created_TS = DateTime.Now;
    }

    #endregion
    
}