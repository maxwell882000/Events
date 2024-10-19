namespace EventsBookingBackend.Domain.Files.Services;

public interface IFileService
{
    public Task<string> UploadFileAsync(IFormFile file);
}