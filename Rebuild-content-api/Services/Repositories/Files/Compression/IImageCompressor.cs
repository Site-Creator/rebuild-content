namespace Rebuild_content_api.Services.Repositories.Files.Compression
{
    public interface IFileCompressor
    {
        Task CompressAndSaveAsync(IFormFile file, string destinationPath);
    }
}
