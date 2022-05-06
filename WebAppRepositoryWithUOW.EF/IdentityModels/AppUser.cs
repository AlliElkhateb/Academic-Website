using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.EF.IdentityModels
{
    public class AppUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}
