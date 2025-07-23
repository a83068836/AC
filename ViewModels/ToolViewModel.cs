using CommunityToolkit.Mvvm.ComponentModel;

namespace AC.ViewModels
{
	partial class ToolViewModel : PaneViewModel
	{
        #region fields
        [ObservableProperty]
        private bool _isVisible = true; 

        #endregion fields

        #region constructor
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="name"></param>
        public ToolViewModel(string name)
		{
			Name = name;
			Title = name;
		}
		#endregion constructor

		#region Properties
		public string Name { get; private set; }

		
		#endregion Properties
	}
}
