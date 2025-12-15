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

namespace Muhametshin41razmer
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        int currentNumRecords, AllNumRecords;
        public ProductPage(User user)
        {
            InitializeComponent();

            FIOTB.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            switch (user.UserRole)
            {
                case 1:
                    RoleTB.Text = "Клиент";break;
                case 2:
                    RoleTB.Text = "Менеджер"; break;
                case 3:
                    RoleTB.Text = "Администратор"; break;
            }

            var currentProducts = Muhametshin41Entities.GetContext().Product.ToList();

            ProductListView.ItemsSource = currentProducts;

            ComboType.SelectedIndex = 0;

            UpdateProducts();
        }

        private void UpdateProducts()
        {
            var currentProducts = Muhametshin41Entities.GetContext().Product.ToList();

            AllNumRecords = currentProducts.Count;

            if (ComboType.SelectedIndex == 0)
            {
                currentProducts = currentProducts.Where(p => ((p.ProductDiscountAmount) >= 0 && (p.ProductDiscountAmount) <= 100)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentProducts = currentProducts.Where(p => ((p.ProductDiscountAmount) >= 0 && (p.ProductDiscountAmount) < 10)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentProducts = currentProducts.Where(p => ((p.ProductDiscountAmount) >= 10 && (p.ProductDiscountAmount) < 15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentProducts = currentProducts.Where(p => ((p.ProductDiscountAmount) >= 15 && (p.ProductDiscountAmount) <= 100)).ToList();
            }

            currentProducts = currentProducts.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            ProductListView.ItemsSource = currentProducts.ToList();

            if (RButtonDown.IsChecked.Value)
            {
                currentProducts = currentProducts.OrderByDescending(p => p.ProductCost).ToList();
            }

            if (RButtonUp.IsChecked.Value)
            {
                currentProducts = currentProducts.OrderBy(p => p.ProductCost).ToList();
            }

            currentNumRecords = currentProducts.Count;
            ProductListView.ItemsSource = currentProducts;
            TBAllRecords.Text = currentNumRecords.ToString() + " из " + AllNumRecords.ToString();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProducts();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProducts();
        }
    }

    /*private void Button_Click(object sender, RoutedEventArgs e)
    {
        Manager.MainFrame.Navigate(new AddEditPage());
    }*/
}