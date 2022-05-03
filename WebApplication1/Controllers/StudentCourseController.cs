using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;

namespace WebApplication1.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StudentCourseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            //if (!Request.Form["ListOfCoursesId"].Any())   //check if student did no choose any courses
            //{
            //    ModelState.AddModelError("ListOfCoursesId", "you should select courses");
            //    model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);

            //    model.Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);

            //    var listOfCourseId = new List<int>();
            //    foreach (var course in Request.Form["ListOfCoursesId"])
            //    {
            //        listOfCourseId.Add(int.Parse(course));
            //    }
            //    model.ListOfCoursesId = listOfCourseId;

            //    return View(model);
            //}

            //foreach (var courseId in Request.Form["ListOfCoursesId"])   //to add record for student with course
            //{
            //    _unitOfWork.StudentCourseRepository.Create(new StudentCourse { StudentId = result.Id, CourseId = int.Parse(courseId) });
            //}


            return View();
        }
    }
}
