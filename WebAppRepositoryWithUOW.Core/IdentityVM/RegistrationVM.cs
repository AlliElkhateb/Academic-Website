using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core.IdentityVM
{
    public class RegistrationVM
    {
        [Required(ErrorMessage = "First Name required"),
         Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name required"),
         Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email required"),
         EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password required"),
         DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password required"),
         DataType(DataType.Password),
         Compare(otherProperty: "Password", ErrorMessage = "Confirm Password should be the same of Password"),
         Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Profile Picture")]
        public byte[]? ProfilePicture { get; set; }


        [Display(Name = "remember me")]
        public bool RememberMe { get; set; }
    }
}
