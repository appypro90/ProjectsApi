using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User: IdentityUser
    {
        public ICollection<Project>? OwnedProjects { get; set; }
    }
}
