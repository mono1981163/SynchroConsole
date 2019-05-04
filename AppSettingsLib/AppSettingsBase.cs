using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AppSettingsLib
{
	public class AppSettingsBase
	{
		#region Properties
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Get/set the special folder where the settings file will be stored
		/// </summary>
		public System.Environment.SpecialFolder SpecialFolder { get; set; }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Get/set the flag that indicates whether this is a "default" settings object
		/// </summary>
		public bool IsDefault { get; protected set; }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Get/set the settings filename 
		/// </summary>
		public string FileName { get; set; }

		public string DataFilePath { get; protected set; }
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Get/set the fully qualified path to the settings file
		/// </summary>
		public string FullyQualifiedPath { get; protected set; }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Get/set the comments placed in the settings file (optional)
		/// </summary>
		public string SettingsFileComment { get; set; }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Get/set the key name for the user settings section
		/// </summary>
		public string SettingsKeyName { get; set; }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Get/set the base class' data. This will throw an exception that reminds you 
		/// that you have to override it (because properties cannot be made abstract).
		/// </summary>
		public virtual XElement XElement
		{
			get { throw new Exception("You must provide your own XElement property."); }
			set { throw new Exception("You must provide your own XElement property."); }
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Get/set the default settings data (as an XElement object)
		/// </summary>
		public XElement DefaultSettings { get; set; }

		public XDocument Document { get; set; }
		#endregion Properties

		#region Constructors
		//--------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		protected AppSettingsBase()
		{
			this.SpecialFolder       = Environment.SpecialFolder.CommonApplicationData;
			this.DefaultSettings     = null;
			this.Document            = null;
			this.XElement            = null;
			this.FileName            = "";
			this.FullyQualifiedPath  = "";
			this.IsDefault           = false;
			this.SettingsFileComment = "";
			this.SettingsKeyName     = "";
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor - since we don't want the programmer to instantiate this class, 
		/// this constructor is protected (thanks Naveenth).
		/// </summary>
		/// <param name="appFolder">The name of the application settings folder</param>
		/// <param name="fileName">The name of the data file</param>
		protected AppSettingsBase(string appFolder, string fileName, string settingsKeyName, 
							   string fileComment, XElement defaultSettings):this()
		{
			this.DefaultSettings		= defaultSettings;
			this.IsDefault				= (this.DefaultSettings == null);
			this.FileName				= fileName;
			this.SettingsKeyName		= settingsKeyName;
			this.SettingsFileComment	= fileComment;
			//this.DataFilePath			= CreateAppDataFolder(appFolder);
			//this.FullyQualifiedPath      = System.IO.Path.Combine(this.DataFilePath, this.FileName);
			//if (!IsDefault && this.DefaultSettings != null)
			//{
			//    XElement = this.DefaultSettings;
			//}
		}
		#endregion Constructors

		#region Virtual methods
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Resets the user settings to the default settings
		/// </summary>
		/// <param name="defaultData">The data to be set</param>
		public virtual void Reset()
		{
			if (!IsDefault && this.DefaultSettings != null)
			{
				XElement = this.DefaultSettings;
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Loads the user settings, and then the default settings.
		/// </summary>
		public virtual void Load()
		{
			bool loaded = false;
			if (File.Exists(this.FullyQualifiedPath))
			{
				try
				{
					this.Document = XDocument.Load(this.FullyQualifiedPath);
					var settings = this.Document.Descendants(SettingsKeyName);
					if (settings.Count() > 0)
					{
						foreach (XElement element in settings)
						{
							this.XElement = element;
							loaded = true;
							// just in case we have more than one, only take the first one
							break;
						}
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			if (!loaded && !this.IsDefault && this.DefaultSettings != null)
			{
				Reset();
			}
		}


		//--------------------------------------------------------------------------------
		/// <summary>
		/// Saves the user settings and the default settings
		/// </summary>
		public virtual void Save()
		{
			if (!IsDefault)
			{
				//string fileName = System.IO.Path.Combine(m_dataFilePath, m_fileName);
				try
				{
					if (this.Document == null)
					{
						this.Document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), 
												  new XComment(this.SettingsFileComment));
					}
					var root = new XElement("ROOT", this.XElement);
					this.Document.Elements("ROOT").Remove();
					this.Document.Add(root);
					this.Document.Save(this.FullyQualifiedPath);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}


		//--------------------------------------------------------------------------------
		/// <summary>
		/// Sets the user settings to the current default settings
		/// </summary>
		public virtual void SetAsDefaults(ref XElement element)
		{
			if (!IsDefault)
			{
				element = XElement;
			}
		}
		#endregion Virtual methods

		#region Utility methods
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Casts the specified integer to the appropriate enum ordinal. If all else fails, 
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
		/// <summary>
		/// Create (if necessary) the specified application data folder. This method 
		/// only creates the root folder, and will throw an exception if more than one 
		/// folder is specified.  For instance, "\MyApp" is valid, but 
		/// "\MyApp\MySubFolder" is not.
		/// </summary>
		/// <param name="folderName">A single folder name (can have a bcakslash at either or both ends).</param>
		/// <returns>The fully qualified path that was created (or that already exists)</returns>
		protected string CreateAppDataFolder(string folderName)
		{
			string appDataPath = "";
			string dataFilePath = "";

			folderName = folderName.Trim();
			if (folderName != "")
			{
				try
				{
					// Set the directory where the file will come from.  The folder name 
					// returned will be different between XP and Vista. Under XP, the default 
					// folder name is "C:\Documents and Settings\All Users\Application Data\[folderName]"
					// while under Vista, the folder name is "C:\Program Data\[folderName]".
					appDataPath = System.Environment.GetFolderPath(this.SpecialFolder);
				}
				catch (Exception)
				{
					throw;
				}

				if (folderName.Contains("\\"))
				{
					string[] path = folderName.Split('\\');
					int folderCount = 0;
					int folderIndex = -1;
					for (int i = 0; i < path.Length; i++)
					{
						string folder = path[i];
						if (folder != "")
						{
							if (folderIndex == -1)
							{
								folderIndex = i;
							}
							folderCount++;
						}
					}
					if (folderCount != 1)
					{
						throw new Exception("Invalid folder name specified (this function only creates the root app data folder for the application).");
					}
					folderName = path[folderIndex];
				}
			}
			if (folderName == "")
			{
				throw new Exception("Processed folder name resulted in an empty string.");
			}
			try
			{
				dataFilePath = System.IO.Path.Combine(appDataPath, folderName);
				if (!Directory.Exists(dataFilePath))
				{
					Directory.CreateDirectory(dataFilePath);
				}
			}
			catch (Exception)
			{
				throw;
			}
			return dataFilePath;
		}
		#endregion Utility methods

	}

}

