namespace RebuilderLib.Models
{
    public class ArticleImage
    {
        public string Id { get; set; } = null!;

        public int Index { get; set; }

        public string ParentId { get; set; } = null!;

        public string Path { get; set; } = null!;

        public string Name { get; set; } = null!;

        public bool IsThumbnail { get; set; }
    }
}
