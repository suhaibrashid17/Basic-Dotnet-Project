using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.SectionCourseInstructorDto;
using api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("section-course-instructor")]
    [ApiController]
    public class SectionCourseInstructorController : ControllerBase
    {
        private readonly ISectionCourseInstructorRepository _sectionCourseInstructorRepository;

        public SectionCourseInstructorController(ISectionCourseInstructorRepository sectionCourseInstructorRepository)
        {
            _sectionCourseInstructorRepository = sectionCourseInstructorRepository;
        }

        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignInstructor([FromBody] AssignInstructorDto dto)
        {
            var (isSuccess, message, sectionCourseInstructor) = await _sectionCourseInstructorRepository.AssignInstructorToSectionCourseAsync(dto.SectionName, dto.CourseCode, dto.InstructorUsername);

            if (!isSuccess)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message, sectionCourseInstructor });
        }

    }
}