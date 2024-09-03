using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace TokenRequestService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            ServiceController sc = new ServiceController(this.serviceInstaller1.ServiceName);

            sc.Start();
        }

        public override void Uninstall(IDictionary savedState)
        {
            ServiceController sc = new ServiceController(this.serviceInstaller1.ServiceName);

            if (sc.Status == ServiceControllerStatus.Running)
                sc.Stop();

            base.Uninstall(savedState);
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

    }
}
