using E_Framework.Context;
using E_Framework.Models;
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

namespace E_Framework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CarContext carcontext;
        public MainWindow()
        {
            InitializeComponent();
            carcontext = new CarContext("name = Info_car");
        }

        private void addCarBt_Click(object sender, RoutedEventArgs e)
        {
            if (CarIsValid())
            {
                Car car = new Car()
                {
                    Name = this.nameTb.Text,
                    Model = this.modelTb.Text,
                    Color = this.colorTb.Text,
                    Hp = Int32.Parse(this.hpTb.Text),
                    Engine = float.Parse(this.engineTb.Text),
                    Country = this.countryTb.Text,
                    Price = Int32.Parse(this.priceTb.Text),
                    Year = Int32.Parse(this.yearTb.Text),
                    Acceleration = float.Parse(this.accelerationTb.Text),
                    Weight = float.Parse(this.weightTb.Text),
                };
                this.carcontext.Cars.Add(car);
                this.carcontext.SaveChanges();
            };
        }

        private void showCarBt_Click(object sender, RoutedEventArgs e)
        {
            this.mainGrid.ItemsSource = this.carcontext.Cars.ToList();
        }

        private void deleteCarBt_Click(object sender, RoutedEventArgs e)
        {
            this.carcontext.Cars.Remove(this.mainGrid.SelectedItem as Car);
            this.carcontext.SaveChanges();
        }
        private bool CarIsValid()
        {
            ClearBorders();
            if (String.IsNullOrEmpty(this.nameTb.Text) || String.IsNullOrWhiteSpace(this.nameTb.Text))
            {
                this.nameTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.modelTb.Text) || String.IsNullOrWhiteSpace(this.modelTb.Text))
            {
                this.modelTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.colorTb.Text) || String.IsNullOrWhiteSpace(this.colorTb.Text))
            {
                this.colorTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.hpTb.Text) || String.IsNullOrWhiteSpace(this.hpTb.Text))
            {
                this.hpTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.engineTb.Text) || String.IsNullOrWhiteSpace(this.engineTb.Text))
            {
                this.engineTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.countryTb.Text) || String.IsNullOrWhiteSpace(this.countryTb.Text))
            {
                this.countryTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.priceTb.Text) || String.IsNullOrWhiteSpace(this.priceTb.Text))
            {
                this.priceTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.yearTb.Text) || String.IsNullOrWhiteSpace(this.yearTb.Text))
            {
                this.yearTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.weightTb.Text) || String.IsNullOrWhiteSpace(this.weightTb.Text))
            {
                this.weightTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.accelerationTb.Text) || String.IsNullOrWhiteSpace(this.accelerationTb.Text))
            {
                this.accelerationTb.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }
        private void ClearBorders()
        {
            this.nameTb.BorderBrush = Brushes.LightCyan;
            this.modelTb.BorderBrush = Brushes.LightCyan;
            this.colorTb.BorderBrush = Brushes.LightCyan;
            this.hpTb.BorderBrush = Brushes.LightCyan;
            this.engineTb.BorderBrush = Brushes.LightCyan;
            this.countryTb.BorderBrush = Brushes.LightCyan;
            this.priceTb.BorderBrush = Brushes.LightCyan;
            this.yearTb.BorderBrush = Brushes.LightCyan;
            this.accelerationTb.BorderBrush = Brushes.LightCyan;
            this.weightTb.BorderBrush = Brushes.LightCyan;
        }
    }
}
