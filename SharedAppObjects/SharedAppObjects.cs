using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.ServiceProcess;

namespace Common
{
	public enum SSSExitCodes {	Success=0, 
								NotAdminMode, 
								ServiceNotFound, 
								ServiceNotStarted, 
								ServiceNotStopped, 
								Exception, 
								InvalidParameters, 
								NoParameters, 
								Unexpected };

	//////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Represents methods and objects that all of the apps/libs might use, and that are 
	/// not involved with the communications side of things.
	/// </summary>
	public static class SynchCommon
	{
		const int SERVICE_TIMEOUT = 30000;

		private static string            m_serviceName    = "Synchronicity Service";
		public  static ServiceController SynchroService { get; set; }

		public static event StartStopServiceHandler StartStopServiceEvent = delegate{};


		//--------------------------------------------------------------------------------
		/// <summary>
		/// Casts the specified integer to an appropriate enum. If all else fails, 
		/// the enum will be returned as the specified default ordinal.
		/// </summary>
		/// <param name="value">The integer value representing an enumeration element</param>
		/// <param name="deafaultValue">The default enumertion to be used if the specified "value" does not exist in the enumeration definition</param>
		/// <returns></returns>
		public static T IntToEnum<T>(int value, T defaultValue)
		{
			T enumValue = (Enum.IsDefined(typeof(T), value)) ? (T)(object)value : defaultValue;
			return enumValue;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Determines if the appplication is running in admin mode.
		/// </summary>
		/// <returns></returns>
		public static bool RunningAsAdministrator()
		{
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(identity);
			return principal.IsInRole(WindowsBuiltInRole.Administrator);
		}

		//--------------------------------------------------------------------------------
		public static bool IsServiceInstalled()
		{
			bool installed = false;
			// get the list of currently installed Windows services
			ServiceController[] services = ServiceController.GetServices();
			// look for the name
			foreach (ServiceController service in services)
			{
				if (service.ServiceName == m_serviceName)
				{
					SynchroService = new ServiceController(m_serviceName);
					installed      = true;
					break;
				}
			}
			return installed;
		}

		//--------------------------------------------------------------------------------
		public static bool IsServiceInstalled(ServiceControllerStatus status)
		{
			bool installed = (IsServiceInstalled() && SynchroService.Status == status) ? true : false;
			return installed;
		}

		//--------------------------------------------------------------------------------
		public static void StartStopService(bool restart)
		{
			try
			{
				if (IsServiceInstalled())
				{
					TimeSpan timeout = TimeSpan.FromMilliseconds(SERVICE_TIMEOUT);
					if (SynchroService.Status == ServiceControllerStatus.Running)
					{
						SynchroService.Stop();
						SynchroService.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
					}
					if (restart)
					{
						SynchroService.Start();
						SynchroService.WaitForStatus(ServiceControllerStatus.Running, timeout);
					}
					StartStopServiceEvent(SynchroService, new StartStopServiceEventArgs());					
				}
				else
				{
					throw new Exception(string.Format("Could not {0} service - SynchroService object was null.", (restart) ? "start/restart" : "stop"));
				}
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Service could not be {0}:\n\n{1}", (restart) ? "started/restarted" : "stopped", ex.Message));
			}
		}

		//--------------------------------------------------------------------------------
		public static void StartService()
		{
			//try
			//{
			//    if (IsServiceInstalled())
			//    {
			//        TimeSpan timeout = TimeSpan.FromMilliseconds(SERVICE_TIMEOUT);
			//        if (SynchroService.Status == ServiceControllerStatus.Running)
			//        {
			//            SynchroService.Stop();
			//            SynchroService.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
			//        }
			//        SynchroService.Start();
			//        SynchroService.WaitForStatus(ServiceControllerStatus.Running, timeout);
			//    }
			//    else
			//    {
			//        Console.WriteLine("Service could not be started (SynchroService object was null).");
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Console.WriteLine(string.Format("Service could not be started:\n\n{0}", ex.Message));
			//}
			StartStopService(true);
		}

		//--------------------------------------------------------------------------------
		public static void StopService()
		{
			//try
			//{
			//    if (IsServiceInstalled())
			//    {
			//        TimeSpan timeout = TimeSpan.FromMilliseconds(SERVICE_TIMEOUT);
			//        SynchroService.Stop();
			//        SynchroService.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
			//    }
			//    else
			//    {
			//        Console.WriteLine("Service could not be stopped (SynchroService object was null).");
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Console.WriteLine(string.Format("Service could not be stopped:\n\n{0}", ex.Message));
			//}
			StartStopService(false);
		}
	}
}
