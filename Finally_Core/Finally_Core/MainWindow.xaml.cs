using BLL.interfaces;
using BLL.models;
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

namespace Finally_Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IService<Product> service;
        private windowselltopeople windowsellpeople;
        public ObservableCollection<Product> Products;
        private WindowAddProduct windowAddProduct;
        int num;
        public MainWindow(IService<Product> serviceObj)
        {
            InitializeComponent();
            service = serviceObj;
            Products = new ObservableCollection<Product>();
            InitializedCollection();
        }

        public void InitializedCollection()
        {
            diskGrid.Opacity = 0;
            genreGrid.Opacity = 0;
            mainGrid.Opacity = 1;
            Products.Clear();
            foreach (var product in this.service.GetInfo())
            {
                Products.Add(product);
            }
            this.mainGrid.ItemsSource = Products;
        }

        private void addProductBt_Click(object sender, RoutedEventArgs e)
        {
            windowAddProduct = new WindowAddProduct();
            windowAddProduct.ShowDialog();
            if(windowAddProduct != null)
            {
                this.service.Add(windowAddProduct.ProductInsert);
                this.
                InitializedCollection();
            }
        }

        private void EditProductBt_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainGrid.SelectedItem != null && this.mainGrid.SelectedItem is Product)
            {
                int num = (this.mainGrid.SelectedItem as Product).Id;
                windowAddProduct = new WindowAddProduct();
                windowAddProduct.EditNameButton("Edit");
                windowAddProduct.ProductsChoice(this.mainGrid.SelectedItem as Product);
                windowAddProduct.ShowDialog();
                if (windowAddProduct.ProductInsert != null)
                {
                    windowAddProduct.ProductInsert.Id = num;
                    this.service.Edit(windowAddProduct.ProductInsert);
                    InitializedCollection();
                }
            }
        }

        private void ShowProductBt_Click(object sender, RoutedEventArgs e)
        {
            InitializedCollection();
        }
        private void deletteProductBt_Click(object sender, RoutedEventArgs e)
        {
            if(this.mainGrid.SelectedItem != null && this.mainGrid.SelectedItem is Product)
            {
                int num = (this.mainGrid.SelectedItem as Product).Id;
                windowAddProduct = new WindowAddProduct();
                windowAddProduct.EditNameButton("Delette");
                windowAddProduct.ProductsChoice(this.mainGrid.SelectedItem as Product);
                windowAddProduct.ShowDialog();
                if (windowAddProduct.ProductInsert != null)
                {
                    windowAddProduct.ProductInsert.Id = num;
                    this.service.Delete(windowAddProduct.ProductInsert);
                    InitializedCollection();
                }
            }
        }

        private void FindByNameBt_Click(object sender, RoutedEventArgs e)
        {
            if (this.findnamedisktb != null)
            {
                Products.Clear();
                foreach(var item in this.service.GetInfo())
                {
                    if(item.Name_Disk == this.findnamedisktb.Text)
                    {
                        Products.Add(item);
                    }
                }
                this.mainGrid.ItemsSource = Products;
            }
        }

        private void FindByAvtor_Click(object sender, RoutedEventArgs e)
        {
            if (this.findavtortb != null)
            {
                Products.Clear();
                foreach (var item in this.service.GetInfo())
                {
                    if (item.Name_Avtor == this.findavtortb.Text)
                    {
                        Products.Add(item);
                    }
                }
                this.mainGrid.ItemsSource = Products;
            }
        }

        private void FindByGenre_Click(object sender, RoutedEventArgs e)
        {
            if (this.findgenretb != null)
            {
                Products.Clear();
                foreach (var item in this.service.GetInfo())
                {
                    if (item.Genre == this.findgenretb.Text)
                    {
                        Products.Add(item);
                    }
                }
                this.mainGrid.ItemsSource = Products;
            }
        }

        private void Showdisk_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Opacity = 0;
            genreGrid.Opacity = 0;
            diskGrid.Opacity = 1;
            Products.Clear();
            foreach (var product in this.service.GetInfo())
            {
                Products.Add(product);
            }
            this.diskGrid.ItemsSource = Products;
        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            if(this.mainGrid.SelectedItem != null)
            {
                Product tempprod = (this.mainGrid.SelectedItem as Product);
                windowsellpeople = new windowselltopeople();
                windowsellpeople.ShowDialog();
                if(windowsellpeople.peopleInsert != null)
                {
                    Sold tempsold = new Sold()
                    {
                        dateSold = DateTime.Now,
                        People = new People
                        {
                            Name = windowsellpeople.peopleInsert.Name,
                            LastName = windowsellpeople.peopleInsert.LastName,
                            Phone = windowsellpeople.peopleInsert.Phone,
                            Birthday = windowsellpeople.peopleInsert.Birthday,
                        }
                    };
                    tempprod.Solds.Add(tempsold);
                    //tempprod.Nnows.Remove(tempprod.Nnows[tempprod.Nnows.Count]);
                    InitializedCollection();
                }
            }
        }

        private void shownew_Click(object sender, RoutedEventArgs e)
        {
            Products.Clear();
            foreach (var product in this.service.GetInfo())
            {
                if(product.Issue > Convert.ToDateTime("02.05.2025"))
                    Products.Add(product);
            }
            this.mainGrid.ItemsSource = Products;
        }

        private void fossale_Click(object sender, RoutedEventArgs e)
        {//////////////////////////////////////////
            Products.Clear();
            foreach (var product in this.service.GetInfo())
            {
                if (product.Number_Sound > 0)
                    Products.Add(product);
            }
            this.mainGrid.ItemsSource = Products;
        }

        private void genreday_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Opacity = 0;
            diskGrid.Opacity = 0;
            genreGrid.Opacity = 1;
            Products.Clear();
            foreach (var product in this.service.GetInfo())
            {
                if (product.Issue > Convert.ToDateTime("01.06.2025"))
                    Products.Add(product);
            }
            this.genreGrid.ItemsSource = Products;
        }

        private void genremonth_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Opacity = 0;
            diskGrid.Opacity = 0;
            genreGrid.Opacity = 1;
            Products.Clear();
            foreach (var product in this.service.GetInfo())
            {
                if (product.Issue > Convert.ToDateTime("01.05.2025"))
                    Products.Add(product);
            }
            this.genreGrid.ItemsSource = Products;
        }

        private void genreweek_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Opacity = 0;
            diskGrid.Opacity = 0;
            genreGrid.Opacity = 1;
            Products.Clear();
            foreach (var product in this.service.GetInfo())
            {
                if (product.Issue > Convert.ToDateTime("24.05.2025"))
                    Products.Add(product);
            }
            this.genreGrid.ItemsSource = Products;
        }

        private void genreYear_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Opacity = 0;
            diskGrid.Opacity = 0;
            genreGrid.Opacity = 1;
            Products.Clear();
            foreach (var product in this.service.GetInfo())
            {
                if (product.Issue > Convert.ToDateTime("02.06.2024"))
                    Products.Add(product);
            }
            this.genreGrid.ItemsSource = Products;
        }
    }
}