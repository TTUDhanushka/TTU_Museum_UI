using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Yumi_Interface
{
    /// <summary>
    /// Interaction logic for counting.xaml
    /// </summary>
    public partial class counting : Page, INotifyPropertyChanged
    {


        public counting()
        {
            InitializeComponent();
            this.DataContext = this;
            MainWindow.onDataTransfer += mainWindow_onDataTransfer;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _currentPos;

        public string CurrentPos
        {
            get { return _currentPos; }

            set
            {
                _currentPos = value;
                OnPropertyChanged("CurrentPos");
            }
        }

        private void mainWindow_onDataTransfer(string data)
        {
            int receiveNum = 0;
            int totalSequences = 479001600;
            int.TryParse(data, out receiveNum);

            int remainingSeq = totalSequences - receiveNum;

            string strRemainingPos = remainingSeq.ToString("000 000 000");
            string strCurrentPos = receiveNum.ToString("000 000 000");

            CurrentPos = strCurrentPos;
            RemainingPos = strRemainingPos;
        }

        private string _remainingPos;

        public string RemainingPos
        {
            get
            {
                return _remainingPos;
            }

            set
            {
                _remainingPos = value;
                OnPropertyChanged("RemainingPos");
            }
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            CurrentPos = "000 000 001";
            RemainingPos = "479 001 599";
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            mainWindow.RequestData = "00";
        }
    }
}
