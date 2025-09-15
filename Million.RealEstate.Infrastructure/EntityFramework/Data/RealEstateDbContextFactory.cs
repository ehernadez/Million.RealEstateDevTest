using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Million.RealEstate.Infrastructure.EntityFramework.Data
{
    public class RealEstateDbContextFactory : IDesignTimeDbContextFactory<RealEstateDbContext>
    {
        public RealEstateDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RealEstateDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=RealEstateDb;User Id=sa;Password=RealEstate123!;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True");

            return new RealEstateDbContext(optionsBuilder.Options);
        }
    }
}