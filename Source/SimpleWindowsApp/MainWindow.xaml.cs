using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SimpleWindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string ServiceName = "SimpleWindowsService";
        public ServiceController ServiceController { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            ServiceController = new ServiceController();
            ServiceController.ServiceName = ServiceName;

            changeBtnState();
        }

        private void btnStartService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblError.Content = string.Empty;
                btnStartService.IsEnabled = false;

                ServiceController.Start();

                TimeSpan t = new TimeSpan(0, 0, 0, 20);
                ServiceController.WaitForStatus(ServiceControllerStatus.Running, t);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                lblError.Content = ex.Message;
            }

            changeBtnState();
        }

        private void btnStopService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblError.Content = string.Empty;
                btnStopService.IsEnabled = false;

                ServiceController.Stop();

                TimeSpan t = new TimeSpan(0, 0, 0, 20);
                ServiceController.WaitForStatus(ServiceControllerStatus.Stopped, t);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                lblError.Content = ex.Message;
            }

            changeBtnState();
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
            }
            changeBtnState();
        }

        private void changeBtnState()
        {
            btnStartService.IsEnabled = ServiceController.Status == ServiceControllerStatus.Stopped;
            btnStopService.IsEnabled = ServiceController.Status == ServiceControllerStatus.Running;
            btnRestartService.IsEnabled = ServiceController.Status == ServiceControllerStatus.Running;
        }
    }
}
