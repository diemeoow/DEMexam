using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для TovarWindow.xaml
    /// </summary>
    public partial class TovarWindow : Window
    {
        private byte[] _productImageBytes = null;
        private string _originalImagePath = null;
        private Product _product;
        private bool _isEditMode = false;
        public TovarWindow(Product product)
        {
            InitializeComponent();
            LoadData();
            _product = product;
            LoadProduct();
        }
        private void LoadData()
        {
            using (var context = new TestContext())
            {
                CategoryComboBox.ItemsSource = context.Categories
                .OrderBy(c => c.Name)
                .ToList();

                ManufacturerComboBox.ItemsSource = context.Manufacturers
                .OrderBy(m => m.Name)
                .ToList();

                SupplierComboBox.ItemsSource = context.Suppliers
                .OrderBy(s => s.Name)
                .ToList();
            }
        }
        private void LoadProduct()
        {
            if (_product.Article != null)
            {
                _isEditMode = true;
                NameTextBox.Text = _product.Name;
                DescriptionTextBox.Text = _product.Description ?? "";
                QuantityTextBox.Text = _product.Quantity.ToString();
                PriceTextBox.Text = _product.Price.ToString("F2");
                SaleTextBox.Text = _product.Sale.ToString();
                UnitTextBox.Text = _product.Unit.ToString();
                ProductImage.Source = new BitmapImage(new Uri("pack://application:,,,/" + _product.ImageFullPath));

                if (_product.Category != null)
                    CategoryComboBox.SelectedItem = CategoryComboBox.Items
                        .Cast<Category>()
                        .FirstOrDefault(c => c.Id == _product.CategoryId);

                if (_product.Manufacturer != null)
                    ManufacturerComboBox.SelectedItem = ManufacturerComboBox.Items
                        .Cast<Manufacturer>()
                        .FirstOrDefault(m => m.Id == _product.ManufacturerId);

                if (_product.Supplier != null)
                    SupplierComboBox.SelectedItem = SupplierComboBox.Items
                        .Cast<Supplier>()
                        .FirstOrDefault(s => s.Id == _product.SupplierId);
            }
        }
        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void ClearImageButton_Click(object sender, RoutedEventArgs e)
        {
            ProductImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _product.Name = NameTextBox.Text;
            _product.Description = DescriptionTextBox.Text;
            _product.Quantity = int.Parse(QuantityTextBox.Text);
            _product.Price = Decimal.Parse(PriceTextBox.Text);
            _product.Unit = UnitTextBox.Text;
            _product.Sale = int.Parse(SaleTextBox.Text);

            if (CategoryComboBox.SelectedItem is Category selectedCategory)
            {
                _product.CategoryId = selectedCategory.Id;
            }
            if (ManufacturerComboBox.SelectedItem is Manufacturer selectedManufacturer)
            {
                _product.ManufacturerId = selectedManufacturer.Id;
            }
            if (SupplierComboBox.SelectedItem is Supplier selectedSupplier)
            {
                _product.SupplierId = selectedSupplier.Id;
            }

            using (var context = new TestContext())
            {
                if (_isEditMode)
                {
                    context.Products.Update(_product);
                }
                else
                {
                    _product.Article = GenerateArticleCode();
                    context.Products.Add(_product);
                }

                context.SaveChanges();
            }
            Close();
        }
        public static string GenerateArticleCode()
        {
            Random random = new Random();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";

            // Формат: Буква + 3 цифры + Буква + Цифра
            char firstLetter = letters[random.Next(letters.Length)];
            string threeDigits = new string(Enumerable.Repeat(digits, 3)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            char secondLetter = letters[random.Next(letters.Length)];
            char lastDigit = digits[random.Next(digits.Length)];

            return $"{firstLetter}{threeDigits}{secondLetter}{lastDigit}";
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
