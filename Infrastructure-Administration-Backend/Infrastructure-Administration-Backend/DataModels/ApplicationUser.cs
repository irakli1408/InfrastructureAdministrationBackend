using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.DataModels
{
    public class ApplicationUser: IdentityUser
    {
        public string Surname { get; set; }
        public string Possition { get; set; }
        public int StatusId { get; set; }
        public Status Statuses { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int? StatusChangeUserId { get; set; }
        public int? DeleteChangeUserId { get; set; }
    }
}
