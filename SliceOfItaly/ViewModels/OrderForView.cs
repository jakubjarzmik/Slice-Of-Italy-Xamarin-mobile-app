using System.ComponentModel.DataAnnotations;
using SliceOfItalyAPI.Models.Abstract;
using SliceOfItalyAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using SliceOfItalyAPI.Helpers;

namespace SliceOfItalyAPI.ViewModels
{
    public class OrderForView : BaseDataTable
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = default!;
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual ICollection<OrderDishForView> OrderDishes { get; set; } = default!;

        public static explicit operator Order(OrderForView order)
        {
            var result = new Order().CopyProperties(order);
            return result;
        }
        public static implicit operator OrderForView(Order order)
        {
            var result = new OrderForView
            {
                CustomerId = order.CustomerId,
                CustomerName = order?.Customer?.Name ?? string.Empty,
                TotalPrice = order?.TotalPrice ?? 0,
                OrderDate = order?.OrderDate ?? DateTime.Now
            }.CopyProperties(order);
            return result;
        }
        public static OrderForView ConvertWithDishes(Order order)
        {
            var result = (OrderForView)order;
            if (order.OrderDishes?.Any() == true)
            {
                result.OrderDishes = order.OrderDishes
                    .Select(acc => (OrderDishForView)acc)
                    .ToList();
            }

            return result;
        }
    }
}
