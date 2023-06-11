using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Controllers.Abstract;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Models;

namespace SliceOfItalyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderDishesController : BaseController<OrderDish>
{
    public OrderDishesController(SliceOfItalyContext context) : base(context)
    {
    }
}