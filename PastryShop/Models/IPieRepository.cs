namespace PastryShop.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }
        IEnumerable<Pie> PiesOfWeek();
        Pie? GetPieById(int PieId);
        IEnumerable<Pie> SearchPies(string searchQuery);
    }
}
