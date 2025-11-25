using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class OrderProduct
{
    public int OrderId { get; set; }

    public string ProductArticle { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product ProductArticleNavigation { get; set; } = null!;
}
