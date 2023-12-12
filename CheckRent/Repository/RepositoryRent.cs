using CheckRent.Context;
using CheckRent.Models;

namespace CheckRent.Repository
{
    public class RepositoryRent : IRepositoryRent
    {
        private readonly AppDbContext _dbContext;
        public RepositoryRent(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Rent> GetByRents()
        {
            var model = _dbContext.Rents.Select(r => new Rent
            {
                Rent_id = r.Rent_id,
                Tenant_fullname = r.Tenant_fullname,
                Start_date = r.Start_date,
                End_date = r.End_date,
                Faculty = r.Faculty,
                Wrote_date = r.Wrote_date,
            });

            return model;
        }
    }
}
