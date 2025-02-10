using System;
using System.Collections.Generic;

namespace Aktywator.Models;

public partial class Order
{
    public int Id { get; set; }

    public string Nip { get; set; } = null!;

    public int ProductId { get; set; }

    public decimal AmountPaid { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();
}
