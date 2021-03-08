using ApiWithbasicAuthentication.Domain.Model;
using ApiWithbasicAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCoreApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        IStudentService _studentService;
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStudentService studentService)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        //  [Authentication.BasicAuthorization] 
        public IActionResult Get()
        {
            try
            {
                return Ok( _studentService.GetAllStudents());
            }
            catch
            {

                throw new Exception("Exception while fetching Weather Forecast ");

            }

        }
    }
}
