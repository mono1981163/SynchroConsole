using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynchroWCF
{
	//////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////
	public class SynchroHostEventArgs
	{
		public string   Message { get; set; }
		public DateTime Date    { get; set; }

		//--------------------------------------------------------------------------------
		public SynchroHostEventArgs(string msg, DateTime datetime)
		{
			this.Message = msg;
			this.Date    = datetime;
		}
	}
	public delegate void SynchroHostEventHandler(object sender, SynchroHostEventArgs e);

}
