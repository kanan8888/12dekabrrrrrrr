using System.ComponentModel.DataAnnotations;

namespace Uniqlo.DTOs.Account;

public class ResetPasswordVM
{
    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [DataType(DataType.Password), Compare(nameof(NewPassword))]
    public string ConfirmNewPassword { get; set; }
    public string userId { get; set; }
    public string token { get; set; }
}
