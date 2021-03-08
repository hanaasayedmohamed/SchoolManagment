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
            var symmetricKey = "minimumSixteenCharacters"; // Convert.FromBase64String("MTIz");

            var tokenHandler = new JwtSecurityTokenHandler();
            
           /* var authenticateduser = new AuthenticatedUser(
                JwtBearerDefaults.AuthenticationScheme,
                true,
                "roundthecode");*/

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

        public static IEnumerable<Claim> IsValidToken( string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("minimumSixteenCharacters");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;

              return  jwtToken.Payload.Claims; 
                // attach user to context on successful jwt validation
                // context.Items["User"] = userService.GetById(userId);
            }
            catch(Exception e)
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
              return  null;
            }
        }
    }
}