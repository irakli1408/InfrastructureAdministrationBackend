using Infrastructure_Administration_Backend.Data;
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
        private readonly InfrastructureAdminitrationDBContext context;
        private readonly IFilterRepository filt;

        public SAdminController(
            IRepository repository,
            UserManager<ApplicationUser> userManager,
            InfrastructureAdminitrationDBContext context,
           IFilterRepository filt
            )
        {
            this.filt = filt;
            this.context = context;
            this.repository = repository;
            this.userManager = userManager;
        }



        public IActionResult FilterMethod([FromBody] FilterModel model)
        {
            var res = new PagingForFrontService(filt);

            return Ok(res.FilterPagingMethod(model));
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
               
                //var roleQuery = context.Roles.Where(x => model.Role.Contains(x.Id));
                var OneTimePassword = "Mawoni123";
                //SendEmail(OneTimePassword, auth);
                var newUser = new ApplicationUser
                {
                    UserName = auth.Name,
                    Surname = auth.Surname,
                    Possition = auth.Possition,
                    Email = auth.Email,
                    StatusId = auth.Status,
                    CreateDate = DateTime.Now
                };

                // gasaketebelia ro shemovides RoleId
                await userManager.CreateAsync(newUser, OneTimePassword);

                //await userManager.AddToRoleAsync(newUser, model.Role);

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
