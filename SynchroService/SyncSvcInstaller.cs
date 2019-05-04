using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;


namespace SynchroService
{
	//////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// This class overrides the ServiceInstaller class so that we can start 
	/// the service automatically after it's been installed (if we want to). 
	/// </summary>
	public class ExtendedServiceInstaller : ServiceInstaller
	{
		public override void Commit(System.Collections.IDictionary savedState)
		{
			// Let the base commit do its job first.
			base.Commit(savedState);

			// Now start the newly installed service.
			ServiceController controller = new ServiceController(this.ServiceName);
			controller.Start();
			controller.Close();
		}
	}


	//////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////
	[RunInstallerAttribute(true)]
	public partial class SyncSvcInstaller : Installer
	{
		private ServiceInstaller m_serviceInstaller;
		private ServiceProcessInstaller m_processInstaller;

		public SyncSvcInstaller()
		{
			InitializeComponent();

			try
			{
				// create and configure our installer and processinstaller objects
				m_serviceInstaller = new ServiceInstaller();

				// If you don't want the service to start automatically, change the 
				// following line to whatever start mode is appropriate.
				m_serviceInstaller.StartType = ServiceStartMode.Manual;

				//TODO: Change these three items to more accurately reflect the service's purpose
				m_serviceInstaller.DisplayName = "Synchronicity Service";
				m_serviceInstaller.ServiceName = "Synchronicity Service";
				m_serviceInstaller.Description = "Synchronizes files between specified folder pairs";

				m_processInstaller         = new ServiceProcessInstaller();
				m_processInstaller.Account = ServiceAccount.LocalSystem;

				// add our installers to the list
				this.Installers.Add(m_serviceInstaller);
				this.Installers.Add(m_processInstaller);

				// perform any other preparatory steps necessary to make your service 
				// function properly.
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
