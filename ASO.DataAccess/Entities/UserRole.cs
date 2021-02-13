using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ASO.DataAccess.Entities
{
    public class UserRole : IdentityRole<long>
    {
        [Required] [MaxLength(100)] public string DisplayName { get; set; }

        public List<User> Users { get; set; }
    }
}