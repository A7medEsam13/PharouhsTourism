using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PharouhsTourism.Application.DTOs.Book;
using PharouhsTourism.Application.Interfaces;

namespace PharouhsTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _bookServices;

        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpPost]
        [EnableRateLimiting("FixedWindowPolicy")]
        public async Task<IActionResult> Create(int? hotelId, int? tripId, int? honeymoonId, AddBookDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _bookServices.AddBook(hotelId, tripId, honeymoonId, dto);

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDestinationBooks(int destinationId)
        {
            if (destinationId <= 0)
                return BadRequest("Id Is Invalid.");

            var books = await _bookServices.GetAllDestinationBooks(destinationId);

            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id.");

            var book = await _bookServices.GetByID(id);

            return Ok(book);
        }


        [HttpPut]
        public async Task<IActionResult> Update(int id, AddBookDto dto)
        {
            if (id <= 0 || !ModelState.IsValid)
                return BadRequest();

            await _bookServices.UpdateBook(id, dto);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id.");
            await _bookServices.DeleteBook(id);

            return Ok();
        }
    }
}
