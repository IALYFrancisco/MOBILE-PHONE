using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MOBILE_PHONE.Models;
using MOBILE_PHONE.Data;

namespace MOBILE_PHONE.Controllers;

public class DashboardController : Controller {
    
    private readonly ILogger<DashboardController> _logger;

    private readonly ApplicationDbContext _context;

    public DashboardController(ILogger<DashboardController> logger, ApplicationDbContext context){
        _logger = logger;
        _context = context;
    }

    public IActionResult Index(){
        return View();
    }

    public IActionResult Product(){
        return View();
    }

    public IActionResult AddProduct(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([Bind("Mark,Model,Stock,UnitPrice,Image")] Products _model){
        if(ModelState.IsValid){
            _model.RegisterDate = DateTime.Now;
            _context.Products.Add(_model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Product");
        }
        return View(_model);
    }
}