using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Index()
        {
            var courses = _unitOfWork.CourseRepository.GetAll();
            var model = _mapper.Map<IEnumerable<CourseVM>>(courses).OrderBy(x => x.Name);
            return View(model);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var course = _unitOfWork.CourseRepository.Find(x => x.Id == id);
            var model = _mapper.Map<CourseVM>(course);
            model.Instructors = _unitOfWork.InstructorRepository.GetAll(x => x.CourseId == course.Id).OrderBy(x => x.Name);
            model.Department = _unitOfWork.DepartmentRepository.Find(x => x.Id == course.DepartmentId);
            model.StudentCourses = _unitOfWork.StudentCourseRepository.GetAll(x => x.CourseId == course.Id);
            return View(model);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var model = new CourseVM
            {
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name)
            };
            return View(model);
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

            var obj = _mapper.Map<Course>(model);
            _unitOfWork.CourseRepository.Create(obj);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var course = _unitOfWork.CourseRepository.Find(x => x.Id == id);
            var model = _mapper.Map<CourseVM>(course);
            model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
            return View(model);
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

            var obj = _mapper.Map<Course>(model);
            _unitOfWork.CourseRepository.Update(obj);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }


        //delete object from database
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var obj = new Course { Id = id };
                _unitOfWork.CourseRepository.Delete(obj);
                _unitOfWork.Commit();
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
