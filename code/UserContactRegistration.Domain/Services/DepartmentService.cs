using Microsoft.Extensions.Logging;
using UserContactRegistration.Domain.Attributes;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Domain.Interfaces;
using UserContactRegistration.Domain.Models;

namespace UserContactRegistration.Domain.Services
{
    [DomainService]
    public class DepartmentService
    {
        private readonly IPostgreRepositoryClient _postgreRepositoryClient;
        private readonly ILogger<DepartmentService> _logger;
        public DepartmentService(IPostgreRepositoryClient postgreRepositoryClient, ILogger<DepartmentService> logger)
        {
            _postgreRepositoryClient = postgreRepositoryClient;
            _logger = logger;
        }


        public async Task<ApiResponse<List<Department>>> GetAllDepartment()
        {
            var response = new ApiResponse<List<Department>>();

            try
            {
                List<Department> result = await _postgreRepositoryClient.GetAllRecordsTable<Department>();

                response.Success = true;
                response.Message = "Department retrieved successfully.";
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all Department: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while retrieving all Department: {ex.Message}";
                response.Data = null;
                return response;
            }
        }

        public async Task<Department> GetDepartmentById(long departmentId)
        {
            try
            {
                _ = departmentId < 0 ? throw new ArgumentNullException(nameof(departmentId), "Value of country is invalid") : default(long);

                List<Department> countrys = await _postgreRepositoryClient.GetRecordById<Department>(departmentId);
                return countrys.FirstOrDefault(e => e.Id > 0) ?? new Department();
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while retrieving all Department.");
                throw new Exception(e.Message);
            }
        }

    }
}
