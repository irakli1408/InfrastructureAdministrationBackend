using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.DataModels.AddNewRole
{
    public class CreateRoleModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
