using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.CourseDto;
using api.Interface;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/course")]
    [ApiController]

    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course.ToCourseDto());
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto createCourseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = createCourseDto.ToCourseFromCreate();
            var (success, message, createdCourse) = await _courseRepository.CreateAsync(course);

            if (!success)
            {
                return BadRequest(message);
            }
            return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse.ToCourseDto());
        }
        [HttpDelete("{courseCode}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string courseCode)
        {
            var (success, message) = await _courseRepository.DeleteAsync(courseCode);

            if (!success)
            {
                return NotFound(message);
            }

            return Ok(message);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseRepository.GetAllAsync();
            return Ok(courses);
        }
    }
}