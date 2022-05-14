using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.ViewModel;
using WebAppRepositoryWithUOW.EF.UnitOfWork;

namespace WebApplication1.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //httpGet: get all departments
        [Authorize]
        public IActionResult Index()
        {
            var courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);

            if (courses is null)
            {
                return NotFound("no datat found");
            }
            //var model = _mapper.Map<IEnumerable<CourseVM>>(courses);
            return View(courses);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var course = new CourseVM
            {
                Course = _unitOfWork.CourseRepository.GetObj(x => x.Id == id, x => x.Department),
                Instructors = _unitOfWork.InstructorRepository.GetAll(x => x.CourseId == id).OrderBy(x => x.Name),
                StudentCourses = _unitOfWork.StudentCourseRepository.GetAll(x => x.CourseId == id)
            };

            if (course.Course is null)
            {
                return NotFound("invalid id");
            }

            return View(course);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var model = new CourseVM
            {
                Course = new Course(),
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

            if (!ModelState.IsValid)
            {
                model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
                return View(model);
            }

            _unitOfWork.CourseRepository.Create(model.Course);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var model = new CourseVM
            {
                Course = _unitOfWork.CourseRepository.GetObj(x => x.Id == id),
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name)
            };

            if (model.Course is null)
            {
                return NotFound("invalid id");
            }

            return View(model);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] CourseVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
                return View(model);
            }

            _unitOfWork.CourseRepository.Update(model.Course);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        //delete object from database
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var course = _unitOfWork.CourseRepository.GetObj(x => x.Id == id);

                if (course is null)
                {
                    return NotFound("invalid id");
                }
                _unitOfWork.CourseRepository.Delete(course);
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
