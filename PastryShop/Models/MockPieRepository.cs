
namespace PastryShop.Models;

public class MockPieRepository : IPieRepository
{
    public IEnumerable<Pie> AllPies => throw new NotImplementedException();

    public IEnumerable<Pie> PiesOfWeek() => AllPies.Where(p => p.IsPieOfTheWeek);

    public Pie? GetPieById(int PieId) =>
    
        AllPies.FirstOrDefault(p => p.PieId == PieId);

    public IEnumerable<Pie> SearchPies(string searchQuery) =>
    
        AllPies.Where(p =>p.Name.Contains(searchQuery)).
        ToList();
    
}
