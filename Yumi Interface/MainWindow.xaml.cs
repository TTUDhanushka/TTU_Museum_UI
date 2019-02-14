using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Net;
using System.Net.Sockets;

namespace Yumi_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string strGet = "Get";
        string msg;
        byte[] msgbuffer;
        byte[] buffer;
        int rec;
        string Rec;
        string output;

        Socket listner;

        // Communication settings
        string ipAddress = "192.168.125.1";
        int port = 55555;

        public delegate void SendRobotData(string data);
        public static event SendRobotData onDataTransfer;

        private Thread backgroundThreadTCP;
        private Thread backgroundThreadinputdata;

        public MainWindow()
        {
            InitializeComponent();

            //Start background thread for communication with yumi.
            backgroundThreadTCP = new Thread(new ThreadStart(this.Connection));
            backgroundThreadTCP.IsBackground = true;
            backgroundThreadTCP.Start();

            mainFrame.Navigate(new counting());

        }

        
        /// <summary>
        /// This function runs only once at the beginning. In the case of broken communication won't be able to re-establish.
        /// </summary>
        public void Connection()
        {
            listner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);

            try
            {
                listner.Connect(ipEndPoint);
                //MessageBox.Show("Connected");
            }
            catch (SocketException ex)
            {
                //MessageBox.Show(ex.ToString());
                Thread.Sleep(1000);
                return;
            }


            while (true) {
                msgbuffer = new byte[255];
                msgbuffer = Encoding.Default.GetBytes(strGet);

                int sendDataCount = listner.Send(msgbuffer);

                buffer = new byte[255];
                rec = listner.Receive(buffer, 0, buffer.Length, 0);
                Array.Resize(ref buffer, rec);
                Rec = Encoding.Default.GetString(buffer);

                // Invoking UI transfer.
                Dispatcher.BeginInvoke(new Action(() =>
                            {
                                onDataTransfer(Rec);
                            }));
            }
        }

        private string _requestData;

        public string RequestData
        {
            get { return _requestData; }

            set
            {
                _requestData = value;
            }
        }
    }
}
