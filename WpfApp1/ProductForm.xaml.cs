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
using Microsoft.EntityFrameworkCore;
using WpfApp1.Data;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ProductForm.xaml
    /// </summary>
    public partial class ProductForm : Window
    {

        private Role userRole;
        public ProductForm(Role role)
        {
            InitializeComponent();
            userRole = role;
            LoadProducts();
        }
        private void LoadProducts()
        {
            using (var context = new TestContext())
            {

                ProductsItemsControl.ItemsSource = context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Manufacturer)
                    .Include(p => p.Supplier)
                    .ToList();
            }
        }


    }
}
