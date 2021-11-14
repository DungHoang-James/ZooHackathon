using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ZooHackathonAPI.Constants;
using ZooHackathonAPI.Models.User;
using ZooHackathonAPI.Responses;
using ZooHackathonAPI.ViewModels;

namespace ZooHackathonAPI.Utilities
{
    public class TokenUtil
    {
        private static string secretKey;

        private static void SetPrivateKey(IConfiguration configuration)
        {
            secretKey = configuration.GetSection("Security:SecretKey").Value;
        }

        public static string GenerateJWTToken(UserDTO userModel, IConfiguration configuration)
        {
            SetPrivateKey(configuration);

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                        new Claim(PayloadKeyConstant.ID, userModel.Id.ToString()),
                        new Claim(PayloadKeyConstant.ROLE, userModel.Role.ToString()),
                        new Claim(PayloadKeyConstant.EMAIL, userModel.Email.ToString()),
                        new Claim(PayloadKeyConstant.FULLNAME, userModel.FullName.ToString()),

            };

            var token = new JwtSecurityToken("",
                "",
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static TokenViewModel ReadJWTTokenToModel(string token, IConfiguration configuration)
        {
            string tempToken = token;

            if (token.Contains("Bearer"))
            {
                token = tempToken.Split(' ')[1];
            }

            SetPrivateKey(configuration);

            bool isValid = IsTokenValid(token);

            if (!isValid)
            {
                throw new ErrorResponse((int)HttpStatusCode.Unauthorized, "Request Secret Token is invalid");
            }

            var result = new JwtSecurityTokenHandler().ReadJwtToken(token);

            int id = int.Parse(result.Claims.First(claim => claim.Type == PayloadKeyConstant.ID).Value);
            int role = int.Parse(result.Claims.First(claim => claim.Type == PayloadKeyConstant.ROLE).Value);

            return new TokenViewModel(id, role);
        }

        private static SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(secretKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

        private static TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey(),
                ValidateLifetime = false
            };
        }

        private static bool IsTokenValid(string token)
        {
            try
            {
                ClaimsPrincipal tokenValid = new JwtSecurityTokenHandler().ValidateToken(token, GetTokenValidationParameters(), out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
