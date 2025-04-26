using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MOBILE_PHONE.Models;
using MOBILE_PHONE.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MOBILE_PHONE.Attributes;

namespace MOBILE_PHONE.Controllers {

    public class AuthenticationController : Controller {


        private readonly ILogger<AuthenticationController> _logger;

        private readonly ApplicationDbContext _context;

        public AuthenticationController(ILogger<AuthenticationController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [RestrictAuthenticatedUser]
        [HttpGet]
        public IActionResult Login(){
            return View();
        }

        [RestrictAuthenticatedUser]
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] Users model){
            if(ModelState.IsValid){
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
#pragma warning disable CS8604 // Possible null reference argument.
                if (user != null && VerifyPassword(model.Password, user.Password)) {
                    await SignInUser(user.Email, user.Name);
                    return RedirectToAction("Product", "Dashboard");
                }
#pragma warning restore CS8604 // Possible null reference argument.
                ViewData["Error"] = "Email ou mot de passe incorrect.";
            }
            return View(model);
        }

        private bool VerifyPassword(string password, string storedHash){
            return HashPassword(password) == storedHash;
        }

        private async Task SignInUser(string Email, string Name){
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Name, Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }

        [HttpPost]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [RestrictAuthenticatedUser]
        [HttpGet]
        public IActionResult ForgotPassword(){
            return View();
        }

        [RestrictAuthenticatedUser]
        [HttpGet]
        public IActionResult Register(){
            return View();
        }

        [RestrictAuthenticatedUser]
        // Action en charge du requête POST sur la route /Authentication/Register.
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name,Email,Password,ConfirmPassword")] RegisterFormModel model, [Bind("Name,Email,Password")] Users _model){
            if(ModelState.IsValid){
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if(existingUser != null){
                    ViewData["Error"] = "Un utilisateur avec cet email existe déjà.";
                    return View(model);
                }
                if (model.Password == null) { // Validation du mot de passe
                    return View(model);
                }
                if (_model.Password == null) { // Validation du mot de passe
                    return View(model);
                }
                if(model.Password != model.ConfirmPassword) {
                    ViewData["Error"] = "Les mots de passe doivent être identiques.";
                    return View(model);
                }
                _model.Password = HashPassword(_model.Password);
                _model.RegisterDate = DateTime.Now;
                _context.Users.Add(_model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Vous êtes bien inscrit(e) !";
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
