using EventsBookingBackend.Domain.File.Repositories;
using EventsBookingBackend.Shared.Options.File;
using Microsoft.Extensions.Options;

namespace EventsBookingBackend.Infrastructure.Repositories.File;

public class FileSystemRepository(IOptions<FileOption> options, ILogger<FileSystemRepository> logger)
    : IFileSystemRepository
{
    private readonly string _storagePath = options.Value.StoragePath;

    public async Task<string> UploadFile(IFormFile file)
    {
        if (file.Length <= 0)
            throw new ArgumentException("File is empty");

        var filePath = Path.Combine(_storagePath,
            DateTime.Now + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName));

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

    public async Task RemoveFile(string filePath)
    {
        try
        {
            await Task.Run(() =>
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    logger.LogInformation($"File {filePath} deleted successfully.");
                }
                else
                {
                    logger.LogWarning($"File {filePath} does not exist.");
                }
            });
        }
        catch (Exception ex)
        {
            logger.LogError($"An error occurred removeFile: {ex.Message}. Stack Trace: {ex.StackTrace}");
        }
    }
}