using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core.IdentityVM
{
    public class EditNameMail
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


        [Display(Name = "Profile Picture")]
        public byte[]? ProfilePicture { get; set; }
    }
}
