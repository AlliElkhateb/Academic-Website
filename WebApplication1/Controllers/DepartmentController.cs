using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.ViewModel;
using WebAppRepositoryWithUOW.EF.UnitOfWork;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //httpGet: get all departments
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);

            if (departments is null)
            {
                return NotFound("no datat found");
            }

            return View(departments);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var dapartment = new DepartmentVM
            {
                Department = _unitOfWork.DepartmentRepository.GetObj(x => x.Id == id),
                Courses = _unitOfWork.CourseRepository.GetAll(x => x.DepartmentId == id),
                Instructors = _unitOfWork.InstructorRepository.GetAll(x => x.DepartmentId == id),
                Students = _unitOfWork.StudentRepository.GetAll(x => x.DepartmentId == id),
            };

            if (dapartment.Department is null)
            {
                return NotFound("invalid id");
            }

            //return PartialView(dapartment);
            return View(dapartment);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var model = new DepartmentVM
            {
                Department = new Department()
            };

            return View(model);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] DepartmentVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _unitOfWork.DepartmentRepository.Create(model.Department);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var dapartment = new DepartmentVM
            {
                Department = _unitOfWork.DepartmentRepository.GetObj(x => x.Id == id)
            };

            if (dapartment.Department is null)
            {
                return NotFound("invalid id");
            }

            return View(dapartment);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] DepartmentVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _unitOfWork.DepartmentRepository.Update(model.Department);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        //delete object from database
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var dapartment = _unitOfWork.DepartmentRepository.GetObj(x => x.Id == id);

                if (dapartment is null)
                {
                    return NotFound("invalid id");
                }

                _unitOfWork.DepartmentRepository.Delete(dapartment);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                return BadRequest("can't delete this item since it's in use");
            }
        }


        //remote validation checks if name  exist
        //in form must be hidden field for (id) because remote take its parameter from input fields 
        public IActionResult NameExist(int id, string name)
        {
            var department = _unitOfWork.DepartmentRepository.GetObj(x => x.Name.ToLower() == name.ToLower());

            if (id == 0) //add new object
            {
                if (department is null) //name not exist
                {
                    return Json(true);
                }
                else //name already exist
                {
                    return Json(false);
                }
            }
            else //edit object
            {
                if (department is null) //name not exist
                {
                    return Json(true);
                }
                else //name already exist
                {

                    if (department.Id == id) //not change the name
                    {
                        return Json(true);
                    }
                    else //change name with name already exist
                    {
                        return Json(false);
                    }
                }

            }
        }
    }
}
