using CheckRent.Models;

namespace CheckRent.Repository
{
    public interface IRepositoryRent
    {
        IEnumerable<Rent> GetByRents();
    }
}
