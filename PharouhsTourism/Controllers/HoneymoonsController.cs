using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharouhsTourism.Application.DTOs.Honeymoon;
using PharouhsTourism.Application.DTOs.Trip;
using PharouhsTourism.Application.Interfaces;

namespace PharouhsTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoneymoonsController : ControllerBase
    {
        private readonly IHoneymoonServices _honeymoonServices;
        private readonly IDestinationServices _destinationServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HoneymoonsController(IHoneymoonServices honeymoonServices,
            IWebHostEnvironment webHostEnvironment,
            IDestinationServices destinationServices)
        {
            _honeymoonServices = honeymoonServices;
            _webHostEnvironment = webHostEnvironment;
            _destinationServices = destinationServices;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddHoneymoonDto dto, [FromForm] List<IFormFile> images)
        {
            if (dto == null || images.Count == 0)
                return BadRequest("invalid data.");

            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "trips");

            List<string> photosPaths = new();

            foreach (var p in images)
            {
                photosPaths.Add(await FileHandler.FileHandler.Store(p, folderPath));
            }

            await _honeymoonServices.Add(dto, photosPaths);

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("invalid data.");

            var honeymoon = await _honeymoonServices.GetById(id);

            return (honeymoon is not null) ? Ok(honeymoon) : BadRequest("There is no data with this id.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDestinationHoneymoons(int destinationId)
        {
            var honeymoons = await _honeymoonServices.GetAllDestinationhoneymoons(destinationId);
            return Ok(honeymoons);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, DisplayHoneymoonDto dto)
        {
            if (id <= 0 || dto == null)
            {
                return BadRequest("Invalid data.");
            }


            await _honeymoonServices.Update(id, dto);
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("invalid data.");

            await _honeymoonServices.Delete(id);

            return Ok();
        }

    }
}
