using Microsoft.AspNetCore.Mvc;
using Rebuild_content_api.Contracts;
using Rebuild_content_api.Services.Article;

namespace Rebuild_content_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase 
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IArticleService _articleService;

        public ArticlesController(ILogger<ArticlesController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllArticles()
        {
            var result = await _articleService.GetArticles();
            return Ok(result);
        }

        [HttpGet("{articleId}/image")]
        public async Task<ActionResult> GetThumbnail(string articleId)
        {
            var result = await _articleService.GetThumbnailAsync(articleId);
            var image = System.IO.File.ReadAllBytes(result.Path);
            return File(image, "image/webp", result.Name);
        }

        [HttpGet("{articleId}/images/{imageId}")]
        public async Task<ActionResult> GetImage(string articleId, string imageId)
        {
            var result = await _articleService.GetImageAsync(articleId, imageId);
            var image = System.IO.File.ReadAllBytes(result.Path);
            return File(image, "image/webp", result.Name);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetArticleDetails(string id)
        {
            var result = await _articleService.GetArticleDetails(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddArticle([FromForm] UpsertArticleRequest request)
        {
            var article = await _articleService.CreateArticle(request);
            return CreatedAtRoute(string.Empty, article);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArticle([FromForm] UpsertArticleRequest request, string id)
        {
            await _articleService.UpdateArticle(request, id);
            return CreatedAtRoute(string.Empty, id);
        }

        [HttpDelete("{articleId}/images/{imageId}")]
        public async Task<ActionResult> DeleteImage(string articleId, string imageId)
        {
            await _articleService.DeleteArticleImage(articleId, imageId);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(string id)
        {
            await _articleService.DeleteArticle(id);
            return Accepted();
        }
    }
}