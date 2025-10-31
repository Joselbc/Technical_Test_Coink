using Microsoft.AspNetCore.Mvc;
using UserContactRegistration.Domain.Services;

namespace UserContactRegistration.Application.Controllers
{
    [ApiController]
    [Route("api/country")]
    public class CountryController : ControllerBase
    {
        private readonly CountryService _countryService;
        public CountryController(CountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("all-countrys")]
        public async Task<IActionResult> GetAllCountrys()
        {
            return Ok(await _countryService.GetAllCountrys());
        }

    }
}
