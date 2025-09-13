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

namespace Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int CorrectCounter = 0;
        public List<bool> list = new List<bool>()
        {
            false, false, false, false, false, false, false, false,
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WrapPanel_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == true)
                {
                    CorrectCounter++;
                }
            }
            string str = "Correct uswer: " + CorrectCounter.ToString();
            MessageBox.Show(str, " ", MessageBoxButton.OK, MessageBoxImage.Information);
            CorrectCounter = 0;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            RadioButton stack = sender as RadioButton;
            int count = (int)stack.GroupName[2] - '0';
            list[count - 1] = true;
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            RadioButton stack = sender as RadioButton;
            int count = (int)stack.GroupName[2] - '0';
            list[count - 1] = false;
        }
    }
}
