using Rebuild_content_api.Services.Repositories.Files;

namespace Rebuild_content_api.Contracts
{
    public class ArticleImageFile : IFile
    {
        public string Id { get; set; } = null!;

        public string ParentId { get; set; } = null!;

        public string Path { get; set; } = null!;
    }
}
