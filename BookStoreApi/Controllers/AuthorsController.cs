using BookStoreApi.DTOs;
using BookStoreApi.Entities;
using BookStoreApi.Interfaces;
using BookStoreApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;
        public AuthorsController(IAuthorService s) { _service = s; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorViewModel>>> Get() =>
        Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorViewModel>> GetById(int id)
        {
            var vm = await _service.GetById(id);
            if (vm == null) return NotFound();
            return Ok(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AuthorViewModel>> Post([FromBody] AuthorDto dto)
        {
            try
            {
                var vm = await _service.Create(dto, User.Identity.Name);
                return CreatedAtAction(nameof(GetById), new { id = vm.Id, version = HttpContext.GetRequestedApiVersion().ToString() }, vm);
            }
            catch (Exception ex)
            {
                return Conflict(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }           
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, AuthorDto dto)
        {
            try
            {
                var updated = await _service.Update(id, dto, User.Identity.Name);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Conflict(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id, User.Identity.Name);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
