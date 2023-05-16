using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IUserServices
    {
        public string CreateToken(User user);

        public bool IsVaildEmail(string email);
        
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        public bool TryVerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}

