using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_RazorPage.Models;

public partial class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "پر کردن این فیلد الزامی می‌باشد")]
    [MaxLength(30, ErrorMessage = "حداکثر 30 کاراکتر مجاز می‌باشد")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "پر کردن این فیلد الزامی می‌باشد")]
    [MinLength(11, ErrorMessage = "لطفا شماره تماس را به صورت صحیح وارد نمائید")]
    [MaxLength(11, ErrorMessage = "لطفا شماره تماس را به صورت صحیح وارد نمائید")]
    public string? Mobile { get; set; }

    [Range(0, 120, ErrorMessage = "لطفا سن را به صورت صحیح وارد نمائید")]
    public int? Age { get; set; }

    [MaxLength(50, ErrorMessage = "حداکثر 50 کاراکتر مجاز می‌باشد")]
    public string? Address { get; set; }
}
