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

        public MySqlDataReader GetDataUseDBFunc(string func)
        {
            using (MySqlCommand command = new MySqlCommand(func, connection))
            {
                command.CommandType = CommandType.StoredProcedure; // Устанавливаем тип команды как StoredProcedure
                MySqlDataReader reader = command.ExecuteReader();
                return reader;
            }
        }

    }
}
