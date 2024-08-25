using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helper;
using api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("search")]
    [ApiController]
    public class SearchController:ControllerBase
    {

        private readonly ISearchRepository _searchRepository;
        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }
        [HttpPost("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search([FromBody] SearchCriteriaAdmin criteria)
        {
            try
            {
                var results = await _searchRepository.SearchAsync(criteria);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}