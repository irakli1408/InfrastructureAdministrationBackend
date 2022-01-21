using InfrastructureAdministration.DataModels.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.DataModels.GetUser
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Possition { get; set; }
        public int Status { get; set; }
        public IQueryable Role { get; set; }
    }
}
