using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Security.Principal;

namespace SynchroConsole
{
	static class Program
	{
		private static bool m_mustBeAdmin = false;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (m_mustBeAdmin)
			{
				try
				{
					WindowsIdentity identity = WindowsIdentity.GetCurrent();
					WindowsPrincipal principal = new WindowsPrincipal(identity);
					if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
					{
						MessageBox.Show("You must run this application as administrator. Terminating.");
						Application.Exit();
					}
					else
					{
					}
				}
				catch (Exception ex)
				{
					if (ex != null) {}
				}
			}

			Application.Run(new FormMainConsole());
		}
	}
}
