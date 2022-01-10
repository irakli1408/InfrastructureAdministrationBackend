using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.DataModels.GetUsersWithRoles
{
    public class UserKeyValue
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Possition { get; set; }
        public int Status { get; set; }
        public string Role { get; set; }
    }
}
