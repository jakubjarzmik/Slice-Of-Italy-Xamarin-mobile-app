using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Helpers;
using SliceOfItalyAPI.Models;
using SliceOfItalyAPI.ViewModels;

namespace SliceOfItalyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : ControllerBase
{
    private readonly SliceOfItalyContext _context;

    public AddressesController(SliceOfItalyContext context)
    {
        _context = context;
    }

    // GET: api/Addresses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressForView>>> GetAddresses()
    {
        if (_context.Address == null)
            return NotFound();

        return (await _context.Address
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.CreatedAt)
                .Include(a => a.Customer)
                .ToListAsync()).
                Select(a => (AddressForView)a)
                .ToList();
    }

    // GET: api/Addresses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AddressForView>> GetAddress(int id)
    {
        if (_context.Address == null)
            return NotFound();

        var address = await _context.Address
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (address == null)
        {
            return NotFound();
        }

        return Ok((AddressForView)address);
    }

    // PUT: api/Addresses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAddress(int id, AddressForView address)
    {
        if (id != address.Id || _context.Address == null)
        {
            return BadRequest();
        }

        var a = await _context.Address.FindAsync(id);

        if (a == null)
        {
            return BadRequest();
        }

        a.CopyProperties(address);

        _context.Entry(address).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AddressExists(id))
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

    // POST: api/Addresses
    [HttpPost]
    public async Task<ActionResult<AddressForView>> PostAddress(AddressForView address)
    {
        if (_context.Address == null)
        {
            return Problem("Entity set 'SliceOfItalyContext.Address' is null.");
        }
        _context.Address.Add((Address)address);
        await _context.SaveChangesAsync();

        return Ok(address);
    }

    // DELETE: api/Addresses/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<AddressForView>> DeleteAddress(int id)
    {
        if (_context.Address == null)
        {
            return NotFound();
        }
        var address = await _context.Address.FindAsync(id);
        if (address == null)
        {
            return NotFound();
        }

        address.IsActive = false;
        address.DeletedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AddressExists(int id)
    {
        return (_context.Address?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
