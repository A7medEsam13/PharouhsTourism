using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharouhsTourism.Application.DTOs.Destination;
using PharouhsTourism.Application.Interfaces;

namespace PharouhsTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        
        private readonly IDestinationServices _destinationServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        //public Task Add(AddDestinationDto dto);  ///////////////////////////////////////
        //public Task Delete(int id);
        //public Task Update(int id, AddDestinationDto dto);
        //public Task<DisplayDestinationDto> GetById(int id); /////////////
        public DestinationsController(IDestinationServices destinationServices,
            IWebHostEnvironment webHostEnvironment)
        {
            _destinationServices = destinationServices;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddDestination(string name, IFormFile photo)
        {
            if (photo is null || photo.Length == 0)
                return BadRequest("File is empty.");

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "destinations");

            string filePath = await FileHandler.FileHandler.Store(photo, uploadsFolder);


            var dest = new AddDestinationDto
            {
                Name = name,
                PhotoURL = filePath
            };

            await _destinationServices.Add(dest);

            return Created();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(id<=0)
                return BadRequest("Invalid ID.");
            var destination = await _destinationServices.GetById(id);
            if (destination is null)
                return NotFound();
            return Ok(destination);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id.");

            await _destinationServices.Delete(id);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int id,AddDestinationDto dto)
        {
            if (id <= 0 || dto is null)
                return BadRequest("Invalid data.");

            await _destinationServices.Update(id, dto);
            return Ok();
        }
    }
}
