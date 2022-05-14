using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.ViewModel;
using WebAppRepositoryWithUOW.EF.UnitOfWork;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //httpGet: get all Student
        public IActionResult Index()
        {
            var students = _unitOfWork.StudentRepository.GetAll(x => x.Department).OrderBy(x => x.Name);

            if (students is null)
            {
                return NotFound("no datat found");
            }

            return View(students);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var student = new StudentVM
            {
                Student = _unitOfWork.StudentRepository.GetObj(x => x.Id == id, x => x.Department),
                StudentCourses = _unitOfWork.StudentCourseRepository.GetAll(x => x.StudentId == id),
            };

            var listOfCourses = new List<Course>();
            foreach (var course in student.StudentCourses)
            {
                listOfCourses.Add(_unitOfWork.CourseRepository.GetObj(c => c.Id == course.CourseId));
            }
            student.Courses = listOfCourses;

            if (student.Student is null)
            {
                return NotFound("invalid id");
            }

            return View(student);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var student = new StudentVM
            {
                Student = new Student(),
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name),
            };

            return View(student);
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

            _unitOfWork.StudentRepository.Create(model.Student);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var student = new StudentVM
            {
                Student = _unitOfWork.StudentRepository.GetObj(x => x.Id == id),
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name),
            };

            if (student.Student is null)
            {
                return NotFound("invalid id");
            }

            return View(student);
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

            _unitOfWork.StudentRepository.Update(model.Student);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        //delete object from database
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var student = _unitOfWork.StudentRepository.GetObj(x => x.Id == id);

                if (student is null)
                {
                    return NotFound("invalid id");
                }

                _unitOfWork.StudentRepository.Delete(student);
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
