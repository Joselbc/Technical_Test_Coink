using Microsoft.AspNetCore.Mvc;
using UserContactRegistration.Domain.Services;

namespace UserContactRegistration.Application.Controllers
{
    [ApiController]
    [Route("api/phone")]
    public class PhoneController : ControllerBase
    {
        private readonly PhoneService _phoneService;
        public PhoneController(PhoneService phoneService)
        {
            _phoneService = phoneService;
        }
        [HttpGet("all-phones")]
        public async Task<IActionResult> GetAllPhones()
        {
            return Ok(await _phoneService.GetAllPhones());
        }
    }
}
