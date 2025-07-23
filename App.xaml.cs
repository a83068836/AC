using AC.ViewModels;
using AC.Views;
using ServiceLocator;
using System.Configuration;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace AC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainView _mainWindow = null;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 注册编码提供程序以支持 GB2312 编码
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        static App()
        {
            ServiceInjector.InjectServices();
        }
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            Application.Current.MainWindow = _mainWindow = new MainView();
            MainWindow.DataContext = Workspace.This;
            if (MainWindow != null && Workspace.This != null)
            {
                Dispatcher.InvokeAsync(() => MainWindow.Show(), DispatcherPriority.ApplicationIdle);
            }
        }
        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //MessageBox.Show("发生错误！请联系支持部门。" + Environment.NewLine + e.Exception.Message);
            MessageBox.Show("Error encountered! Details: " + Environment.NewLine +
                   "Exception: " + e.Exception.GetType().Name + Environment.NewLine +
                   "Message: " + e.Exception.Message + Environment.NewLine +
                   "Stack Trace: " + e.Exception.StackTrace);
            Shutdown(1);
            e.Handled = true;
        }
    }

}
