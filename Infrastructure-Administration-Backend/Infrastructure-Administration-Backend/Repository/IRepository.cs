using Infrastructure_Administration_Backend.DataModels;
using Infrastructure_Administration_Backend.DataModels.AddNewRole;
using Infrastructure_Administration_Backend.DataModels.ChangePassword;
using Infrastructure_Administration_Backend.DataModels.GetUsersWithRoles;
using Infrastructure_Administration_Backend.DataModels.Register;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.Repository
{
    public interface IRepository
    {
        List<RolesKeyValue> GiveAllRoles();
        string EditRoles(RolesKeyValue roleModel);
        List<UserKeyValue> GetUsers();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel auth);
        Task<IdentityResult> CreateRole(CreateRoleModel roleModel);
        Task<IdentityResult> Register(RegisterKeyValue auth);

    }
}
