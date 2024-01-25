using RebuilderLib.Services.Repositories.Files;

namespace RebuilderLib.Contracts
{
    public class ArticleImageFile : IFile
    {
        public string Id { get; set; } = null!;

        public string ParentId { get; set; } = null!;

        public string Path { get; set; } = null!;
    }
}
