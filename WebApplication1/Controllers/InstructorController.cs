using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.Models;

namespace WebApplication1.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public InstructorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //httpGet: get all departments
        public IActionResult Index()
        {
            var instructor = _unitOfWork.InstructorRepository.GetAll(x => x.Department, x => x.Course);
            return View(instructor);
        }


        //httpGet: get detail of object
        public IActionResult Details(int id)
        {
            var instructor = _unitOfWork.InstructorRepository.Find(x => x.Id == id, x => x.Department, x => x.Course);
            return View(instructor);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var newInstructor = new Instructor();
            return View(newInstructor);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public IActionResult Create([Bind("Name, Age, Address, Salary, Image, CourseId, DepartmentId")] Instructor newInstructor)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.InstructorRepository.Create(newInstructor);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(newInstructor);
        }


        //httpGet: create view to edit object
        public IActionResult Update(int id)
        {
            var instructor = _unitOfWork.InstructorRepository.Find(x => x.Id == id);
            return View(instructor);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public IActionResult Update(Instructor modifiedInstructor)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.InstructorRepository.Update(modifiedInstructor);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(modifiedInstructor);
        }


        //httpGet: delete
        public IActionResult Delete(int id)
        {
            try
            {
                var instructor = _unitOfWork.InstructorRepository.Find(x => x.Id == id);
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
