using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.ServiceProcess;

using Common;

namespace SynchroServiceStarter
{
	/// <summary>
	/// This application is responsible for starting or stopping the SynchroService. I had 
	/// to write this app because you can't elevate privileges for just part of an 
	/// application.  What a pain (but an understandable pain, at least).
	/// </summary>
	class Program
	{

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Application entry point
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			try
			{
				// If this application isn't running as administrator, set the 
				// appropriate error code, and display a message at the console.
				if (!SynchCommon.RunningAsAdministrator())
				{
					Console.WriteLine("You must run this application as administrator.");
					SetExitCode(SSSExitCodes.NotAdminMode);
					return;
				}

				// If the service isn't installed, set the appropriate exit code and 
				// display a message at the console.
				if (!SynchCommon.IsServiceInstalled())
				{
					SetExitCode(SSSExitCodes.ServiceNotFound);
					Console.WriteLine("Service not found.");
					return;
				}

				// If we have arguments, try to start or stop the service
				if (args           != null && 
					args.Length    == 1 && 
					args[0].Length >  1 && 
					(args[0][0] == '-' || args[0][0] == '/'))
				{
					SetExitCode(SSSExitCodes.Success);
					ServiceControllerStatus currentStatus = SynchCommon.SynchroService.Status;
					switch (args[0].Substring(1).ToLower())
					{
						case "start":
							SynchCommon.StartService();
							if (!SynchCommon.IsServiceInstalled(ServiceControllerStatus.Running))
							{
								SetExitCode(SSSExitCodes.ServiceNotStarted);
								Console.WriteLine("Service found, but could not be started.");
							}
							else
							{
								Console.WriteLine("Service found, and started.");
							}
							break;
						case "stop":
							SynchCommon.StopService();
							if (!SynchCommon.IsServiceInstalled(ServiceControllerStatus.Stopped))
							{
								SetExitCode(SSSExitCodes.ServiceNotStopped);
								Console.WriteLine("Service found, but could not be stopped.");
							}
							else
							{
								Console.WriteLine("Service found, and stopped.");
							}
							break;
						default:
							SetExitCode(SSSExitCodes.InvalidParameters);
							Console.WriteLine("Service found, but no appropriate commandline parameters specified. Expecting either '-start' or '-stop'");
							break;
					}
				}
				else
				{
					SetExitCode(SSSExitCodes.NoParameters);
					Console.WriteLine("Service found, but could not be stopped.");
				}
			}
			catch (Exception ex)
			{
				SetExitCode(SSSExitCodes.Exception);
		        Console.WriteLine(string.Format("Exception: {0}", ex.Message));
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Set the apps's exit code so the calling application can report errors and/or 
		/// react accordingly.
		/// </summary>
		/// <param name="code"></param>
		static void SetExitCode(SSSExitCodes code)
		{
			Environment.ExitCode = (int)code;
		}
	}
}
