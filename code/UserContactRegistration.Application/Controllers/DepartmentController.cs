using Microsoft.AspNetCore.Mvc;
using UserContactRegistration.Domain.Services;

namespace UserContactRegistration.Application.Controllers
{
    [ApiController]
    [Route("api/department")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;
        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        [HttpGet("all-departments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            return Ok(await _departmentService.GetAllDepartment());
        }
    }
}
