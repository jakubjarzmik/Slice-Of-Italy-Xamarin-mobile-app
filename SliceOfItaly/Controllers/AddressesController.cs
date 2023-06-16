using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Models;

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
    public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
    {
        return await _context.Address
            .Include(a => a.Customer) // Include the Customer data
            .ToListAsync();
    }

    // GET: api/Addresses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Address>> GetAddress(int id)
    {
        var address = await _context.Address
            .Include(a => a.Customer) // Include the Customer data
            .FirstOrDefaultAsync(a => a.Id == id);

        if (address == null)
        {
            return NotFound();
        }

        return address;
    }

    // PUT: api/Addresses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAddress(int id, Address address)
    {
        if (id != address.Id)
        {
            return BadRequest();
        }

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
    public async Task<ActionResult<Address>> PostAddress(Address address)
    {
        _context.Address.Add(address);
        await _context.SaveChangesAsync();

        return Ok(address);
    }

    // DELETE: api/Addresses/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Address>> DeleteAddress(int id)
    {
        var address = await _context.Address.FindAsync(id);
        if (address == null)
        {
            return NotFound();
        }

        _context.Address.Remove(address);
        await _context.SaveChangesAsync();

        return address;
    }

    private bool AddressExists(int id)
    {
        return (_context.Address?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
