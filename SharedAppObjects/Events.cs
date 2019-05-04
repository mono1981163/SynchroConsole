using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	public class StartStopServiceEventArgs : EventArgs
	{
		public StartStopServiceEventArgs()
		{
		}
	}

	public delegate void StartStopServiceHandler(object sender, StartStopServiceEventArgs e);
}
