using Rebuild_content_api.Models;

namespace Rebuild_content_api.Contracts
{
    public class GetArticleDetailsResult
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public IEnumerable<GetImageDetailsResult> Images { get; set; } = Enumerable.Empty<GetImageDetailsResult>();

        public DateTime CreationDate { get; set; }
    }
}
