using EventsBookingBackend.Domain.File.Repositories;
using EventsBookingBackend.Domain.File.Services;

namespace EventsBookingBackend.Infrastructure.Services.File;

public class FileDomainService(
    IFileDbRepository fileDbRepository,
    IFileSystemRepository fileSystemRepository) : IFileDomainService
{
    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var filePath = await fileSystemRepository.UploadFile(file);
        try
        {
            await fileDbRepository.Create(new Domain.File.Entities.File()
            {
                FileName = file.FileName,
                FilePath = filePath,
                FileSize = file.Length,
                ContentType = file.ContentType
            });
            return filePath;
        }
        catch (Exception e)
        {
            await fileSystemRepository.RemoveFile(filePath);
            throw;
        }
    }
}