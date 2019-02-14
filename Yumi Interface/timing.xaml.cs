using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yumi_Interface
{
    /// <summary>
    /// Interaction logic for timing.xaml
    /// </summary>
    public partial class timing : Page
    {
        public timing()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private string _elapsedTime;

        public string ElapsedTime
        {
            get { return _elapsedTime; }

            set
            {
                _elapsedTime = value;
            }
        }

        private string _remainingTime;

        public string RemainingTime
        {
            get
            {
                return _remainingTime;
            }

            set
            {
                _remainingTime = value;
            }
        }
    }
}
