using System.Threading.Tasks;
using api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/section-course")]
    [ApiController]
    public class SectionCourseController : ControllerBase
    {
        private readonly ISectionCourseRepository _sectionCourseRepository;

        public SectionCourseController(ISectionCourseRepository sectionCourseRepository)
        {
            _sectionCourseRepository = sectionCourseRepository;
        }

        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignSectionToCourse(string sectionName, string courseCode)
        {
            var (isSuccess, message) = await _sectionCourseRepository.AssignSectionToCourseAsync(sectionName, courseCode);

            if (!isSuccess)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }

    }
}
