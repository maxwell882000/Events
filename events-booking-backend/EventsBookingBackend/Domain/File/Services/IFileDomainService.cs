namespace EventsBookingBackend.Domain.File.Services;

public interface IFileDomainService
{
    public Task<string> UploadFileAsync(IFormFile file);
}