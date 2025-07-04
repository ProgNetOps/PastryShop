﻿using Microsoft.AspNetCore.Mvc;
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

    //public IActionResult List()
    //{

    //    PieListViewModel list = new()
    //    {
    //        CurrentCategory = "All Pies",
    //        Pies = _pieRepository.AllPies
    //    };
    //    return View(list);
    //}

    public ViewResult List(string category)
    {
        IEnumerable<Pie> pies;
        string? currentCategory;

        if (string.IsNullOrEmpty(category))
        {
            pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
            currentCategory = "All pies";
        }
        else
        {
            pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                .OrderBy(p => p.PieId);
            currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
        }

        return View(new PieListViewModel(pies, currentCategory));
    }


    public IActionResult Details(int id)
    {
        var pie = _pieRepository.AllPies.FirstOrDefault(x => x.PieId == id);

        return pie == null ? NotFound() : View(pie);
    }
}
