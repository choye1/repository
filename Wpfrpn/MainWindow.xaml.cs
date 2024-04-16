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
            double valueVar;
            string input = tbInput.Text;
            if (string.IsNullOrEmpty(tbValueVar.Text)) valueVar = 1;
            else valueVar = Convert.ToDouble(tbValueVar.Text);

            List <Token> rpnExpression = Program.Logic(input);
            lblOutExpression.Content = Program.GetResultString(rpnExpression);
            lblResultExpression.Content = Program.GetResultDouble(rpnExpression, valueVar);
        }
    }
}