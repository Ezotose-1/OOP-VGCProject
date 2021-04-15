using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CurrentClass { get; set; }
    }
}
