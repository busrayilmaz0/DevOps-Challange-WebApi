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

        // Kullanıcı Kayıt Olma (Register) Uç Noktası
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            if (newUser == null) return BadRequest("Geçersiz veri.");

            // Aynı T.C. numarasıyla daha önce kayıt olunmuş mu kontrol et
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.TcNo == newUser.TcNo);
            if (existingUser != null) return BadRequest("Bu T.C. numarası ile zaten bir hesap var.");

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Kayıt başarılı", userId = newUser.Id });
        }

        // Kullanıcı Giriş Yapma (Login) Uç Noktası
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginData)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.TcNo == loginData.TcNo && u.Password == loginData.Password);
            if (user == null) return Unauthorized("T.C. veya şifre hatalı.");

            return Ok(user);
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