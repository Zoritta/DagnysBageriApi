
using Microsoft.AspNetCore.Identity;
namespace DagnysBageriApi.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } = "User";
    }
}