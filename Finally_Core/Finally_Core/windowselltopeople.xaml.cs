using BLL.models;
using DLL.models;
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
using System.Windows.Shapes;

namespace Finally_Core
{
    /// <summary>
    /// Interaction logic for windowselltopeople.xaml
    /// </summary>
    public partial class windowselltopeople : Window
    {
        public People peopleInsert {  get; set; }
        public windowselltopeople()
        {
            InitializeComponent();
        }

        private void sellbt_Click(object sender, RoutedEventArgs e)
        {
            if (PewopleIsValid())
            {
                peopleInsert = new People()
                {
                    Name = this.nametb.Text,
                    LastName = this.lastnametb.Text,
                    Phone = this.phonetb.Text,
                    Birthday = Convert.ToDateTime(this.birthdaytb.SelectedDate)
                };
                this.Close();
            }
        }

        private bool PewopleIsValid()
        {
            Clears();
            if (String.IsNullOrEmpty(this.nametb.Text) || String.IsNullOrWhiteSpace(this.nametb.Text))
            {
                this.nametb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.lastnametb.Text) || String.IsNullOrWhiteSpace(this.lastnametb.Text))
            {
                this.lastnametb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.birthdaytb.Text) || String.IsNullOrWhiteSpace(this.birthdaytb.Text))
            {
                this.birthdaytb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.phonetb.Text) || String.IsNullOrWhiteSpace(this.phonetb.Text))
            {
                this.phonetb.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }

        private void Clears()
        {
            this.nametb.BorderBrush = Brushes.LightCyan;
            this.lastnametb.BorderBrush = Brushes.LightCyan;
            this.birthdaytb.BorderBrush = Brushes.LightCyan;
            this.phonetb.BorderBrush = Brushes.LightCyan;
        }
    }
}
