using Infrastructure_Administration_Backend.DataModels;
using Infrastructure_Administration_Backend.DataModels.AddNewRole;
using Infrastructure_Administration_Backend.DataModels.ChangePassword;
using Infrastructure_Administration_Backend.DataModels.Register;
using Infrastructure_Administration_Backend.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.Controllers
{
    public class SAdminController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;

        public SAdminController(
            IRepository repository,
            UserManager<ApplicationUser> userManager
            )
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        #region createRole
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.CreateRole(roleModel);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    return Ok(new { isSaved = "Succceded" });
                }
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
            }
            return Ok(new { response = $"ModelState {roleModel} is not Valid" });
        }
        #endregion

        #region GetRoleList
        [HttpGet]
        public IActionResult ListRole()
        {
            return Ok(repository.GiveAllRoles());
        }
        #endregion

        #region ChangeRoleName
        [HttpPut]
        public IActionResult EditRole([FromBody] RolesKeyValue roleModel)
        {
            return Content(repository.EditRoles(roleModel));
        }
        #endregion

        #region Register
        public async Task<IActionResult> Register([FromBody] RegisterKeyValue auth)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await userManager.FindByEmailAsync(auth.Email);
                if (userEmail != null)
                {
                    throw new Exception("UserName does not exist");
                }
                var result = await repository.Register(auth);
                if (!result.Succeeded)
                {
                    throw new Exception("Registration Failed");
                }
                return Ok(new { success = true });
            }
            throw new Exception("ModelState Invalid");
        }
        #endregion

        #region ChangePassword
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel auth)
        {
            var errorList= new List<IdentityError>();
            if (ModelState.IsValid)
            {
                var result = await repository.ChangePasswordAsync(auth);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                }
                foreach (var errors in result.Errors)
                {
                    errorList.Add(errors);
                }
            }
            return Ok(errorList);
        }
        #endregion

        #region GetAllUsers
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(repository.GetUsers());
        }
        #endregion
    }
}
