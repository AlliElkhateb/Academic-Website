using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;

namespace WebApplication1.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var courses = _unitOfWork.CourseRepository.GetAllAsync(x => x.Department);
            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View();
        }
    }
}
