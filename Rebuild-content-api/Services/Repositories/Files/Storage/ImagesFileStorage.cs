using Microsoft.Extensions.Options;
using Rebuild_content_api.Contracts;
using Rebuild_content_api.Exceptions;
using Rebuild_content_api.Services.Repositories.Files.Compression;

namespace Rebuild_content_api.Services.Repositories.Files.Storage
{
    public class ImagesFileStorage : IFileRepository
    {
        private readonly ImagesFileStorageConfig _config;
        private readonly IFileCompressor _compressor;

        public ImagesFileStorage(IOptions<ImagesFileStorageConfig> options, IFileCompressor fileCompressor)
        {
            _config = options.Value;
            _compressor = fileCompressor;
        }

        public Task DeleteAll(string parentId)
        {
            var path = Path.Combine(_config.ImagesPath, parentId);
            Directory.Delete(path, true);
            return Task.CompletedTask;
        }

        public Task Delete(string parentId, string imageId)
        {
            var directoryPath = Path.Combine(_config.ImagesPath, parentId);
            var filePath = Path.Combine(directoryPath, Path.ChangeExtension(imageId.ToString(), "webp"));
            File.Delete(filePath);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<IFile>> Save(IEnumerable<IFormFile> files, string parentId)
        {
            var directoryPath = Path.Combine(_config.ImagesPath, parentId);
            var results = new List<IFile>();

            if (Directory.Exists(directoryPath))
                Directory.Delete(directoryPath, true);

            Directory.CreateDirectory(directoryPath);

            foreach (var file in files)
            {
                if(file.Length == 0)
                    continue;

                var imageId = Guid.NewGuid().ToString();
                var filePath = Path.Combine(directoryPath, Path.ChangeExtension(imageId, "webp"));
                var result = new ArticleImageFile()
                {
                    Id = imageId,
                    ParentId = parentId,
                    Path = filePath
                };
                results.Add(result);
                await _compressor.CompressAndSaveAsync(file, filePath);
            }

            return results.AsEnumerable();
        }

        public async Task<IFile> Save(IFormFile file, string parentId, string imageId)
        {
            var directoryPath = Path.Combine(_config.ImagesPath, parentId);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            if (file.Length == 0)
                throw new FileRepositoryException("Cannot read file.");

            var filePath = Path.Combine(directoryPath, Path.ChangeExtension(imageId.ToString(), "webp"));
            var result = new ArticleImageFile()
            {
                Id = imageId,
                ParentId = parentId,
                Path = filePath
            };
            await _compressor.CompressAndSaveAsync(file, filePath);

            return result;
        }
    }
}
