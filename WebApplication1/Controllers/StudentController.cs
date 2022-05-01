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
            var result = _mapper.Map<IEnumerable<StudentVM>>(students);
            return View(result);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var student = _unitOfWork.StudentRepository.Find(x => x.Id == id);
            var result = _mapper.Map<StudentVM>(student);
            result.Department = _unitOfWork.DepartmentRepository.Find(x => x.Id == student.DepartmentId);
            result.StudentCourses = _unitOfWork.StudentCourseRepository.GetAll(x => x.StudentId == student.Id, x => x.Course); 
            return View(result);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var newStudent = new StudentVM
            {
                Departments = _unitOfWork.DepartmentRepository.GetAll()
            };
            return View(newStudent);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public IActionResult Create([FromForm] StudentVM newStudent)
        {
            //Bind("Name, MaxDegree, MinDegree, DepartmentId"),
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Student>(newStudent);
                _unitOfWork.StudentRepository.Create(result);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(newStudent);
        }

        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var student = _unitOfWork.DepartmentRepository.Find(x => x.Id == id);
            var result = _mapper.Map<StudentVM>(student);
            result.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(result);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public IActionResult Update([FromForm] StudentVM modifiedStudent)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Student>(modifiedStudent);
                _unitOfWork.StudentRepository.Update(result);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(modifiedStudent);
        }


        //httpGet: delete
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var student = new Student { Id = id };
                _unitOfWork.StudentRepository.Delete(student);
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
