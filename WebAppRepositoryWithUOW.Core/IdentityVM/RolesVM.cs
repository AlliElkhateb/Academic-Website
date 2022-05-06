using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core.IdentityVM
{
    public class RolesVM
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}
