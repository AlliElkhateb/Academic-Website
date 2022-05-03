using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.Core.ViewModel;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //httpGet: get all Student
        public IActionResult Index()
        {
            var students = _unitOfWork.StudentRepository.GetAll();
            var result = _mapper.Map<IEnumerable<StudentVM>>(students).OrderBy(x => x.Name);
            foreach (var student in result)
            {
                student.Department = _unitOfWork.DepartmentRepository.Find(x => x.Id == student.DepartmentId);
            }
            return View(result);
        }


        //httpGet: get detail of object
        public IActionResult Details([FromRoute] int id)
        {
            var student = _unitOfWork.StudentRepository.Find(x => x.Id == id);
            var result = _mapper.Map<StudentVM>(student);
            result.Department = _unitOfWork.DepartmentRepository.Find(x => x.Id == student.DepartmentId);

            result.StudentCourses = _unitOfWork.StudentCourseRepository.GetAll(x => x.StudentId == id);
            var listOfCourses = new List<Course>();
            foreach (var course in result.StudentCourses)
            {
                listOfCourses.Add(_unitOfWork.CourseRepository.Find(c => c.Id == course.CourseId));
            }
            result.Courses = listOfCourses;
            return View(result);
        }


        //httpGet: create view to add new object
        public IActionResult Create()
        {
            var newStudent = new StudentVM
            {
                Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name),
            };
            return View(newStudent);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] StudentVM model)
        {
            //Bind("Name, MaxDegree, MinDegree, DepartmentId"),

             model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);   //dropdown list of departments

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

            model.Image = dataStream.ToArray();     //convert file to byte array and save it

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

            var result = _mapper.Map<Student>(model);
            _unitOfWork.StudentRepository.Create(result);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //httpGet: create view to edit object
        public IActionResult Update([FromRoute] int id)
        {
            var student = _unitOfWork.StudentRepository.Find(x => x.Id == id);
            var result = _mapper.Map<StudentVM>(student);
            result.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);
            return View(result);
        }


        //httpPost: check for validation and confirm save data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] StudentVM model)
        {
            model.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(x => x.Name);   //dropdown list of departments

            if (!ModelState.IsValid)   //check model state
            {
                return View(model);
            }

            var files = HttpContext.Request.Form.Files;    //get files from request

            if (!files.Any())    //check if ther are any files in request
            {
                ModelState.AddModelError("image", "you should insert image");
                return View(model);
            }

            var img = files.FirstOrDefault();   //get file from request

            using var dataStream = new MemoryStream();   //creates streams that have memory as a backing store instead of a disk or a network connection 

            img.CopyTo(dataStream);   //copy image to stream memory as a backing store

            model.Image = dataStream.ToArray();   //convert file to byte array and save it

            var extentions = new List<string> { ".jpg", ".png" };

            if (!extentions.Contains(Path.GetExtension(img.FileName).ToLower()))   //check file extention
            {
                ModelState.AddModelError("image", "only .jpg, .png image are allowed");
                return View(model);
            }

            if (img.Length > 2097152)   //check file size
            {
                ModelState.AddModelError("image", "image can not be more than 2 MB");
                return View(model);
            }

            var result = _mapper.Map<Student>(model);
            _unitOfWork.StudentRepository.Update(result);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        //httpGet: delete
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var student = new Student { Id = id };
                _unitOfWork.StudentRepository.Delete(student);
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
