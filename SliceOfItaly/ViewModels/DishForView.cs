using SliceOfItalyAPI.Models.Abstract;
using SliceOfItalyAPI.Models;
using SliceOfItalyAPI.Helpers;

namespace SliceOfItalyAPI.ViewModels
{
    public class DishForView : DictionaryTable
    {
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
        public static explicit operator Dish(DishForView dish)
        {
            var result = new Dish().CopyProperties(dish);
            return result;
        }
        public static implicit operator DishForView(Dish dish)
        {
            var result = new DishForView
            {
                CategoryId = dish.CategoryId,
                CategoryName = dish.Category.Name,
                Price = dish.Price
            }.CopyProperties(dish);
            return result;
        }
    }
}
