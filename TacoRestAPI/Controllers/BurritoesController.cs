using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TacoRestAPI.Models;

namespace TacoRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurritoesController : ControllerBase
    {
        private readonly FastFoodTacoDbContext _context;

        public BurritoesController(FastFoodTacoDbContext context)
        {
            _context = context;
        }

        // GET: api/Burritoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Burrito>>> GetBurritos()
        {
          if (_context.Burritos == null)
          {
              return NotFound();
          }
            return await _context.Burritos.ToListAsync();
        }

        // GET: api/Burritoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Burrito>> GetBurrito(int id)
        {
          if (_context.Burritos == null)
          {
              return NotFound();
          }
            var burrito = await _context.Burritos.FindAsync(id);

            if (burrito == null)
            {
                return NotFound();
            }

            return burrito;
        }

        // PUT: api/Burritoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBurrito(int id, Burrito burrito)
        {
            if (id != burrito.Id)
            {
                return BadRequest();
            }

            _context.Entry(burrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BurritoExists(id))
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

        // POST: api/Burritoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Burrito>> PostBurrito(Burrito burrito)
        {
          if (_context.Burritos == null)
          {
              return Problem("Entity set 'FastFoodTacoDbContext.Burritos'  is null.");
          }
            _context.Burritos.Add(burrito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBurrito", new { id = burrito.Id }, burrito);
        }

        // DELETE: api/Burritoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBurrito(int id)
        {
            if (_context.Burritos == null)
            {
                return NotFound();
            }
            var burrito = await _context.Burritos.FindAsync(id);
            if (burrito == null)
            {
                return NotFound();
            }

            _context.Burritos.Remove(burrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BurritoExists(int id)
        {
            return (_context.Burritos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
