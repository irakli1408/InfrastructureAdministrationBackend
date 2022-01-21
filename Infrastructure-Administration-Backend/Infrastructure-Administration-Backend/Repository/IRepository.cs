using Infrastructure_Administration_Backend.DataModels;
using Infrastructure_Administration_Backend.DataModels.AddNewRole;
using Infrastructure_Administration_Backend.DataModels.ChangePassword;
using Infrastructure_Administration_Backend.DataModels.GetUser;
using Infrastructure_Administration_Backend.DataModels.Register;
using InfrastructureAdministration.DataModels.GetUsers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.Repository
{
    public interface IRepository
    {
        SuccesFailEnum CreateRole(CreateRoleModel roleModel);
        List<RolesModel> GiveRoles();
        void EditRole(RolesModel model, IdentityRole RoleModel);
        Task CreateUser(ApplicationUser newUser, CreateUserModel model , string OneTimePassword);
        
        Task ChangePassword(ChangePasswordModel model);
        UserModel GetProfile(ApplicationUser model);
        Task EditProfile(EditProfileModule model);
        IQueryable GetUsers();

    }
}
