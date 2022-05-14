using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.ViewModel;
using WebAppRepositoryWithUOW.EF.UnitOfWork;

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
            var instructors = _unitOfWork.InstructorRepository.GetAll(x => x.Course).OrderBy(x => x.Name);

            if (instructors is null)
            {
                return NotFound("no datat found");
            }

            //foreach (var instructor in instructors)
            //{
            //    instructor.Course = _unitOfWork.CourseRepository.GetObj(x => x.Id == instructor.CourseId);
            //}
            return View(instructors);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var instructor = new InstructorVM
            {
                Instructor = _unitOfWork.InstructorRepository.GetObj(x => x.Id == id, x => x.Course, x => x.Department),
            };

            if (instructor.Instructor is null)
            {
                return NotFound("invalid id");
            }

            return View(instructor);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var model = new InstructorVM
            {
                Instructor = new Instructor(),
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

            _unitOfWork.InstructorRepository.Create(model.Instructor);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var instructor = new InstructorVM
            {
                Instructor = _unitOfWork.InstructorRepository.GetObj(x => x.Id == id),
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name),
                Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name)
            };

            if (instructor.Instructor is null)
            {
                return NotFound("invalid id");
            }

            return View(instructor);
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

            _unitOfWork.InstructorRepository.Update(model.Instructor);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        //delete object from database
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var instructor = _unitOfWork.InstructorRepository.GetObj(x => x.Id == id);

                if (instructor is null)
                {
                    return NotFound("invalid id");
                }
                _unitOfWork.InstructorRepository.Delete(instructor);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                return BadRequest("can't delete this item since it's in use");
            }
        }
    }
}
