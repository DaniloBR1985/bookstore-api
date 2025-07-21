using BookStoreApi.DTOs;
using BookStoreApi.Entities;
using BookStoreApi.Interfaces;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> Get()
        {
            var genres = await _genreService.GetAll();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var genre = await _genreService.GetById(id);
            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GenreDto>> Create([FromBody] GenreDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var created = await _genreService.Create(dto, User.Identity.Name);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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
        public async Task<IActionResult> Update(int id, [FromBody] GenreDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var exists = await _genreService.GetById(id);
                if (exists == null)
                    return NotFound();

                await _genreService.Update(id, dto, User.Identity.Name);
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
            var exists = await _genreService.GetById(id);
            if (exists == null)
                return NotFound();

            await _genreService.Delete(id, User.Identity.Name);
            return NoContent();
        }
    }
}
