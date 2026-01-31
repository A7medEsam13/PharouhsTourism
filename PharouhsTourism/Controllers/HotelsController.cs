using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharouhsTourism.Application.DTOs.Hotel;
using PharouhsTourism.Application.Interfaces;
using PharouhsTourism.FileHandler;
using System.Net.WebSockets;

namespace PharouhsTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelServices _hotelServices;
        private readonly IDestinationServices _destinationServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HotelsController(IHotelServices hotelServices,
            IWebHostEnvironment webHostEnvironment,
            IDestinationServices destinationServices)
        {
            _hotelServices = hotelServices;
            _webHostEnvironment = webHostEnvironment;
            _destinationServices = destinationServices;
        }

        //public Task Add(AddHotelDto dto);   ///////////////
        //public Task Update(int id, DisplayHotelDto dto);
        //public Task Delete(int id);
        //public Task<DisplayHotelDto> GetById(int id); ///////
        //public Task<ICollection<DisplayHotelDto>> GetAll();//////////////////////

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddHotelDto dto, [FromForm] List<IFormFile> images)
        {
            if (dto == null || images.Count == 0)
                return BadRequest("invalid data.");

            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "hotels");

            List<string> photosPaths = new();

            foreach(var p in  images)
            {
                photosPaths.Add(await FileHandler.FileHandler.Store(p, folderPath));
            }

            await _hotelServices.Add(dto, photosPaths);

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("invalid data.");

            var hotel = await _hotelServices.GetById(id);

            return (hotel is not null) ? Ok(hotel) : BadRequest("There is no data with this id.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hotels = await _hotelServices.GetAll();

            return Ok(hotels);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, DisplayHotelDto dto)
        {
            if(id <= 0|| dto == null)
            {
                return BadRequest("Invalid data.");
            }


            await _hotelServices.Update(id, dto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("invalid data.");

            await _hotelServices.Delete(id);

            return Ok();
        }

        [HttpGet("destination/{id}")]
        public async Task<IActionResult> GetAllDestinationHotels(int id)
        {
            if (id <= 0 || await _destinationServices.GetById(id) is null)
                return BadRequest("Invalid data");

            var hotels = await _hotelServices.GetAllDestinationHotels(id);
            return Ok(hotels);
        }
            
    }
}
