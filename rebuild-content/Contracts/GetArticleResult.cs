namespace RebuilderLib.Contracts
{
    public class GetArticleResult
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public DateTime CreationDate { get; set; }
    }
}
