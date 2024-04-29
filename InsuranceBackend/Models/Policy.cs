using System;
using System.Collections.Generic;

namespace InsuranceBackend.Models;

public partial class Policy
{
    public int User_id { get; }
    public int PolicyId { get; }

    public string PolicyName { get; set; } = null!;

    public string? PolicyDetail { get; set; }

    public string PolicyInsurer { get; set; } = null!;

    public string? PolicyTpa { get; set; }

    public DateTime PolicyFrom { get; set; }

    public DateTime PolicyTo { get; set; }
}
