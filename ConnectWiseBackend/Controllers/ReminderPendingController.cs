using ConnectWiseBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ConnectWiseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderPendingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReminderPendingController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reminder>>> GetPendingReminders()
        {
            IEnumerable <Reminder> reminders= null;
            IEnumerable<Invoice> invoicePending = _context.Invoice.Where(x => x.Status == 0).ToList();
            List<int> invoiceIds = invoicePending.Select(x => x.InvoiceId).ToList();
            
            reminders = await _context.Reminder.Where(x => invoiceIds.Contains(x.InvoiceId)).ToListAsync();
            var result = reminders.Select(r => new
            {
                r.ReminderId,
                r.InvoiceId,
                r.UpdatedDate,
                r.Frequency
            });

            return Ok(result);
        }
    }
}
