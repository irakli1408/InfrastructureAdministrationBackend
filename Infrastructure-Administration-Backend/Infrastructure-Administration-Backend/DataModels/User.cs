using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Infrastructure_Administration_Backend.DataModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public Status statuses { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int? StatusChangeUserId { get; set; }
        public int? DeleteChangeUserId { get; set; }
        
    }
}
