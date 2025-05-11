using Dapper;
using Discount.Api.Entities;
using Npgsql;

namespace Discount.Api.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public async Task<Coupon> GetDiscount(string productName)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var result = await connection.QueryFirstOrDefaultAsync<Coupon>(
            "SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

        return result ?? new();
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var affect = await connection.ExecuteAsync(
            "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)", new
            {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            });
        return affect > 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var affect = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName=@ProductName, Description=@Description , Amount=@Amount",
            new
            {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            });
        return affect > 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var affect = await connection.ExecuteAsync(
            "DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
        return affect > 0;
    }
}