using Microsoft.Extensions.Configuration;
using Million.RealEstate.Domain.Interfaces;

namespace Million.RealEstate.Infrastructure.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _baseImagePath;

        public LocalFileStorageService(IConfiguration configuration)
        {
            _baseImagePath = configuration["FileStorage:ImagesPath"] ?? "wwwroot/images/properties";
            
            if (!Directory.Exists(_baseImagePath))
            {
                Directory.CreateDirectory(_baseImagePath);
            }
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
        {
            var uniqueFileName = $"{DateTime.UtcNow.Ticks}_{Path.GetFileName(fileName)}";
            var filePath = Path.Combine(_baseImagePath, uniqueFileName);

            using var fileStreamWrite = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(fileStreamWrite);

            return filePath;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_baseImagePath, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string fileName)
        {
            var fileUrl = Path.GetFullPath(fileName).Replace("\\", "/");
            return fileUrl;
        }
    }
}