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
            var model = _mapper.Map<IEnumerable<DepartmentVM>>(departments).OrderBy(x => x.Name);
            return View(model);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var department = _unitOfWork.DepartmentRepository.Find(x => x.Id == id);
            var model = _mapper.Map<DepartmentVM>(department);
            model.Courses = _unitOfWork.CourseRepository.GetAll(x => x.DepartmentId == department.Id);
            model.Instructors = _unitOfWork.InstructorRepository.GetAll(x => x.DepartmentId == department.Id);
            model.Students = _unitOfWork.StudentRepository.GetAll(x => x.DepartmentId == department.Id);
            //return PartialView(model);
            return View(model);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var model = new DepartmentVM();
            return View(model);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] DepartmentVM model)
        {
            //Bind("Name, Manager")
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var obj = _mapper.Map<Department>(model);
            _unitOfWork.DepartmentRepository.Create(obj);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }


        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var department = _unitOfWork.DepartmentRepository.Find(x => x.Id == id);
            var model = _mapper.Map<DepartmentVM>(department);
            return View(model);
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

            var obj = _mapper.Map<Department>(model);
            _unitOfWork.DepartmentRepository.Update(obj);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }


        //delete object from database
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var obj = new Department { Id = id };
                _unitOfWork.DepartmentRepository.Delete(obj);
                _unitOfWork.Commit();
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
