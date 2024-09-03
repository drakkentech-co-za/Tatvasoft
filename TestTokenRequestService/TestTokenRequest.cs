using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TokenRequestService;
//using WandESMSService;

namespace TestTokenRequestService
{
 
    public partial class TestTokenRequest : Form
    {
        #region "Variable/Property Declarations"

        //Service1 objWandESMAService = new Service1();

        TokenRequest objTokenRequest = new TokenRequest();

        #endregion

        #region "Initialize Componentes"

        /// <summary>
        /// Default Construction - Initialize Component
        /// </summary>
        public TestTokenRequest()
        {
            InitializeComponent();
            btnStop.Enabled = false;
        }

        #endregion

        #region "Control Events"

        /// <summary>
        /// Handles btnstart Click - Starting tokenrequest service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //objWandESMAService.Start();
            objTokenRequest.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        /// <summary>
        /// Handles btnStop click - Ending tokenrequest service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            //objWandESMAService.Stop();
            objTokenRequest.Stop();
            btnStop.Enabled = false;
            btnStart.Enabled = true;
        }

        #endregion
    }
}

