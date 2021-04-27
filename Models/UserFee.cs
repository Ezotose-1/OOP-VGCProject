using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.Models
{
    public class UserFee
    {
        public int Id { get; set; }
        public string PaidUserId { get; set; }
        public int FeeId { get; set; }
    }
}
