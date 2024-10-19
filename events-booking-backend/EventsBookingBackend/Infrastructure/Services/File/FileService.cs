using EventsBookingBackend.Domain.File.Repositories;
using EventsBookingBackend.Domain.Files.Services;
using EventsBookingBackend.Shared.Options.File;
using Microsoft.Extensions.Options;

namespace EventsBookingBackend.Infrastructure.Services.File;

public class FileService(
    IOptions<FileOption> options,
    IFileDbRepository fileDbRepository,
    IFileSystemRepository fileSystemRepository) : IFileService
{
    private readonly string _storagePath = options.Value.StoragePath;

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        try
        {
            var filePath = await fileSystemRepository.UploadFile(file);

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
            Console.WriteLine(e);
            throw;
        }
    }
}