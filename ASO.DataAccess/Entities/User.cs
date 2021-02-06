using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ASO.DataAccess.Entities
{
    public class User : IdentityUser<long>
    {
        [Required] [MaxLength(100)] public string FirstName { get; set; }

        [Required] [MaxLength(100)] public string LastName { get; set; }

        [Required] public Sex Sex { get; set; }

        [MaxLength(100)] public string Patronymic { get; set; }
    }
}