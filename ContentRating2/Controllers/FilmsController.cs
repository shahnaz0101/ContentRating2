using ContentRating2.Models;
using ContentRating2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContentRating2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly FilmsRepository _filmsRepository = new FilmsRepository();

        [HttpPost]
        public IActionResult CreateFilms([FromBody] FilmsM request)
        {
            var result = _filmsRepository.CreateFilms(request);

            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateFilms([FromBody] FilmsM request)
        {
            var result = _filmsRepository.UpdateFilms(request);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetFilms()
        {
            var result = _filmsRepository.GetFilms();

            return Ok(result);
        }
        [HttpGet("get")]
        public IActionResult GetFilmsById(int id)
        {
            var result = _filmsRepository.GetFilmsById(id);
            return Ok(result);
        }
        [HttpDelete("delete by id")]
        public IActionResult DeleteFilms(int id)
        {
            var result = _filmsRepository.DeleteFilms(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
