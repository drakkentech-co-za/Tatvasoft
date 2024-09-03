using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Timers;
using DataAccess;

namespace TokenRequestService
{
    /// <summary>
    /// Summary TokenRequest - Receive mail and through subject get account number and create request.
    /// </summary>
    /// <CreatedBy> Darpan Khandhar </CreatedBy>
    /// <CreatedDate> 19SEP2013 </CreatedDate>
    /// <ModifiedBy> Darpan Khandhar </ModifiedBy>
    /// <ModifiedDate> 19SEP2013 </ModifiedDate>
    public partial class TokenRequest : ServiceBase
    {
        #region "Private Variables / Property Declarations"

        private static string logFilePath = Properties.Settings.Default.LogFilePath;
        private Timer _pollTimer = new Timer();
        private static int interval = Properties.Settings.Default.Interval;
        ReadEmail readEmail;

        private string _fileExtension { get; set; }
        private string _fileName { get; set; }

        #endregion

        #region "Initialization"

        public TokenRequest()
        {
            InitializeComponent();
            readEmail = new ReadEmail();
        }

        #endregion

        #region "Start Service"

        public void Start()
        {
            OnStart(null);
        }

        public void Stop()
        {
            OnStop();
        }

        #endregion

        #region "Service Methods"

        protected override void OnStart(string[] args)
        {
            try
            {
                if (!Directory.Exists(Properties.Settings.Default.LogFilePath))
                    Directory.CreateDirectory(Properties.Settings.Default.LogFilePath);

                SetLogPath();

                File.AppendAllText(logFilePath, "------- Token Request Service Started. -------" + System.Environment.NewLine);

                ProcessTokenRequest();

                _pollTimer.Interval = interval;
                _pollTimer.Enabled = true;
                _pollTimer.Elapsed += new ElapsedEventHandler(tmrPollSeconds_Elapsed);
                _pollTimer.Start();

                base.OnStart(args);
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, "Error Occurs :" + ex.Message + System.Environment.NewLine);
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected void tmrPollSeconds_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _pollTimer.Enabled = false;
            try
            {
                SetLogPath();
                ProcessTokenRequest();
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, "Error Occurs :" + ex.Message + System.Environment.NewLine);
            }
            finally
            {
                _pollTimer.Enabled = true;
            }

        }

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Sets logfie path - logs activity and error into file.
        /// </summary>
        public void SetLogPath()
        {
            logFilePath = Path.Combine(Properties.Settings.Default.LogFilePath, "Log" + DateTime.Now.ToString("ddMMyyyy") + ".txt");
        }

        /// <summary>
        /// Process token requests.
        /// </summary>
        private void ProcessTokenRequest()
        {
            try
            {
                File.AppendAllText(logFilePath, "------- Token Request Process Started. -------" + System.Environment.NewLine + System.Environment.NewLine);

                File.AppendAllText(logFilePath, "------- Searching For New Emails. -------" + System.Environment.NewLine + System.Environment.NewLine);

                readEmail.LogFilePath = logFilePath;

                DataTable dtRequestedAccounts = readEmail.ReceiveMails();

                File.AppendAllText(logFilePath, "------- Completed searching for new emails. -------" + System.Environment.NewLine + System.Environment.NewLine);

                if (dtRequestedAccounts != null && dtRequestedAccounts.Rows.Count > 0)
                {
                    File.AppendAllText(logFilePath, "-----Start Insertion Token Request into Database.-----" + System.Environment.NewLine);

                    DataSet dsRequestResult = Token_Request_Detail.InsertTokenRequestDetail(dtRequestedAccounts, logFilePath, Properties.Settings.Default.ConnectionString.ToString());

                    File.AppendAllText(logFilePath, "-----Completed Insertion Token Request into Database.-----" + System.Environment.NewLine);

                    if (dsRequestResult != null && dsRequestResult.Tables.Count > 1)
                    {
                        DataTable dtSuccess = dsRequestResult.Tables[0];
                        DataTable dtFailure = dsRequestResult.Tables[1];

                        SendSuccessMail(dtSuccess);
                        SendFailureMail(dtFailure);
                    }
                }

                File.AppendAllText(logFilePath, "------- Token Request Processing Completed. -------" + System.Environment.NewLine + System.Environment.NewLine);
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, "Error Occurs :" + ex.Message + System.Environment.NewLine);
            }
        }

        /// <summary>
        /// Send mail on successfull token request
        /// </summary>
        /// <param name="dtSuccess"></param>
        private void SendSuccessMail(DataTable dtSuccess)
        {
            File.AppendAllText(logFilePath, "-----Start Sending Success Mail.-----" + System.Environment.NewLine);

            foreach (DataRow drSuccess in dtSuccess.Rows)
            {
                string toAddress = ConvertTo.String(drSuccess["FromAddress"]);
                string accountNumber = ConvertTo.String(drSuccess["AccountNumber"]);
                string tokenNumber = ConvertTo.String(drSuccess["TokenNumber"]);

                string body = string.Format(Properties.Settings.Default.RequestSuccessBody.ToString(), tokenNumber);

                SendEmail.SendMail(Properties.Settings.Default.FromAddress.ToString(), Properties.Settings.Default.FromDisplay.ToString(), toAddress, "", "", Properties.Settings.Default.TokenRequestSuccess.ToString(), body, true);
            }

            File.AppendAllText(logFilePath, "-----Complete Sending Success Mail.-----" + System.Environment.NewLine);
        }

        /// <summary>
        /// Send mail on failure token request
        /// </summary>
        /// <param name="dtFailure"></param>
        private void SendFailureMail(DataTable dtFailure)
        {
            File.AppendAllText(logFilePath, "-----Start Sending Failure Mail.-----" + System.Environment.NewLine);

            foreach (DataRow drFailure in dtFailure.Rows)
            {
                string toAddress = ConvertTo.String(drFailure["FromAddress"]);
                string accountNumber = ConvertTo.String(drFailure["AccountNumber"]);

                string body = Properties.Settings.Default.RequestFailureBody.ToString();

                SendEmail.SendMail(Properties.Settings.Default.FromAddress.ToString(), Properties.Settings.Default.FromDisplay.ToString(), toAddress, "", "", Properties.Settings.Default.TokenRequestFailure.ToString(), body, true);
            }

            File.AppendAllText(logFilePath, "-----Complete Sending Failure Mail.-----" + System.Environment.NewLine);
        }

        #endregion
    }
}
