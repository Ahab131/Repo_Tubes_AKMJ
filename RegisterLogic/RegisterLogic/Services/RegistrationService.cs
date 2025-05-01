using System.Linq;
using RegistrationLogic.Models;
using RegistrationLogic.Services;
using RegistrationLogic.Helpers;
using RegistrationLogic.Models;

namespace RegistrationLogic.Services
{
    public class RegistrationService
    {
        private readonly List<User> _registeredUsers = new List<User>();

        public RegistrationResult Register(User newUser)
        {
            var validation = RegistrationValidator.Validate(newUser);
            if (!validation.IsValid)
            {
                return new RegistrationResult
                {
                    Success = false,
                    Errors = validation.Errors
                };
            }

            // Cek duplikasi email
            if (_registeredUsers.Any(u => u.Email == newUser.Email))
            {
                return new RegistrationResult
                {
                    Success = false,
                    Errors = new List<string> { "Email sudah terdaftar" }
                };
            }

            // Hash password (simulasi)
            newUser.Password = HashPassword(newUser.Password);

            _registeredUsers.Add(newUser);

            return new RegistrationResult { Success = true };
        }

        private string HashPassword(string password)
        {
            // Contoh sederhana (JANGAN gunakan di production!)
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}