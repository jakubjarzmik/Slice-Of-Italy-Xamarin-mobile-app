using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Models;
using SliceOfItalyAPI.Models.BusinessLogic;
using SliceOfItalyAPI.ViewModels;

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
    public async Task<ActionResult<IEnumerable<OrderDishForView>>> GetOrderDishes()
    {
        if (_context.OrderDish == null)
        {
            return NotFound();
        }
        return (await _context.OrderDish
                .Where(od => od.IsActive)
                .OrderByDescending(od => od.CreatedAt)
                .ToListAsync())
                .Select(od => (OrderDishForView)od)
                .ToList();
    }

    // GET: api/OrderDishes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDishForView>> GetOrderDish(int id)
    {
        if (_context.OrderDish == null)
        {
            return NotFound();
        }
        var orderDish = await _context.OrderDish
            .FindAsync(id);

        if (orderDish == null)
        {
            return NotFound();
        }

        return Ok(orderDish);
    }

    // PUT: api/OrderDishes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrderDish(int id, OrderDishForView orderDish)
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
    public async Task<ActionResult<OrderDishForView>> PostOrderDish(OrderDishForView orderDish)
    {
        try
        {
            if (_context.OrderDish == null)
            {
                throw new Exception("Entity set 'SliceOfItalyContext.OrderDish' is null.");
            }
            await OrderDishB.ValidateAndFillOrderDish(orderDish, _context);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        _context.OrderDish?.Add((OrderDish)orderDish);
        await _context.SaveChangesAsync();

        await OrderDishB.UpdateTotalPrice(orderDish, _context);

        return Ok(orderDish);
    }


    // DELETE: api/OrderDishes/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<OrderDishForView>> DeleteOrderDish(int id)
    {
        var orderDish = await _context.OrderDish.FindAsync(id);
        if (orderDish == null)
        {
            return NotFound();
        }

        orderDish.IsActive = false;
        orderDish.DeletedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return Ok(orderDish);
    }

    private bool OrderDishExists(int id)
    {
        return (_context.OrderDish?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
