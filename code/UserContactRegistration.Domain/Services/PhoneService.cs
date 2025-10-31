using Microsoft.Extensions.Logging;
using UserContactRegistration.Domain.Attributes;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Domain.Interfaces;
using UserContactRegistration.Domain.Models;

namespace UserContactRegistration.Domain.Services
{
    [DomainService]
    public class PhoneService
    {
        private readonly IPostgreRepositoryClient _postgreRepositoryClient;
        private readonly ILogger<PhoneService> _logger;
        public PhoneService(IPostgreRepositoryClient postgreRepositoryClient, ILogger<PhoneService> logger)
        {
            _postgreRepositoryClient = postgreRepositoryClient;
            _logger = logger;
        }

        public async Task<ApiResponse<List<User>>> GetAllPhones()
        {
            var response = new ApiResponse<List<User>>();

            try
            {
                List<User> result = await _postgreRepositoryClient.GetAllRecordsTable<User>();

                response.Success = true;
                response.Message = "User retrieved successfully.";
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all User: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while retrieving all User: {ex.Message}";
                response.Data = null;
                return response;
            }
        }


    }
}
