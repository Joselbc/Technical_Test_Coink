using Microsoft.AspNetCore.Mvc;

namespace UserContactRegistration.Application.Controllers
{
    [ApiController]
    [Route("api/check")]
    public class CheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult HealthyCheck()
        {
            return Ok(new { status = "Ok", code = 200 });
        }
    }
}
