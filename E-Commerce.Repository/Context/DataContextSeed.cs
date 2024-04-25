using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.Order;
using System.Text.Json;

namespace E_Commerce.Repository.Context
{
    public static class DataContextSeed
    {
        public static async Task SeedData(DataContext context)
        {
            if (!context.Set<ProductBrand>().Any())
            {
                //Read Data From Files
                var BrandsData = await File
                    .ReadAllTextAsync(@"..\E-Commerce.Repository\Context\DataSeeding\brands.json");
                //Convert Data to C# Object
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                //Insert Data into DB
                if (brands is not null && brands.Any())
                {
                    await context.Set<ProductBrand>().AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Set<ProductType>().Any())
            {
                //Read Data From Files
                var TypesData = await File
                    .ReadAllTextAsync(@"..\E-Commerce.Repository\Context\DataSeeding\types.json");
                //Convert Data to C# Object
                var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                //Insert Data into DB
                if (types is not null && types.Any())
                {
                    await context.Set<ProductType>().AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }
            }
             
            if (!context.Set<Product>().Any())
            {
                //Read Data From Files
                var ProductData = await File
                    .ReadAllTextAsync(@"..\E-Commerce.Repository\Context\DataSeeding\products.json");
                //Convert Data to C# Object
                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                //Insert Data into DB
                if (products is not null && products.Any())
                {
                    await context.Set<Product>().AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }
            
            if (!context.Set<DeliveryMethods>().Any())
            {
                //Read Data From Files
                var MethodData = await File
                    .ReadAllTextAsync(@"..\E-Commerce.Repository\Context\DataSeeding\delivery.json");
                //Convert Data to C# Object
                var methods = JsonSerializer.Deserialize<List<DeliveryMethods>>(MethodData);
                //Insert Data into DB
                if (methods is not null && methods.Any())
                {
                    await context.Set<DeliveryMethods>().AddRangeAsync(methods);
                    await context.SaveChangesAsync();
                }
            }

        }
    }
}
