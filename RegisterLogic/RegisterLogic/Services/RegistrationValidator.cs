using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RegistrationLogic.Helpers;
using RegistrationLogic.Models;
using RegistrationLogic.Helpers;
using RegistrationLogic.Models;

namespace RegistrationLogic.Services
{
    public class RegistrationValidator
    {
        // TABEL ATURAN VALIDASI (TABLE-DRIVEN APPROACH)
        private static readonly Dictionary<string, Func<string, bool>> ValidationRules =
            new Dictionary<string, Func<string, bool>>
            {
                { "Username", value => ValidationHelper.ValidateLength(value, 5, 20) },
                { "Email", value => ValidationHelper.ValidateEmail(value) },
                { "Password", value => ValidationHelper.ValidateLength(value, 8, 30) },
            };

        // TABEL PESAN ERROR
        private static readonly Dictionary<string, string> ErrorMessages =
            new Dictionary<string, string>
            {
                { "Username", "Username harus 5-20 karakter" },
                { "Email", "Email tidak valid" },
                { "Password", "Password harus 8-30 karakter" },
            };

        public static (bool IsValid, List<string> Errors) Validate(User user)
        {
            var errors = new List<string>();
            bool isValid = true;

            // Validasi berdasarkan tabel aturan
            foreach (var rule in ValidationRules)
            {
                var property = typeof(User).GetProperty(rule.Key);
                if (property == null) continue;

                var value = property.GetValue(user)?.ToString();
                if (!rule.Value(value))
                {
                    isValid = false;
                    errors.Add(ErrorMessages[rule.Key]);
                }
            }

            return (isValid, errors);
        }
    }
}