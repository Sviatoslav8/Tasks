using BLL.models;
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
    /// Interaction logic for WindowAddProduct.xaml
    /// </summary>
    public partial class WindowAddProduct : Window
    {
        public Product ProductInsert { get; set; }
        public WindowAddProduct()
        {
            InitializeComponent();
        }
        public void ProductsChoice(Product produtc)
        {
            this.diskTb.Text = produtc.Name_Disk;
            this.avtorTb.Text = produtc.Name_Avtor;
            this.collectiveTb.Text = produtc.Name_Collective;
            this.priceTb.Text = produtc.Price.ToString();
            this.costTb.Text = produtc.Cost.ToString();
            this.startDateTb.SelectedDate = produtc.Issue;
            this.genreTb.Text = produtc.Genre;
            this.numberSound.Text = produtc.Number_Sound.ToString();
        }

        private void AddProdyctBt_Click(object sender, RoutedEventArgs e)
        {
            if (ProductIsValid())
            {
                ProductInsert = new Product()
                {
                    Name_Disk = this.diskTb.Text,
                    Name_Avtor = this.avtorTb.Text,
                    Name_Collective = this.collectiveTb.Text,
                    Genre = this.genreTb.Text,
                    Price = Int32.Parse(this.priceTb.Text),
                    Cost = Int32.Parse(this.costTb.Text),
                    Number_Sound = Int32.Parse(this.numberSound.Text),
                    Issue = Convert.ToDateTime(this.startDateTb.SelectedDate),
                };
                this.Close();
            }
        }
        public void EditNameButton(string newName)
        {
            this.AddProdyctBt.Content = newName;
        }
        private void ClearBorder()
        {
            this.diskTb.BorderBrush = Brushes.LightCyan;
            this.collectiveTb.BorderBrush= Brushes.LightCyan;
            this.avtorTb.BorderBrush = Brushes.LightCyan;
            this.genreTb.BorderBrush = Brushes.LightCyan;
            this.numberSound.BorderBrush = Brushes.LightCyan;
            this.startDateTb.BorderBrush = Brushes.LightCyan;
            this.priceTb.BorderBrush = Brushes.LightCyan;
            this.costTb.BorderBrush = Brushes.LightCyan;
        }

        private bool ProductIsValid()
        {
            ClearBorder();
            if (String.IsNullOrEmpty(this.diskTb.Text) || String.IsNullOrWhiteSpace(this.diskTb.Text))
            {
                this.diskTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.collectiveTb.Text) || String.IsNullOrWhiteSpace(this.collectiveTb.Text))
            {
                this.collectiveTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.avtorTb.Text) || String.IsNullOrWhiteSpace(this.avtorTb.Text))
            {
                this.avtorTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.genreTb.Text) || String.IsNullOrWhiteSpace(this.genreTb.Text))
            {
                this.genreTb.BorderBrush = Brushes.Red;
                return false;
            }
            int Number;
            if (!Int32.TryParse(this.numberSound.Text, out Number) || Number <= 0)
            {
                this.numberSound.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.startDateTb.Text) || String.IsNullOrWhiteSpace(this.startDateTb.Text))
            {
                this.startDateTb.BorderBrush = Brushes.Red;
                return false;
            }
            int Price;
            if (!Int32.TryParse(this.priceTb.Text, out Price) || Price <= 0)
            {
                this.priceTb.BorderBrush = Brushes.Red;
                return false;
            }
            int Cost;
            if (!Int32.TryParse(this.costTb.Text, out Cost) || Cost <= 0)
            {
                this.costTb.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }
    }
}
