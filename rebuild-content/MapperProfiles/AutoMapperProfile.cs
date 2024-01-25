using AutoMapper;
using RebuilderLib.Contracts;
using RebuilderLib.Models;

namespace zadumka_backend.MapperProfile
{
    internal class AutoMapperProfile : Profile
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
