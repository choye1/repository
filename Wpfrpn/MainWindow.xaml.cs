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
            if (!string.IsNullOrEmpty(tbScale.Text)) 
            { 
                Graph1.Plot.ScaleFactor = Convert.ToDouble(tbScale.Text);  // Вот тут масштаб настраивается
            } 

            Graph1.Refresh();
            double valueVar; ;
            string[] input = { tbInput.Text, tbStartClc.Text, tbEndClc.Text, tbStepClc.Text};

            if (string.IsNullOrEmpty(tbValueVar.Text))
            {
                valueVar = 1;
            }

            else
            {
                valueVar = Convert.ToDouble(tbValueVar.Text);
            }

            List<double> dataX = new List<double> { };
            List <double> dataY = new List<double> { };

            List < Token > a = Program.Logic(input, dataX, dataY);
            lblOut. Content = Program.GetResultString(a);
            lblRes.Content = Program.GetResultDouble(a, valueVar);
            Graph1.Plot.Add.Scatter(dataX, dataY);
            Graph1.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Graph1.Plot.Clear();
            Graph1.Refresh();
        }
    }
}