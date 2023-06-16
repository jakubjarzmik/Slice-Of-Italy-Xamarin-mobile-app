using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.ViewModels;

namespace SliceOfItalyAPI.Models.BusinessLogic
{
    public class OrderDishB
    {
        public static async Task ValidateAndFillOrderDish(OrderDishForView orderDish, SliceOfItalyContext _context)
        {
            if (_context.Order == null)
            {
                throw new Exception("Entity set 'SliceOfItalyContext.Order' is null.");
            }

            if (_context.Dish == null)
            {
                throw new Exception("Entity set 'SliceOfItalyContext.Dish' is null.");
            }

            var order = await _context.Order.FindAsync(orderDish.OrderId);
            if (order == null)
            {
                throw new Exception("Order is null.");
            }

            var dish = await _context.Dish.FindAsync(orderDish.DishId);
            if (dish == null)
            {
                throw new Exception("Dish is null.");
            }

            orderDish.OrderId = order.Id;
            orderDish.DishName = dish.Name;
        }

        public static async Task UpdateTotalPrice(OrderDishForView orderDish, SliceOfItalyContext _context)
        {
            var dish = await _context.Dish!.FindAsync(orderDish.DishId);
            var order = await _context.Order!.FindAsync(orderDish.OrderId);
            order!.TotalPrice += dish!.Price;
            await _context.SaveChangesAsync();
        }
    }
}
