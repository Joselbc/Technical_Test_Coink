using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Infrastructure.PostgreSQL.Entities;

namespace UserContactRegistration.Infrastructure.Extensions
{
    public static class TypesExtensions
    {
        public static readonly Dictionary<Type, (Type DtoType, string TableName, string PrimaryKey)> TableMappings = new()
        {
            { typeof(User), (typeof(UserDto), "users", "user_id") },
            { typeof(Address), (typeof(AddressDto), "addresses", "address_id") },
            { typeof(Phone), (typeof(PhoneDto), "phones", "phone_id") },
            { typeof(Country), (typeof(CountryDto), "cat_countries", "country_id") },
            { typeof(Department), (typeof(DepartmentDto), "cat_departments", "department_id") },
            { typeof(Municipality), (typeof(MunicipalityDto), "cat_municipalities", "municipality_id") },
            { typeof(DocumentType), (typeof(DocumentTypeDto), "cat_document_types", "document_type_id") },

        };

        public static string GetColumnListFor(this Type type)
        {
            var properties = type.GetProperties();
            return string.Join(", ", properties.Select(p => $"{p.Name.ToLower()} {p.PropertyType.GetPostgresType()}"));
        }

        public static string GetPostgresType(this Type type)
        {
            if (type == typeof(long) || type == typeof(int))
                return "BIGINT";
            if (type == typeof(string))
                return "TEXT";
            if (type == typeof(DateTime))
                return "TIMESTAMPTZ";
            if (type == typeof(bool))
                return "BOOLEAN";

            return "TEXT"; 
        }

        public static string GetTableName<T>()
        {
            if (TableMappings.TryGetValue(typeof(T), out var mapping))
                return mapping.TableName;

            throw new InvalidOperationException($"No se encontró una tabla asociada para el tipo {typeof(T).Name}");
        }

        public static Type GetDtoType<T>()
        {
            if (TableMappings.TryGetValue(typeof(T), out var mapping))
                return mapping.DtoType;

            throw new InvalidOperationException($"No se encontró un DTO asociado para el tipo {typeof(T).Name}");
        }
    }
}
