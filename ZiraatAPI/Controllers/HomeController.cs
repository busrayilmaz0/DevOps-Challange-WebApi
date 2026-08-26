using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZiraatApi.Models;

namespace ZiraatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Kullanıcı bilgilerini ve bakiyesini getiren uç nokta
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");
            return Ok(user);
        }

        // Son işlemleri getiren uç nokta
        [HttpGet("transactions/{userId}")]
        public async Task<IActionResult> GetTransactions(int userId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            return Ok(transactions);
        }
    }
}