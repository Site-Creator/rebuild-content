using RebuilderLib.Models;

namespace RebuilderLib.Contracts
{
    public class GetArticleDetailsResult
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public IEnumerable<GetImageDetailsResult> Images { get; set; } = Enumerable.Empty<GetImageDetailsResult>();

        public DateTime CreationDate { get; set; }
    }
}
