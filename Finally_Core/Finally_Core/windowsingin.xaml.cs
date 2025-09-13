using BLL.interfaces;
using BLL.models;
using DLL;
using DLL.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for windowsingin.xaml
    /// </summary>
    public partial class windowsingin : Window
    {
        public MainWindow mainwindow;
        private ShopContext shopContext;
        private People people;
        public windowsingin()
        {
            InitializeComponent();
        }

        private void registerbt_Click(object sender, RoutedEventArgs e)
        {
            people = new People()
            {
                Name = this.nametb.Text,
                LastName = this.lastnametb.Text,
                Phone = this.phonetb.Text,
                Birthday = Convert.ToDateTime(this.birthdaytb.SelectedDate)
            };
           // mainwindow = new MainWindow();
            mainwindow.ShowDialog();
            this.shopContext.Add(people);
            this.shopContext.SaveChanges();
        }

        private void signintb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void adminbt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
