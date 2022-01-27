using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack.Models
{
    public class AppUser : IdentityUser
    {
        [Required, StringLength(maximumLength: 50)]
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}