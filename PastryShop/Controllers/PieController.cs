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
            CurrentCategory = "All Pies",
            Pies = _pieRepository.AllPies
        };
        return View(list);
    }

    public IActionResult Details(int id)
    {
        var pie = _pieRepository.AllPies.FirstOrDefault(x => x.PieId == id);

        return pie == null ? NotFound() : View(pie);
    }
}
