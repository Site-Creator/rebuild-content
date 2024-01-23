using AutoMapper;
using Rebuild_content_api.Contracts;
using Rebuild_content_api.Models;

namespace zadumka_backend.MapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, GetArticleDetailsResult>();
            CreateMap<Article, Article>();
            CreateMap<Article, GetArticleResult>();
            CreateMap<ArticleImage, GetImageDetailsResult>();
            CreateMap<UpsertArticleRequest, Article>()
                .ForMember(m => m.Images, opt => opt.Ignore());
        }
    }
}
