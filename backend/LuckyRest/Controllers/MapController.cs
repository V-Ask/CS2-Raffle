using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuckyRest.Database;
using LuckyRest.Database.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LuckyRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MapController : ControllerBase
    {
        private readonly LuckyDbContext _context;

        public MapController(LuckyDbContext context)
        {
            _context = context;
        }

        // GET: api/Map
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkshopMap>>> GetMaps()
        {
            return await _context.Maps.ToListAsync();
        }

        // GET: api/Map/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkshopMap>> GetWorkshopMap(int id)
        {
            var workshopMap = await _context.Maps.FindAsync(id);

            if (workshopMap == null)
            {
                return NotFound();
            }

            return workshopMap;
        }

        // PUT: api/Map/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkshopMap(int id, WorkshopMap workshopMap)
        {
            if (id != workshopMap.WorkshopMapId)
            {
                return BadRequest();
            }

            _context.Entry(workshopMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkshopMapExists(id))
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

        // POST: api/Map
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkshopMap>> PostWorkshopMap(WorkshopMap workshopMap)
        {
            _context.Maps.Add(workshopMap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkshopMap", new { id = workshopMap.WorkshopMapId }, workshopMap);
        }

        // DELETE: api/Map/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkshopMap(int id)
        {
            var workshopMap = await _context.Maps.FindAsync(id);
            if (workshopMap == null)
            {
                return NotFound();
            }

            _context.Maps.Remove(workshopMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkshopMapExists(int id)
        {
            return _context.Maps.Any(e => e.WorkshopMapId == id);
        }
    }
}
