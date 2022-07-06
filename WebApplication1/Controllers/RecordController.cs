using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToFifty.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly OneToFiftyContext _context;

        public RecordController(OneToFiftyContext context)
        {
            _context = context;
        }

        /* public static JsonSerializerOptions options = new JsonSerializerOptions
         {
             Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
             WriteIndented = true
         };


         [HttpPost]
         public IActionResult postRecord(Record record)
         {
             await _context.record.AddAsync(record);
         }

         [HttpGet]
         public IActionResult getAllRecords()
         {
             List<Record> sortedRecords = records.OrderBy(x => x.recordMilli).ToList();
             string result = JsonSerializer.Serialize(sortedRecords, options);
             return Ok(sortedRecords);
         }
 */
        // GET: api/Record
        [HttpGet]
        public async ActionResult<IEnumerable<Record>> GetRecords()
        {
            return await _context.record  ToListAsync();
        }

        // GET: api/Record/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> GetRecord(int id)
        {
            var @record = await _context.record.FindAsync(id);

            if (@record == null)
            {
                return NotFound();
            }

            return @record;
        }

        // PUT: api/Record/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecord(int id, Record @record)
        {
            if (id != @record.id)
            {
                return BadRequest();
            }

            _context.Entry(@record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
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

        // POST: api/Record
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord(Record @record)
        {
            _context.record.Add(@record);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecord", new { id = @record.id }, @record);
        }

        // DELETE: api/Record/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var @record = await _context.record.FindAsync(id);
            if (@record == null)
            {
                return NotFound();
            }

            _context.record.Remove(@record);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecordExists(int id)
        {
            return _context.record.Any(e => e.id == id);
        }
    }
}
