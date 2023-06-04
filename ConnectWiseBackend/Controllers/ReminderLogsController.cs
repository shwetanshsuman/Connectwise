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
    public class ReminderLogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReminderLogsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ReminderLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReminderLog>>> GetReminderLog()
        {
            return await _context.ReminderLog.ToListAsync();
        }

        // GET: api/ReminderLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderLog>> GetReminderLog(int id)
        {
            var reminderLog = await _context.ReminderLog.FindAsync(id);

            if (reminderLog == null)
            {
                return NotFound();
            }

            return reminderLog;
        }

        

        // POST: api/ReminderLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReminderLog>> PostReminderLog(ReminderLog reminderLog)
        {
            _context.ReminderLog.Add(reminderLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReminderLog", new { id = reminderLog.ReminderLogId }, reminderLog);
        }

    }
}
