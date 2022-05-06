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
            var model = _mapper.Map<IEnumerable<InstructorVM>>(instructors).OrderBy(x => x.Name);
            foreach (var instructor in model)
            {
                instructor.Course = _unitOfWork.CourseRepository.Find(x => x.Id == instructor.CourseId);
            }
            return View(model);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var instructor = _unitOfWork.InstructorRepository.Find(x => x.Id == id);
            var model = _mapper.Map<InstructorVM>(instructor);
            model.Course = _unitOfWork.CourseRepository.Find(x => x.Id == instructor.CourseId);
            model.Department = _unitOfWork.DepartmentRepository.Find(x => x.Id == instructor.DepartmentId);
            return View(model);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var model = new InstructorVM
            {
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name),
                Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name)
            };
            return View(model);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] InstructorVM model)
        {
            if (!ModelState.IsValid)   //check model state
            {
                model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);   //dropdown list for departments
                model.Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);   //dropdown list for course
                return View(model);
            }

            var obj = _mapper.Map<Instructor>(model);
            _unitOfWork.InstructorRepository.Create(obj);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }


        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var instructor = _unitOfWork.InstructorRepository.Find(x => x.Id == id);
            var model = _mapper.Map<InstructorVM>(instructor);
            model.Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);
            model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
            return View(model);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] InstructorVM model)
        {
            if (!ModelState.IsValid)   //check model state
            {
                model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);   //dropdown list for departments
                model.Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);   //dropdown list for course
                return View(model);
            }

            var obj = _mapper.Map<Instructor>(model);
            _unitOfWork.InstructorRepository.Update(obj);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }


        //delete object from database
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var obj = new Instructor { Id = id };
                _unitOfWork.InstructorRepository.Delete(obj);
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
