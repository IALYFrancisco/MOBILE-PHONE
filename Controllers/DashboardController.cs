using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MOBILE_PHONE.Models;

namespace MOBILE_PHONE.Controllers;

public class DashboardController : Controller {
    
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(ILogger<DashboardController> logger){
        _logger = logger;
    }

    public IActionResult Index(){
        return View();
    }

    public IActionResult Product(){
        return View();
    }
}