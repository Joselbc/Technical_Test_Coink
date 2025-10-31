using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using UserContactRegistration.Domain.Interfaces;
using UserContactRegistration.Domain.Models;
using UserContactRegistration.Infrastructure.Extensions;
using UserContactRegistration.Infrastructure.MySqlDb.Queries;
using UserContactRegistration.Infrastructure.PostgreSQL.Settings;

namespace UserContactRegistration.Infrastructure.PostgreSQL.Repositorys
{
    public class PostgreRepositoryClient : IPostgreRepositoryClient
    {
        private readonly string _connection;
        private readonly IMapper _mapper;
        private readonly ILogger<PostgreRepositoryClient> _logger;

        public PostgreRepositoryClient(IOptions<PostgreSettings> postgreSettings, IMapper mapper,
                                       ILogger<PostgreRepositoryClient> logger)
        {
            _connection = postgreSettings.Value.GetConnectionString() ?? string.Empty;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<T>> GetAllRecordsTable<T>()
        {
            try
            {
                if (!TypesExtensions.TableMappings.TryGetValue(typeof(T), out var mapping))
                    throw new InvalidOperationException($"No mapping found for type {typeof(T).Name}");

                using IDbConnection connection = new NpgsqlConnection(_connection);

                var dtoInstance = Activator.CreateInstance(mapping.DtoType);
                var columnList = mapping.DtoType.GetColumnListFor();

                string sql = SqlBuilder.BuildSelectAllQuery(mapping.TableName);

                _logger.LogInformation($"Executing SQL: {sql} with table {mapping.TableName}");


                var dtoResult = await connection.QueryAsync(
                    mapping.DtoType,
                    sql,
                    commandType: CommandType.Text
                );

                var result = _mapper.Map<List<T>>(dtoResult);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllRecordsTable<{typeof(T).Name}>: {ex.Message}");
                throw;
            }
        }

        public async Task<List<T>> GetRecordById<T>(long id)
        {
            try
            {
                if (!TypesExtensions.TableMappings.TryGetValue(typeof(T), out var mapping))
                    throw new InvalidOperationException($"No mapping found for type {typeof(T).Name}");

                using IDbConnection connection = new NpgsqlConnection(_connection);

                var sql = SqlBuilder.BuildSelectByIdQuery(mapping.TableName, mapping.PrimaryKey);

                var dtoResult = await connection.QueryAsync(
                    mapping.DtoType,
                    sql,
                    new { id },
                    commandType: CommandType.Text
                );

                return _mapper.Map<List<T>>(dtoResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetRecordById<{typeof(T).Name}>: {ex.Message}");
                throw;
            }
        }

        public async Task RegisterUser(UserRegistration request)
        {
            try
            {
                using IDbConnection connection = new NpgsqlConnection(_connection);

                var parameters = new DynamicParameters();
                parameters.Add("p_document_number", request.DocumentNumber);
                parameters.Add("p_document_type_id", request.DocumentTypeId);
                parameters.Add("p_first_name", request.FirstName);
                parameters.Add("p_last_name", request.LastName);
                parameters.Add("p_email", request.Email);
                parameters.Add("p_phone_value", request.PhoneValue);
                parameters.Add("p_phone_type", request.PhoneType);
                parameters.Add("p_country_id", request.CountryId);
                parameters.Add("p_department_id", request.DepartmentId);
                parameters.Add("p_municipality_id", request.MunicipalityId);
                parameters.Add("p_address", request.AddressValue);
                parameters.Add("p_complement", request.Complement);
                parameters.Add("p_postal_code", request.PostalCode);

                await connection.ExecuteScalarAsync<long>(
                    SqlBuilder.SpRegisterUser,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in RegisterUserAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateUser(UserUpdate request)
        {
            try
            {
                using IDbConnection connection = new NpgsqlConnection(_connection);

                var parameters = new DynamicParameters();
                parameters.Add("p_user_id", request.UserId);
                parameters.Add("p_document_number", request.DocumentNumber);
                parameters.Add("p_document_type_id", request.DocumentTypeId);
                parameters.Add("p_first_name", request.FirstName);
                parameters.Add("p_last_name", request.LastName);
                parameters.Add("p_email", request.Email);
                parameters.Add("p_phone_value", request.PhoneValue);
                parameters.Add("p_phone_type", request.PhoneType);
                parameters.Add("p_country_id", request.CountryId);
                parameters.Add("p_department_id", request.DepartmentId);
                parameters.Add("p_municipality_id", request.MunicipalityId);
                parameters.Add("p_address", request.AddressValue);
                parameters.Add("p_complement", request.Complement);
                parameters.Add("p_postal_code", request.PostalCode);

                await connection.ExecuteAsync(
                    SqlBuilder.SpUpdateUser,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateUser: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteUser(long userId)
        {
            try
            {
                using IDbConnection connection = new NpgsqlConnection(_connection);
                await connection.ExecuteAsync(
                    SqlBuilder.SpDeleteUser,
                    new { p_user_id = userId },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteUser: {ex.Message}");
                throw;
            }
        }



    }
}
