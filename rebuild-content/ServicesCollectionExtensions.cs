using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RebuilderLib.Services.Article;
using RebuilderLib.Services.Repositories.Data;
using RebuilderLib.Services.Repositories.Files.Compression;
using RebuilderLib.Services.Repositories.Files.Storage;

namespace RebuilderLib
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services,
            IConfiguration config,
            string dataSectionName,
            string filesSectionName)
        {
            services.Configure<ArticleDataStorageConfig>(config.GetSection(dataSectionName));
            services.Configure<ImagesFileStorageConfig>(config.GetSection(filesSectionName));
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IDataRepository<Models.Article>, ArticleDataStorage>();
            services.AddScoped<IFileRepository, ImagesFileStorage>();
            services.AddScoped<IFileCompressor, ImageCompressor>();
            services.AddScoped<Rebuilder>();
            return services;
        }
    }
}
