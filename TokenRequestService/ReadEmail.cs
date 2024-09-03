using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using OpenPop.Pop3.Exceptions;
using OpenPop.Common.Logging;
using Message = OpenPop.Mime.Message;
using System.Net.Mail;
using ICSharpCode.SharpZipLib.Zip;
using DataAccess;

namespace TokenRequestService
{
    /// <summary>
    /// Summary Read Email and create datatable of fromaddress and subject (accountnumber).
    /// </summary>
    /// <CreatedBy> Darpan Khandhar </CreatedBy>
    /// <CreatedDate> 19-Sep-2013 </CreatedDate>
    /// <ModifiedBy> Darpan Khandhar </ModifiedBy>
    /// <ModifiedDate> 19-Sep-2013 </ModifiedDate>
    public class ReadEmail
    {
        #region PrivateVariables

        private Pop3Client pop3Client;
        private Dictionary<int, Message> messages;
        MailMessage mMailMessage = new MailMessage();

        /// <summary>
        /// Sets log file path.
        /// </summary>
        public string LogFilePath
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor - Initialize new object.
        /// </summary>
        public ReadEmail()
        {
            pop3Client = new Pop3Client();
            messages = new Dictionary<int, Message>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Connnect to pop3 client
        /// </summary>
        public void Connect()
        {
            if (pop3Client.Connected)
                pop3Client.Disconnect();

            pop3Client.Connect("rpzcrpnsrvex", Properties.Settings.Default.Port, Properties.Settings.Default.IsSSL);
            pop3Client.Authenticate(Properties.Settings.Default.UserName, Properties.Settings.Default.Password);
        }

        /// <summary>
        /// Eecieves mail and create datatable
        /// </summary>
        /// <returns></returns>
        public DataTable ReceiveMails()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeNo", System.Type.GetType("System.String"));
            dt.Columns.Add("FromAddress", System.Type.GetType("System.String"));
            dt.Columns.Add("RequestType", System.Type.GetType("System.Int32"));
            dt.Columns.Add("UserId", System.Type.GetType("System.Int32"));

            try
            {
                Connect();
                messages.Clear();
                int j = 0;

                //pop3Client.DeleteAllMessages();

                int _totalMessage = pop3Client.GetMessageCount();

                for (int i = _totalMessage; i >= 1; i -= 1)
                {
                    try
                    {
                        Message message = pop3Client.GetMessage(i);


                        string _uids = pop3Client.GetMessageUid(i);
                        string _subject = message.Headers.Subject;

                        RfcMailAddress FromMail = message.Headers.From;
                        string _messageID = message.Headers.MessageId;
                        string _fromAddress = FromMail.Address;
                        string _receivedDate = message.Headers.DateSent.ToLocalTime().ToString();

                        // Add the message to the dictionary from the messageNumber to the Message
                        messages.Add(i, message);

                        if (Properties.Settings.Default.IsDelete)
                            DeleteMessageByMessageId(i);

                        DataRow dr = dt.NewRow();
                        string empNumber = string.Empty;

                        if ((_subject.IndexOf(")") - _subject.IndexOf("(")) > 0)
                            empNumber = _subject.Substring(_subject.IndexOf("(") + 1, (_subject.IndexOf(")") - _subject.IndexOf("(") - 1));

                        DataTable dtCloseDates = Token_Request_Detail.CheckTokenRequestCloseDays(LogFilePath, Properties.Settings.Default.ConnectionString.ToString(), _subject, _fromAddress, Token_Request_Detail.RequestTypes.Email.GetHashCode());

                        bool allow = false;
                        DateTime dStartDate = DateTime.Now;

                        if (dtCloseDates.Rows.Count > 0)
                        {
                            allow = ConvertTo.Integer(dtCloseDates.Rows[0]["Result"]) == 0 ? false : true;
                            dStartDate = ConvertTo.Date(dtCloseDates.Rows[0]["StartDate"]);
                        }
                        if (allow)
                        {
                            if (!string.IsNullOrEmpty(empNumber))
                            {
                                dr["EmployeeNo"] = ConvertTo.String(empNumber.Trim());
                                dr["FromAddress"] = ConvertTo.String(_fromAddress.Trim());
                                dr["RequestType"] = ConvertTo.Integer(Token_Request_Detail.RequestTypes.Email.GetHashCode());
                                dr["UserId"] = 0;
                                dt.Rows.Add(dr);
                            }
                            else
                            {
                                dr["EmployeeNo"] = ConvertTo.String(_subject.Trim());
                                dr["FromAddress"] = ConvertTo.String(_fromAddress.Trim());
                                dr["RequestType"] = ConvertTo.Integer(Token_Request_Detail.RequestTypes.Email.GetHashCode());
                                dr["UserId"] = 0;
                                dt.Rows.Add(dr);
                                //SendEmail.SendMail(Properties.Settings.Default.FromAddress.ToString(), Properties.Settings.Default.FromDisplay.ToString(), _fromAddress, "", "", Properties.Settings.Default.TokenRequestFailure.ToString(), body, true);
                            }
                        }
                        else
                        {
                            string body = string.Format(Properties.Settings.Default.RequestClose.ToString(), dStartDate.ToString("dddd, MMMM dd, yyyy"));
                            SendEmail.SendMail(Properties.Settings.Default.FromAddress.ToString(), Properties.Settings.Default.FromDisplay.ToString(), _fromAddress, "", "", Properties.Settings.Default.TokenRequestFailure.ToString(), body, true);
                        }
                    }
                    catch (Exception e)
                    {
                        File.AppendAllText(LogFilePath, "------- Error in individual email reading. -------" + e.Message + System.Environment.NewLine);
                    }
                }
            }
            catch (Exception e)
            {
                File.AppendAllText(LogFilePath, "------- Error in email reading. -------" + e.Message + System.Environment.NewLine);
            }
            finally
            {
                Disconnect();
            }

            return dt;
        }

        /// <summary>
        /// Disconnect from POP3
        /// </summary>
        public void Disconnect()
        {
            if (pop3Client.Connected)
                pop3Client.Disconnect();
        }

        /// <summary>
        /// Delete Message by Id
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public bool DeleteMessageByMessageId(int messageId)
        {
            try
            {
                pop3Client.DeleteMessage(messageId);
                return true;
            }
            catch (Exception ex)
            {
                // We did not find any message with the given messageId, report this back
                return false;
            }
        }

        #endregion
    }
}
