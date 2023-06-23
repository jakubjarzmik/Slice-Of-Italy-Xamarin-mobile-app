using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Helpers;
using SliceOfItalyAPI.Models;
using SliceOfItalyAPI.ViewModels;

namespace SliceOfItalyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly SliceOfItalyContext _context;

    public OrdersController(SliceOfItalyContext context)
    {
        _context = context;
    }

    // GET: api/Order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderForView>>> GetOrders()
    {
        if (_context.Order == null)
        {
            return NotFound();
        }
        try
        {
            var order = await _context.Order
                    .Where(oc => oc.IsActive)
                    .OrderByDescending(oc => oc.CreatedAt)
                    .Include(ord => ord.Customer)
                    .Include(ord => ord.OrderDishes)
                    .ThenInclude(ord => ord.Dish)
                    .ToListAsync();
            return order
                    .Select(ord => OrderForView.ConvertWithDishes(ord))
                    .ToList();
        }
        catch (Exception e)
        {

            throw;
        }
    }

    // GET: api/Order/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderForView>> GetOrder(int id)
    {
        if (_context.Order == null)
        {
            return NotFound();
        }
        var order = await _context.Order
                            .Include(ord => ord.Customer)
                            .Include(ord => ord.OrderDishes)
                            .ThenInclude(ord => ord.Dish)
                            .FirstAsync(ord => ord.Id == id);

        if (order == null)
        {
            return NotFound();
        }
        OrderForView result = OrderForView.ConvertWithDishes(order);
        return Ok(result);
    }


    // PUT: api/Order/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, OrderForView order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }
        var ord = await _context.Order.FindAsync(id);
        if (ord == null)
        {
            return BadRequest();
        }
        ord.CopyProperties(order);
        _context.Entry(ord).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(id))
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

    // POST: api/Order
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<OrderForView>> PostOrder(OrderForView order)
    {
        if (_context.Order == null)
        {
            return Problem("Entity set 'SliceOfItalyContext.Order'  is null.");
        }
        _context.Order.Add((Order)order);
        await _context.SaveChangesAsync();

        //var res = await _context.Order
        //    .Include(ord => ord.Customer)
        //    .FirstOrDefaultAsync(ord => ord.Id == order.Id);
        try
        {
            return Ok(order);
        }
        catch (Exception e)
        {

            throw;
        }
    }

    // DELETE: api/Order/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        if (_context.Order == null)
        {
            return NotFound();
        }
        var order = await _context.Order.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        order.IsActive = false;
        order.DeletedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OrderExists(int id)
    {
        return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}