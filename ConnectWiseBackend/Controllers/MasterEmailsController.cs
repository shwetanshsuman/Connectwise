using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConnectWiseBackend.Models;

namespace ConnectWiseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterEmailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MasterEmailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MasterEmails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterEmail>>> GetMasterEmail()
        {
            return await _context.MasterEmail.ToListAsync();
        }

        // GET: api/MasterEmails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterEmail>> GetMasterEmail(int id)
        {
            var masterEmail = await _context.MasterEmail.FindAsync(id);

            if (masterEmail == null)
            {
                return NotFound();
            }

            return masterEmail;
        }

        

        // POST: api/MasterEmails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MasterEmail>> PostMasterEmail(MasterEmail masterEmail)
        {
            _context.MasterEmail.Add(masterEmail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMasterEmail", new { id = masterEmail.MasterEmailId }, masterEmail);
        }

        

        private bool MasterEmailExists(int id)
        {
            return _context.MasterEmail.Any(e => e.MasterEmailId == id);
        }
    }
}
