using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.Core.ViewModel;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //httpGet: get all Student
        public IActionResult Index()
        {
            var students = _unitOfWork.StudentRepository.GetAll();
            var model = _mapper.Map<IEnumerable<StudentVM>>(students).OrderBy(x => x.Name);
            foreach (var student in model)
            {
                student.Department = _unitOfWork.DepartmentRepository.Find(x => x.Id == student.DepartmentId);
            }
            return View(model);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var student = _unitOfWork.StudentRepository.Find(x => x.Id == id);
            var model = _mapper.Map<StudentVM>(student);
            model.Department = _unitOfWork.DepartmentRepository.Find(x => x.Id == student.DepartmentId);

            model.StudentCourses = _unitOfWork.StudentCourseRepository.GetAll(x => x.StudentId == id);
            var listOfCourses = new List<Course>();
            foreach (var course in model.StudentCourses)
            {
                listOfCourses.Add(_unitOfWork.CourseRepository.Find(c => c.Id == course.CourseId));
            }
            model.Courses = listOfCourses;
            return View(model);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var model = new StudentVM
            {
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name),
            };
            return View(model);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] StudentVM model)
        {
            if (!ModelState.IsValid)   //check model state
            {
                model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);   //dropdown list of departments
                return View(model);
            }

            var obj = _mapper.Map<Student>(model);
            _unitOfWork.StudentRepository.Create(obj);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var student = _unitOfWork.StudentRepository.Find(x => x.Id == id);
            var model = _mapper.Map<StudentVM>(student);
            model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
            return View(model);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] StudentVM model)
        {
            if (!ModelState.IsValid)   //check model state
            {
                model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);   //dropdown list of departments
                return View(model);
            }

            var obj = _mapper.Map<Student>(model);
            _unitOfWork.StudentRepository.Update(obj);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }


        //delete object from database
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var obj = new Student { Id = id };
                _unitOfWork.StudentRepository.Delete(obj);
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
