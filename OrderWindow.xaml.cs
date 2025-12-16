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

namespace Muhametshin41razmer
{
    public partial class OrderWindow : Window
    {
        List<OrderProduct> selectedOrderProducts = new List<OrderProduct>();
        List<Product> selectedProducts = new List<Product>();
        private Order currentOrder = new Order();
        private OrderProduct currentOrderProduct = new OrderProduct();

        public OrderWindow(List<OrderProduct> selectedOrderProducts, List<Product> selectedProducts, string FIO)
        {
            InitializeComponent();
            var currentPickups = Muhametshin41Entities.GetContext().PickUpPoint.ToList();
            PickupCombo.ItemsSource = currentPickups;

            ClientTB.Text = FIO;
            TBOrderID.Text = selectedOrderProducts.First().OrderID.ToString();

            ShoeListView.ItemsSource = selectedProducts;

            foreach (Product p in selectedProducts)
            {
                p.ProductQuantityInStock = 1;
                foreach (OrderProduct q in selectedOrderProducts)
                {
                    if (p.ProductArticleNumber == q.ProductArticleNumber)
                    {
                        p.ProductQuantityInStock = q.Count;
                    }
                }
            }
            this.selectedOrderProducts = selectedOrderProducts;
            this.selectedProducts = selectedProducts;
            OrderDP.Text = DateTime.Now.ToString();
            SetDeliveryDate();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            currentOrder.OrderClientID = ClientTB.Text;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ShoeListView.SelectedIndex >= 0)
            {
                var prod = ShoeListView.SelectedItem as Product;
                selectedProducts.Add(prod);

                var newOrderProd = new OrderProduct();
                newOrderProd.OrderID = newOrderID;

                newOrderProd.ProductArticleNumber = prod.ProductArticleNumber;
                newOrderProd.Count = 1;

                var selOP = selectedOrderProducts.Where(p => Equals(p.ProductArticleNumber, prod.ProductArticleNumber));
                //MessageBox.Show(selOP.Count().ToString());
                if (selOP.Count() == 0)
                {
                    //MessageBox.Show(newOrderProd.OrderID.ToString() + " " + newOrderProd.ProductArticleNumber.ToString() + " " + newOrderProd.Quantity.Tostring());

                    selectedOrderProducts.Add(newOrderProd);
                    //MessageBox.Show("кол-во в selecteOP = " + selectedOrderProducts.Count().ToString());
                }
                else
                {
                    foreach(OrderProduct p in selectedOrderProducts)
                    {
                        if (p.ProductArticleNumber == prod.ProductArticleNumber)
                            p.Count++;
                        //MessageBox.Show("кол-во = " + p.Quantity.ToString());
                    }
                }

                OrderBtn.Visibility = Visibility.Visible;
                ShoeListView.SelectedIndex = -1;
            }
        }

    }
}