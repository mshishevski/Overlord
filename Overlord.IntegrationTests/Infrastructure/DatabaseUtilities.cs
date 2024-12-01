using Microsoft.Data.SqlClient;
using Overlord.DataAccess;

namespace Overlord.IntegrationTests.Infrastructure
{
    public static class DatabaseUtilities
    {
        public static async Task<string[]> GetDatabaseNamesWithPrefix(string databasePrefix)
        {
            using var connection = new SqlConnection(GetConnectionStringWithoutDatabase());

            await connection.OpenAsync();

            var command = new SqlCommand($@"SELECT name
                                               FROM master.dbo.sysdatabases
                                               WHERE name LIKE '{databasePrefix}%'", connection);

            var result = await command.ExecuteReaderAsync();

            var databaseNames = new List<string>();

            if (!result.HasRows) { return Array.Empty<string>(); }

            while (result.Read())
            {
                databaseNames.Add(result.GetString(0));
            }

            return databaseNames.ToArray();
        }

        public static string GetSqlConnectionString(string databaseName)
        {
            return GetConnectionStringWithoutDatabase() + $"Database={databaseName};";
        }

        private static string GetConnectionStringWithoutDatabase()
        {
            return $"Server=localhost;" +
                   $"Integrated Security=SSPI;" +
                   $"MultipleActiveResultSets=true;" +
                   $"TrustServerCertificate=True;";
        }

        public static void ResetDatabase(OverlordContext dbContext)
        {
            //dbContext.AddSeedData();
        }

        public static async Task DropOldDatabasesAsync(string databasePrefix)
        {
            await using var connection = new SqlConnection(GetConnectionStringWithoutDatabase());

            await connection.OpenAsync();

            try
            {
                var command = new SqlCommand(@$"
DECLARE @databaseName VARCHAR(MAX);

DECLARE db_cursor CURSOR FOR
SELECT name
FROM master.dbo.sysdatabases
WHERE crdate < GETDATE()-0.1 AND name LIKE '{databasePrefix}%';

OPEN db_cursor;
FETCH NEXT FROM db_cursor
INTO @databaseName;


WHILE @@FETCH_STATUS = 0
BEGIN

	-- Kill current connections to the database
    DECLARE @KillCurrentProcessesSql VARCHAR(MAX);
    SELECT @KillCurrentProcessesSql = COALESCE(@KillCurrentProcessesSql, '') + 'Kill ' + CONVERT(VARCHAR, spid) + ';'
    FROM master..sysprocesses
    WHERE dbid = DB_ID(@databaseName)
          AND spid <> @@SPId;
    EXEC (@KillCurrentProcessesSql);

    -- Drop the database
    EXEC ('DROP DATABASE [' + @databaseName + ']');

    FETCH NEXT FROM db_cursor
    INTO @databaseName;
END;

CLOSE db_cursor;
DEALLOCATE db_cursor;
", connection);
                command.ExecuteNonQuery();
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public static async Task KillAllConnectionsAndDropDatabase(string databaseName)
        {
            await using var connection = new SqlConnection(GetConnectionStringWithoutDatabase());

            await connection.OpenAsync();

            try
            {
                var command = new SqlCommand(@$"DECLARE @DatabaseName nvarchar(max)
SET @DatabaseName = N'{databaseName}'

DECLARE @SQL varchar(max)

SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
FROM MASTER..SysProcesses
WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId

EXEC(@SQL)

-- Drop the database
EXEC ('DROP DATABASE [' + @DatabaseName + ']');
", connection);
                command.ExecuteNonQuery();
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
