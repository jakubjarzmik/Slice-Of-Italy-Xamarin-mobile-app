using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Models;

namespace SliceOfItalyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderDishesController : ControllerBase
{
    private readonly SliceOfItalyContext _context;

    public OrderDishesController(SliceOfItalyContext context)
    {
        _context = context;
    }

    // GET: api/OrderDishes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDish>>> GetOrderDishes()
    {
        return await _context.OrderDish
            .Include(od => od.Order)
            .Include(od => od.Dish)
            .ToListAsync();
    }

    // GET: api/OrderDishes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDish>> GetOrderDish(int id)
    {
        var orderDish = await _context.OrderDish
            .Include(od => od.Order)
            .Include(od => od.Dish)
            .FirstOrDefaultAsync(od => od.Id == id);

        if (orderDish == null)
        {
            return NotFound();
        }

        return orderDish;
    }

    // PUT: api/OrderDishes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrderDish(int id, OrderDish orderDish)
    {
        if (id != orderDish.Id)
        {
            return BadRequest();
        }

        _context.Entry(orderDish).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderDishExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/OrderDishes
    [HttpPost]
    public async Task<ActionResult<OrderDish>> PostOrderDish(OrderDish orderDish)
    {
        _context.OrderDish?.Add(orderDish);
        await _context.SaveChangesAsync();

        return Ok(orderDish);
    }

    // DELETE: api/OrderDishes/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<OrderDish>> DeleteOrderDish(int id)
    {
        var orderDish = await _context.OrderDish.FindAsync(id);
        if (orderDish == null)
        {
            return NotFound();
        }

        _context.OrderDish.Remove(orderDish);
        await _context.SaveChangesAsync();

        return orderDish;
    }

    private bool OrderDishExists(int id)
    {
        return _context.OrderDish.Any(e => e.Id == id);
    }
}
