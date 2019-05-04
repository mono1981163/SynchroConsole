using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Common;

namespace SynchroSetup
{
	//////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Provides a static instance of the SynchroService object and support methods
	/// </summary>
	public static class Globals
	{
		const int SERVICE_TIMEOUT = 30000;

		private static string            m_serviceName    = "Synchronicity Service";
		public  static ServiceController SynchroService { get; set; }
		public  static Process           StarterProcess { get; set; }

		static Globals()
		{
			string appToRun = Application.ExecutablePath;
			appToRun = appToRun.Replace(System.IO.Path.GetFileName(appToRun), "SynchroServiceStarter.exe");

			ProcessStartInfo info       = new ProcessStartInfo();
			info.FileName               = appToRun;
			info.WorkingDirectory       = System.IO.Path.GetDirectoryName(Environment.CurrentDirectory);
			info.CreateNoWindow         = false;
			info.UseShellExecute        = true;
			info.Verb                   = "runas";

			StarterProcess                     = new Process();
			StarterProcess.StartInfo           = info;
			StarterProcess.EnableRaisingEvents = true;
		}


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
					if (SynchCommon.RunningAsAdministrator())
					{
						SynchCommon.StartService();
					}
					else
					{
						RunServiceStartingProcess("-start");
					}
				}
				else
				{
					MessageBox.Show("Service could not be started (SynchroService object was null).");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Service could not be started:\n\n{0}", ex.Message));
			}
		}

		//--------------------------------------------------------------------------------
		public static void StopService()
		{
			try
			{
				if (IsServiceInstalled())
				{
					if (SynchCommon.RunningAsAdministrator())
					{
						SynchCommon.StopService();
					}
					else
					{
						RunServiceStartingProcess("-stop");
					}
				}
				else
				{
					MessageBox.Show("Service could not be stopped (SynchroService object was null).");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Service could not be stopped:\n\n{0}", ex.Message));
			}
		}

		//--------------------------------------------------------------------------------
		public static void RunServiceStartingProcess(string parameter)
		{
			try
			{
				if (StarterProcess != null)
				{
					StarterProcess.StartInfo.Arguments = parameter;
				}
				StarterProcess.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Exception Encountered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		[DllImport("kernel32.dll", SetLastError=true)]
		private static extern int GetModuleFileName([In]IntPtr hModule, [Out]StringBuilder lpFilename, [In][MarshalAs(UnmanagedType.U4)] int nSize);

		//--------------------------------------------------------------------------------
		// When you run an app inside Visual Studio, the call to GetFileModulename will 
		// return a different path than Application.ExecutablePath, so we can use that to 
		// our advantage when trying to determine whether or not your app is running in 
		// the IDE.
		/// <summary>
		/// Determines if the application is running in Visual Studio or not.
		/// </summary>
		/// <returns></returns>
		public static bool RunningInVisualStudio()
		{
			StringBuilder moduleName = new StringBuilder(1024);
			int result = GetModuleFileName(IntPtr.Zero, moduleName, moduleName.Capacity);
			string appName = Application.ExecutablePath.ToLower();
			return (appName != moduleName.ToString().ToLower());
		}
	}
}
