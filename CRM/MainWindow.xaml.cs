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

namespace CRM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void openLetters (object sender, RoutedEventArgs e)
        {
            if (incoming.Visibility == Visibility.Collapsed)
            {
                incoming.Visibility = Visibility.Visible;
                outgoing.Visibility = Visibility.Visible;
            }
            else
            {
                incoming.Visibility = Visibility.Collapsed;
                outgoing.Visibility = Visibility.Collapsed;
            }
        }
        private void openDesign(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Page_Documents(Classes.DocumentType.KD);
        }
        private void openIncoming(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Page_Documents(Classes.DocumentType.Incoming);
        }

        private void openOutgoing(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Page_Documents(Classes.DocumentType.Outgoing);
        }
    }
}
