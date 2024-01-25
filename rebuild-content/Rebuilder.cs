using RebuilderLib.Contracts;
using RebuilderLib.Models;
using RebuilderLib.Services.Article;

namespace RebuilderLib
{
    public class Rebuilder
    {
        private readonly IArticleService _articleService;

        public Rebuilder(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public Task<IEnumerable<GetArticleResult>> GetAllArticles()
        {
            return _articleService.GetArticles();
        }

        public Task<ArticleImage> GetThumbnail(string articleId)
        {
            return _articleService.GetThumbnailAsync(articleId);
        }

        public Task<ArticleImage> GetImage(string articleId, string imageId)
        {
            return _articleService.GetImageAsync(articleId, imageId);
        }

        public Task<GetArticleDetailsResult?> GetArticleDetails(string id)
        {
            return _articleService.GetArticleDetails(id);
        }

        public Task<string> AddArticle(UpsertArticleRequest request)
        {
            return _articleService.CreateArticle(request);
        }

        public Task UpdateArticle(UpsertArticleRequest request, string id)
        {
            return _articleService.UpdateArticle(request, id);
        }

        public Task DeleteImage(string articleId, string imageId)
        {
            return _articleService.DeleteArticleImage(articleId, imageId);
        }

        public Task DeleteArticle(string id)
        {
            return _articleService.DeleteArticle(id);
        }
    }
}
