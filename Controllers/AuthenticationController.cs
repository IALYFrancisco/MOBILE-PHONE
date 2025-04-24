using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MOBILE_PHONE.Models;
using MOBILE_PHONE.Data;

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
        public async Task<IActionResult> Register([Bind("Name,Email,Password,ConfirmPassword")] RegisterFormModel model, [Bind("Name,Email,Password")] Users _model){
            if(ModelState.IsValid){
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if(existingUser != null){
                    ViewData["Error"] = "Un utilisateur avec cet email existe déjà.";
                    return View(model);
                }
                if (model.Password == null) { // Validation du mot de passe
                   ViewData["Error"] = "Mot de passe requis.";
                    return View(model);
                }
                if (_model.Password == null) { // Validation du mot de passe
                   ViewData["Error"] = "Mot de passe requis.";
                    return View(model);
                }
                if(model.Password != model.ConfirmPassword) {
                    ViewData["Error"] = "Les mots de passe doivent être identiques.";
                    return View(model);
                }
                _model.Password = HashPassword(_model.Password);
                _context.Users.Add(_model);
                await _context.SaveChangesAsync();
                ViewData["Error"] = "Bien inscrit!";
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
