using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleWindowsApp.SimpleWindowsService;

namespace SimpleWindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string ServiceName = "SimpleWindowsService";
        public ServiceController ServiceController { get; set; }
        public ManagementEventWatcher EventWatcher { get; set; }
        public SettingsServiceClient WindowsServiceReference { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                EventQuery eventQuery = new EventQuery();
                eventQuery.QueryString = string.Format("SELECT * FROM __InstanceModificationEvent within 2 WHERE targetinstance isa 'Win32_Service' and targetinstance.name = '{0}'", ServiceName);
                EventWatcher = new ManagementEventWatcher(eventQuery);
                EventWatcher.Options.Timeout = new TimeSpan(1, 0, 0);
                EventWatcher.EventArrived += new EventArrivedEventHandler(eventWatcher_EventArrived);
                EventWatcher.Start();

                ServiceController = new ServiceController();
                ServiceController.ServiceName = ServiceName;

                lblServiceStatus.Content = ServiceController.Status.ToString();

                if (ServiceController.Status == ServiceControllerStatus.Running)
                {
                    WindowsServiceReference = new SettingsServiceClient();
                    lblCurrentOutputMessage.Content = WindowsServiceReference.GetOutputMessage();
                }

                changeBtnState();
            }
            catch (Exception ex)
            {
                lblError.Content = ex.Message;

                btnStartService.IsEnabled = false;
                btnStopService.IsEnabled = false;
                btnRestartService.IsEnabled = false;
                txtNewOutputMessage.IsEnabled = false;
                btnChangeOutputMessage.IsEnabled = false;
            }
        }

        void eventWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = ((ManagementBaseObject)e.NewEvent["targetinstance"]);
            PropertyData state = targetInstance.Properties["state"];

            this.Dispatcher.Invoke((Action)(() =>
            {
                lblServiceStatus.Content = state.Value.ToString();
                btnStartService.IsEnabled = state.Value.ToString() == ServiceControllerStatus.Stopped.ToString();
                btnStopService.IsEnabled = state.Value.ToString() == ServiceControllerStatus.Running.ToString();
                btnRestartService.IsEnabled = state.Value.ToString() == ServiceControllerStatus.Running.ToString();
                txtNewOutputMessage.IsEnabled = state.Value.ToString() == ServiceControllerStatus.Running.ToString();
                btnChangeOutputMessage.IsEnabled = state.Value.ToString() == ServiceControllerStatus.Running.ToString();

                if (state.Value.ToString() == ServiceControllerStatus.Running.ToString())
                {
                    WindowsServiceReference = new SettingsServiceClient();
                    lblCurrentOutputMessage.Content = WindowsServiceReference.GetOutputMessage();

                    ServiceController = new ServiceController();
                    ServiceController.ServiceName = ServiceName;
                }
                else
                {
                    lblCurrentOutputMessage.Content = string.Empty;
                }
            }));
        }

        private void btnStartService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblError.Content = string.Empty;
                btnStartService.IsEnabled = false;

                ServiceController.Start();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                lblError.Content = ex.Message;
                changeBtnState();
            }
        }

        private void btnStopService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblError.Content = string.Empty;
                btnStopService.IsEnabled = false;

                ServiceController.Stop();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                lblError.Content = ex.Message;
                changeBtnState();
            }
        }

        private void btnRestartService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblError.Content = string.Empty;
                btnRestartService.IsEnabled = false;

                ServiceController.Stop();
                TimeSpan t = new TimeSpan(0, 0, 0, 20);
                ServiceController.WaitForStatus(ServiceControllerStatus.Stopped, t);
                ServiceController.Start();
                ServiceController.WaitForStatus(ServiceControllerStatus.Running, t);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                lblError.Content = ex.Message;
                changeBtnState();
            }
        }

        private void changeBtnState()
        {
            btnStartService.IsEnabled = ServiceController.Status == ServiceControllerStatus.Stopped;
            btnStopService.IsEnabled = ServiceController.Status == ServiceControllerStatus.Running;
            btnRestartService.IsEnabled = ServiceController.Status == ServiceControllerStatus.Running;
        }

        private void btnChangeOutputMessage_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceController.Status == ServiceControllerStatus.Running)
            {
                WindowsServiceReference.SetOutputMessage(txtNewOutputMessage.Text);
                lblCurrentOutputMessage.Content = WindowsServiceReference.GetOutputMessage();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EventWatcher.Stop();
            EventWatcher.Dispose();
            EventWatcher = null;
        }
    }
}
