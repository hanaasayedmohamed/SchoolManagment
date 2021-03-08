using ApiWithbasicAuthentication.Common;
using ApiWithbasicAuthentication.Domain.Model;
using ApiWithbasicAuthentication.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithbasicAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;
        private readonly IMapper _mapper;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService,
           IMapper mapper)
        {
            _studentService = studentService;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpPost]
        [Authorize]

        public IActionResult Insert([FromBody] Student student)
        {
            var students = _studentService.InsertStudent(student);
            return Ok(students);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] StudentParameters parameters)
        {

            var students = _studentService.GetStudents(parameters);
            var studentsList = _mapper.Map<List<StudentDetail>>(students);
            var metadata = new
            {
                students.TotalCount,
                students.PageSize,
                students.CurrentPage,
                students.TotalPages,
                students.HasNext,
                students.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(studentsList);

        }

    }
}
