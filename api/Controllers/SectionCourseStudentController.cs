using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.SectionCourseStudentDto;
using api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("section-course-student")]
    [ApiController]
    public class SectionCourseStudentController : ControllerBase
    {

        private readonly ISectionCourseStudentRepository _sectionCourseStudentRepository;
        public SectionCourseStudentController(ISectionCourseStudentRepository sectionCourseStudentRepository)
        {
            _sectionCourseStudentRepository = sectionCourseStudentRepository;
        }
        [HttpPost("register")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Register([FromBody] RegisterStudentDto dto)
        {
            var (isSuccess, message, sectionCourseStudent) = await _sectionCourseStudentRepository.RegisterStudentToSectionCourseAsync(dto.SectionName, dto.CourseCode, dto.StudentUsername);

            if (!isSuccess)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message, sectionCourseStudent });
        }

    }
}