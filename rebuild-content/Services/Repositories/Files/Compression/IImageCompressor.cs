using Microsoft.AspNetCore.Http;

namespace RebuilderLib.Services.Repositories.Files.Compression
{
    public interface IFileCompressor
    {
        Task CompressAndSaveAsync(IFormFile file, string destinationPath);
    }
}
