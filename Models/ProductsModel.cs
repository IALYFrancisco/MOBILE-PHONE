using System.ComponentModel.DataAnnotations;

namespace MOBILE_PHONE.Models;

public class ProductsModel {
    public int Id { get; set; }
    public required string Mark { get; set; }
    public required string Model { get; set; }
    public required int Stock { get; set; }
    [DataType(DataType.Date)]
    public DateTime RegisterDate { get; set; }
    public required int UnitPrice { get; set; }
    public required string Image { get; set; }
}
