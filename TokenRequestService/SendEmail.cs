using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace TokenRequestService
{
    public static class SendEmail
    {
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

            mSmtpClient.Host = Properties.Settings.Default.MailServerHost.ToString();
            mSmtpClient.Port = Convert.ToInt32(Properties.Settings.Default.SMTPPort.ToString());
            mSmtpClient.EnableSsl = Convert.ToBoolean(Properties.Settings.Default.SMTPSSL.ToString());
            mSmtpClient.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.SMTPuserName.ToString(), Properties.Settings.Default.SMTPpassword.ToString());
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

    }
}
