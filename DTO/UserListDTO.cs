using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.DTO
{
    public class UserListDTO : IdentityUser
    {
        public string Role { get; set; }
    }
}
