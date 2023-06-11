using System.ComponentModel.DataAnnotations.Schema;
using SliceOfItalyAPI.Models.Abstract;

namespace SliceOfItalyAPI.Models;

public class OrderDish : BaseDataTable
{
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; } = default!;

    public int DishId { get; set; }
    [ForeignKey("DishId")]
    public virtual Dish Dish { get; set; } = default!;
}