using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using GsmComm.GsmCommunication;
using GsmComm.Interfaces;
using GsmComm.PduConverter;
using GsmComm.Server;
using System.IO;
using DataAccess;

namespace WandESMSService
{
    public partial class Service1 : ServiceBase
    {
        #region Constructor

        public Service1()
            : base()
        {

            // This call is required by the Component Designer.
            InitializeComponent();

            this.tmr_main = new Timer();
            this.tmr_main.BeginInit();
            this.tmr_main.Enabled = false;
            this.tmr_main.Interval = Convert.ToInt32(Properties.Settings.Default.TimeInterval);
            this.tmr_main.EndInit();
        }

        #endregion

        /// <summary>
        /// timer for call smsservice.
        /// </summary>
        /// <remarks></remarks>

        private Timer _tmr_main;

        /// <summary>
        /// timer for call smsservice.
        /// </summary>
        /// <remarks></remarks>
        internal virtual Timer tmr_main
        {
            get { return this._tmr_main; }
            set
            {
                if (((this._tmr_main != null)))
                {
                    this._tmr_main.Elapsed -= new ElapsedEventHandler(this.tmr_main_Elapsed);
                }
                this._tmr_main = value;
                if (((this._tmr_main != null)))
                {
                    this._tmr_main.Elapsed += new ElapsedEventHandler(this.tmr_main_Elapsed);
                }
            }
        }

        private static string logFilePath = Properties.Settings.Default.LogFilePath;

        public void Start()
        {
            OnStart(null);
        }

        public void Stop()
        {
            OnStop();
        }

        protected override void OnStart(string[] args)
        {

            if (!Directory.Exists(Properties.Settings.Default.LogFilePath))
            {
                Directory.CreateDirectory(Properties.Settings.Default.LogFilePath);
            }

            SetLogPath();

            File.AppendAllText(logFilePath, "------- SMS Service Started. -------" + System.Environment.NewLine);

            this.tmr_main.Enabled = true;

            //// Add code here to start your service. This method should set things
            //// in motion so your service can do its work.            
            ////if ((ConfigHelper.InitializeConfiguration()))
            ////{
            //int commPort = Convert.ToInt32(Properties.Settings.Default.CommPort);
            //int commBaudRate = Convert.ToInt32(Properties.Settings.Default.CommBaudRate);
            //int commTimeOut = Convert.ToInt32(Properties.Settings.Default.CommTimeOut);
            //this.tmr_main.Enabled = true;
            //CommSetting.Comm_Port = commPort;
            //CommSetting.Comm_BaudRate = commBaudRate;
            //CommSetting.Comm_TimeOut = commTimeOut;
            //CommSetting.comm = new GsmCommMain(commPort, commBaudRate, commTimeOut);
            //try
            //{
            //    CommSetting.comm.Open();
            //    File.AppendAllText(logFilePath, "------- Comm Port Opened -------" + System.Environment.NewLine);
            //}
            //catch (Exception ex)
            //{
            //    commClose();
            //    File.AppendAllText(logFilePath, "comm not opened:" + ex.Message + System.Environment.NewLine);
            //}

            ////TimerTick();

            ////}
        }

        protected override void OnStop()
        {
            // Add code here to perform any tear-down necessary to stop your service.

            this.tmr_main.Enabled = false;
            commClose();
        }

        protected void commClose()
        {
            if (CommSetting.comm != null)
            {
                if (CommSetting.comm.IsOpen())
                {
                    CommSetting.comm.Close();
                }
            }
        }

        private void tmr_main_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimerTick();
        }

