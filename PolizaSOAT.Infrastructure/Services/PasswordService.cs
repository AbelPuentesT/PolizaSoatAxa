using Microsoft.Extensions.Options;
using PolizaSOAT.Core.Exceptions;
using PolizaSOAT.Infrastructure.Interfaces;
using PolizaSOAT.Infrastructure.Options;
using System.Security.Cryptography;

namespace PolizaSOAT.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordsOptions _Options;
        public PasswordService(IOptions<PasswordsOptions> options)
        {
            _Options = options.Value;

        }
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new BusinessException("Unexpected hash format");
            }
            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations)
                )
            {
                var keyToCheck = (algorithm.GetBytes(_Options.KeySize));
                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                _Options.SaltSize,
                _Options.Iterations)
                )
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_Options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{_Options.Iterations}.{salt}.{key}";
            }
        }
    }
}
