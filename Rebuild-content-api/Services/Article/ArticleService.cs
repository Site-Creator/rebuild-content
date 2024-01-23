using AutoMapper;
using Rebuild_content_api.Contracts;
using Rebuild_content_api.Exceptions;
using Rebuild_content_api.Models;
using Rebuild_content_api.Services.Repositories.Data;
using Rebuild_content_api.Services.Repositories.Files;
using Rebuild_content_api.Services.Repositories.Files.Storage;

namespace Rebuild_content_api.Services.Article
{
    public class ArticleService : IArticleService
    {
        private readonly IDataRepository<Models.Article> _dataRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public ArticleService(IDataRepository<Models.Article> dataRepository, IFileRepository fileRepository, IMapper mapper)
        {
            _dataRepository = dataRepository;
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<GetArticleDetailsResult?> GetArticleDetails(string articleId)
        {
            var article = await _dataRepository.FetchSingle(articleId);
            return _mapper.Map<GetArticleDetailsResult>(article);
        }

        public async Task<IEnumerable<GetArticleResult>> GetArticles()
        {
            var articles = await _dataRepository.FetchAll();
            return _mapper.Map<IEnumerable<GetArticleResult>>(articles);
        }

        public async Task<string> CreateArticle(UpsertArticleRequest request)
        {
            var articles = await _dataRepository.FetchAll();
            var newArticleId = Guid.NewGuid().ToString();
            var newArticle = await ProcessArticleImages(request, newArticleId);
            articles.Insert(0, newArticle);
            await _dataRepository.Save(articles);
            return newArticleId;
        }

        public async Task UpdateArticle(UpsertArticleRequest request, string articleId)
        {
            var articles = await _dataRepository.FetchAll();
            var oldArticle = articles.FirstOrDefault(a => a.Id == articleId) ?? throw new ArticleServiceException("No article with provided Id was found");
            await _fileRepository.DeleteAll(articleId);
            var newArticle = await ProcessArticleImages(request, articleId);
            _mapper.Map(newArticle, oldArticle);
            await _dataRepository.Save(articles);
        }

        public async Task DeleteArticleImage(string articleId, string imageId)
        {
            var article = await _dataRepository.FetchSingle(articleId);
            var deletedImageIndex = article.Images.FirstOrDefault(i => i.Id == imageId)?.Index ?? throw new ArticleServiceException("No image with provided id found.");
            var orderedImages = article.Images.Where(i => i.Index > deletedImageIndex).OrderBy(i => i.Index);

            foreach (var image in orderedImages)
                image.Index--;

            await _fileRepository.Delete(articleId, imageId);
            await _dataRepository.Save(article);
        }

        public async Task<ArticleImage> GetThumbnailAsync(string articleId)
        {
            var article = await _dataRepository.FetchSingle(articleId);
            var thumbnail = article.Images.FirstOrDefault(i => i.IsThumbnail) ?? throw new ArticleServiceException("Thumbnail not found.");
            return thumbnail;
        }

        public async Task<ArticleImage> GetImageAsync(string articleId, string imageId)
        {
            var article = await _dataRepository.FetchSingle(articleId);
            var image = article.Images.FirstOrDefault(i => i.Id == imageId) ?? throw new ArticleServiceException("Thumbnail not found.");
            return image;
        }

        public async Task DeleteArticle(string articleId)
        {
            var articles = await _dataRepository.FetchAll();
            articles.RemoveAll(a => a.Id == articleId);
            await _dataRepository.Save(articles);
            await _fileRepository.DeleteAll(articleId);
        }

        private async Task<Models.Article> ProcessArticleImages(UpsertArticleRequest request, string id)
        {
            var newArticle = _mapper.Map<Models.Article>(request);
            var images = await _fileRepository.Save(request.Images, id);

            var articleImages = new List<ArticleImage>();
            var imageIndex = 0;
            foreach (var image in images)
            {
                var imageFile = request.Images.ElementAt(imageIndex);
                articleImages.Add(new ArticleImage()
                {
                    Id = image.Id,
                    Index = imageIndex,
                    Name = imageFile.FileName,
                    ParentId = image.ParentId,
                    Path = image.Path,
                    IsThumbnail = request.ThumbnailIndex == imageIndex
                });
                imageIndex++;
            }

            newArticle.Images = articleImages;
            newArticle.Id = id;
            return newArticle;
        }
    }
}
