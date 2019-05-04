using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SynchroService
{
	//////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// 
	/// </summary>
	public static class Program
	{
		//--------------------------------------------------------------------------------
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		public static void Main(string[] args)
		{
			if (args           != null && 
				args.Length    == 1 && 
				args[0].Length >  1 && 
				(args[0][0] == '-' || args[0][0] == '/'))
            {
                switch (args[0].Substring(1).ToLower())
                {
                    case "install":
                    case "i":
                        SelfInstaller.Install();
                        break;
                    case "uninstall":
                    case "u":
                        SelfInstaller.Uninstall();
                        break;
                    default:
                        break;
                }
            }
            else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] 
				{ 
					new SynchroService() 
				};
				ServiceBase.Run(ServicesToRun);
			}
		}
	}
}
