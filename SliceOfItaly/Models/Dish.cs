using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SliceOfItaly.Models.Abstract;

namespace SliceOfItaly.Models
{
    public class Dish : DictionaryTable
    {
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = default!;
        public virtual ICollection<OrderDish> OrderDishes { get; set; } = default!;
    }
}
