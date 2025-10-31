namespace UserContactRegistration.Infrastructure.MySqlDb.Queries
{
    public class SqlBuilder
    {
        public const string SpRegisterUser = "sp_register_user";
        public const string SpUpdateUser = "sp_update_user";
        public const string SpDeleteUser = "sp_delete_user";


        public static string BuildSelectAllQuery(string tableName)
        {
            return $"SELECT * FROM {tableName}";
        }

        public static string BuildSelectByIdQuery(string tableName, string primaryKey)
        {
            return $"SELECT * FROM {tableName} WHERE {primaryKey} = @id";
        }


    }
}
