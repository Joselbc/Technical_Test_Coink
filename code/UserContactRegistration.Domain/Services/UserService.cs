using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using UserContactRegistration.Domain.Attributes;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Domain.Enums;
using UserContactRegistration.Domain.Interfaces;
using UserContactRegistration.Domain.Models;

namespace UserContactRegistration.Domain.Services
{
    [DomainService]
    public class UserService
    {
        private readonly IPostgreRepositoryClient _postgreRepositoryClient;
        private readonly CountryService _countryService;
        private readonly MunicipalityService _municipalityService;
        private readonly DepartmentService _departmentService;
        private readonly DocumentTypeService _documentTypeService;
        private readonly ILogger<UserService> _logger;
        public UserService(IPostgreRepositoryClient postgreRepositoryClient, ILogger<UserService> logger,
                           CountryService countryService, DepartmentService departmentService, 
                           MunicipalityService municipalityService, DocumentTypeService documentType)
        {
            _postgreRepositoryClient = postgreRepositoryClient;
            _countryService = countryService;
            _departmentService = departmentService;
            _municipalityService = municipalityService;
            _documentTypeService = documentType;
            _logger = logger;
        }

        public async Task<ApiResponse<List<User>>> GetAllUsers()
        {
            var response = new ApiResponse<List<User>>();

            try
            {
                List<User> users = await _postgreRepositoryClient.GetAllRecordsTable<User>();

                response.Success = true;
                response.Message = "Users retrieved successfully.";
                response.Data = users;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all users: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while retrieving all users: {ex.Message}";
                response.Data = null;
                return response;
            }
        }


        public async Task<ApiResponse<User>> GetUserById(long userId)
        {
            var response = new ApiResponse<User>();

            try
            {
                _ = userId <= 0 ? throw new ArgumentException("Invalid userId.") : default(long);

                var users = await _postgreRepositoryClient.GetRecordById<User>(userId);
                var user = users.FirstOrDefault(e => e.Id > 0);

                _ = user == null ? throw new ArgumentException($"User with ID {userId} not found.") : default(object);

                response.Success = true;
                response.Message = "User retrieved successfully.";
                response.Data = user;

                return response;
            }
            catch (ArgumentException argEx)
            {
                _logger.LogWarning($"Validation error: {argEx.Message}");
                response.Success = false;
                response.Message = argEx.Message;
                response.Data = null;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving user with ID {userId}: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while retrieving the user: {ex.Message}";
                response.Data = null;
                return response;
            }
        }



        public async Task<ApiResponse<object>> RegisterUser(UserRegistration request)
        {
            ApiResponse<object> response = new ();
            try
            {

                 ValidateRequest(request);
                 await ValidateExistenceRelationships(request);
                 await _postgreRepositoryClient.RegisterUser(request);

                response.Success = true;
                response.Data = request;
                response.Message = "User registered successfully.";

                return response;
            }
            catch (ArgumentException argEx)
            {
                _logger.LogWarning($"Validation error: {argEx.Message}");
                response.Success = false;
                response.Message = argEx.Message;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error registering user: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while registering the user {ex.Message}";
                return response;
            }
        }

        public async Task<ApiResponse<object>> UpdateUser(UserUpdate request)
        {
            ApiResponse<object> response = new();

            try
            {
                ValidateRequest(request);

                ApiResponse<User> user = await GetUserById(request.UserId);
                _ = user?.Data?.Id == null ? throw new ArgumentException($"User with ID {request.UserId} not found.") : default(object);

                await ValidateExistenceRelationships(request);

                await _postgreRepositoryClient.UpdateUser(request);

                response.Success = true;
                response.Message = "User updated successfully.";
                response.Data = request;

                return response;
            }
            catch (ArgumentException argEx)
            {
                _logger.LogWarning($"Validation error: {argEx.Message}");
                response.Success = false;
                response.Message = argEx.Message;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating user: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while updating the user: {ex.Message}";
                return response;
            }
        }

        public async Task<ApiResponse<object>> DeleteUser(long userId)
        {
            ApiResponse<object> response = new();

            try
            {

                ApiResponse<User> user = await GetUserById(userId);
                _ = user?.Data?.Id == null ? throw new ArgumentException($"User with ID {userId} not found.") : default(object);

                await _postgreRepositoryClient.DeleteUser(userId);
                response.Success = true;
                response.Message = $"User with ID {userId} deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting user: {ex.Message}");
                response.Success = false;
                response.Message = $"An unexpected error occurred while deleting the user: {ex.Message}";
            }

