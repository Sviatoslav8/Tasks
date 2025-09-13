using BLL.interfaces;
using BLL.Models;
using System.Collections.ObjectModel;
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

namespace E_Framework_Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IsServices<People> service;
        public ObservableCollection<People> Peoples;
        public MainWindow(IsServices<People> serviceObj)
        {
            InitializeComponent();
            service = serviceObj;
            Peoples = new ObservableCollection<People>();
        }

        private void Run_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.popLink.IsOpen = true;
        }

        private void registerbt_Click(object sender, RoutedEventArgs e)
        {
            if (PeopleIsValid())
            {
                People temp = new People()
                {
                    Name = this.nametb.Text,
                    LastName = this.lastnametb.Text,
                    Phone = Int32.Parse(this.phonetb.Text),
                    Birthday = Convert.ToDateTime(this.birthdaytb.SelectedDate),
                };
                service.Add(temp);
            }
        }
        private void ClearBorder()
        {
            this.nametb.BorderBrush = Brushes.Blue;
            this.lastnametb.BorderBrush = Brushes.Blue;
            this.phonetb.BorderBrush = Brushes.Blue;
            this.birthdaytb.BorderBrush = Brushes.Blue;
        }
        private bool PeopleIsValid()
        {
            ClearBorder();
            if(String.IsNullOrEmpty(this.nametb.Text) || String.IsNullOrWhiteSpace(this.nametb.Text))
            {
                this.nametb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.lastnametb.Text) || String.IsNullOrWhiteSpace(this.lastnametb.Text))
            {
                this.lastnametb.BorderBrush = Brushes.Red;
                return false;
            }
            int phone;
            if (!Int32.TryParse(this.phonetb.Text, out phone) || phone <= 0)
            {
                this.phonetb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.birthdaytb.Text) || String.IsNullOrWhiteSpace(this.birthdaytb.Text))
            {
                this.birthdaytb.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }
    }
}