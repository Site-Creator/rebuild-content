using RebuilderLib.Contracts;
using RebuilderLib.Models;

namespace RebuilderLib.Services.Article
{
    public interface IArticleService
    {
        public Task<IEnumerable<GetArticleResult>> GetArticles();

        public Task<GetArticleDetailsResult?> GetArticleDetails(string articleId);

        public Task<string> CreateArticle(UpsertArticleRequest request);

        public Task UpdateArticle(UpsertArticleRequest request, string articleId);

        public Task DeleteArticleImage(string articleId, string imageId);

        public Task<ArticleImage> GetThumbnailAsync(string articleId);

        public Task<ArticleImage> GetImageAsync(string articleId, string imageId);

        public Task DeleteArticle(string articleId);
    }
}
