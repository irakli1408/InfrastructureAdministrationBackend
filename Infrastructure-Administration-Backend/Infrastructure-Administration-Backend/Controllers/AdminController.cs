using Infrastructure_Administration_Backend.DataModels;
using Infrastructure_Administration_Backend.DataModels.AddNewRole;
using Infrastructure_Administration_Backend.DataModels.ChangePassword;
using Infrastructure_Administration_Backend.DataModels.GetUser;
using Infrastructure_Administration_Backend.DataModels.Register;
using Infrastructure_Administration_Backend.Repository;
using Infrastructure_Administration_Backend.Services;
using InfrastructureAdministration.DataModels.GetUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IService service;
        private readonly IFilterRepository filter;
        private readonly ILogger<AdminController> logger;

        public AdminController(
            IService service,
            IFilterRepository filter,
            ILogger<AdminController> logger
            )
        {
            this.logger = logger;
            this.service = service;
            this.filter = filter;
        }
        [Route("FilterMethod")]
        [HttpGet]
        public IActionResult FilterMethod([FromBody] FilterModel model)
        {
            logger.LogError("sdfjhaskdfhalsdf");
            var res = new PagingForFrontService(filter);
            return Ok(res.FilterPagingMethod(model));
        }

        #region CreateRole
        [Route("CreateRole")]
        [HttpPost]
        public IActionResult CreateRole([FromBody] CreateRoleModel model)
        {
            return Ok(service.CreateRole(model));
        }
        #endregion

        #region GetRoles
        [Route("GiveRole")]
        [HttpGet]
        public IActionResult GiveRoles()
        {
            return Ok(service.GiveRoles());
        }
        #endregion

        #region EditRoleName
        [Route("EditRole")]
        [HttpPut]
        public IActionResult EditRole([FromBody] RolesModel model)
        {
            return Ok(service.EditRole(model));
        }
        #endregion

        #region CreateUser ar gadadis repozitorshi ratomgac createuser
        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        {
            await service.CreateUser(model);
            return Ok();
        }
        #endregion

        #region ChangePassword
        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            await service.ChangePassword(model);
            return Ok();
        }
        #endregion

        #region Resetpassword
        [Route("RessetPassword")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ChangePasswordModel model)
        {
            await service.ResetPassword(model);
            return Ok();
        }

        #endregion

        #region GetProfile
        [Route("GetProfile")]
        [HttpGet]
        public IActionResult GetProfile([FromBody] GetProfileModel model)
        {
            return Ok(service.GetProfile(model));
        }

        #endregion

        #region EditProfile
        [Route("EditProfile")]
        [HttpPut]
        public async Task<IActionResult> EditProfile([FromBody] EditProfileModule model)
        {
             await service.EditProfile(model);
            return Ok();
        }

        #endregion

        #region GetUsers -- userebis gamotana aris gasaketebeli. rolebit da active passive
        [Route("GetUsers")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(service.GetUsers());
        }
        #endregion
    }
}
