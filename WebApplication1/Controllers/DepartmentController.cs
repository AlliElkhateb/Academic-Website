using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.Core.ViewModel;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //httpGet: get all departments
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var result = _mapper.Map<IEnumerable<DepartmentVM>>(departments).OrderBy(x => x.Name);
            return View(result);
        }


        //httpGet: get detail of object
        [HttpGet(template: "{id:int}")]
        public IActionResult Details([FromRoute] int id)
        {
            var department = _unitOfWork.DepartmentRepository.Find(x => x.Id == id);
            var result = _mapper.Map<DepartmentVM>(department);
            result.Courses = _unitOfWork.CourseRepository.GetAll(x => x.DepartmentId == department.Id);
            result.Instructors = _unitOfWork.InstructorRepository.GetAll(x => x.DepartmentId == department.Id);
            result.Students = _unitOfWork.StudentRepository.GetAll(x => x.DepartmentId == department.Id);
            //return PartialView(result);
            return View(result);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var newDepartment = new DepartmentVM();
            return View(newDepartment);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public IActionResult Create([FromForm] DepartmentVM newDepartment)
        {
            //Bind("Name, Manager")
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Department>(newDepartment);
                _unitOfWork.DepartmentRepository.Create(result);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(newDepartment);
        }


        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var department = _unitOfWork.DepartmentRepository.Find(x => x.Id == id);
            var result = _mapper.Map<DepartmentVM>(department);
            return View(result);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public IActionResult Update([FromForm] DepartmentVM modifiedDepartment)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Department>(modifiedDepartment);
                _unitOfWork.DepartmentRepository.Update(result);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(modifiedDepartment);
        }


        //httpGet: delete
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var department = new Department { Id = id };
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(null, exception.InnerException.Message);
                return View(nameof(Index));
            }
        }


        //remote validation checks if name  exist
        //in form must be hidden field for (id) because remote take its parameter from input fields 
        public IActionResult NameExist(int id, string name)
        {
            var department = _unitOfWork.DepartmentRepository.Find(x => x.Name.ToLower() == name.ToLower());

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
