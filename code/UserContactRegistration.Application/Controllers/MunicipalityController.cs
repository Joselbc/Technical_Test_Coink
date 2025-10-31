using Microsoft.AspNetCore.Mvc;
using UserContactRegistration.Domain.Services;

namespace UserContactRegistration.Application.Controllers
{
    [ApiController]
    [Route("api/municipality")]
    public class MunicipalityController : ControllerBase
    {
        private readonly MunicipalityService _municipalityService;
        public MunicipalityController(MunicipalityService municipalityService)
        {
            _municipalityService = municipalityService;
        }

        [HttpGet("all-municipalitys")]
        public async Task<IActionResult> GetAlMunicipalitys()
        {
            return Ok(await _municipalityService.GetAlMunicipalitys());
        }
    }
}