        private void TimerTick()
        {
            bool bProcessStart = false;
            try
            {
                this.tmr_main.Enabled = false;
                File.AppendAllText(logFilePath, "------- Timer Ticked -------" + System.Environment.NewLine);

                int commPort = Convert.ToInt32(Properties.Settings.Default.CommPort);
                int commBaudRate = Convert.ToInt32(Properties.Settings.Default.CommBaudRate);
                int commTimeOut = Convert.ToInt32(Properties.Settings.Default.CommTimeOut);
                

                CommSetting.Comm_Port = commPort;
                CommSetting.Comm_BaudRate = commBaudRate;
                CommSetting.Comm_TimeOut = commTimeOut;
            
            
                CommSetting.comm = new GsmCommMain(commPort, commBaudRate, commTimeOut);
                CommSetting.comm.Open();

                File.AppendAllText(logFilePath, "------- Comm Port Opened -------" + System.Environment.NewLine);

                if (CommSetting.comm.IsConnected())
                {
                    string storage = PhoneStorageType.Sim;

                    bProcessStart = true;
                        // Read all SMS messages from the storage

                        DecodedShortMessage[] messages = CommSetting.comm.ReadMessages(PhoneMessageStatus.All, storage);
                        foreach (DecodedShortMessage message in messages)
                        {
                            if (message.Status == PhoneMessageStatus.ReceivedUnread)
                            {
                                File.AppendAllText(logFilePath, "Process Message:" + message.Index + System.Environment.NewLine);
                                ProcessMessage(message.Data);
                                CommSetting.comm.DeleteMessage(message.Index, storage);
                            }
                        }
                    
                    
                }
                else
                {
                    File.AppendAllText(logFilePath, "comm port is not connected:" + System.Environment.NewLine);
                }

            }
            catch (Exception ex)
            {
                if (bProcessStart)
                {
                    File.AppendAllText(logFilePath, "Error while Processing a message:" + ex.Message + System.Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(logFilePath, "Comm port is not Open:" + System.Environment.NewLine);
                }
                commClose();
                
            }
            finally
            {
                commClose();
                this.tmr_main.Enabled = true;
            }


            
        }

        private void ProcessMessage(SmsPdu pdu)
        {
            if (pdu is SmsDeliverPdu)
            {
                // Received message
                SmsDeliverPdu data = (SmsDeliverPdu)pdu;
                string sender = data.OriginatingAddress.ToString();
                string time = data.SCTimestamp.ToString();
                string message = data.UserDataText;
                File.AppendAllText(logFilePath, "sender:" + sender + System.Environment.NewLine);
                File.AppendAllText(logFilePath, "time:" + time + System.Environment.NewLine);
                File.AppendAllText(logFilePath, "message:" + message + System.Environment.NewLine);

                if (!string.IsNullOrEmpty(message))
                {
                    DataTable dtCloseDates = Token_Request_Detail.CheckTokenRequestCloseDays(logFilePath, Properties.Settings.Default.ConnectionString.ToString(), message, sender, Token_Request_Detail.RequestTypes.SMS.GetHashCode());

                    bool allow = false;
                    DateTime dStartDate = DateTime.Now;

                    if (dtCloseDates.Rows.Count > 0)
                    {
                        allow = ConvertTo.Integer(dtCloseDates.Rows[0]["Result"]) == 0 ? false : true;
                        dStartDate = ConvertTo.Date(dtCloseDates.Rows[0]["StartDate"]);
                    }

                    if (allow)
                    {
                        if (!message.ToLower().Contains("account does not exist") && !message.ToLower().Contains("your token number"))
                        {
                            DataSet dsRequestResult = Token_Request_Detail.InsertTokenRequestDetail(paramToBePassed(message, sender), logFilePath, Properties.Settings.Default.ConnectionString.ToString());

                            if (dsRequestResult != null && dsRequestResult.Tables.Count > 1)
                            {
                                DataTable dtSuccess = dsRequestResult.Tables[0];
                                DataTable dtFailure = dsRequestResult.Tables[1];

                                SendSuccessMessage(dtSuccess);
                                SendFailureMessage(dtFailure);
                            }
                        }
                    }
                    else
                    {
                        SendAccountClosedMessage(sender, dStartDate);
                    }
                }
            }
        }

        protected void SetLogPath()
        {
            logFilePath = Path.Combine(Properties.Settings.Default.LogFilePath, "Log" + DateTime.Now.ToString("ddMMyyyy") + ".txt");
        }

        protected DataTable paramToBePassed(string accountNumber, string sender)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeNo", System.Type.GetType("System.String"));
            dt.Columns.Add("FromAddress", System.Type.GetType("System.String"));
            dt.Columns.Add("RequestType", System.Type.GetType("System.Int32"));
            dt.Columns.Add("UserId", System.Type.GetType("System.Int32"));

            DataRow dr = dt.NewRow();
            dr["EmployeeNo"] = ConvertTo.String(accountNumber.Trim());
            dr["FromAddress"] = ConvertTo.String(sender.Trim());
            dr["RequestType"] = ConvertTo.Integer(Token_Request_Detail.RequestTypes.SMS.GetHashCode());
            dr["UserId"] = 0;
            dt.Rows.Add(dr);

            return dt;
        }

        /// <summary>
        /// Send mail on successfull token request
        /// </summary>
        /// <param name="dtSuccess"></param>
        private void SendSuccessMessage(DataTable dtSuccess)
        {
            File.AppendAllText(logFilePath, "-----Start Sending Success Message.-----" + System.Environment.NewLine);

            foreach (DataRow drSuccess in dtSuccess.Rows)
            {
                string destinationNo = ConvertTo.String(drSuccess["FromAddress"]);
                string accountNumber = ConvertTo.String(drSuccess["AccountNumber"]);
                string tokenNumber = ConvertTo.String(drSuccess["TokenNumber"]);

                string body = string.Format(Properties.Settings.Default.RequestSuccessBody.ToString(), tokenNumber);

                SmsSubmitPdu pduSucc = new SmsSubmitPdu(body, destinationNo, "");
                CommSetting.comm.SendMessage(pduSucc);
            }
            File.AppendAllText(logFilePath, "-----Complete Sending Success Message.-----" + System.Environment.NewLine);
        }

        /// <summary>
        /// Send mail on failure token request
        /// </summary>
        /// <param name="dtFailure"></param>
        private void SendFailureMessage(DataTable dtFailure)
        {
            File.AppendAllText(logFilePath, "-----Start Sending Failure Message.-----" + System.Environment.NewLine);

            foreach (DataRow drFailure in dtFailure.Rows)
            {
                string destinationNo = ConvertTo.String(drFailure["FromAddress"]);
                string accountNumber = ConvertTo.String(drFailure["AccountNumber"]);

                string body = Properties.Settings.Default.RequestFailureBody.ToString();

                SmsSubmitPdu pdufail = new SmsSubmitPdu(body, destinationNo, "");
                CommSetting.comm.SendMessage(pdufail);
            }

            File.AppendAllText(logFilePath, "-----Complete Sending Failure Message.-----" + System.Environment.NewLine);
        }

        /// <summary>
        /// Send mail on failure token request
        /// </summary>
        /// <param name="dtFailure"></param>
        private void SendAccountClosedMessage(string sender, DateTime startDate)
        {
            File.AppendAllText(logFilePath, "-----Start Sending Account Closed Message.-----" + System.Environment.NewLine);

            string body = string.Format(Properties.Settings.Default.RequestClose.ToString(), startDate.ToString("dddd, MMMM dd, yyyy"));

            SmsSubmitPdu pduclosed = new SmsSubmitPdu(body, sender, "");
            CommSetting.comm.SendMessage(pduclosed);

            File.AppendAllText(logFilePath, "-----Complete Sending Account Closed Message.-----" + System.Environment.NewLine);
        }
    }
}
