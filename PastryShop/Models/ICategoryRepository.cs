﻿namespace PastryShop.Models;

public interface ICategoryRepository
{
    IEnumerable<Category> AllCategories { get; }

}
