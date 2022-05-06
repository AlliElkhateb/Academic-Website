using AutoMapper;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.Core.ViewModel;

namespace WebApplication1.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Course, CourseVM>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<Instructor, InstructorVM>().ReverseMap();
            CreateMap<Student, StudentVM>().ReverseMap();
            CreateMap<StudentCourse, StudentCourseVM>().ReverseMap();
        }
    }
}
