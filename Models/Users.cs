using System.ComponentModel.DataAnnotations;

namespace MOBILE_PHONE.Models;

public class Users
{
 public int Id { get; set; }
 public string? Name { get; set; }
 public string? Email { get; set; }
 public string? Password { get; set; }
 [DataType(DataType.Date)]
 public DateTime RegisterDate { get; set; }
}