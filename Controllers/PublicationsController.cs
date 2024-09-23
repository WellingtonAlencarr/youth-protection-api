using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using YouthProtection.Models;

namespace YouthProtectionApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublicationsController : Controller
    {
        
        public PublicationsController() { }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var posts = "Await _postUseCase.???";
            return Ok(posts);
        }



        [HttpGet("GetAllByUserId/{userId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAllByUserId(int userId)
        {
            var posts = "await _postUseCase.???";

            return Ok(posts);
        }

        [HttpPost("CreatePubli")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CreatePost(PublicationsModel publicationsModel)
        {
            var registerResultPubli = "djsiadjias";

            return Ok(registerResultPubli);
        }

    }
}
