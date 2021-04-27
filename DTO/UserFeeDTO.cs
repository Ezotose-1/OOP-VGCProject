using OOP_VGCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.DTO
{
    public class UserFeeDTO
    {
        public Fees Fee { get; set; }
        public bool IsPaid { get; set; }
    }
}
