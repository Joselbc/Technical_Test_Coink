using Microsoft.AspNetCore.Mvc;
using UserContactRegistration.Domain.Services;

namespace UserContactRegistration.Application.Controllers
{
    [ApiController]
    [Route("api/adress")]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;
        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }


        [HttpGet("all-addresses")]
        public async Task<IActionResult> GetAllAddresses()
        {
            return Ok(await _addressService.GetAllAddresses());
        }


    }
}
