using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleWindowsService
{
    public partial class Service1 : ServiceBase
    {
        public const int Port = 3000;
        public ServiceHost serviceHost = null;

        private static TcpListener tcpListener;
        private static Thread listenThread;
        
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, Port);
                listenThread = new Thread(new ThreadStart(ListenForClients));
                listenThread.Start();

                if (serviceHost != null)
                {
                    serviceHost.Close();
                }

                // Create a ServiceHost for the SettingsService type
                serviceHost = new ServiceHost(typeof(SettingsService));
                serviceHost.Open();

                Settings.OutputMessage = "Hello";
            }
            catch (Exception ex)
            {
            }
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
            tcpListener.Stop();
            listenThread.Abort();
        }

        private static void ListenForClients()
        {
            tcpListener.Start();

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();

                //create a thread to handle communication with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private static void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                //Echo message
                Send(clientStream, Settings.OutputMessage);
                Send(clientStream, "");
                Thread.Sleep(5000);
            }

            tcpClient.Close();
        }

        public static void Send(NetworkStream clientStream, string packet)
        {
            var buffer = Convert(packet + Environment.NewLine);
            var empty = Convert("");
            if (clientStream != null)
            {
                clientStream.Write(buffer, 0, buffer.Length);
            }

        }
        private static byte[] Convert(string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }

    }
}
