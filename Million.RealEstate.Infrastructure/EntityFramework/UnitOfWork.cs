using Million.RealEstate.Domain.Interfaces;
using Million.RealEstate.Infrastructure.EntityFramework.Data;

namespace Million.RealEstate.Infrastructure.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RealEstateDbContext _context;
        public IPropertyRepository Properties { get; }
        public IOwnerRepository Owners { get; }
        public IPropertyImageRepository PropertyImages { get; }
        public IPropertyTraceRepository PropertyTraces { get; }
        public IUserRepository Users { get; }
        public IFileStorageService FileStorageService { get; }
        public IAuthenticationService AuthenticationService { get; }
        public IPasswordHasher PasswordHasher { get; }

        public UnitOfWork(
            RealEstateDbContext context,
            IPropertyRepository propertyRepository,
            IOwnerRepository ownerRepository,
            IPropertyImageRepository propertyImageRepository,
            IPropertyTraceRepository propertyTraceRepository,
            IFileStorageService fileStorageService,
            IUserRepository users,
            IAuthenticationService authenticationService,
            IPasswordHasher passwordHasher)
        {
            _context = context;
            Properties = propertyRepository;
            Owners = ownerRepository;
            PropertyImages = propertyImageRepository;
            PropertyTraces = propertyTraceRepository;
            FileStorageService = fileStorageService;
            Users = users;
            AuthenticationService = authenticationService;
            PasswordHasher = passwordHasher;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
