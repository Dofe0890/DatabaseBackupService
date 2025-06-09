using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Database_backup
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                Console.WriteLine("Running in Console Mode......");
                Database_backup service = new Database_backup ();
                service.StartInConsole();

            }
            else 

           { ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Database_backup()
            };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
