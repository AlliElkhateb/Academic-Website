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
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }


        //httpGet: get detail of object
        public async Task<IActionResult> Details(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetObjectAsync(x => x.Id == id);
            return PartialView(department);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            return View();
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Manager")] Department newDepartment)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.DepartmentRepository.CreateAsync(newDepartment);
                await _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(newDepartment);
        }


        //httpGet: create view to edit object
        public async Task<IActionResult> Update(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetObjectAsync(x => x.Id == id);
            return View(department);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        public async Task<IActionResult> Update(Department modifiedDepartment)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Update(modifiedDepartment);
                await _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(modifiedDepartment);
        }


        //httpGet: delete
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var department = await _unitOfWork.DepartmentRepository.GetObjectAsync(x => x.Id == id);
                _unitOfWork.DepartmentRepository.Delete(department);
                await _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(null, ex.InnerException.Message);
                return View(nameof(Details));
            }
        }


        //remote validation checks if name  exist
        //in form must be hidden field for (id) because remote take its parameter from input fields 
        public async Task<IActionResult> NameExist(int id, string name)
        {
            var department = await _unitOfWork.DepartmentRepository.GetObjectAsync(x => x.Name.ToLower() == name.ToLower());

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
