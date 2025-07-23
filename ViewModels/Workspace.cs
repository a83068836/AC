using AvalonDock.Themes;
using BusinessLib.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FilterTreeViewLib.Models;
using FilterTreeViewLib.ViewModels;
using HL.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AC.ViewModels
{
    internal partial class Workspace : ViewModelBase, IRecipient<OpenLocationMessage>
    {
        #region fields

        private static Workspace _this = new Workspace();
        private ToolViewModel[] _tools;
        private readonly ObservableCollection<FileViewModel> _files = new ObservableCollection<FileViewModel>();
        private ReadOnlyObservableCollection<FileViewModel> _readOnlyFiles;

        [ObservableProperty]
        private FileViewModel _activeDocument;

        private ExplorerViewModel? _explorer;
        public ExplorerViewModel Explorer
        {
            get => _explorer ??= new ExplorerViewModel();
            set => SetProperty(ref _explorer, value); // 保留通知能力
        }
        private PropertiesViewModel? _props;
        public PropertiesViewModel Props
        {
            get => _props ??= new PropertiesViewModel();
            set => SetProperty(ref _props, value);
        }

        private ErrorViewModel? _errors;
        public ErrorViewModel Errors
        {
            get => _errors ??= new ErrorViewModel();
            set => SetProperty(ref _errors, value);
        }

        private OutputViewModel? _output;
        public OutputViewModel Output
        {
            get => _output ??= new OutputViewModel();
            set => SetProperty(ref _output, value);
        }

        private GitChangesViewModel? _git;
        public GitChangesViewModel Git
        {
            get => _git ??= new GitChangesViewModel();
            set => SetProperty(ref _git, value);
        }

        private ToolboxViewModel? _toolbox;
        public ToolboxViewModel Toolbox
        {
            get => _toolbox ??= new ToolboxViewModel();
            set => SetProperty(ref _toolbox, value);
        }

        [ObservableProperty]
        private Tuple<string, Theme> _selectedTheme;

        #endregion fields

        #region constructors

        /// <summary>
        /// Class constructor
        /// </summary>
        public Workspace()
        {
            SelectedTheme = Themes[0];
            WeakReferenceMessenger.Default.Register(this);
        }


        // 实现 IRecipient<OpenLocationMessage> 接口
        public void Receive(OpenLocationMessage message)
        {
            if (message.Location != null)
            {
                OpenLocationInDockingManager(message.Location);
            }
        }
        // 双击命令（通过 TreeViewBehavior 绑定）
        [RelayCommand]
        private void ItemDoubleClick(MetaLocationViewModel location)
        {
            if (location != null)
            {
                OpenLocationInDockingManager(location);
            }
        }

        // 回车命令（通过 TreeViewBehavior 绑定）
        [RelayCommand]
        private void ItemEnterKey(MetaLocationViewModel location)
        {
            if (location != null)
            {
                OpenLocationInDockingManager(location);
            }
        }

        // 在 DockingManager 中打开位置
        private void OpenLocationInDockingManager(MetaLocationViewModel location)
        {
            if (location.Model.Type == LocationType.File)
            {
                var fileViewModel = Open(location.FullPath);
                ActiveDocument = fileViewModel;
            }
        }










        #endregion constructors

        public event EventHandler ActiveDocumentChanged;

        #region properties

        public static Workspace This => _this;

        public ReadOnlyObservableCollection<FileViewModel> Files
        {
            get
            {
                if (_readOnlyFiles == null)
                {
                    _readOnlyFiles = new ReadOnlyObservableCollection<FileViewModel>(_files);
                }
                return _readOnlyFiles;
            }
        }

        public IEnumerable<ToolViewModel> Tools
        {
            get
            {
                if (_tools == null)
                {
                    _tools = new ToolViewModel[]
                    {
                        Explorer, Props, Errors, Output, Git, Toolbox
                    };
                }

                return _tools;
            }
        }

        partial void OnActiveDocumentChanged(FileViewModel oldValue, FileViewModel newValue)
        {
            ActiveDocumentChanged?.Invoke(this, EventArgs.Empty);
        }
        public List<Tuple<string, Theme>> Themes { get; set; } = new List<Tuple<string, Theme>>
        {
            new Tuple<string, Theme>(nameof(Vs2013DarkTheme),new Vs2013DarkTheme()),
            new Tuple<string, Theme>(nameof(Vs2013LightTheme),new Vs2013LightTheme())
        };

        partial void OnSelectedThemeChanged(Tuple<string, Theme> oldValue, Tuple<string, Theme> newValue)
        {
            SwitchExtendedTheme();
        }
        #endregion properties

        #region methods

        private void SwitchExtendedTheme()
        {
            var hlManager = Base.ModelBase.GetService<IThemedHighlightingManager>();
            switch (_selectedTheme.Item1)
            {
                case string name when name.Contains("Dark"):
                    Application.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MLib;component/Themes/DarkTheme.xaml");
                    Application.Current.Resources.MergedDictionaries[1].Source = new Uri("pack://application:,,,/AC;component/Themes/DarkBrushsExtended.xaml");
                    Application.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/TextEditLib;component/Themes/DarkBrushs.xaml");
                    Application.Current.Resources.MergedDictionaries[3].Source = new Uri("pack://application:,,,/AvalonEditB;component/Themes/DarkBrushs.xaml");
                    //Application.Current.Resources.MergedDictionaries[4].Source = new Uri("pack://application:,,,/AC;component/Resources/Themes/SkinDark.xaml");
                    Application.Current.Resources.MergedDictionaries[4].Source = new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDark.xaml");
                    Application.Current.Resources.MergedDictionaries[5].Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml");
                    Application.Current.Resources.MergedDictionaries[6].Source = new Uri("pack://application:,,,/FilterTreeViewLib;component/Themes/DarkSkin.xaml");
                    hlManager.SetCurrentTheme("VS2019_Dark");
                    foreach (var file in _files)
                    {
                        OnPropertyChanged(nameof(file.HighlightingDefinitions));
                        if (file != null)
                            file.OnAppThemeChanged(hlManager);
                    }
                    break;
                case string name when name.Contains("Light"):
                    Application.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MLib;component/Themes/LightTheme.xaml");
                    Application.Current.Resources.MergedDictionaries[1].Source = new Uri("pack://application:,,,/AC;component/Themes/LightBrushsExtended.xaml");
                    Application.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/TextEditLib;component/Themes/LightBrushs.xaml");
                    Application.Current.Resources.MergedDictionaries[3].Source = new Uri("pack://application:,,,/AvalonEditB;component/Themes/LightBrushs.xaml");
                    //Application.Current.Resources.MergedDictionaries[4].Source = new Uri("pack://application:,,,/AC;component/Resources/Themes/SkinDefault.xaml");
                    Application.Current.Resources.MergedDictionaries[4].Source = new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDefault.xaml");
                    Application.Current.Resources.MergedDictionaries[5].Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml");
                    Application.Current.Resources.MergedDictionaries[6].Source = new Uri("pack://application:,,,/FilterTreeViewLib;component/Themes/DefaultSkin.xaml");
                    hlManager.SetCurrentTheme("Light");
                    foreach (var file in _files)
                    {
                        OnPropertyChanged(nameof(file.HighlightingDefinitions));
                        if (file != null)
                            file.OnAppThemeChanged(hlManager);
                    }
                    break;
                default:
                    break;
            }
        }

        public void Close(FileViewModel fileToClose)
        {
            if (fileToClose.IsDirty)
            {
                var res = MessageBox.Show(string.Format("Save changes for file '{0}'?", fileToClose.FileName), "AvalonDock Test App", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Cancel)
                    return;
                if (res == MessageBoxResult.Yes)
                {
                    Save(fileToClose);
                }
            }

            _files.Remove(fileToClose);
        }

        internal void Save(FileViewModel fileToSave, bool saveAsFlag = false)
        {
            string newTitle = string.Empty;

            if (fileToSave.FilePath == null || saveAsFlag)
            {
                var dlg = new SaveFileDialog();
                if (dlg.ShowDialog().GetValueOrDefault())
                {
                    fileToSave.FilePath = dlg.FileName;
                    newTitle = dlg.SafeFileName;
                }
            }
            if (fileToSave.FilePath == null)
            {
                return;
            }
            //File.WriteAllText(fileToSave.FilePath, fileToSave.TextContent);
            ActiveDocument.IsDirty = false;

            if (string.IsNullOrEmpty(newTitle)) return;
            ActiveDocument.Title = newTitle;
        }

        internal FileViewModel Open(string filepath)
        {
            var fileViewModel = _files.FirstOrDefault(fm => fm.FilePath == filepath);
            if (fileViewModel != null)
                return fileViewModel;

            fileViewModel = new FileViewModel(filepath);
            _files.Add(fileViewModel);
            return fileViewModel;
        }

        #region OpenCommand

        [RelayCommand]
        private void OnOpen(object parameter)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog().GetValueOrDefault())
            {
                var fileViewModel = Open(dlg.FileName);
                ActiveDocument = fileViewModel;
            }
        }

        #endregion OpenCommand

        #region NewCommand
        [RelayCommand]
        private void OnNew(object parameter)
        {
            _files.Add(new FileViewModel());
            ActiveDocument = _files.Last();
        }

        #endregion NewCommand

        #endregion methods
    }
}
