using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure_Administration_Backend.DataModels.Register;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
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
        public void SendEmail(string OTP, RegisterKeyValue auth)
        {
            var user = new TempUser();
            var message = new MimeMessage();

            var from = new MailboxAddress(user.Name, user.Email);
            message.From.Add(from);

            var to = new MailboxAddress(auth.Name, auth.Email);
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
        //generate random password with options
        public string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 6,
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
    }
}
