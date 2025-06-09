using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Database_backup
{
    [RunInstaller(true)]
    public partial class InstallerDatabaseBackup : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller ServiceInstaller;
        public InstallerDatabaseBackup()
        {
            InitializeComponent();
            serviceProcessInstaller = new ServiceProcessInstaller
            {

                Account = ServiceAccount.LocalService
            };

            ServiceInstaller = new ServiceInstaller
            {
                ServiceName = "Database_backup",
                DisplayName = "Database Backup Service ",
                Description = "its backup database every secluded time ",
                StartType = ServiceStartMode.Automatic,
                ServicesDependedOn = new string[] { "EventLog", "RpcSs", "MSSQLSERVER" }
            };

            Installers.Add(serviceProcessInstaller);
            Installers.Add(ServiceInstaller );

        }
    }
}
