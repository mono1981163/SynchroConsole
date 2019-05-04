using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using AppSettingsLib;

namespace SynchroLib
{
	public class SyncSettings : AppSettingsBase
	{
		#region Data members
		// used to seed the base constructor
		private	const string APP_DATA_FOLDER	= "PaddedwallSync";
		private const string APP_DATA_FILENAME	= "Settings.xml";
		private const string FILE_COMMENT		= "Synch Settings";
		private const string SETTINGS_KEYNAME   = "Settings";
		private const string SYNC_ITEMS_KEYNAME = "SyncItems";
		#endregion Data members

		#region Properties

		//................................................................................
		/// <summary>
		/// Get/set number of minutes between sync updates
		/// </summary>
		public int    SyncMinutes     { get; set; }

		//................................................................................
		/// <summary>
		/// Get/set flag indicating whether or not to normalize times
		/// </summary>
		public bool   NormalizeTime   { get; set; }

		//................................................................................
		/// <summary>
		/// Get/set the backup files path
		/// </summary>
		public string InstallUtilPath { get; set; }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Get/set data as XElement object
		/// </summary>
		public override XElement XElement
		{
			get
			{
				return new XElement(this.SettingsKeyName
									,new XElement("SyncMinutes",	 this.SyncMinutes.ToString())
									,new XElement("NormalizeTime",   this.NormalizeTime.ToString())
									,new XElement("InstallUtilPath", this.InstallUtilPath)
									);
			}
			set
			{
				if (value != null)
				{
					this.SyncMinutes     = value.GetValue("SyncMinutes",     5);
					this.NormalizeTime   = value.GetValue("NormalizeTime",   true);
					this.InstallUtilPath = value.GetValue("InstallUtilPath", "");
				}
			}
		}

		public SyncItemCollection SyncItems { get; set; }
		#endregion Properties

		#region Constructors
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="appDataFolder">The name of the app data folder to use</param>
		/// <param name="appDataFilename">The name of the data file name</param>
		/// <param name="settingsKeyName">The name of the settings element we're looking for</param>
		/// <param name="fileComment">The text of the comment (if any)</param>
		/// <param name="element"></param>
		public SyncSettings(XElement defaultSettings)
			: base()
		{
			this.SyncItems              = new SyncItemCollection();
			this.SpecialFolder          = System.Environment.SpecialFolder.CommonApplicationData;
			this.DefaultSettings		= defaultSettings;
			this.IsDefault				= false;
			this.FileName				= APP_DATA_FILENAME;
			this.SettingsKeyName		= SETTINGS_KEYNAME;
			this.SettingsFileComment	= FILE_COMMENT;
			this.DataFilePath			= CreateAppDataFolder(APP_DATA_FOLDER);
			this.FullyQualifiedPath     = System.IO.Path.Combine(this.DataFilePath, this.FileName);

			this.SyncMinutes      = 5;
			this.NormalizeTime    = true;
			this.InstallUtilPath  = "";

			Reset();
		}
		#endregion Constructors

		//--------------------------------------------------------------------------------
		public override void Load()
		{
			if (File.Exists(this.FullyQualifiedPath))
			{
				try
				{
					XDocument doc = XDocument.Load(this.FullyQualifiedPath);
					XElement root = doc.Element("ROOT");
					if (root != null)
					{
						XElement settings = root.Element(this.SettingsKeyName);
						if (settings != null)
						{
							this.XElement = settings;
						}
						if (SyncItems != null)
						{
							this.SyncItems.Clear();
						}
						this.SyncItems = new SyncItemCollection(root.Element("SyncItems"));
					}
				}
				catch (Exception ex)
				{
					throw new Exception("Exception encountered while loading settings file", ex);
				}
			}
		}

		//--------------------------------------------------------------------------------
		public override void Save()
		{
			try
			{
				if (File.Exists(this.FullyQualifiedPath))
				{
					File.Delete(this.FullyQualifiedPath);
				}
				XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), 
											  new XComment(this.SettingsFileComment));
				XElement root = new XElement("ROOT");
				root.Add(this.XElement);
				root.Add(this.SyncItems.XElement);
				doc.Add(root);
				doc.Save(this.FullyQualifiedPath);
			}
			catch (Exception ex)
			{
				throw new Exception("Exception encountered while saving settings file", ex);
			}
		}
	}
}
