using Microsoft.Extensions.Logging;
using UserContactRegistration.Domain.Attributes;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Domain.Interfaces;
using UserContactRegistration.Domain.Models;

namespace UserContactRegistration.Domain.Services
{
    [DomainService]
    public class CountryService
    {
        private readonly IPostgreRepositoryClient _postgreRepositoryClient;
        private readonly ILogger<CountryService> _logger;
        public CountryService(IPostgreRepositoryClient postgreRepositoryClient, ILogger<CountryService> logger)
        {
            _postgreRepositoryClient = postgreRepositoryClient;
            _logger = logger;
        }


        public async Task<ApiResponse<List<Country>>> GetAllCountrys()
        {
            var response = new ApiResponse<List<Country>>();

            try
            {
                List<Country> result = await _postgreRepositoryClient.GetAllRecordsTable<Country>();

                response.Success = true;
                response.Message = "Countrys retrieved successfully.";
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all Country: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while retrieving all Country: {ex.Message}";
                response.Data = null;
                return response;
            }
        }

        public async Task<Country> GetCountryById(long countryId) {
            try
            {
                _ = countryId < 0 ? throw new ArgumentNullException(nameof(countryId), "Value of country is invalid") : default(long);

                 List<Country> countrys = await _postgreRepositoryClient.GetRecordById<Country>(countryId);
                 return countrys.FirstOrDefault(e => e.Id>0) ?? new Country();
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while retrieving all Country.");
                throw new Exception(e.Message);
            }
        }


    }
}
