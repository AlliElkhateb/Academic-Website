using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core.IdentityVM;
using WebAppRepositoryWithUOW.EF.IdentityModels;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Registration()
        {
            var model = new RegistrationVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromForm] RegistrationVM model, IFormFile? file)
        {
            #region ImageValidation

            var files = Request.Form.Files;   //get files from request

            if (!files.Any())    //check if ther are any files in request
            {
                ModelState.AddModelError("ProfilePicture", "you should insert image");
                return View(model);
            }

            var img = files.FirstOrDefault();   //get file from request

            using var dataStream = new MemoryStream();   //creates streams that have memory as a backing store instead of a disk or a network connection 

            img.CopyTo(dataStream);    //copy image to stream memory as a backing store

            model.ProfilePicture = dataStream.ToArray();    //convert file to byte array and save it

            var extentions = new List<string> { ".jpg", ".png" };

            if (!extentions.Contains(Path.GetExtension(img.FileName).ToLower()))    //check file extention
            {
                ModelState.AddModelError("imagProfilePicturee", "only .jpg, .png image are allowed");
                return View(model);
            }

            if (img.Length > 2097152)    //check file size
            {
                ModelState.AddModelError("image", "image can not be more than 2 MB");
                return View(model);
            }

            #endregion

            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    ProfilePicture = model.ProfilePicture,
                    UserName = $"{model.FirstName}_{model.LastName}"
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    Response.Cookies.Append("userId", user.Id);
                    //if (string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    //{
                    //    return LocalRedirect(ReturnUrl);
                    //}
                    //else
                    //{
                    //    return RedirectToAction("index", "home");
                    //}
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public IActionResult AdminRegistration()
        {
            var model = new RegistrationVM();
            return View(nameof(Registration), model);
        }

        [HttpPost]
        public async Task<IActionResult> AdminRegistration([FromForm] RegistrationVM model)
        {
            #region ImageValidation

            var files = Request.Form.Files;   //get files from request

            if (!files.Any())    //check if ther are any files in request
            {
                ModelState.AddModelError("ProfilePicture", "you should insert image");
                return View(model);
            }

            var img = files.FirstOrDefault();   //get file from request

            using var dataStream = new MemoryStream();   //creates streams that have memory as a backing store instead of a disk or a network connection 

            img.CopyTo(dataStream);    //copy image to stream memory as a backing store

            model.ProfilePicture = dataStream.ToArray();    //convert file to byte array and save it

            var extentions = new List<string> { ".jpg", ".png" };

            if (!extentions.Contains(Path.GetExtension(img.FileName).ToLower()))    //check file extention
            {
                ModelState.AddModelError("imagProfilePicturee", "only .jpg, .png image are allowed");
                return View(model);
            }

            if (img.Length > 2097152)    //check file size
            {
                ModelState.AddModelError("image", "image can not be more than 2 MB");
                return View(model);
            }

            #endregion

            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    ProfilePicture = model.ProfilePicture,
                    UserName = $"{model.FirstName}_{model.LastName}"
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "admin");
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    Response.Cookies.Append("userId", user.Id);
                    //if (string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    //{
                    //    return LocalRedirect(ReturnUrl);
                    //}
                    //else
                    //{
                    //    return RedirectToAction("index", "home");
                    //}
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new EditNameMail
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditNameMail model)
        {
            #region ImageValidation

            var files = Request.Form.Files;   //get files from request

            if (!files.Any())    //check if ther are any files in request
            {
                ModelState.AddModelError("ProfilePicture", "you should insert image");
                return View(model);
            }

            var img = files.FirstOrDefault();   //get file from request

            using var dataStream = new MemoryStream();   //creates streams that have memory as a backing store instead of a disk or a network connection 

            img.CopyTo(dataStream);    //copy image to stream memory as a backing store

            model.ProfilePicture = dataStream.ToArray();    //convert file to byte array and save it

            var extentions = new List<string> { ".jpg", ".png" };

            if (!extentions.Contains(Path.GetExtension(img.FileName).ToLower()))    //check file extention
            {
                ModelState.AddModelError("imagProfilePicturee", "only .jpg, .png image are allowed");
                return View(model);
            }

            if (img.Length > 2097152)    //check file size
            {
                ModelState.AddModelError("image", "image can not be more than 2 MB");
                return View(model);
            }

            #endregion

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.ProfilePicture = model.ProfilePicture;
                user.UserName = $"{model.FirstName}_{model.LastName}";
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    //Response.Cookies.Append("userId", user.Id);
                    //if (string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    //{
                    //    return LocalRedirect(ReturnUrl);
                    //}
                    //else
                    //{
                    //    return RedirectToAction("index", "home");
                    //}
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError(string.Empty, "invalid operation");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            var model = new LoginVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        //Response.Cookies.Append("userId", user.Id);
                        //if (string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        //{
                        //    return LocalRedirect(ReturnUrl);
                        //}
                        //else
                        //{
                        //    return RedirectToAction("index", "home");
                        //}
                        return RedirectToAction("index", "home");
                    }
                    ModelState.AddModelError("password", "invalid password");
                }
                ModelState.AddModelError("email", "invalid email");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //if (string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            //{
            //    return LocalRedirect(ReturnUrl);
            //}
            //else
            //{
            //    return RedirectToAction("index", "home");
            //}
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Profile([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is not null)
            {
                if (User.IsInRole("instructor"))
                    return RedirectToAction("create", "instructor", new { user });
                if (User.IsInRole("student"))
                    return RedirectToAction("create", "student", new { user });
                return View(user);
            }
            return NotFound();
        }
    }
}
