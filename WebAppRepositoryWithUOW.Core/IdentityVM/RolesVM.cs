using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core.IdentityVM
{
    public class RolesVM
    {
        [Required,
         StringLength(50)]
        public string? Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
