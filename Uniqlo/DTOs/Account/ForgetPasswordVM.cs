using System.ComponentModel.DataAnnotations;

namespace Uniqlo.DTOs.Account;

public class ForgetPasswordVM
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
