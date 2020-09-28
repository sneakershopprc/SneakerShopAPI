using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SneakerShopAPI.Models;
using SneakerShopAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShopAPI.Repositories
{
    public class AuthenticationRepository
    {
        private readonly SneakerShopContext context;
        private readonly IConfiguration config;

        public AuthenticationRepository(SneakerShopContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        public AccountVModel SignUp(AccountVModel userReg)
        {
            byte[] salt = this.GenSalt();
            byte[] hashedPass = this.HashPass(salt, userReg.Password);

            Account acc = new Account()
            {
                Username = userReg.Username,
                PasswordHash = hashedPass,
                PasswordSalt = salt,
                Fullname = userReg?.Fullname,
                Email = userReg?.Email,
                Role = "customer"
            };

            context.Account.Add(acc);
            context.SaveChanges();

            AccountVModel vmodel = AccountVModel.ToVModel(acc);
            vmodel.Token = this.GenerateJSONWebToken(vmodel);
            return vmodel;
        }

        public AccountVModel LogIn(AccountVModel account)
        {
            string username = account?.Username ?? "";
            string password = account?.Password ?? "";

            Account curAcc = context.Account.Find(username);

            if (curAcc == null) return null;

            byte[] CurHashedPass = this.HashPass(curAcc.PasswordSalt, password);

            if (Convert.ToBase64String(CurHashedPass).Equals(Convert.ToBase64String(curAcc.PasswordHash)))
            {
                AccountVModel vmodel = AccountVModel.ToVModel(curAcc);
                vmodel.Token = this.GenerateJSONWebToken(vmodel);
                return vmodel;
            }
            return null;
        }

        private byte[] GenSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private byte[] HashPass(byte[] Salt, string Password)
        {
            byte[] hashed = KeyDerivation.Pbkdf2(
                    password: Password,
                    salt: Salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8);

            return hashed;
        }

        private string GenerateJSONWebToken(AccountVModel account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Username),
                    new Claim(ClaimTypes.Role, account.Role)
                }),
                Audience = config["Jwt:Issuer"],
                Issuer = config["Jwt:Issuer"],
                Expires = DateTime.Now.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
