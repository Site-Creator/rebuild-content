using System.ComponentModel.DataAnnotations;

namespace RebuilderLib.Models
{
    public class Article
    {
        [MaxLength(36)]
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public IEnumerable<ArticleImage> Images { get; set; } = Enumerable.Empty<ArticleImage> ();
    }
}
