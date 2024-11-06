using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YouthProtectionApi.Models.Dtos;
using YouthProtectionApi.Services;

namespace YouthProtectionApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly AttendanceService _attendanceService;

        public AttendanceController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("start")]
        [Authorize(Roles = "Voluntary")]
        public async Task<IActionResult> StartAttendance(long publicationId)
        {
            var userClaims = HttpContext.User;

            var chatId = await _attendanceService.StartAttendance(publicationId, userClaims);
            return Ok(new {ChatId = chatId});
        }

        [HttpPost("complete")]
        [Authorize(Roles = "Voluntary")]
        public async Task<IActionResult> CompleteAttendance(int attendanceId)
        {
            await _attendanceService.CompleteAttendance(attendanceId);
            return Ok();
        }
    }
}
