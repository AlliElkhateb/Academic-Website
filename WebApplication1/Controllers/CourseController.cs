using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.Core.ViewModel;

namespace WebApplication1.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //httpGet: get all departments
        public IActionResult Index()
        {
            var courses = _unitOfWork.CourseRepository.GetAll();
            var result = _mapper.Map<IEnumerable<CourseVM>>(courses).OrderBy(x => x.Name);
            return View(result);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var course = _unitOfWork.CourseRepository.Find(x => x.Id == id);
            var result = _mapper.Map<CourseVM>(course);
            result.Instructors = _unitOfWork.InstructorRepository.GetAll(x => x.CourseId == course.Id).OrderBy(x => x.Name);
            result.Department = _unitOfWork.DepartmentRepository.Find(x => x.Id == course.DepartmentId);
            result.StudentCourses = _unitOfWork.StudentCourseRepository.GetAll(x => x.CourseId == course.Id);
            return View(result);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var newCourse = new CourseVM
            {
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name)
            };
            return View(newCourse);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] CourseVM model)
        {
            //Bind("Name, MaxDegree, MinDegree, DepartmentId"),
            model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _mapper.Map<Course>(model);
            _unitOfWork.CourseRepository.Create(result);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var course = _unitOfWork.CourseRepository.Find(x => x.Id == id);
            var result = _mapper.Map<CourseVM>(course);
            result.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
            return View(result);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] CourseVM model)
        {
            model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _mapper.Map<Course>(model);
            _unitOfWork.CourseRepository.Update(result);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        //httpGet: delete
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var course = new Course { Id = id };
                _unitOfWork.CourseRepository.Delete(course);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(null, exception.InnerException.Message);
                return View(nameof(Index));
            }
        }

    }
}
