using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using DataAccess;
using System.Data;
using System.IO;
using System.Reflection;


/// <summary>
/// Summary Description Common
/// </summary>
/// <CreatedBy>Darpan Khandhar</CreatedBy>
/// <CreatedDate> 25-Jun-2013 </CreatedDate>
/// <ModifiedBy>Darpan Khandhar</ModifiedBy>
/// <ModifiedDate> 26-Jun-2013 </ModifiedDate>
public class Common
{
    #region "Constructor"

    public Common()
    {
    }

    #endregion

    #region "Private Methods"

    /// <summary>
    /// Send Mail
    /// </summary>
    /// <param name="FromAddress"></param>
    /// <param name="FromDisplay"></param>
    /// <param name="Recipients"></param>
    /// <param name="BCC"></param>
    /// <param name="CC"></param>
    /// <param name="Subject"></param>
    /// <param name="Body"></param>
    /// <param name="IsHTML"></param>
    /// <returns></returns>
    public static bool SendMail(string FromAddress, string FromDisplay, string Recipients, string BCC, string CC, string Subject, string Body, bool IsHTML)
    {
        // Instantiate a new instance of MailMessage
        MailMessage mMailMessage = new MailMessage();

        if (!string.IsNullOrEmpty(FromAddress))
        {
            // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(FromAddress, FromDisplay);
            //mMailMessage.fr.
        }

        char[] arrSplitChar = { ',' };

        if (!string.IsNullOrEmpty(Recipients))
        {
            string[] individualRecipients = Recipients.Split(arrSplitChar);
            foreach (string recipient in individualRecipients)
            {
                if (recipient.Contains(";"))
                {
                    char[] arrSplitTempChar = { ';' };
                    string[] arrTempTo = recipient.Split(arrSplitTempChar);
                    foreach (string strTOTemp in arrTempTo)
                    {
                        // Set the recepient address of the mail message
                        mMailMessage.To.Add(new MailAddress(strTOTemp));
                    }
                }
                else
                {
                    // Set the recepient address of the mail message
                    mMailMessage.To.Add(new MailAddress(recipient));
                }
            }
        }


        // Check if the bcc value is nothing or an empty string
        if (!string.IsNullOrEmpty(BCC))
        {
            string[] arrBCC = BCC.Split(arrSplitChar);
            foreach (string strBCC in arrBCC)
            {
                if (strBCC.Contains(";"))
                {
                    char[] arrSplitTempChar = { ';' };
                    string[] arrTempBCC = strBCC.Split(arrSplitTempChar);
                    foreach (string strBCCTemp in arrTempBCC)
                    {
                        // Set the recepient address of the mail message
                        mMailMessage.Bcc.Add(new MailAddress(strBCCTemp));
                    }
                }
                else
                {
                    // Set the recepient address of the mail message
                    mMailMessage.Bcc.Add(new MailAddress(strBCC));
                }
            }
        }

        // Check if the cc value is nothing or an empty value
        if (!string.IsNullOrEmpty(CC))
        {
            string[] arrCC = CC.Split(arrSplitChar);
            foreach (string strCC in arrCC)
            {
                if (strCC.Contains(";"))
                {
                    char[] arrSplitTempChar = { ';' };
                    string[] arrTempCC = strCC.Split(arrSplitTempChar);
                    foreach (string strCCTemp in arrTempCC)
                    {
                        // Set the recepient address of the mail message
                        mMailMessage.CC.Add(new MailAddress(strCCTemp));
                    }
                }
                else
                {
                    // Set the recepient address of the mail message
                    mMailMessage.CC.Add(new MailAddress(strCC));
                }
            }
        }


        // Set the subject of the mail message
        mMailMessage.Subject = Subject;
        // Set the body of the mail message
        mMailMessage.Body = Body;
        // Set the format of the mail message body as HTML
        mMailMessage.IsBodyHtml = IsHTML;
        // Set the priority of the mail message to normal
        mMailMessage.Priority = MailPriority.Normal;

        SmtpClient mSmtpClient = new SmtpClient();

        mSmtpClient.Host = ProjectConfiguration.MailServerHost;
        mSmtpClient.Credentials = new System.Net.NetworkCredential(ProjectConfiguration.SMTPuserName, ProjectConfiguration.SMTPpassword);
        try
        {
            // Send the mail message
            mSmtpClient.Send(mMailMessage);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Download Files
    /// </summary>
    /// <param name="response"></param>
    /// <param name="path"></param>
    /// <param name="fileExt"></param>
    public static void DownloadFile(HttpResponse response, string path, string fileExt)
    {
        try
        {
            System.IO.FileStream LiveFileStream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] fileBuffer = new byte[Convert.ToInt32(LiveFileStream.Length) + 1];

            LiveFileStream.Read(fileBuffer, 0, Convert.ToInt32(LiveFileStream.Length));
            LiveFileStream.Close();

            if (fileExt == SystemEnum.FileType.XLSX.ToString())
                HttpContext.Current.Response.Redirect("~/ExportedCharts/Excel-1.xlsx");
            else
            {
                response.Clear();
                response.AppendHeader("Content-Disposition", "attachment; filename=" + (new System.IO.FileInfo(path)).Name);
                response.AppendHeader("Content-Length", fileBuffer.Length.ToString());
                response.ContentType = "application/octet-stream";
                response.WriteFile(path);
            }
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// Gets html table and returns datatable
    /// </summary>
    /// <param name="tableHtml"></param>
    /// <returns></returns>
    public static DataTable HtmlTableToDataTable(string tableHtml)
    {
        string dataString = tableHtml;
        int startIndex;
        int endIndex;
        bool flage = false;
        string findData;
        DataTable dtData = new DataTable();
        int rowCount = 0;
        while ((flage == false))
        {
            startIndex = dataString.IndexOf("<tr");
            endIndex = dataString.IndexOf("</tr>");
            if ((startIndex == -1 | endIndex == -1))
            {
                flage = true;
            }
            else
            {
                endIndex = endIndex + 5;
                findData = dataString.Substring(startIndex, endIndex - startIndex);
                if (rowCount == 0)
                {
                    string[] spliData = findData.Split(new string[] { "<td" }, StringSplitOptions.RemoveEmptyEntries);
                    if (spliData.Length > 0)
                    {
                        for (int i = 1; i <= spliData.Length; i++)
                        {
                            dtData.Columns.Add("column" + i.ToString());
                        }
                    }
                }

                GetTabularData(ref dtData, findData);
                rowCount = rowCount + 1;
                dataString = dataString.Substring(endIndex, dataString.Length - endIndex);
            }
        }

        return dtData;
    }

    /// <summary>
    /// create row of datatable from htmltable
    /// </summary>
    /// <param name="dtData"></param>
    /// <param name="findData"></param>
    private static void GetTabularData(ref DataTable dtData, string findData)
    {
        DataRow dr = dtData.NewRow();
        bool blnFlage = false;
        int startIndexFindData = 0;
        int endIndexFindData = 0;
        int dataCounter = 0;
        string subDataString = string.Empty;

        while ((blnFlage == false))
        {
            startIndexFindData = findData.IndexOf("<td");
            endIndexFindData = findData.IndexOf("</td>");

            if ((startIndexFindData == -1 | endIndexFindData == -1))
            {
                blnFlage = true;
            }
            else
            {
                endIndexFindData = endIndexFindData + 5;
                subDataString = findData.Substring(startIndexFindData, endIndexFindData - startIndexFindData);

                if (subDataString.Contains("span"))
                {
                    int startIndexSpanData = findData.IndexOf("<span");
                    int endIndexSpanData = findData.IndexOf("</span>");

                    subDataString = findData.Substring(startIndexSpanData, endIndexSpanData - startIndexSpanData);

                    dr[dataCounter] = subDataString.Substring(subDataString.IndexOf(">") + 1, subDataString.Length - subDataString.IndexOf(">") - 1).Trim();
                }
                else
                    dr[dataCounter] = subDataString.Substring(subDataString.IndexOf(">") + 1, subDataString.IndexOf("</td") - subDataString.IndexOf(">") - 1).Trim();

                dataCounter = dataCounter + 1;
                blnFlage = false;
                findData = findData.Substring(endIndexFindData, findData.Length - endIndexFindData);
            }
        }
        dtData.Rows.Add(dr);
    }

    public static void DisposeOf(object @object)
    {
        if ((@object != null))
        {
            if (@object is IDisposable)
            {
                ((IDisposable)@object).Dispose();
            }

            @object = null;
        }
    }

    public static List<T> ToCollection<T>(DataTable dt)
    {
        List<T> lst = new System.Collections.Generic.List<T>();
        Type tClass = typeof(T);
        PropertyInfo[] pClass = tClass.GetProperties();
        List<DataColumn> dc = dt.Columns.Cast<DataColumn>().ToList();
        T cn;
        foreach (DataRow item in dt.Rows)
        {
            cn = (T)Activator.CreateInstance(tClass);
            foreach (PropertyInfo pc in pClass)
            {

                DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                if (d != null)
                    pc.SetValue(cn, item[pc.Name], null);
            }
            lst.Add(cn);
        }
        return lst;
    }

    #endregion
}