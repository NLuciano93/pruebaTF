using AutoMapper;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile()
        {
            CreateMap(typeof(IPagination<>), typeof(Pagination<>));
            CreateMap(typeof(IPagination<,>), typeof(Pagination<,>));

            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            CreateMap(typeof(Pagination<,>), typeof(Pagination<,>));
        }
    }
}
