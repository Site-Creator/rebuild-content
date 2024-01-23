using Rebuild_content_api.Services.Article;
using Rebuild_content_api.Services.Repositories.Data;
using Rebuild_content_api.Services.Repositories.Files.Compression;
using Rebuild_content_api.Services.Repositories.Files.Storage;

namespace Rebuild_content_api.Services
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ArticleDataStorageConfig>(config.GetSection("Data"));
            services.Configure<ImagesFileStorageConfig>(config.GetSection("Files"));
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IDataRepository<Models.Article>, ArticleDataStorage>();
            services.AddScoped<IFileRepository, ImagesFileStorage>();
            services.AddScoped<IFileCompressor, ImageCompressor>();
            return services;
        }
    }
}
