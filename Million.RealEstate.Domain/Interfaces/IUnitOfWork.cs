namespace Million.RealEstate.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPropertyRepository Properties { get; }
        IOwnerRepository Owners { get; }
        IPropertyImageRepository PropertyImages { get; }
        IPropertyTraceRepository PropertyTraces { get; }
        IUserRepository Users { get; }
        IFileStorageService FileStorageService { get; }
        IAuthenticationService AuthenticationService { get; }
        IPasswordHasher PasswordHasher { get; }
        Task<int> SaveChangesAsync();
    }
}
