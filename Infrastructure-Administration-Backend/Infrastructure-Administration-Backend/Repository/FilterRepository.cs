using Infrastructure_Administration_Backend.Data;
using System;
using System.Collections.Generic;
using Infrastructure_Administration_Backend.DataModels;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.Repository
{
    public class FilterRepository : IFilterRepository
    {
        private readonly InfrastructureAdminitrationDBContext context;
        public FilterRepository(InfrastructureAdminitrationDBContext context)
        {
            this.context = context;
        }

        public IQueryable<Ex> FilterMethod(FilterModel model)
        {
            var users = from u in context.Users
                        where u.StatusId == 1 //&& u.DeleteDate == null
                        select u;

            users = Method(model, users);

            var res = users.ToList();

            var result = users.Select(x => new Ex
            {
                FirstName = x.UserName,
                LastName = x.Surname,
                Possition = x.Possition,
                Email = x.Email

            });

            return result;
        }

        public IQueryable<ApplicationUser> Method(FilterModel model, IQueryable<ApplicationUser> users)
        {

            users = users.Where(x =>

              x.UserName.Contains(model.FirstName) ||

              x.Surname.Contains(model.LastName) ||

              x.Possition.Contains(model.Possition) ||

              x.Email.Contains(model.Email)
           );


            return users;
        }

        public class Ex
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Possition { get; set; }
            public string Email { get; set; }
        }

    }
}
