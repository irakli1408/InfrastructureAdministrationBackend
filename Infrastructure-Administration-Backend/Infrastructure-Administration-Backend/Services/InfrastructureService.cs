using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure_Administration_Backend.Data;
using Infrastructure_Administration_Backend.DataModels;
using Infrastructure_Administration_Backend.DataModels.AddNewRole;
using Infrastructure_Administration_Backend.DataModels.ChangePassword;
using Infrastructure_Administration_Backend.DataModels.GetUser;
using Infrastructure_Administration_Backend.DataModels.Register;
using Infrastructure_Administration_Backend.Repository;
using InfrastructureAdministration.DataModels.GetUsers;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace Infrastructure_Administration_Backend.Services
{
    public class TempUser
    {
        public string Name { get; set; } = "RuRu Delux";
        public string Email { get; set; } = "rurudelux@gmail.com";
        public string Password { get; set; } = "Simindi!@#";
        public string SmtpMail { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
    }
    public class InfrastructureService : IService
    {
        private readonly IRepository repository;
        private readonly InfrastructureAdminitrationDBContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public InfrastructureService(IRepository repository,
            InfrastructureAdminitrationDBContext context,
            UserManager<ApplicationUser> userManager
            )
        {
            this.userManager = userManager;
            this.context = context;
            this.repository = repository;
        }


        #region CreateRole
        public string CreateRole(CreateRoleModel model)
        {
            var result = repository.CreateRole(model);
            return result.ToString();
        }

        #endregion

        #region GiveRoles
        public List<RolesModel> GiveRoles()
        {
            return repository.GiveRoles();
        }
        #endregion

        #region EditRole
        public SuccesFailEnum EditRole(RolesModel model)
        {
            var role = context.Roles.FirstOrDefault(x => x.Id == model.Id);
            if (role == null)
            {
                return SuccesFailEnum.Fail;
            }
            repository.EditRole(model, role);
            return SuccesFailEnum.Success;
        }

        #endregion

        #region CreateUser
        public async Task<SuccesFailEnum> CreateUser(CreateUserModel model)
        {
            var userEmail = await userManager.FindByEmailAsync(model.Email);
            if (userEmail != null)
            {
                return SuccesFailEnum.Fail;
            }
            //var roleQuery = context.Roles.Where(x => model.Role.Contains(x.Id));
            var OneTimePassword = GenerateRandomPassword();
            //SendEmail(OneTimePassword, auth);
            var newUser = new ApplicationUser
            {
                UserName = model.FirstName,
                Surname = model.LastName,
                Possition = model.Possition,
                Email = model.Email,
                StatusId = model.Status,
                CreateDate = DateTime.Now
            };

            // gasaketebelia ro shemovides RoleId
            await repository.CreateUser(newUser, model, OneTimePassword);
            //await userManager.CreateAsync(newUser, OneTimePassword);
            
            //foreach (var role in model.Role)
            //{
            //   await userManager.AddToRoleAsync(newUser, role);
            //}

            return SuccesFailEnum.Success;
        }

        #endregion

        #region ChangePassword
        public async Task ChangePassword(ChangePasswordModel model)
        {
            if(model.NewPassword == model.NewPasswordConfirm)
                await repository.ChangePassword(model);
        }
        #endregion

        #region ResetPassword
        public async Task ResetPassword(ChangePasswordModel model)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(model.Email);

            if (user != null && model.NewPassword == model.NewPasswordConfirm)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                await userManager.ResetPasswordAsync(user, token, model.NewPassword);
            }
            //await repository.PasswordRecovery();
        }
        #endregion

        #region GetProfile
        public UserModel GetProfile(GetProfileModel model)
        {
            var user = FindUserById(model.Id);
            if (user != null)
            {
                return repository.GetProfile(user);
            }
            UserModel Empty = new() { };
            return Empty;
        }


        #endregion

        #region EditProfile

        public async Task EditProfile(EditProfileModule model)
        {
            var user = FindUserById(model.Id);
            if (user != null)
                await repository.EditProfile(model);
        }

        #endregion


        #region GetUsers
        public IQueryable GetUsers()
        {
            return repository.GetUsers();
        }
        #endregion

        #region FindUserById

        public ApplicationUser FindUserById(string id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }
        #endregion

        #region SendEmail
        public void SendEmail(string OTP, CreateUserModel auth)
        {
            var user = new TempUser();
            var message = new MimeMessage();

            var from = new MailboxAddress(user.Name, user.Email);
            message.From.Add(from);

            var to = new MailboxAddress(auth.FirstName, auth.Email);
            message.To.Add(to);

            message.Subject = $"noreplay";
            message.Body = new TextPart("plain")
            {
                Text = $"თქვენი დროებითი პაროლია {OTP}"
            };
            using var client = new SmtpClient();
            client.Connect(user.SmtpMail, user.Port, false);
            client.Authenticate(user.Email, user.Password);
            client.Send(message);
            client.Disconnect(true);
        }

        #endregion

        #region generate random password with options
        public string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };
            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",
            "abcdefghijkmnopqrstuvwxyz",
            "0123456789",
            "!@$?_-"
            };
            Random rand = new(Environment.TickCount);
            List<char> chars = new();
            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);
            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);
            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);
            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);
            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }
            return new string(chars.ToArray());
        }

        #endregion
    }
}
    

