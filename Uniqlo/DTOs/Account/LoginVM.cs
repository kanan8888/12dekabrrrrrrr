namespace Uniqlo.DTOs.Account;
using System.ComponentModel.DataAnnotations;

public class LoginVm
{
    [Required]
    public string EmailOrUsername { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool Remember { get; set; }
}
