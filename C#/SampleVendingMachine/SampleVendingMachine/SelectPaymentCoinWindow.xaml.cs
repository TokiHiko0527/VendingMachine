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
using System.Windows.Shapes;

namespace SampleVendingMachine
{
    /// <summary>
    /// SelectPaymentCoinWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectPaymentCoinWindow : Window
    {
        public int Payment { get; private set; }

        public SelectPaymentCoinWindow()
        {
            InitializeComponent();
            Payment = 0;
        }

        private void SelectPaymentCoin_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse target = sender as Ellipse;
            if (target == null) { return; }

            switch(target.Name)
            {
                case "TEN":
                    Payment += 10;
                    break;

                case "FIFTY":
                    Payment += 50;
                    break;

                case "FIFTY_CENTER":
                    Payment += 50;
                    break;

                case "HANDRED":
                    Payment += 100;
                    break;

                case "FIVE_HANDRED":
                    Payment += 500;
                    break;

                default:
                    Payment += 0;
                    break;
            }

            this.Close();            
        }

        public void ShowDialog(int payment)
        {
            Payment = payment;
            this.ShowDialog();
        }
    }
}
