using Npgsql;

namespace Discount.Api.Extensions;

public static class HostExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {
        int retryForAvailability = retry.Value;

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Migrating postgresql database");
                using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand
                {
                    Connection = connection,
                };

                command.CommandText = "DROP table if exists Coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon (Id SERIAL PRIMARY KEY,
                                        ProductName VARCHAR(50) NOT NULL,
                                        Description TEXT,
                                        Amount INT)";
                command.ExecuteNonQuery();
                

            }
            catch (NpgsqlException e)
            {
                logger.LogError("an error has been occured");
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, retryForAvailability);
                }
            }
        }
        return host;
    }
}