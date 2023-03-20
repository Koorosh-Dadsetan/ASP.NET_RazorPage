using System;
using System.Collections.Generic;

namespace ASP.NET_RazorPage.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Mobile { get; set; }

    public int? Age { get; set; }

    public string? Address { get; set; }
}
