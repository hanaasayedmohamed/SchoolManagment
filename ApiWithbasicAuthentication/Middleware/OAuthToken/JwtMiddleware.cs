using ApiWithbasicAuthentication.Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ApiWithbasicAuthentication.OAuthToken
{
    public static class JwtMiddleware
    {
        public static string GenerateToken()
        {
            //TODO : read secret from configurations
            var symmetricKey = "minimumSixteenCharacters"; 

            var tokenHandler = new JwtSecurityTokenHandler();
          
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new ClaimsIdentity(
                                new List<Claim>
                                {
                                    new Claim("UserId","2e701e62-0953-4dd3-910b-dc6cc93ccb0d"),
                                    new Claim("UserName","hanaa"),
                                    new Claim("Email","admin@abp.io") ,
                                    new Claim("Role","admin")
                                }
                            )
                    ),
                 
                Expires = now.AddMinutes(Convert.ToInt32("5")),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(symmetricKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return token;
        }

      
    }
}