            return response;
        }


        private void ValidateRequest(UserRegistration request)
        {
            if (string.IsNullOrWhiteSpace(request.DocumentNumber))
                throw new ArgumentException("Document number is required.");
            if (request.DocumentTypeId <= 0)
                throw new ArgumentException("Invalid document type.");

            ValidateDocumentType(request);

            if (string.IsNullOrWhiteSpace(request.FirstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(request.Email) ||
                !Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid email format.");

            if (string.IsNullOrWhiteSpace(request.PhoneValue))
                throw new ArgumentException("Phone number is required.");
            if (string.IsNullOrWhiteSpace(request.PhoneType))
                throw new ArgumentException("Phone type is required.");

            if (request.CountryId <= 0)
                throw new ArgumentException("Invalid CountryId.");
            if (request.DepartmentId <= 0)
                throw new ArgumentException("Invalid DepartmentId.");
            if (request.MunicipalityId <= 0)
                throw new ArgumentException("Invalid MunicipalityId.");

            if (string.IsNullOrWhiteSpace(request.AddressValue))
                throw new ArgumentException("Address is required.");
        }

        private void ValidateDocumentType(UserRegistration request)
        {
            string documentNumber = request.DocumentNumber.Trim();
            bool isNumeric = Regex.IsMatch(documentNumber, @"^\d+$");

            switch ((DocumentTypeEnum)request.DocumentTypeId)
            {
                case DocumentTypeEnum.CC:
                    if (!isNumeric)
                        throw new ArgumentException("The CC document number must contain only digits.");
                    if (documentNumber.Length < 6 || documentNumber.Length > 10)
                        throw new ArgumentException("CC document number must be between 6 and 10 digits.");
                    break;

                case DocumentTypeEnum.TI:
                    if (!isNumeric)
                        throw new ArgumentException("The TI document number must contain only digits.");
                    if (documentNumber.Length < 5 || documentNumber.Length > 11)
                        throw new ArgumentException("TI document number must be between 5 and 11 digits.");
                    break;

                case DocumentTypeEnum.PAS:
                    if (!Regex.IsMatch(documentNumber, @"^[A-Za-z0-9]+$"))
                        throw new ArgumentException("Passport number must contain only alphanumeric characters.");
                    if (documentNumber.Length < 5 || documentNumber.Length > 15)
                        throw new ArgumentException("Passport number must be between 5 and 15 characters.");
                    break;

                default:
                    throw new ArgumentException("Unsupported document type.");
            }
        }



        private async Task ValidateExistenceRelationships(UserRegistration request)
        {
            try
            {
                var countryTask = _countryService.GetCountryById(request.CountryId);
                var departmentTask = _departmentService.GetDepartmentById(request.DepartmentId);
                var municipalityTask = _municipalityService.GetMunicipalityById(request.MunicipalityId);
                var documentTypeTask = _documentTypeService.GetDocumentTypeById(request.DocumentTypeId);

                await Task.WhenAll(countryTask, departmentTask, municipalityTask, documentTypeTask);

                var country = countryTask.Result;
                var department = departmentTask.Result;
                var municipality = municipalityTask.Result;

                if (country == null || country.Id <= 0)
                    throw new ArgumentException($"Country with ID {request.CountryId} does not exist.");

                if (department == null || department.Id <= 0)
                    throw new ArgumentException($"Department with ID {request.DepartmentId} does not exist.");

                if (municipality == null || municipality.Id <= 0)
                    throw new ArgumentException($"Municipality with ID {request.MunicipalityId} does not exist.");

                if (documentTypeTask == null || documentTypeTask.Id <= 0)
                    throw new ArgumentException($"Municipality with ID {request.DocumentTypeId} does not exist.");
            }
            catch (ArgumentException ae)
            {
                _logger.LogError(ae, $"Validation failed while checking related entities for user registration: {ae.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while validating relationships for user registration.");
                throw new InvalidOperationException("An unexpected error occurred while validating relationships.", ex);
            }
        }

    }

}

