using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Models.Abstract;
using System.Security.Principal;

namespace SliceOfItalyAPI.Controllers.Abstract;

public class BaseController<T> : ControllerBase where T : BaseDataTable
{
    protected readonly SliceOfItalyContext Context;

    public BaseController(SliceOfItalyContext context)
    {
        Context = context;
    }

    // GET: api/[controller]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> Get()
    {
        if (Context.Set<T>() == null)
        {
            return NotFound();
        }
        return await Context.Set<T>().ToListAsync();
    }

    // GET: api/[controller]/5
    [HttpGet("{id}")]
    public async Task<ActionResult<T>> Get(int id)
    {
        if (Context.Set<T>() == null)
        {
            return NotFound();
        }
        var entity = await Context.Set<T>().FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    // PUT: api/[controller]/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] T entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }

        Context.Entry(entity).State = EntityState.Modified;

        try
        {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EntityExists(id))
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

    // POST: api/[controller]
    [HttpPost]
    public async Task<ActionResult<T>> Post(T entity)
    {
        if (Context.Set<T>() == null)
        {
            return Problem($"Entity set '{typeof(T).Name}' is null.");
        }
        Context.Set<T>().Add(entity);
        await Context.SaveChangesAsync();

        return Ok(entity);
    }

    // DELETE: api/[controller]/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (Context.Set<T>() == null)
        {
            return NotFound();
        }
        var entity = await Context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();

        return NoContent();
    }

    private bool EntityExists(int id)
    {
        return (Context.Set<T>()?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}

