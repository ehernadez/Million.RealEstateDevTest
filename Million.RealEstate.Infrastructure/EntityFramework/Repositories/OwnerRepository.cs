using Million.RealEstate.Domain.Entities;
using Million.RealEstate.Domain.Interfaces;
using Million.RealEstate.Infrastructure.EntityFramework.Data;

namespace Million.RealEstate.Infrastructure.EntityFramework.Repositories
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(RealEstateDbContext context) : base(context) { }
    }
}
