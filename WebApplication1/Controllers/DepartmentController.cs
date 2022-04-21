using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.Models;

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
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }


        //httpGet: get detail of object
        public IActionResult Details(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Find(x => x.Id == id);
            return PartialView(department);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            return View();
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public IActionResult Create([Bind("Name, Manager")] Department newDepartment)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Create(newDepartment);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(newDepartment);
        }


        //httpGet: create view to edit object
        public IActionResult Update(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Find(x => x.Id == id);
            return View(department);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public IActionResult Update(Department modifiedDepartment)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Update(modifiedDepartment);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(modifiedDepartment);
        }


        //httpGet: delete
        public IActionResult Delete(int id)
        {
            try
            {
                var department = _unitOfWork.DepartmentRepository.Find(x => x.Id == id);
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(null, ex.InnerException.Message);
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
