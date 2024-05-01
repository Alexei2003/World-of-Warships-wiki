using MySqlConnector;

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

        private object? GetDataUseDBFunc(string func)
        {
            using (MySqlCommand command = new MySqlCommand("SELECT AddTwoNumbers(5, 7)", connection))
            {
                return command.ExecuteScalar();
            }
        }
    }
}
