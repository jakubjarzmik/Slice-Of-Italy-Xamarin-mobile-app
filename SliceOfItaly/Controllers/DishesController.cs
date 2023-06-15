using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Models;

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
    public async Task<ActionResult<IEnumerable<Dish>>> GetDishes()
    {
        return await _context.Dish
            .Include(d => d.Category)
            .ToListAsync();
    }

    // GET: api/Dishes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Dish>> GetDish(int id)
    {
        var dish = await _context.Dish
            .Include(d => d.Category)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dish == null)
        {
            return NotFound();
        }

        return dish;
    }

    // PUT: api/Dishes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDish(int id, Dish dish)
    {
        if (id != dish.Id)
        {
            return BadRequest();
        }

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
    public async Task<ActionResult<Dish>> PostDish(Dish dish)
    {
        _context.Dish?.Add(dish);
        await _context.SaveChangesAsync();

        return Ok(dish);
    }

    // DELETE: api/Dishes/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Dish>> DeleteDish(int id)
    {
        var dish = await _context.Dish.FindAsync(id);
        if (dish == null)
        {
            return NotFound();
        }

        _context.Dish.Remove(dish);
        await _context.SaveChangesAsync();

        return dish;
    }

    private bool DishExists(int id)
    {
        return _context.Dish.Any(e => e.Id == id);
    }
}
