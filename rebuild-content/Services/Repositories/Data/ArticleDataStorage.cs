using AutoMapper;
using Microsoft.Extensions.Options;
using RebuilderLib.Exceptions;
using System.Text.Json;

namespace RebuilderLib.Services.Repositories.Data
{
    internal class ArticleDataStorage : IDataRepository<Models.Article>
    {
        private readonly ArticleDataStorageConfig _config;
        private readonly IMapper _mapper;

        public ArticleDataStorage(IOptions<ArticleDataStorageConfig> options, IMapper mapper)
        {
            _config = options.Value;
            _mapper = mapper;
        }

        public async Task<List<Models.Article>> FetchAll()
        {
            using FileStream stream = File.OpenRead(_config.ArticlesPath);
            var articles = await JsonSerializer.DeserializeAsync<List<Models.Article>>(stream);
            return articles is not null ? articles : throw new DataRepositoryException("Could not load articles from provided path");
        }

        public async Task<Models.Article> FetchSingle(string Id)
        {
            var articles = await FetchAll();
            var article = articles.FirstOrDefault(a => a.Id == Id);
            return article is not null ? article : throw new DataRepositoryException("Could not load article with provided id");
        }

        public Task Save(List<Models.Article> articles)
        {
            string jsonString = JsonSerializer.Serialize(articles);
            return File.WriteAllTextAsync(_config.ArticlesPath, jsonString);
        }

        public async Task Save(Models.Article article)
        {
            var articles = await FetchAll();
            var oldArticle = articles.FirstOrDefault(a => a.Id == article.Id) ?? throw new DataRepositoryException("Could not find the original article");
            _mapper.Map(article, oldArticle);
            await Save(articles);
        }
    }
}
