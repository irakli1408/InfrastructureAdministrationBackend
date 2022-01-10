using Infrastructure_Administration_Backend.DataModels.Register;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.Services
{
    public interface IService
    {
        public void SendEmail(string OTP, RegisterKeyValue auth);
        string GenerateRandomPassword(PasswordOptions opts = null);
    }
}