using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharouhsTourism.Application.DTOs.Hotel;
using PharouhsTourism.Application.DTOs.Trip;
using PharouhsTourism.Application.Interfaces;

namespace PharouhsTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripServices _tripServices;
        private readonly IDestinationServices _destinationServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TripsController(ITripServices tripServices,
            IWebHostEnvironment webHostEnvironment,
            IDestinationServices destinationServices)
        {
            _tripServices = tripServices;
            _webHostEnvironment = webHostEnvironment;
            _destinationServices = destinationServices;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddTripDto dto, [FromForm] List<IFormFile> images)
        {
            if (dto == null || images.Count == 0)
                return BadRequest("invalid data.");

            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "trips");

            List<string> photosPaths = new();

            foreach (var p in images)
            {
                photosPaths.Add(await FileHandler.FileHandler.Store(p, folderPath));
            }

            await _tripServices.Add(dto, photosPaths);

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("invalid data.");

            var trip = await _tripServices.GetById(id);

            return (trip is not null) ? Ok(trip) : BadRequest("There is no data with this id.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDestinationTrips(int destinationId)
        {
            var trips = await _tripServices.GetAllDestinationTrips(destinationId);
            return Ok(trips);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, DisplayTripDto dto)
        {
            if (id <= 0 || dto == null)
            {
                return BadRequest("Invalid data.");
            }


            await _tripServices.Update(id, dto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("invalid data.");

            await _tripServices.Delete(id);

            return Ok();
        }

        

    }
}
