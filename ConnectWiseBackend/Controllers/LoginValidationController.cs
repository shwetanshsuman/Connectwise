using ConnectWiseBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConnectWiseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginValidationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginValidationController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<int>> LoginValidation(UserLogin userLogin)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x=>x.UserName==userLogin.userEmail);
            if (user!=null && user.Password == userLogin.userPassword)
            {
                return (user.UserId);
            }
            return NoContent();
        }
    }
}
