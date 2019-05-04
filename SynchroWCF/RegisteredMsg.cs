using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SynchroWCF
{
	public static class RegisteredMsg
	{
		public const string  SynchroMessage = "SynchroServiceIPC";
		public static uint   RegisteredMessage { get; set; }
		public static IntPtr TargetMainWindow  { get; set; }

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		static extern uint RegisterWindowMessage(string lpString);

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor - initialize our registered message
		/// </summary>
		static RegisteredMsg()
		{
			TargetMainWindow  = IntPtr.Zero;
			RegisteredMessage = RegisterWindowMessage(SynchroMessage);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Post the desired text
		/// </summary>
		/// <param name="text"></param>
		public static void PostUpdateMsg(string text)
		{
			IntPtr lpData = Marshal.StringToHGlobalUni(text);
			IntPtr lpLength = new IntPtr(text.Length);
			HandleRef handleRef = new HandleRef(null, TargetMainWindow);
			PostMessage(handleRef, RegisteredMessage, lpLength, lpData);
		}

		public static IntPtr FindProcess(string name)
		{
			Process[] processList = Process.GetProcesses();
			IntPtr target;
			target = (from process in processList
					  where process.ProcessName == name
					  select process.MainWindowHandle).FirstOrDefault();
			TargetMainWindow = target;
			return target;
		}
	}
}
