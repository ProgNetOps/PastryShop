using Microsoft.AspNetCore.Mvc;
using PastryShop.Models;
using PastryShop.ViewModels;

namespace PastryShop.Controllers;

public class HomeController : Controller
{
    private readonly IPieRepository _pieRepository;

    public HomeController(IPieRepository pieRepository)
    {
        _pieRepository = pieRepository;
    }
    public IActionResult Index()
    {
        var piesOfTheWeek = _pieRepository.PiesOfWeek();
        HomeViewModel homeViewModel = new(piesOfTheWeek);
        return View(homeViewModel);        
        
    }
}
