using Microsoft.Extensions.Logging;
using UserContactRegistration.Domain.Attributes;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Domain.Interfaces;
using UserContactRegistration.Domain.Models;

namespace UserContactRegistration.Domain.Services
{
    [DomainService]
    public class AddressService
    {
        private readonly IPostgreRepositoryClient _postgreRepositoryClient;
        private readonly ILogger<AddressService> _logger;
        public AddressService(IPostgreRepositoryClient postgreRepositoryClient, ILogger<AddressService> logger)
        {
            _postgreRepositoryClient = postgreRepositoryClient;
            _logger = logger;
        }

        public async Task<ApiResponse<List<Address>>> GetAllAddresses()
        {
            var response = new ApiResponse<List<Address>>();

            try
            {
                List<Address> users = await _postgreRepositoryClient.GetAllRecordsTable<Address>();

                response.Success = true;
                response.Message = "Users retrieved successfully.";
                response.Data = users;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all users: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while retrieving all allAddresses: {ex.Message}";
                response.Data = null;
                return response;
            }
        }

    }
}
