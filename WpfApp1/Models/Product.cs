using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
namespace WpfApp1.Models;

public partial class Product
{
    public string Article { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int Sale { get; set; }

    public string? ImageData { get; set; }

    public string Unit { get; set; } = null!;

    public int SupplierId { get; set; }

    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }
    public Brush BackgroundColor {
        get
        {
            if (Quantity == 0) return new SolidColorBrush(Colors.LightBlue);
            if (Sale > 15) return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57"));
            if (Sale > 0) return new SolidColorBrush(Colors.LightGreen);
            return new SolidColorBrush(Colors.White);
        }
    }
    public string ImageFullPath => !string.IsNullOrEmpty(ImageData)
        ? $"Images/{ImageData}"
        : "Images/picture.png";
    public decimal SalePrice  => Sale > 0 ? Price * (100 - Sale) / 100 : Price;

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
