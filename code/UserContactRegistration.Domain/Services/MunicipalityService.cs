using Microsoft.Extensions.Logging;
using UserContactRegistration.Domain.Attributes;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Domain.Interfaces;
using UserContactRegistration.Domain.Models;

namespace UserContactRegistration.Domain.Services
{
    [DomainService]
    public class MunicipalityService
    {
        private readonly IPostgreRepositoryClient _postgreRepositoryClient;
        private readonly ILogger<MunicipalityService> _logger;
        public MunicipalityService(IPostgreRepositoryClient postgreRepositoryClient, ILogger<MunicipalityService> logger)
        {
            _postgreRepositoryClient = postgreRepositoryClient;
            _logger = logger;
        }


        public async Task<ApiResponse<List<Municipality>>> GetAlMunicipalitys()
        {
            var response = new ApiResponse<List<Municipality>>();

            try
            {
                List<Municipality> result = await _postgreRepositoryClient.GetAllRecordsTable<Municipality>();

                response.Success = true;
                response.Message = "Municipality retrieved successfully.";
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all Municipality: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while retrieving all Municipality: {ex.Message}";
                response.Data = null;
                return response;
            }
        }

        public async Task<Municipality> GetMunicipalityById(long municipalityId)
        {
            try
            {
                _ = municipalityId < 0 ? throw new ArgumentNullException(nameof(municipalityId), "Value of country is invalid") : default(long);

                List<Municipality> municipalitys = await _postgreRepositoryClient.GetRecordById<Municipality>(municipalityId);
                return municipalitys.FirstOrDefault(e => e.Id > 0) ?? new Municipality();
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while retrieving all Municipality.");
                throw new Exception(e.Message);
            }
        }

    }
}
