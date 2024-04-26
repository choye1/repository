using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RPNLogic;
using rpn_v2;
using ScottPlot;

namespace Wpfrpn
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<double> dataX = new List<double> { 1, 2, 3, 4, 5 };
            List<double> dataY = new List<double> { 1, 4, 9, 16, 25 };
            Graph1.Plot.Add.Scatter(dataX, dataY);
            Graph1.Refresh();
            double valueVar; ;
            string input = tbInput.Text;
            if (string.IsNullOrEmpty(tbValueVar.Text))
            {
                valueVar = 1;
                dataX.Add(6);
                dataY.Add(30);
                Graph1.Plot.ScaleFactor = 2; // Вот тут масштаб настраивается
                Graph1.Refresh();
            }

            else
            {
                valueVar = Convert.ToDouble(tbValueVar.Text);
            }

            
            List < Token > a = Program.Logic(input);
            lblOut. Content = Program.GetResultString(a);
            lblRes.Content = Program.GetResultDouble(a, valueVar);
        }
    }
}