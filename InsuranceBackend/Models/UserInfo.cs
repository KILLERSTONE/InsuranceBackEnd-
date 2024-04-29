using System;
using System.Collections.Generic;

namespace InsuranceBackend.Models;

public partial class UserInfo
{
    public int UserId { get; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}
