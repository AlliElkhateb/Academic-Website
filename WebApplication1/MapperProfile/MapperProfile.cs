using AutoMapper;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.ViewModel;

namespace WebApplication1.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<StudentCourse, StudentCourseVM>().ReverseMap();
        }
    }
}
