using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTLWeb.Models;

public partial class TUser
{
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$",
        ErrorMessage = "Please enter a valid Gmail address.")]
    public string Username { get; set; } = null!;

    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$",
        ErrorMessage = "Password must contain at least one letter and one number.")]
    public string Password { get; set; } = null!;

    public byte? LoaiUser { get; set; }

    public virtual ICollection<TKhachHang> TKhachHangs { get; } = new List<TKhachHang>();
}

