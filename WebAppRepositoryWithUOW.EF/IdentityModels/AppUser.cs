using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.EF.IdentityModels
{
    public class AppUser : IdentityUser
    {
        [Required, MaxLength(50), Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required, MaxLength(50), Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Profile Picture")]
        public byte[]? ProfilePicture { get; set; }
    }
}
