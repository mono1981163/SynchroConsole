using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
//using System.Windows.Forms;
using System.Security.Principal;

namespace SynchroServiceStarter
{
	//////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Provides a static instance of the SynchroService object and support methods
	/// </summary>
	public static class Globals
	{
/*
		const int SERVICE_TIMEOUT = 30000;

		private static string            m_serviceName    = "Synchronicity Service";
		public  static ServiceController SynchroService { get; set; }

		static Globals()
		{
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
		public static bool IsServiceInstalledWithStatus(ServiceControllerStatus status)
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
					installed      = (service.Status == status);
					break;
				}
			}
			return installed;
		}

		//--------------------------------------------------------------------------------
		public static void StartService()
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
					SynchroService.Start();
					SynchroService.WaitForStatus(ServiceControllerStatus.Running, timeout);
				}
				else
				{
					Console.WriteLine("Service could not be started (SynchroService object was null).");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("Service could not be started:\n\n{0}", ex.Message));
			}
		}

		//--------------------------------------------------------------------------------
		public static void StopService()
		{
			try
			{
				if (IsServiceInstalled())
				{
					TimeSpan timeout = TimeSpan.FromMilliseconds(SERVICE_TIMEOUT);
					SynchroService.Stop();
					SynchroService.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
				}
				else
				{
					Console.WriteLine("Service could not be stopped (SynchroService object was null).");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("Service could not be stopped:\n\n{0}", ex.Message));
			}
		}
*/
	}
}
