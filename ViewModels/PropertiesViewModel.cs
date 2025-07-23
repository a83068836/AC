using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace AC.ViewModels
{
	internal class PropertiesViewModel : ToolViewModel
	{
		#region fields
		public const string ToolContentId = "Properties";
		private DateTime _lastModified;
		private long _fileSize;
		private string _FileName;
		private string _FilePath;
		#endregion fields

		#region constructors
		/// <summary>
		/// Class constructor
		/// </summary>
		public PropertiesViewModel()
			: base("Properties")
		{
			Workspace.This.ActiveDocumentChanged += new EventHandler(OnActiveDocumentChanged);
			ContentId = ToolContentId;
		}
		#endregion constructors

		#region Properties

		

		#endregion Properties

		#region methods
		private void OnActiveDocumentChanged(object sender, EventArgs e)
		{
			
		}
		#endregion methods

	}
}
