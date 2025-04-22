using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MOBILE_PHONE.Models;

namespace MOBILE_PHONE.Controllers {

    public class AuthenticationController : Controller {


        private readonly ILogger<AuthenticationController> _logger;

        private readonly ApplicationDbContext _context;

        public AuthenticationController(ILogger<AuthenticationController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet] 
        public IActionResult Login(){
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword(){
            return View();
        }

        [HttpGet]
        public IActionResult Register(){
            return View();
        }

        // Action en charge du requête POST sur la route /Authentication/Register.
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name,Email,Password")] Users model){
            if(ModelState.IsValid){
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if(existingUser){
                    ViewData["Error"] = "Un utilisateur avec cet email existe déjà.";
                    return View(model);
                }
                model.Password = HashPassword(model.Password);
                _context.Users.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        private string HashPassword(string Password){
            using ( var sha256 = SHA256.Create() ){
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return Convert.ToBase64String(hashBytes);
            }
        }

    }
}
