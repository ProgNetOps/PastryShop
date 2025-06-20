using Microsoft.AspNetCore.Mvc;
using PastryShop.Models;
using PastryShop.ViewModels;

namespace PastryShop.Controllers;

public class PieController : Controller
{
    private readonly IPieRepository _pieRepository;
    private readonly ICategoryRepository _categoryRepository;

    public PieController(IPieRepository pieRepository,
        ICategoryRepository categoryRepository)
    {
        _pieRepository = pieRepository;
        _categoryRepository = categoryRepository;
    }

    public IActionResult List()
    {
        PieListViewModel list = new()
        {
            CurrentCategory = "Cheese Caked",
            Pies = _pieRepository.AllPies
        };
        return View(list);
    }
}
