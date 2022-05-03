using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.Core.ViewModel;

namespace WebApplication1.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InstructorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //httpGet: get all departments
        public IActionResult Index()
        {
            var instructors = _unitOfWork.InstructorRepository.GetAll();
            var result = _mapper.Map<IEnumerable<InstructorVM>>(instructors).OrderBy(x => x.Name);
            foreach (var instructor in result)
            {
                instructor.Course = _unitOfWork.CourseRepository.Find(x => x.Id == instructor.CourseId);
            }
            return View(result);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var instructor = _unitOfWork.InstructorRepository.Find(x => x.Id == id);
            var result = _mapper.Map<InstructorVM>(instructor);
            result.Course = _unitOfWork.CourseRepository.Find(x => x.Id == instructor.CourseId);
            result.Department = _unitOfWork.DepartmentRepository.Find(x => x.Id == instructor.DepartmentId);
            return View(result);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var newInstructor = new InstructorVM
            {
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name),
                Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name)
            };
            return View(newInstructor);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] InstructorVM model)
        {
            //Bind("Name, Age, Address, Salary, Image, CourseId, DepartmentId")
            model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);   //dropdown list for departments
            model.Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);   //dropdown list for course

            if (!ModelState.IsValid)   //check model state
            {
                return View(model);
            }

            var files = Request.Form.Files;   //get files from request

            if (!files.Any())    //check if ther are any files in request
            {
                ModelState.AddModelError("image", "you should insert image");
                return View(model);
            }

            var img = files.FirstOrDefault();   //get file from request

            using var dataStream = new MemoryStream();   //creates streams that have memory as a backing store instead of a disk or a network connection 

            img.CopyTo(dataStream);    //copy image to stream memory as a backing store

            model.Image = dataStream.ToArray();    //convert file to byte array and save it

            var extentions = new List<string> { ".jpg", ".png" };

            if (!extentions.Contains(Path.GetExtension(img.FileName).ToLower()))    //check file extention
            {
                ModelState.AddModelError("image", "only .jpg, .png image are allowed");
                return View(model);
            }

            if (img.Length > 2097152)    //check file size
            {
                ModelState.AddModelError("image", "image can not be more than 2 MB");
                return View(model);
            }

            var result = _mapper.Map<Instructor>(model);
            _unitOfWork.InstructorRepository.Create(result);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var instructor = _unitOfWork.InstructorRepository.Find(x => x.Id == id);
            var result = _mapper.Map<InstructorVM>(instructor);
            result.Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);
            result.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
            return View(result);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] InstructorVM model)
        {
            model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);   //dropdown list for departments
            model.Courses = _unitOfWork.CourseRepository.GetAll().OrderBy(x => x.Name);   //dropdown list for course

            if (!ModelState.IsValid)   //check model state
            {
                return View(model);
            }

            var files = Request.Form.Files;   //get files from request

            if (!files.Any())    //check if ther are any files in request
            {
                ModelState.AddModelError("image", "you should insert image");
                return View(model);
            }

            var img = files.FirstOrDefault();    //get file from request

            using var dataStream = new MemoryStream();   //creates streams that have memory as a backing store instead of a disk or a network connection 

            img.CopyTo(dataStream);    //copy image to stream memory as a backing store

            model.Image = dataStream.ToArray();    //convert file to byte array and save it

            var extentions = new List<string> { ".jpg", ".png" };

            if (!extentions.Contains(Path.GetExtension(img.FileName).ToLower()))    //check file extention
            {
                ModelState.AddModelError("image", "only .jpg, .png image are allowed");
                return View(model);
            }

            if (img.Length > 2097152)    //check file size
            {
                ModelState.AddModelError("image", "image can not be more than 2 MB");
                return View(model);
            }

            var result = _mapper.Map<Instructor>(model);
            _unitOfWork.InstructorRepository.Update(result);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        //httpGet: delete
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var instructor = new Instructor { Id = id };
                _unitOfWork.InstructorRepository.Delete(instructor);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(null, exception.InnerException.Message);
                return View(nameof(Index));
            }
        }
    }
}
