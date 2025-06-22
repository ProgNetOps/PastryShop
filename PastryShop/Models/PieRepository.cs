
using Microsoft.EntityFrameworkCore;

namespace PastryShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly PastryShopDbContext _pastryShopDbContext;
        public PieRepository(PastryShopDbContext pastryShopDbContext)
        {
            _pastryShopDbContext = pastryShopDbContext;
        }

        public IEnumerable<Pie> AllPies => _pastryShopDbContext.Pies.Include(c => c.Category);

        public Pie? GetPieById(int pieId)
        {
            return _pastryShopDbContext.Pies.Include(c => c.Category).FirstOrDefault(p => p.PieId == pieId);
        }

        public IEnumerable<Pie> PiesOfWeek()
        {
            return _pastryShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek==true);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            throw new NotImplementedException();
        }
    }
}
