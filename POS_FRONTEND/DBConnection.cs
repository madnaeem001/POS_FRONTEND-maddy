using System.Configuration;
using Microsoft.Data.SqlClient;

public static class DBConnection
{
    /// <summary>
    /// Creates and returns a new database connection instance.
    /// </summary>
    /// <returns>A new SqlConnection object.</returns>
    public static SqlConnection GetConnection()
    {
        // Retrieve the connection string from App.config
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        // Create a new connection object and return it
        return new SqlConnection(connectionString);
    }
}
