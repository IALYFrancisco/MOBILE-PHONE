using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MOBILE_PHONE.Models;

namespace MOBILE_PHONE.Controllers;

public class AuthenticationController : Controller {


    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(ILogger<AuthenticationController> logger)
    {
        _logger = logger;
    }

    [HttpGet] 
    public IActionResult Login(){
        return View();
    }

    [HttpGet]
    public IActionResult ForgotPassword(){
        return View();
    }

}