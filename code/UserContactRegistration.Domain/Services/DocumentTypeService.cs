using Microsoft.Extensions.Logging;
using UserContactRegistration.Domain.Attributes;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Domain.Interfaces;
using UserContactRegistration.Domain.Models;

namespace UserContactRegistration.Domain.Services
{
    [DomainService]
    public class DocumentTypeService
    {
        private readonly IPostgreRepositoryClient _postgreRepositoryClient;
        private readonly ILogger<DocumentTypeService> _logger;
        public DocumentTypeService(IPostgreRepositoryClient postgreRepositoryClient, ILogger<DocumentTypeService> logger)
        {
            _postgreRepositoryClient = postgreRepositoryClient;
            _logger = logger;
        }

        public async Task<ApiResponse<List<DocumentType>>> GetAlDocumentTypes()
        {
            var response = new ApiResponse<List<DocumentType>>();

            try
            {
                List<DocumentType> result = await _postgreRepositoryClient.GetAllRecordsTable<DocumentType>();

                response.Success = true;
                response.Message = "DocumentType retrieved successfully.";
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all DocumentType: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while retrieving all DocumentType: {ex.Message}";
                response.Data = null;
                return response;
            }
        }

        public async Task<DocumentType> GetDocumentTypeById(long documentTypeId)
        {
            try
            {
                _ = documentTypeId <= 0 ? throw new ArgumentNullException(nameof(documentTypeId), "Value of country is invalid") : default(long);

                List<DocumentType> documentTypes = await _postgreRepositoryClient.GetRecordById<DocumentType>(documentTypeId);
                return documentTypes.FirstOrDefault(e => e.Id > 0) ?? new DocumentType();
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred while retrieving all Municipality.");
                throw new Exception(e.Message);
            }
        }

    }
}
