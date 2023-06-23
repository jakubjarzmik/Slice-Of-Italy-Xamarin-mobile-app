using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SliceOfItalyAPI.Models.Abstract;

namespace SliceOfItalyAPI.Models;

public class Order : BaseDataTable
{
    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    [Required(ErrorMessage = "Customer is required")]
    public virtual Customer Customer { get; set; } = default!;
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public virtual ICollection<OrderDish> OrderDishes { get; set; } = default!;
}