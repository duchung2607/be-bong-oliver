using BongOliver.DTOs.Booking;
using BongOliver.DTOs.Response;
using BongOliver.Models;
using BongOliver.Services.BookingService;
using BongOliver.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpGet]
        public ActionResult GetAllBooking(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var res = _bookingService.GetAllBooking(page, pageSize, key, sortBy);
            return StatusCode(res.code, res);
        }
        [HttpGet("user/{username}")]
        public ActionResult GetBookingByUser(string username, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var res = _bookingService.GetBookingByUser(username, page, pageSize, key, sortBy);
            return StatusCode(res.code, res);
        }
        [HttpGet("stylist/{username}")]
        public ActionResult GetBookingByStylist(string username, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var res = _bookingService.GetBookingByStylist(username, page, pageSize, key, sortBy);
            return StatusCode(res.code, res);
        }
        [HttpGet("schedules")]
        public ActionResult GetSchedules(int stylistId, DateTime date)
        {
            var res = _bookingService.GetSchedules(stylistId, date);
            return StatusCode(res.code, res);
        }
        [HttpGet("calendar")]
        public ActionResult GetCalendar()
        {
            var res = _bookingService.GetCalendar();
            return StatusCode(res.code, res);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var res = await _bookingService.DeleteBooking(id);
            return (StatusCode(res.code, res));
        }
        [HttpGet("{id}")]
        public ActionResult GetBookingById(int id)
        {
            var res = _bookingService.GetBookingById(id);
            return StatusCode(res.code, res);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO>> UpdateBooking([FromBody] UpdateBookingDTO bookingDTO,[FromRoute] int id)
        {
            var res = await _bookingService.UpdateBooking(bookingDTO, id);
            return StatusCode(res.code,res);
        }
        [HttpPost("create")]
        public async Task<ActionResult<ResponseDTO>> CreateBooking([FromBody] CreateBookingDTO createBookingDTO)
        {
            var res = await _bookingService.CreateBooking(createBookingDTO);
            return StatusCode(res.code, res);
        }
    }
}
