using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.DataModels.Register
{
    public class RegisterKeyValue
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Possition { get; set; }
        [Required]
        public string Email { get; set; }
        public int Status { get; set; }
        public string Role { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
    }
}
