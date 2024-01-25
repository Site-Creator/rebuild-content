using Microsoft.AspNetCore.Http;
using SkiaSharp;

namespace RebuilderLib.Services.Repositories.Files.Compression
{
    internal class ImageCompressor : IFileCompressor
    {
        public Task CompressAndSaveAsync(IFormFile file, string destinationPath)
        {
            using var bitmap = SKBitmap.Decode(file.OpenReadStream());
            var scaledBitmap = bitmap.Resize(new SKImageInfo(1920, bitmap.Height * 1920 / bitmap.Width), SKFilterQuality.High);
            using var fs = new SKFileWStream(destinationPath);
            scaledBitmap.Encode(fs, SKEncodedImageFormat.Webp, 100);
            return Task.CompletedTask;
        }
    }
}
