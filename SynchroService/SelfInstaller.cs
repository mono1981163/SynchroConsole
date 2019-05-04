using System.Reflection;
using System.Configuration.Install;

namespace SynchroService
{

	//////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// 
	/// </summary>
	public static class SelfInstaller
	{
		private static readonly string m_appPath = Assembly.GetExecutingAssembly().Location;

		//--------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static bool Install()
		{
			try
			{
				ManagedInstallerClass.InstallHelper(new string[] { m_appPath } );
			}
			catch
			{
				return false;
			}
			return true;
		}

		//--------------------------------------------------------------------------------
		public static bool Uninstall()
		{
			try
			{
				ManagedInstallerClass.InstallHelper(new string[] { "/u", m_appPath } );
			}
			catch
			{
				return false;
			}
			return true;
		}
	}
}
