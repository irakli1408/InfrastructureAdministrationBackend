using Infrastructure_Administration_Backend.Data;
using Infrastructure_Administration_Backend.DataModels;
using Infrastructure_Administration_Backend.DataModels.Register;
using Infrastructure_Administration_Backend.DataModels.ChangePassword;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure_Administration_Backend.DataModels.AddNewRole;
using Infrastructure_Administration_Backend.Services;

using Microsoft.Extensions.DependencyInjection;
using InfrastructureAdministration.DataModels.GetUsers;
using Infrastructure_Administration_Backend.DataModels.GetUser;

namespace Infrastructure_Administration_Backend.Repository
{
    public class InfrastructureRepository : IRepository
    {
        private readonly InfrastructureAdminitrationDBContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public InfrastructureRepository(InfrastructureAdminitrationDBContext context,
            UserManager<ApplicationUser> userManager
            )
        {
            this.context = context;
            this.userManager = userManager;
        }

        #region CreateRole
        public SuccesFailEnum CreateRole(CreateRoleModel model)
        {
            IdentityRole identityRole = new() { 
                Name = model.RoleName ,
                NormalizedName= model.RoleName.ToUpper()
            };
            context.Roles.Add(identityRole);
            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {

                return SuccesFailEnum.Fail;
            }
            return SuccesFailEnum.Success;
        }

        #endregion

        #region GiveRoles
        public List<RolesModel> GiveRoles()
        { 
            return context.Roles.Select(x => new RolesModel() {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
        #endregion

        #region EditRole
        public void EditRole(RolesModel model, IdentityRole RoleModel)
        {
            RoleModel.Name = model.Name;
            RoleModel.NormalizedName = model.Name.ToUpper();
            context.Roles.Update(RoleModel);
            context.SaveChanges();
        }
        #endregion

        #region CreateUser - ar gadadis repozitorshi ratomgac createuser
        public async Task CreateUser(ApplicationUser newUser, CreateUserModel model, string OneTimePassword)
        {

            await userManager.CreateAsync(newUser, OneTimePassword);

            foreach (var role in model.Role)
            {
                await userManager.AddToRoleAsync(newUser, role);
            }

        }
        #endregion

        
        #region ChangePassword
        public async Task ChangePassword(ChangePasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        }
        #endregion



        #region GetProfile

        public UserModel GetProfile(ApplicationUser model)
        {
            var query = context.Users.FirstOrDefault(x => x.Id.Contains(model.Id) && x.StatusId == 1);
            var user = new UserModel() { 
                FirstName = query.UserName,
                LastName = query.Surname,
                Email = query.Email,
                Possition = query.Possition,
            };

            return user;
        }

        #endregion

        #region EditProfile

        public async Task EditProfile(EditProfileModule model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            user.Email = model.Email;
            user.UserName = model.FirstName;
            user.Surname = model.LastName;
            user.Email = model.Email;
            user.Possition = model.Possition;
            await userManager.UpdateAsync(user);
            
        }

        #endregion

        #region getUsers -- userebis gamotana aris gasaketebeli. rolebit da active passive
        public IQueryable GetUsers()
        {
            var item =  from role in context.Roles
                        join userole in context.UserRoles on role.Id equals userole.RoleId
                        join user in context.Users on userole.UserId equals user.Id
                        select new UserModel()
                        {
                            FirstName = user.UserName,
                            LastName = user.Surname,
                            Email = user.Email,
                            Status = user.StatusId,
                            Possition = user.Possition
                        };

            //var myDeal = (from u in context.Users
            //              join urr in context.UserRoles on u.Id equals urr.UserId
            //              select new UserModel()
            //              {
            //                  FirstName = u.UserName,
            //                  LastName = u.Surname,
            //                  Email = u.Email,
            //                  Status = u.StatusId,
            //                  Possition = u.Possition,
            //                  Role = (from role in context.Roles where u.Id == role.UserId select role)
            //              });

            //var item2 = (
            //            from us in context.Users
            //            join ur in context.UserRoles on us.Id equals ur.UserId
            //            join r in context.Roles on ur.RoleId equals r.Id
            //            select new UserModel()
            //            {
            //                FirstName = us.UserName,
            //                LastName = us.Surname,
            //                Email = us.Email,
            //                Status = us.StatusId,
            //                Possition = us.Possition,
            //                //Role = new List<Roles>().Add(new Roles()
            //                //{
            //                //    Name = r.Name
            //                //})
            //                Role = (
            //                        from rr in context.Roles
            //                        join urr in context.UserRoles on rr.Id equals urr.RoleId
            //                        where ur.UserId == us.Id
            //                        select rr 
            //                        ).ToList()
            //            }) ;

            return item;
        }

        #endregion


    }
}
