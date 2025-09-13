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

namespace Project_PF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Button button = new Button() { Width = 200, Height = 80, Content = "New Button Click"};
            //button.Click += Button_Click;
            //this.ma.Children.Add(button);
            
        }

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            this.infoTb.Text = "Hello world";
            //MessageBox.Show("Hello world");
        }
    }
}
