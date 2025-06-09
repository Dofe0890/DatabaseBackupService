using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Microsoft.SqlServer;
using System.Data.SqlClient;
namespace Database_backup
{
    public partial class Database_backup : ServiceBase
    {

        public string BackupFolder;
        public string BackupIntervalMinutes;

        public Database_backup()
        {
            InitializeComponent();
        }

        public void Log(string Message)
        {

            try
            {

                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH-mm-ss}] {Message}\n";
                string logDirectory = ConfigurationManager.AppSettings["LogFolder"];
                string logFilePath = Path.Combine(logDirectory, "ServiceStatusLogs.txt");
                if(string.IsNullOrEmpty(logDirectory))
                {
                    logDirectory = AppDomain .CurrentDomain.BaseDirectory;
                }


                if (!Directory.Exists(logDirectory ))
                {
                    Directory.CreateDirectory(logDirectory);
                }


                File.AppendAllText(logFilePath, logMessage);

                if (Environment.UserInteractive)
                {
                    Console.WriteLine(logMessage);
                }


            }
            catch (Exception ex)
            {
                Log($"Error detected {ex.Message} ");
                if(Environment.UserInteractive)
                {
                    Console.WriteLine("Error in" +  ex.Message);    
                }
            }


        }

        protected override void OnStart(string[] args)
        {



            BackupFolder = ConfigurationManager.AppSettings["BackupFolder"];

            if(!Directory.Exists(BackupFolder ))
            {
                Directory.CreateDirectory (BackupFolder);

            }


            Log("Service started ....");
            if (Environment.UserInteractive)
            {
                Console.WriteLine("Service is Running");
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {

                    string query = @"Backup Database [DVLD] 
                                To Disk = '"+ BackupFolder+"\\DVLD.bak' with INIT;";



                    connection.Open();

                    if (Environment.UserInteractive )
                    {
                        Console.WriteLine("Connection is opened");
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    if (Environment.UserInteractive)
                    {
                        Console.WriteLine("Database backup successfully");
                    }

                    Log($"Database backup successfully : {BackupFolder}");
                }
            }
            catch (Exception ex )
            {
                Log("Error " +  ex.Message);
            }
         

        }

        protected override void OnStop()
        {
            if(Environment.UserInteractive)
            {
                Console.WriteLine("Service has been stopped");
            }
            Log("Service has been stopped ");
        }

        public void StartInConsole()
        {
            OnStart(null);
            Console.WriteLine("Press Enter to stop this service ....");
            Console.ReadLine();
            OnStop();
            Console.ReadKey();
        }
    }
}
