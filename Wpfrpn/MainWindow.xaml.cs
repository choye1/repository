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
using ScottPlot.Plottables;
using System.Windows.Controls.Primitives;

namespace Wpfrpn
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtStart(object sender, RoutedEventArgs e)
        {
            double valueVar;
            string[] input = {tbInput.Text, tbStartClc.Text, tbEndClc.Text, tbStepClc.Text};
            for (int i = 0; i < input.Length; i++) 
            {
                if (input[i] == "")
                {
                    input[i] = "1";
                }
            }

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

            List < Token > RPN = Program.Logic(input, dataX, dataY);
            lblOutRPN.Content = Program.GetResultString(RPN);
            lblOutRes.Content = Program.GetResultDouble(RPN, valueVar);
            Graph1.Plot.Add.Scatter(dataX, dataY);
            Graph1.Refresh();
        }

        private void BtnClearPlot(object sender, RoutedEventArgs e)
        {
            Graph1.Plot.Clear();
            Graph1.Refresh();
        }

        private void SliderScale(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Graph1.Plot.Axes.SetLimitsX(1, Convert.ToDouble(slScale.Value));
            Graph1.Plot.Axes.SetLimitsY(1, Convert.ToDouble(slScale.Value));
            Graph1.Refresh();
        }
        private void Slider_Ox(object sender, RoutedPropertyChangedEventArgs<double> e) 
        {
            double visibleSpan = 20;
            double fraction = (double)sliderOX.Value / sliderOX.Maximum;
            double scrollableSpan = Graph1.Plot.Grid.XAxis.Width * Graph1.Plot.Grid.YAxis.Max - visibleSpan;
            double xMin = fraction * scrollableSpan;
            double xMax = xMin + visibleSpan;
            Graph1.Plot.Axes.SetLimitsX(xMin, xMax);
            Graph1.Refresh();
        }
        private void Slider_Oy(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double visibleSpan = 20;
            double fraction = (double)sliderOY.Value / sliderOY.Maximum;
            double scrollableSpan = Graph1.Plot.Grid.YAxis.Height * Graph1.Plot.Grid.XAxis.Max - visibleSpan;
            double yMin = fraction * scrollableSpan;
            double yMax = yMin + visibleSpan;
            Graph1.Plot.Axes.SetLimitsY(yMin, yMax);
            Graph1.Refresh();
        }
    }
}
