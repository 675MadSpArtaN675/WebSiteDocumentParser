using Npgsql;
using System.Data;

namespace DataBaseParserWork
{
    public class DataBaseConnectionCreator
    {
        private string connectionString;
        private NpgsqlConnection connection;

        public DataBaseConnectionCreator(string host, string username, string password, string database_name)
        {
            connectionString = $"Host={host};Port=5432;Username={username};Password={password};Database={database_name}";

            connection = new NpgsqlConnection(connectionString);
        }

        ~DataBaseConnectionCreator()
        {
            connection.Close();
        }

        public void OpenConnectionToDataBase()
        {
            try
            {
                connection.Open();
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
