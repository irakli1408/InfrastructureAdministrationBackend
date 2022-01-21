using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfrastructureAdministration.DataModels.GetUsers
{
    public class EditProfileModule
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Possition { get; set; }
        public int Status { get; set; }
        public IQueryable<Roles> Role { get; set; }
    }
}
