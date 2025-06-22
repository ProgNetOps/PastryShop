
namespace PastryShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PastryShopDbContext _pastryShopDbContext;
        public CategoryRepository(PastryShopDbContext pastryShopDbContext)
        {
            _pastryShopDbContext = pastryShopDbContext;
        }

        public IEnumerable<Category> AllCategories => _pastryShopDbContext.Categories.OrderBy(x => x.CategoryName);
    }
}
