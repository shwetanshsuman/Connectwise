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
	public class UserinvoiceController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UserinvoiceController(AppDbContext context)
		{
			_context = context;
		}



		// GET: api/Userinvoice/5
		[HttpGet("{id}")]
		public async Task<IEnumerable<Invoice>> GetInvoice(int id)
		{
			IEnumerable<Invoice> UserInvoice;
			var invoices = await _context.Invoice.ToListAsync();
			var user = await _context.Users.FindAsync(id);

			UserInvoice = invoices.Where(x => x.GeneratedForEmail == user.UserName);
			return UserInvoice;
		}


	}
}
