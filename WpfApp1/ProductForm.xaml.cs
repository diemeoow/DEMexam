using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
        private bool _isAscendingSort = false;
        public ProductForm(Role role)
        {
            InitializeComponent();
            userRole = role;
            LoadSuppliers();
            LoadProducts();
        }
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            var tovarWindow = new TovarWindow(product);
            tovarWindow.ShowDialog();
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
        private void LoadSuppliers()
        {
            using (var context = new TestContext())
            {
                List<Supplier> allSuppliers = new List<Supplier>();
                allSuppliers = context.Suppliers.ToList();

                // Добавляем элемент "Все поставщики"
                allSuppliers.Insert(0, new Supplier { Id = 0, Name = "Все поставщики" });

                SupplierFilterComboBox.ItemsSource = allSuppliers;
                SupplierFilterComboBox.SelectedIndex = 0;
            }
        }
        private void SupplierFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            _isAscendingSort = !_isAscendingSort;
            UpdateSortButtonText();
            ApplyFilters();
        }

        private void UpdateSortButtonText()
        {
            SortButton.Content = _isAscendingSort
                ? "По возрастанию"
                : "По убыванию";
        }

        private void ApplyFilters()
        {
            using (var context = new TestContext())
            {

                var products = context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier)
                .AsQueryable();

                // Фильтр по поиску
                var searchText = SearchTextBox.Text;
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    products = products.Where(p =>
                        p.Name.Contains(searchText) ||
                        p.Description.Contains(searchText) ||
                        p.Category.Name.Contains(searchText) ||
                        p.Manufacturer.Name.Contains(searchText) ||
                        p.Supplier.Name.Contains(searchText));
                }

                // Фильтр по поставщику
                var selectedSupplier = SupplierFilterComboBox.SelectedItem as Supplier;
                if (selectedSupplier != null && selectedSupplier.Id != 0)
                {
                    products = products.Where(p => p.Supplier.Id == selectedSupplier.Id);
                }

                // Сортировка
                products = _isAscendingSort
                    ? products.OrderBy(p => p.Quantity)
                    : products.OrderByDescending(p => p.Quantity);

                ProductsItemsControl.ItemsSource = products.ToList();
            }
        }
    }
}
