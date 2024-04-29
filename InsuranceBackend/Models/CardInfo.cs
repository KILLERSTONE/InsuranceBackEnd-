using System;
using System.Collections.Generic;

namespace InsuranceBackend.Models;

public partial class CardInfo
{
    public int CardId { get; }

    public string CardOwner { get; set; } = null!;

    public long CardNo { get; set; }

    public int SecurityCode { get; set; }

    public DateTime ValidThrough { get; set; }
}
