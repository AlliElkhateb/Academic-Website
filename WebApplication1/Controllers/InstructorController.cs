using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.Core.ViewModel;

namespace WebApplication1.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InstructorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //httpGet: get all departments
        public IActionResult Index()
        {
            var instructors = _unitOfWork.InstructorRepository.GetAll();
            var result = _mapper.Map<IEnumerable<InstructorVM>>(instructors).OrderBy(x => x.Name);
            foreach (var instructor in result)
            {
                instructor.Course = _unitOfWork.CourseRepository.Find(x => x.Id == instructor.CourseId);
            }
            return View(result);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var instructor = _unitOfWork.InstructorRepository.Find(x => x.Id == id);
            var result = _mapper.Map<InstructorVM>(instructor);
            result.Course = _unitOfWork.CourseRepository.Find(x => x.Id == instructor.CourseId);
            result.Department = _unitOfWork.DepartmentRepository.Find(x => x.Id == instructor.DepartmentId);
            return View(result);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var newInstructor = new InstructorVM
            {
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name),
                Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name)
            };
            return View(newInstructor);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public IActionResult Create([FromForm] InstructorVM newInstructor)
        {
            //Bind("Name, Age, Address, Salary, Image, CourseId, DepartmentId")
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Instructor>(newInstructor);
                _unitOfWork.InstructorRepository.Create(result);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            newInstructor.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
            newInstructor.Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);
            return View(newInstructor);
        }


        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var instructor = _unitOfWork.InstructorRepository.Find(x => x.Id == id);
            var result = _mapper.Map<InstructorVM>(instructor);
            result.Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);
            result.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
            return View(result);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public IActionResult Update([FromForm] InstructorVM modifiedInstructor)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Instructor>(modifiedInstructor);
                _unitOfWork.InstructorRepository.Update(result);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            modifiedInstructor.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
            modifiedInstructor.Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);
            return View(modifiedInstructor);
        }


        //httpGet: delete
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var instructor = new Instructor { Id = id };
                _unitOfWork.InstructorRepository.Delete(instructor);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(null, ex.InnerException.Message);
                return View(nameof(Index));
            }
        }
    }
}
