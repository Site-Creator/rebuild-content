using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RebuilderLib.Contracts
{
    public class UpsertArticleRequest
    {
        public string Title { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int ThumbnailIndex { get; set; }

        [Required]
        public IEnumerable<IFormFile> Images { get; init; } = null!;
    }
}
