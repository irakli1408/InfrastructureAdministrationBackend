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

namespace Infrastructure_Administration_Backend.Services
{
    public interface IService
    {
        string CreateRole(CreateRoleModel model);
        List<RolesModel> GiveRoles();
        SuccesFailEnum EditRole(RolesModel model);
        Task<SuccesFailEnum> CreateUser(CreateUserModel model);
        Task ChangePassword(ChangePasswordModel model);
        Task ResetPassword(ChangePasswordModel model);
        UserModel GetProfile(GetProfileModel model);
        Task EditProfile(EditProfileModule model);
        IQueryable GetUsers();
        void SendEmail(string OTP, CreateUserModel auth);
        string GenerateRandomPassword(PasswordOptions opts = null);
    }
}