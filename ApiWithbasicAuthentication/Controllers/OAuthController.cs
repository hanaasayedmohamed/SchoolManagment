using ApiWithbasicAuthentication.Common;
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

        public IActionResult GenerateToken()
        {

                return Ok(new
                {
                    access_Token = JwtMiddleware.GenerateToken(),
                    Token_Type = "bearer"
                });
 
        }
    }
}
