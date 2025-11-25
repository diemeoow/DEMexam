using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class PickUpPoint
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
