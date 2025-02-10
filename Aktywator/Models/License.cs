using System;
using System.Collections.Generic;

namespace Aktywator.Models;

public partial class License
{
    public int Id { get; set; }

    public string Nip { get; set; } = null!;

    public int ProductId { get; set; }

    public string LicenseKey { get; set; } = null!;

    public bool? Activated { get; set; }

    public DateTime? ActivationDate { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;
}
