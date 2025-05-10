using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data;

public class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool existProduct = productCollection.Find(p => true).Any();

        if (!existProduct)
        {
            productCollection.InsertMany(GetSeedData());
        }
    }

    private static IEnumerable<Product> GetSeedData() =>
        new List<Product>
        {
            new()
            {
                Id = "6808bcc7d1051679fba9b476",
                Category = "Phone",
                Name = "Phone X",
                Description = "Phone X",
                Price = 850000,
                Summary = "Phone X",
                ImageName = "Phone X",
            },
            new()
            {
                Id = "6808bcc9d1051679fba9b478",
                Category = "Phone",
                Name = "Phone Y",
                Description = "Phone Y",
                Price = 250000,
                Summary = "Phone Y",
                ImageName = "Phone Y",
            },
            new()
            {
                Id = "6808c50f0812f9073f4fe204",
                Category = "Phone",
                Name = "Phone Z",
                Description = "Phone Z",
                Price = 150000,
                Summary = "Phone Z",
                ImageName = "Phone Z",
            }
        };
}