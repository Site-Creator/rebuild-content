using Microsoft.AspNetCore.Http;

namespace RebuilderLib.Services.Repositories.Files.Storage
{
    public interface IFileRepository
    {
        public Task<IEnumerable<IFile>> Save(IEnumerable<IFormFile> files, string parentId);

        public Task<IFile> Save(IFormFile file, string parentId, string fileId);

        public Task DeleteAll(string parentId);

        public Task Delete(string parentId, string fileId);
    }
}
