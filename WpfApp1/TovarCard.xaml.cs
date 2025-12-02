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
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для TovarCard.xaml
    /// </summary>
    public partial class TovarCard : UserControl
    {
        public TovarCard()
        {
            InitializeComponent();
        }
        private void ProductCard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Получаем карточку (Border) на которую кликнули
            if (sender is Border card && card.DataContext is Product product)
            {
                var tovarWindow = new TovarWindow(product);
                tovarWindow.ShowDialog();
            }
        }
    }
}
