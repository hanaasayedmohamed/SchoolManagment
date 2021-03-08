using ApiWithbasicAuthentication.OAuthToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithbasicAuthentication.Controllers
{
    [Route("OAuth")]
    [ApiController]

    public class OAuthController : ControllerBase
    {
        [HttpGet]
        [Route("Token")]

        public IActionResult Token()
        {
            return Ok(new
            {
                access_Token = JwtMiddleware.GenerateToken(),
                Token_Type = "bearer"
            });
        }

        [HttpGet]
        [Route("test") , Authorize ]
      //  [Authorize(Policy = "Over18")]

        public IActionResult test()
        {
            return Ok("test authorized");
        }

        [HttpGet]
        [Route("validateToken")]

        public IActionResult IsValidToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();


            return Ok(new
            {
                Valid_To  =JwtMiddleware.IsValidToken(token),
            });
        }


    }
}
