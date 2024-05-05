using MySqlConnector;
using System.Data;

namespace ServerConsole
{
    internal class MySQLConnector
    {
        private MySqlConnection connection;

        public MySQLConnector(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        public void Finish()
        {
            connection.Close();
        }

        public MySqlDataReader GetAllDataUseDBFunc(string func)
        {
            using (var command = new MySqlCommand(func, connection))
            {
                command.CommandType = CommandType.StoredProcedure; // Устанавливаем тип команды как StoredProcedure
                MySqlDataReader reader = command.ExecuteReader();
                return reader;
            }
        }

        public MySqlDataReader GetAllDataByCountryIdUseDBFunc(string func, int countryId)
        {
            using (var command = new MySqlCommand(func, connection))
            {
                command.CommandType = CommandType.StoredProcedure; // Устанавливаем тип команды как StoredProcedure

                command.Parameters.AddWithValue("@country_id", countryId);

                MySqlDataReader reader = command.ExecuteReader();
                return reader;
            }
        }

        public MySqlDataReader GetDataByIdUseDBFunc(string func, int id)
        {
            using (var command = new MySqlCommand(func, connection))
            {
                command.CommandType = CommandType.StoredProcedure; // Устанавливаем тип команды как StoredProcedure

                command.Parameters.AddWithValue("@object_id", id);

                MySqlDataReader reader = command.ExecuteReader();
                return reader;
            }
        }

    }
}
