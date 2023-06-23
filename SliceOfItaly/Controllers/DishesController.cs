using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Helpers;
using SliceOfItalyAPI.Models;
using SliceOfItalyAPI.ViewModels;

namespace SliceOfItalyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DishesController : ControllerBase
{
    private readonly SliceOfItalyContext _context;

    public DishesController(SliceOfItalyContext context)
    {
        _context = context;
    }

    // GET: api/Dishes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishForView>>> GetDishes()
    {
        if (_context.Dish == null)
            return NotFound();

        return (await _context.Dish
                .Where(d => d.IsActive)
                .OrderByDescending(d => d.CreatedAt)
                .Include(d => d.Category)
                .ToListAsync()).
                Select(d => (DishForView)d)
                .ToList();
    }

    // GET: api/Dishes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DishForView>> GetDish(int id)
    {
        if (_context.Dish == null)
            return NotFound();

        var dish = await _context.Dish
            .Include(d => d.Category)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dish == null)
        {
            return NotFound();
        }

        return Ok((DishForView)dish);
    }

    // PUT: api/Dishes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDish(int id, DishForView dish)
    {
        if (id != dish.Id || _context.Dish == null)
        {
            return BadRequest();
        }

        var d = await _context.Dish.FindAsync(id);

        if (d == null)
        {
            return BadRequest();
        }

        d.CopyProperties(dish);

        _context.Entry(dish).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DishExists(id))
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

    // POST: api/Dishes
    [HttpPost]
    public async Task<ActionResult<DishForView>> PostDish(DishForView dish)
    {
        if (_context.Dish == null)
        {
            return Problem("Entity set 'SliceOfItalyContext.Dish' is null.");
        }
        _context.Dish.Add((Dish)dish);
        await _context.SaveChangesAsync();

        return Ok(dish);
    }

    // DELETE: api/Dishes/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<DishForView>> DeleteDish(int id)
    {
        if (_context.Dish == null)
        {
            return NotFound();
        }
        var dish = await _context.Dish.FindAsync(id);
        if (dish == null)
        {
            return NotFound();
        }

        dish.IsActive = false;
        dish.DeletedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DishExists(int id)
    {
        return (_context.Dish?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
