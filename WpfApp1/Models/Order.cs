using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public int PickUpPointId { get; set; }

    public int PickUpCode { get; set; }

    public int StatusId { get; set; }

    public int UserId { get; set; }

    public virtual PickUpPoint PickUpPoint { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
