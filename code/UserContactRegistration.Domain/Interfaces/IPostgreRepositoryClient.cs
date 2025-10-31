using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Domain.Models;

namespace UserContactRegistration.Domain.Interfaces
{
    public interface IPostgreRepositoryClient
    {
        Task<List<T>> GetAllRecordsTable<T>();
        Task<List<T>> GetRecordById<T>(long id);
        Task RegisterUser(UserRegistration request);
        Task DeleteUser(long userId);
        Task UpdateUser(UserUpdate request);
    }
}
