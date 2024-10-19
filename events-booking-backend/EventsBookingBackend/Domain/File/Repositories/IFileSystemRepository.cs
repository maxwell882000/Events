namespace EventsBookingBackend.Domain.File.Repositories;

public interface IFileSystemRepository
{
    public Task<string> UploadFile(IFormFile file);
    public Task RemoveFile(string filePath);
}