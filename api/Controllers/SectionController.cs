using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Section;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/section")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateSectionDto createSectionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var section = createSectionDto.ToSectionFromCreate();
                var createdSection = await _sectionRepository.CreateAsync(section);
                return CreatedAtAction(nameof(GetById), new { id = createdSection.Id }, createdSection.ToSectionDto());
            }
            catch (InvalidOperationException ex)
            {
                // Handle the case where a section with the same name already exists
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var sections = await _sectionRepository.GetAllAsync();
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var section = await _sectionRepository.GetByIdAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            return Ok(section.ToSectionDto());
        }
        [HttpDelete("delete/{name}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteByName(string name)
        {
            var deleted = await _sectionRepository.DeleteByNameAsync(name);
            if (!deleted)
            {
                return NotFound(new { message = $"Section with name '{name}' not found." });
            }
            return NoContent();
        }
    }
}
