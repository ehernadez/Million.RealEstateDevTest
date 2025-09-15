using Microsoft.AspNetCore.Http;

namespace Million.RealEstate.Domain.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string fileName);
        Task DeleteFileAsync(string filePath);
        string GetFileUrl(string fileName);
    }
}