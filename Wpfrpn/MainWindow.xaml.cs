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
using RPN;

namespace Wpfrpn
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new Program();
            Program.Main([]) ;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}