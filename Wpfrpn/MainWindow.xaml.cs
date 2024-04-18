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
            Plot Graph = new();
            double[] dataX = { 1, 2, 3, 4, 5 };
            double[] dataY = { 1, 4, 9, 16, 25 };
            Graph.Add.Scatter(dataX, dataY);
            InitializeComponent();


            Graph.SavePng("demo.png", 400, 300);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            double valueVar; ;
            string input = tbInput.Text;
            if (string.IsNullOrEmpty(tbValueVar.Text))
            {
                valueVar = 1;
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