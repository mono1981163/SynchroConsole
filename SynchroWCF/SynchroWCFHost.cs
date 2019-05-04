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
	[ServiceContract]
	public interface ISynchroService
	{
		[OperationContract]
		void SendStatusMessage(string msg);
		[OperationContract]
		void SendStatusMessageEx(string msg, DateTime datetime);
	}


	//////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////
	[ServiceBehavior(InstanceContextMode=InstanceContextMode.Single, IncludeExceptionDetailInFaults=true)]
	public class SynchroService : ISynchroService
	{
		public event SynchroHostEventHandler SynchroHostEvent = delegate { };
		//public delegate void SendStatusMessageDelegate1(object sender, string msg);
		//public delegate void SendStatusMessageDelegate2(object sender, string msg, DateTime datetime);

		public SynchroService()
		{
		}

		//--------------------------------------------------------------------------------
		public void SendStatusMessage(string msg)
		{
			//SendStatusMessageDelegate1 method = new SendStatusMessageDelegate1(SvcGlobals.SendStatusMessage);
			//method.Invoke(this, msg);
			SynchroHostEvent(this, new SynchroHostEventArgs(msg, DateTime.Now));
		}

		//--------------------------------------------------------------------------------
		public void SendStatusMessageEx(string msg, DateTime datetime)
		{
			SynchroHostEvent(this, new SynchroHostEventArgs(msg, datetime));
			//SendStatusMessageDelegate2 method = new SendStatusMessageDelegate2(SvcGlobals.SendStatusMessage);
			//method.Invoke(this, msg, datetime);
		}
	}


}
