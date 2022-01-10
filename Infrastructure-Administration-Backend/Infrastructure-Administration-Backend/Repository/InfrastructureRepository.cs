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
using Infrastructure_Administration_Backend.DataModels.GetUsersWithRoles;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Administration_Backend.Repository
{
    public class InfrastructureRepository : IRepository
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly InfrastructureAdminitrationDBContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IService service;

        public InfrastructureRepository(InfrastructureAdminitrationDBContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IService service
            )
        {
            this.roleManager = roleManager;
            this.context = context;
            this.userManager = userManager;
            this.service = service;
        }

        public List<RolesKeyValue> GiveAllRoles()
        {
            var item = context.Roles.Select(x => new RolesKeyValue()
            {
                Id = x.Id,
                Name = x.Name,
                NormalizedName = x.NormalizedName
            }).ToList();
            return item;
        }

        public string EditRoles(RolesKeyValue roleModel)
        {
            var role = context.Roles.FirstOrDefault(x => x.Id == roleModel.Id);
            if (role == null)
            {
                return $"Role with Id {roleModel.Id} can not be found";
            }
            role.Name = roleModel.Name;
            role.NormalizedName = roleModel.Name.ToUpper();
            context.Roles.Update(role);
            context.SaveChanges();
            return "Saved Successfuly";
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel auth)
        {
            var user = await userManager.FindByEmailAsync(auth.Email);
            return await userManager.ChangePasswordAsync(user, auth.OldPassword, auth.NewPassword);
        }

        public async Task<IdentityResult> CreateRole(CreateRoleModel roleModel)
        {
            IdentityRole identityRole = new() { Name = roleModel.RoleName };
            return await roleManager.CreateAsync(identityRole);
        }

        public async Task<IdentityResult> Register(RegisterKeyValue auth)
        {
            var newUser = new ApplicationUser
            {
                UserName = auth.Name,
                Surname = auth.Surname,
                Possition = auth.Possition,
                Email = auth.Email,
                StatusId = auth.Status,
            };
            var OneTimePassword = service.GenerateRandomPassword();
            Console.WriteLine(OneTimePassword);
            //service.SendEmail(OneTimePassword, auth);
            var result = await userManager.CreateAsync(newUser, OneTimePassword);
            if (result.Succeeded)
            {
                return await userManager.AddToRoleAsync(newUser, auth.Role);
            }
            else
            {
                return result;
            }
        }

        public List<UserKeyValue> GetUsers()
        {
            var item = (from r in context.Roles
                       join ur in context.UserRoles on r.Id equals ur.RoleId
                       join us in context.Users on ur.UserId equals us.Id
                       select new UserKeyValue()
                       {
                           Name = us.UserName,
                           Surname = us.Surname,
                           Email = us.Email,
                           Status = us.StatusId,
                           Possition = us.Possition,
                           Role = r.Name
                       }).ToList();
            return item;
        }
        
    }
}
