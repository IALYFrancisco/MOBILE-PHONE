using System.ComponentModel.DataAnnotations;

namespace MOBILE_PHONE.Models;

public class RegisterFormModel {
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    [DataType(DataType.Date)]
    public DateTime RegisterDate { get; set; }
}