using SliceOfItalyAPI.Models.Abstract;
using SliceOfItalyAPI.Models;
using SliceOfItalyAPI.Helpers;

namespace SliceOfItalyAPI.ViewModels;

public class OrderDishForView : BaseDataTable
{
    public int OrderId { get; set; }

    public int DishId { get; set; }
    public string? DishName { get; set; }
    public decimal? DishPrice { get; set; }
    public static explicit operator OrderDish(OrderDishForView od)
    {
        var result = new OrderDish().CopyProperties(od);
        return result;
    }
    public static implicit operator OrderDishForView(OrderDish od)
    {
        var result = new OrderDishForView
        {
            OrderId = od.OrderId,
            DishId = od.DishId,
            DishName = od.Dish?.Name ?? String.Empty,
            DishPrice = od.Dish?.Price,
        }.CopyProperties(od);
        return result;
    }
}