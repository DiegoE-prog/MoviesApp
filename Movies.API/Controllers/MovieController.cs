using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Dtos.Movie;
using Movies.API.Models;
using Movies.API.Services.Interfaces;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //[Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetMovieDto>>>> GetMovies()
        {
            var response = await _movieService.GetMoviesAsync();
            if(response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize()]
        public async Task<ActionResult<ServiceResponse<GetMovieDto>>> GetMovie(int id)
        {
            var response = await _movieService.GetMovieByIdAsync(id);
            if(response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetMovieDto>>> AddMovie(MovieToAddDto movieToAddDto)
        {
            var response = await _movieService.AddMovieAsync(movieToAddDto);
            if (response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetMovieDto>>> UpdateMovie(MovieToUpdateDto movieToUpdateDto)
        {
            var response = await _movieService.UpdateMovieAsync(movieToUpdateDto);
            if(response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteMovie(int id)
        {
            var response = await _movieService.DeleteMovieAsync(id);
            if(response.Success is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
