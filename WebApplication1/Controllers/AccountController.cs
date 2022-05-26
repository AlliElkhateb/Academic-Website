using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppRepositoryWithUOW.Core.IdentityVM;
using WebAppRepositoryWithUOW.EF.IdentityModels;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.Select(user => new UserVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToList();

            if (users is null)
            {
                return NotFound("no data found");
            }

            return View(users);
        }

        public async Task<IActionResult> ManageUserRoles(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            if (user is null)
            {
                return NotFound("no user found");
            }

            var roles = await _roleManager.Roles.ToListAsync();

            var model = new UserRolesVM
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RolesVM
                {
                    Name = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(UserRolesVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null)
            {
                return NotFound("no user found");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

                if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UserForm(string? userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)   //add new user
            {
                var model = new RegisterFormVM
                {
                    Roles = _roleManager.Roles.Select(r => new RolesVM
                    {
                        Name = r.Name,
                    }).ToListAsync().Result
                };

                return View(model);
            }
            else   //manage existing user
            {
                var roles = await _roleManager.Roles.ToListAsync();

                var model = new RegisterFormVM
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.Select(role => new RolesVM
                    {
                        Name = role.Name,
                        IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                    }).ToList()
                };

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserForm([FromForm] RegisterFormVM model)
        {
            if (!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("roles", "at least one role should be chosen.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                if (model.UserId is null)   //add new user
                {
                    var user = new AppUser
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = $"{model.FirstName}_{model.LastName}",
                        Email = model.Email,
                    };

                    var userWithSameUserName = await _userManager.FindByNameAsync(user.UserName);

                    if (userWithSameUserName is not null)
                    {
                        ModelState.AddModelError("UserName", "this User Name already exists.");
                        model.UserName = user.UserName;
                        return View(model);
                    }

                    var userWithSameEmail = await _userManager.FindByEmailAsync(user.Email);

                    if (userWithSameEmail is not null)
                    {
                        ModelState.AddModelError("Email", "this email already exists.");
                        model.UserName = user.UserName;
                        return View(model);
                    }

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(role => role.Name));
                        //await _signInManager.SignInAsync(user, model.RememberMe);
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else   //manage existing user
                {
                    var user = await _userManager.FindByIdAsync(model.UserId);

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = $"{model.FirstName}_{model.LastName}";
                    user.Email = model.Email;

                    var userWithSameUserName = await _userManager.FindByNameAsync(user.UserName);

                    if (userWithSameUserName is not null && userWithSameUserName.Id != model.UserId)
                    {
                        ModelState.AddModelError("UserName", "this User Name already exists.");
                        model.UserName = user.UserName;
                        return View(model);
                    }

                    var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

                    if (userWithSameEmail is not null && userWithSameEmail.Id != model.UserId)
                    {
                        ModelState.AddModelError("Email", "this email already exists.");
                        model.UserName = user.UserName;
                        return View(model);
                    }

                    var userRoles = await _userManager.GetRolesAsync(user);

                    foreach (var role in model.Roles)
                    {
                        if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
                        {
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                        }

                        if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
                        {
                            await _userManager.AddToRoleAsync(user, role.Name);
                        }
                    }

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public void ImageValidation(RegisterFormVM model)
        {
            #region ImageValidation

            var files = Request.Form.Files;   //get files from request

            if (!files.Any())    //check if ther are any files in request
            {
                ModelState.AddModelError("ProfilePicture", "you should insert image");
                //return View(model);
            }

            var img = files.FirstOrDefault();   //get file from request

            using var dataStream = new MemoryStream();   //creates streams that have memory as a backing store instead of a disk or a network connection 

            img.CopyTo(dataStream);    //copy image to stream memory as a backing store

            //model.ProfilePicture = dataStream.ToArray();    //convert file to byte array and save it

            var extentions = new List<string> { ".jpg", ".png" };

            if (!extentions.Contains(Path.GetExtension(img.FileName).ToLower()))    //check file extention
            {
                ModelState.AddModelError("imagProfilePicturee", "only .jpg, .png image are allowed");
                //return View(model);
            }

            if (img.Length > 2097152)    //check file size
            {
                ModelState.AddModelError("image", "image can not be more than 2 MB");
                //return View(model);
            }

            #endregion

        }

    }
}
