using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Description;

namespace SynchroWCF
{
	//////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Allows creation/closing/resetting of the various WCF objects needed by the 
	/// implementing applications.
	/// </summary>
	public static class SvcGlobals
	{
		private static NetNamedPipeBinding m_binding  = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);

		public  static Uri                 m_baseAddress = new Uri("net.pipe://localhost/SynchroServiceWCF");

		public  static ServiceHost     SvcHost     { get; private set; }
		public  static ISynchroService SvcClient   { get; private set; }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		static SvcGlobals()
		{
			SvcHost     = null;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Creates the service host object
		/// </summary>
		/// <returns></returns>
		public static bool CreateServiceHost()
		{
			bool available = (SvcHost != null);
			if (SvcHost == null)
			{
				try
				{
					SynchroService svc = new SynchroService();
					SvcHost = new ServiceHost(svc, m_baseAddress);
					SvcHost.AddServiceEndpoint(typeof(ISynchroService), m_binding, "");
					SvcHost.Open();
					available = (SvcHost != null);
				}
				catch (Exception ex)
				{
					throw new Exception("Exception encountered while creating SvcHost", ex);
				}
			}
			return available;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Closes the service host object
		/// </summary>
		public static void CloseServiceHost()
		{
			try
			{
				if (SvcHost != null && SvcHost.State == CommunicationState.Opened)
				{
					SvcHost.Close();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Exception encountered while closing SvcHost", ex);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Creates the service client object
		/// </summary>
		/// <returns></returns>
		public static bool CreateServiceClient()
		{
			bool available = (SvcClient != null);
			if (SvcClient == null)
			{
				try
				{
					ChannelFactory<ISynchroService> factory = new ChannelFactory<ISynchroService>(m_binding, new EndpointAddress(m_baseAddress.AbsoluteUri));
					SvcClient = factory.CreateChannel();
					available = (SvcClient != null);
				}
				catch (Exception ex)
				{
					throw new Exception("Exception encountered while creating SvcClient", ex);
				}
			}
			return available;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		///  Sets the service client object to null
		/// </summary>
		public static void ResetServiceClient()
		{
			SvcClient = null;
		}

	}

}
