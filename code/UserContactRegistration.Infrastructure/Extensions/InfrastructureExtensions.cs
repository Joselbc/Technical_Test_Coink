using Microsoft.Extensions.DependencyInjection;
using UserContactRegistration.Domain.Interfaces;
using UserContactRegistration.Infrastructure.PostgreSQL.Repositorys;

namespace UserContactRegistration.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection svc)
        {
            svc.AddTransient<IPostgreRepositoryClient, PostgreRepositoryClient>();
            return svc;
        }

    }
}