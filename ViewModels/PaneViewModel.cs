using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace AC.ViewModels
{
	partial class PaneViewModel : ViewModelBase
	{
        #region fields
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _contentId;

        [ObservableProperty]
        private bool _isSelected;

        [ObservableProperty]
        private bool _isActive;

        public ImageSource IconSource { get; protected set; }

        #endregion fields

        #region constructors
        public PaneViewModel()
		{
		}
		#endregion constructors
	}
}
