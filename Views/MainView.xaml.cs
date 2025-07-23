using AC.ViewModels;
using AvalonDock;
using AvalonDock.Layout;
using AvalonDock.Layout.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AC.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = Workspace.This;
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.Unloaded += new RoutedEventHandler(MainWindow_Unloaded);
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var serializer = new AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            //serializer.LayoutSerializationCallback += (s, args) =>
            //{
            //    args.Content = args.Content;
            //};

            //if (File.Exists(@".\AvalonDock.config"))
            //    serializer.Deserialize(@".\AvalonDock.config");
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.LayoutSerializationCallback += (s, args) =>
            {
                if (args.Model.ContentId == ExplorerViewModel.ToolContentId)
                    args.Content = Workspace.This.Explorer;
                else if (args.Model.ContentId == PropertiesViewModel.ToolContentId)
                    args.Content = Workspace.This.Props;
                else if (args.Model.ContentId == ErrorViewModel.ToolContentId)
                    args.Content = Workspace.This.Errors;
                else if (args.Model.ContentId == OutputViewModel.ToolContentId)
                    args.Content = Workspace.This.Output;
                else if (args.Model.ContentId == GitChangesViewModel.ToolContentId)
                    args.Content = Workspace.This.Git;
                else if (args.Model.ContentId == ToolboxViewModel.ToolContentId)
                    args.Content = Workspace.This.Toolbox;
                else
                {
                    args.Content = Workspace.This.Open(args.Model.ContentId);

                    if (args.Content == null)
                        args.Cancel = true;
                }
            };

            if (File.Exists(@".\AvalonDock.config"))
                layoutSerializer.Deserialize(@".\AvalonDock.config");
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            var serializer = new AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.Serialize(@".\AvalonDock.config");
            Application.Current.Shutdown();
        }
        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void RestoreDownClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            MainWindow_Unloaded(null,null);
            Close();
        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            SetCaptionHeight();
        }

        private void HeaderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetCaptionHeight();
        }

        private void SetCaptionHeight()
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    chrome.CaptionHeight = header.ActualHeight + BorderThickness.Top - chrome.ResizeBorderThickness.Top;
                    break;
                case WindowState.Maximized:
                    chrome.CaptionHeight = header.ActualHeight - BorderThickness.Top;
                    break;
            }
        }
    }
}
