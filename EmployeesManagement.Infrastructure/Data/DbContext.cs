using MySql.Data.MySqlClient;


namespace EmployeesManagement.Infrastructure.Data
{
    public class DbContext
    {
        public string ConnectionString { get; set; }

        public DbContext(string connString)
        {
            this.ConnectionString = connString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }
    }
}
